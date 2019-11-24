
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{ 

	public class ExamMst : BaseClass
	{
		#region Disposable

		private bool disposed = false;

		protected override void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Release managed resources.
				}

				// Release unmanaged resources.
				// Set large fields to null.
				// Call Dispose on your base class.
				disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Fields

		private int f_ExamID;
		private string f_Name;
		private Nullable<decimal> f_Total;
		private string f_Reply;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int ExamID
		{
			get{ return f_ExamID; }
			set{ f_ExamID = value; }
		}

		public string Name
		{
			get{ return f_Name; }
			set{ f_Name = value; }
		}

		public Nullable<decimal> Total
		{
			get{ return f_Total; }
			set{ f_Total = value; }
		}

		public string Reply
		{
			get{ return f_Reply; }
			set{ f_Reply = value; }
		}

		public bool IsDataLoaded
		{
			get{ return f_IsDataLoaded; }
		}

		#endregion

		#region Constant Strings

		const string Query_SelectAll = @" SELECT * FROM [ExamMst] ";

		const string Query_SelectAll_WithPaging = @" SELECT [ExamID], [Name], [Total], [Reply], ROW_NUMBER() Over (Order by ExamID) as 'Row' FROM [ExamMst] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [ExamID], [Name], [Total], [Reply] FROM [ExamMst] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(ExamID), 0) + 1 as ExamID FROM ExamMst";

		const string Query_GetData_byKey = @" SELECT * FROM [ExamMst] WHERE [ExamID] = @ExamID ";

		const string Query_Insert = @" INSERT INTO [ExamMst] ([ExamID], [Name], [Total], [Reply])
		VALUES(@ExamID, @Name, @Total, @Reply) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [ExamMst] SET [Name] = @Name, [Total] = @Total, [Reply] = @Reply
		WHERE [ExamID] = @ExamID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [ExamMst] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

		#endregion

		#region Constructors

		public ExamMst()
		{ }

		public ExamMst(int examID)
		{
			Load(examID);
		}

		public ExamMst(int examID, SqlConnection objConnection)
		{
			Load(examID, objConnection);
		}

		public ExamMst(int examID, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(examID, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(int examID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(examID, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_ExamID = Convert.ToInt32(drRecord["ExamID"]);

				if (drRecord["Name"] != DBNull.Value)
					this.f_Name = Convert.ToString(drRecord["Name"]);


				if (drRecord["Total"] != DBNull.Value)
					this.f_Total = Convert.ToDecimal(drRecord["Total"]);


				if (drRecord["Reply"] != DBNull.Value)
					this.f_Reply = Convert.ToString(drRecord["Reply"]);

			}
		}

		#endregion

		#region Class Methods

		public static DataTable GetAllActiveData(SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataTable dt = null;

			try
			{
				DataSet ds = null;

				if(objTransaction != null)
					ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_SelectAll);
				else if(objConnection != null)
					ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_SelectAll);
				else
					ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_SelectAll);

				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
					dt = ds.Tables[0];
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return dt;
		}

		public static DataRow GetDataByKey(int examID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@ExamID", examID);

				DataSet ds = null;

				if(objTransaction != null)
					ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byKey, sqlparams);
				else if(objConnection != null)
					ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetData_byKey, sqlparams);
				else
					ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetData_byKey, sqlparams);

				if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					dr = ds.Tables[0].Rows[0];
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return dr;
		}

		public static int GetNewExamID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
		{
			int ID = 0;

			try
			{
				if(objTransaction != null)
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(objTransaction, CommandType.Text, Query_Get_MaxId));
				else if(objConnection != null)
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(objConnection, CommandType.Text, Query_Get_MaxId));
				else
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, Query_Get_MaxId));
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return ID;
		}

		public static DataTable GetAllData(string strWhere, ref int ReturnTotalRows, string OrderBy, int? PageIndex = null, int? PageSize = null, 
			Enumaration.SortDirection sortdirection = Enumaration.SortDirection.asc, SqlTransaction objTransaction = null)
		{
			List<SqlParameter> sqlparamsList = new List<SqlParameter>();
			OrderBy = (!string.IsNullOrEmpty(OrderBy) ? " Order By [" + OrderBy + "] " + sortdirection.ToString() : String.Empty);
			DataTable dt = null;

			try
			{
				if (PageIndex != null)
				{
					sqlparamsList.Add(new SqlParameter("@PageIndex", PageIndex));
					sqlparamsList.Add(new SqlParameter("@PageSize", PageSize));
					sqlparamsList.Add(new SqlParameter("@TotalRows", ReturnTotalRows));

					SqlParameter sqlparamTotalRows = sqlparamsList.Find(a => a.ParameterName == "@TotalRows");
					sqlparamTotalRows.Direction = ParameterDirection.Output;

					SqlParameter[] sqlparams = sqlparamsList.ToArray();

					if(objTransaction != null)
						dt = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, string.Format(Paging, Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty)), sqlparams).Tables[0];
					else
						dt = SqlHelper.ExecuteDataset(CommandType.Text, string.Format(Paging, Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty)), sqlparams).Tables[0];

					ReturnTotalRows = int.Parse(sqlparamTotalRows.Value.ToString());
				}
				else
				{
					if(objTransaction != null)
						dt = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_SelectAll_WithoutPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), sqlparamsList.ToArray()).Tables[0];
					else
						dt = SqlHelper.ExecuteDataset(CommandType.Text, Query_SelectAll_WithoutPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), sqlparamsList.ToArray()).Tables[0];
				}
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return dt;
		}

		public bool Insert(SqlTransaction objTransaction = null)
		{
			bool result = false;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[4];
				sqlparams[0] = new SqlParameter("@ExamID", f_ExamID);

				if(!string.IsNullOrEmpty(f_Name))
					sqlparams[1] = new SqlParameter("@Name", f_Name);
				else
					sqlparams[1] = new SqlParameter("@Name", DBNull.Value);

				if(f_Total.HasValue)
					sqlparams[2] = new SqlParameter("@Total", f_Total.Value);
				else
					sqlparams[2] = new SqlParameter("@Total", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Reply))
					sqlparams[3] = new SqlParameter("@Reply", f_Reply);
				else
					sqlparams[3] = new SqlParameter("@Reply", DBNull.Value);

				if(objTransaction == null)
				{
					if (SqlHelper.ExecuteNonQuery(CommandType.Text, Query_Insert, sqlparams) > 0)
						result = true;
				}
				else
				{
					if (SqlHelper.ExecuteNonQuery(objTransaction, CommandType.Text, Query_Insert, sqlparams) > 0)
						result = true;
				}
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return result;
		}

		public bool UpdateDataByKey(SqlTransaction objTransaction = null)
		{
			bool result = false;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[4];
				sqlparams[0] = new SqlParameter("@ExamID", f_ExamID);

				if(!string.IsNullOrEmpty(f_Name))
					sqlparams[1] = new SqlParameter("@Name", f_Name);
				else
					sqlparams[1] = new SqlParameter("@Name", DBNull.Value);

				if(f_Total.HasValue)
					sqlparams[2] = new SqlParameter("@Total", f_Total.Value);
				else
					sqlparams[2] = new SqlParameter("@Total", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Reply))
					sqlparams[3] = new SqlParameter("@Reply", f_Reply);
				else
					sqlparams[3] = new SqlParameter("@Reply", DBNull.Value);

				if(objTransaction == null)
				{
					if (SqlHelper.ExecuteNonQuery(CommandType.Text, Query_Update_By_PrimaryKey, sqlparams) > 0)
						result = true;
				}
				else
				{
					if (SqlHelper.ExecuteNonQuery(objTransaction, CommandType.Text, Query_Update_By_PrimaryKey, sqlparams) > 0)
						result = true;
				}

				result = true;
			}
			catch (Exception ex)
			{
				SqlHelper.LogException(ex);
				throw ex;
			}

			return result;
		}

		#endregion
	}
}

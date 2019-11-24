using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{

	public class FeedBackMst : BaseClass
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

		private int f_FID;
		private string f_Email;
		private string f_Mobile;
		private string f_Feedback;
		private Nullable<DateTime> f_Edate;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int FID
		{
			get{ return f_FID; }
			set{ f_FID = value; }
		}

		public string Email
		{
			get{ return f_Email; }
			set{ f_Email = value; }
		}

		public string Mobile
		{
			get{ return f_Mobile; }
			set{ f_Mobile = value; }
		}

		public string Feedback
		{
			get{ return f_Feedback; }
			set{ f_Feedback = value; }
		}

		public Nullable<DateTime> Edate
		{
			get{ return f_Edate; }
			set{ f_Edate = value; }
		}

		public bool IsDataLoaded
		{
			get{ return f_IsDataLoaded; }
		}

		#endregion

		#region Constant Strings

		const string Query_SelectAll = @" SELECT * FROM [FeedBackMst] ";

		const string Query_SelectAll_WithPaging = @" SELECT [FID], [Email], [Mobile], [Feedback], [Edate], ROW_NUMBER() Over (Order by FID) as 'Row' FROM [FeedBackMst] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [FID], [Email], [Mobile], [Feedback], [Edate] FROM [FeedBackMst] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(FID), 0) + 1 as FID FROM FeedBackMst";

		const string Query_GetData_byKey = @" SELECT * FROM [FeedBackMst] WHERE [FID] = @FID ";

		const string Query_Insert = @" INSERT INTO [FeedBackMst] ([FID], [Email], [Mobile], [Feedback], [Edate])
		VALUES(@FID, @Email, @Mobile, @Feedback, @Edate) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [FeedBackMst] SET [Email] = @Email, [Mobile] = @Mobile, [Feedback] = @Feedback, [Edate] = @Edate
		WHERE [FID] = @FID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [FeedBackMst] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

		#endregion

		#region Constructors

		public FeedBackMst()
		{ }

		public FeedBackMst(int fID)
		{
			Load(fID);
		}

		public FeedBackMst(int fID, SqlConnection objConnection)
		{
			Load(fID, objConnection);
		}

		public FeedBackMst(int fID, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(fID, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(int fID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(fID, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_FID = Convert.ToInt32(drRecord["FID"]);

				if (drRecord["Email"] != DBNull.Value)
					this.f_Email = Convert.ToString(drRecord["Email"]);


				if (drRecord["Mobile"] != DBNull.Value)
					this.f_Mobile = Convert.ToString(drRecord["Mobile"]);


				if (drRecord["Feedback"] != DBNull.Value)
					this.f_Feedback = Convert.ToString(drRecord["Feedback"]);


				if (drRecord["Edate"] != DBNull.Value)
					this.f_Edate = Convert.ToDateTime(drRecord["Edate"]);

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

		public static DataRow GetDataByKey(int fID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@FID", fID);

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

		public static int GetNewFID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
				SqlParameter[] sqlparams = new SqlParameter[5];
				sqlparams[0] = new SqlParameter("@FID", f_FID);

				if(!string.IsNullOrEmpty(f_Email))
					sqlparams[1] = new SqlParameter("@Email", f_Email);
				else
					sqlparams[1] = new SqlParameter("@Email", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Mobile))
					sqlparams[2] = new SqlParameter("@Mobile", f_Mobile);
				else
					sqlparams[2] = new SqlParameter("@Mobile", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Feedback))
					sqlparams[3] = new SqlParameter("@Feedback", f_Feedback);
				else
					sqlparams[3] = new SqlParameter("@Feedback", DBNull.Value);

				if(f_Edate.HasValue)
					sqlparams[4] = new SqlParameter("@Edate", f_Edate.Value);
				else
					sqlparams[4] = new SqlParameter("@Edate", DBNull.Value);

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
				SqlParameter[] sqlparams = new SqlParameter[5];
				sqlparams[0] = new SqlParameter("@FID", f_FID);

				if(!string.IsNullOrEmpty(f_Email))
					sqlparams[1] = new SqlParameter("@Email", f_Email);
				else
					sqlparams[1] = new SqlParameter("@Email", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Mobile))
					sqlparams[2] = new SqlParameter("@Mobile", f_Mobile);
				else
					sqlparams[2] = new SqlParameter("@Mobile", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Feedback))
					sqlparams[3] = new SqlParameter("@Feedback", f_Feedback);
				else
					sqlparams[3] = new SqlParameter("@Feedback", DBNull.Value);

				if(f_Edate.HasValue)
					sqlparams[4] = new SqlParameter("@Edate", f_Edate.Value);
				else
					sqlparams[4] = new SqlParameter("@Edate", DBNull.Value);

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

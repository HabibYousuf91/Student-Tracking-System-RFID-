using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
	public class Attendancemst : BaseClass
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

		private int f_AID;
		private string f_Rollno;
		private string f_Name;
		private string f_Date;
		private string f_Status;
		private string f_StaffName;
		private Nullable<DateTime> f_EDate;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int AID
		{
			get{ return f_AID; }
			set{ f_AID = value; }
		}

		public string Rollno
		{
			get{ return f_Rollno; }
			set{ f_Rollno = value; }
		}

		public string Name
		{
			get{ return f_Name; }
			set{ f_Name = value; }
		}

		public string Date
		{
			get{ return f_Date; }
			set{ f_Date = value; }
		}

		public string Status
		{
			get{ return f_Status; }
			set{ f_Status = value; }
		}

		public string StaffName
		{
			get{ return f_StaffName; }
			set{ f_StaffName = value; }
		}

		public Nullable<DateTime> EDate
		{
			get{ return f_EDate; }
			set{ f_EDate = value; }
		}

		public bool IsDataLoaded
		{
			get{ return f_IsDataLoaded; }
		}

		#endregion

		#region Constant Strings

		const string Query_SelectAll = @" SELECT * FROM [Attendancemst] WHERE [Status] = 1 ";

		const string Query_SelectAll_WithPaging = @" SELECT [AID], [Rollno], [Name], [Date], [Status], [StaffName], [EDate], ROW_NUMBER() Over (Order by AID) as 'Row' FROM [Attendancemst] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [AID], [Rollno], [Name], [Date], [Status], [StaffName], [EDate] FROM [Attendancemst] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(AID), 0) + 1 as AID FROM Attendancemst";

		const string Query_GetData_byKey = @" SELECT * FROM [Attendancemst] WHERE [AID] = @AID ";

		const string Query_Insert = @" INSERT INTO [Attendancemst] ([AID], [Rollno], [Name], [Date], [Status], [StaffName], [EDate])
		VALUES(@AID, @Rollno, @Name, @Date, @Status, @StaffName, @EDate) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [Attendancemst] SET [Rollno] = @Rollno, [Name] = @Name, [Date] = @Date, [Status] = @Status, [StaffName] = @StaffName, [EDate] = @EDate
		WHERE [AID] = @AID ";

		const string Query_Delete_ByKey = @" Update [Attendancemst] Set [Status] = @Status WHERE [AID] = @AID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [Attendancemst] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

		#endregion

		#region Constructors

		public Attendancemst()
		{ }

		public Attendancemst(int aID)
		{
			Load(aID);
		}

		public Attendancemst(int aID, SqlConnection objConnection)
		{
			Load(aID, objConnection);
		}

		public Attendancemst(int aID, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(aID, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(int aID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(aID, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_AID = Convert.ToInt32(drRecord["AID"]);

				if (drRecord["Rollno"] != DBNull.Value)
					this.f_Rollno = Convert.ToString(drRecord["Rollno"]);


				if (drRecord["Name"] != DBNull.Value)
					this.f_Name = Convert.ToString(drRecord["Name"]);


				if (drRecord["Date"] != DBNull.Value)
					this.f_Date = Convert.ToString(drRecord["Date"]);


				if (drRecord["Status"] != DBNull.Value)
					this.f_Status = Convert.ToString(drRecord["Status"]);


				if (drRecord["StaffName"] != DBNull.Value)
					this.f_StaffName = Convert.ToString(drRecord["StaffName"]);


				if (drRecord["EDate"] != DBNull.Value)
					this.f_EDate = Convert.ToDateTime(drRecord["EDate"]);

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

		public static DataRow GetDataByKey(int aID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@AID", aID);

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

		public static int GetNewAID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
				SqlParameter[] sqlparams = new SqlParameter[7];
				sqlparams[0] = new SqlParameter("@AID", f_AID);

				if(!string.IsNullOrEmpty(f_Rollno))
					sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
				else
					sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Name))
					sqlparams[2] = new SqlParameter("@Name", f_Name);
				else
					sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Date))
					sqlparams[3] = new SqlParameter("@Date", f_Date);
				else
					sqlparams[3] = new SqlParameter("@Date", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Status))
					sqlparams[4] = new SqlParameter("@Status", f_Status);
				else
					sqlparams[4] = new SqlParameter("@Status", DBNull.Value);

				if(!string.IsNullOrEmpty(f_StaffName))
					sqlparams[5] = new SqlParameter("@StaffName", f_StaffName);
				else
					sqlparams[5] = new SqlParameter("@StaffName", DBNull.Value);

				if(f_EDate.HasValue)
					sqlparams[6] = new SqlParameter("@EDate", f_EDate.Value);
				else
					sqlparams[6] = new SqlParameter("@EDate", DBNull.Value);

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
				SqlParameter[] sqlparams = new SqlParameter[7];
				sqlparams[0] = new SqlParameter("@AID", f_AID);

				if(!string.IsNullOrEmpty(f_Rollno))
					sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
				else
					sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Name))
					sqlparams[2] = new SqlParameter("@Name", f_Name);
				else
					sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Date))
					sqlparams[3] = new SqlParameter("@Date", f_Date);
				else
					sqlparams[3] = new SqlParameter("@Date", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Status))
					sqlparams[4] = new SqlParameter("@Status", f_Status);
				else
					sqlparams[4] = new SqlParameter("@Status", DBNull.Value);

				if(!string.IsNullOrEmpty(f_StaffName))
					sqlparams[5] = new SqlParameter("@StaffName", f_StaffName);
				else
					sqlparams[5] = new SqlParameter("@StaffName", DBNull.Value);

				if(f_EDate.HasValue)
					sqlparams[6] = new SqlParameter("@EDate", f_EDate.Value);
				else
					sqlparams[6] = new SqlParameter("@EDate", DBNull.Value);

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

		public static bool DeleteDataByKey(SqlTransaction objTransaction, int aID)
		{
			bool result = false;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[2];
				sqlparams[0] = new SqlParameter("@AID", aID);
				sqlparams[1] = new SqlParameter("@Status", false);

				if(objTransaction == null)
				{
					if (SqlHelper.ExecuteNonQuery(CommandType.Text, Query_Delete_ByKey, sqlparams) > 0)
						result = true;
				}
				else
				{
					if (SqlHelper.ExecuteNonQuery(objTransaction, CommandType.Text, Query_Delete_ByKey, sqlparams) > 0)
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

		#endregion
	}
}

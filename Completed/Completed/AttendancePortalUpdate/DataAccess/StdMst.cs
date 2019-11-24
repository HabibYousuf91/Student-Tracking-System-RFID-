using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Models
{

	public class StdMst : BaseClass
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

		private int f_SID;
		private string f_StdName;
		private Nullable<DateTime> f_EDate;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int SID
		{
			get{ return f_SID; }
			set{ f_SID = value; }
		}

		public string StdName
		{
			get{ return f_StdName; }
			set{ f_StdName = value; }
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

		const string Query_SelectAll = @" SELECT * FROM [StdMst] ";

		const string Query_SelectAll_WithPaging = @" SELECT [SID], [StdName], [EDate], ROW_NUMBER() Over (Order by SID) as 'Row' FROM [StdMst] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [SID], [StdName], [EDate] FROM [StdMst] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(SID), 0) + 1 as SID FROM StdMst";

		const string Query_GetData_byKey = @" SELECT * FROM [StdMst] WHERE [SID] = @SID ";

		const string Query_Insert = @" INSERT INTO [StdMst] ([SID], [StdName], [EDate])
		VALUES(@SID, @StdName, @EDate) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [StdMst] SET [StdName] = @StdName, [EDate] = @EDate
		WHERE [SID] = @SID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [StdMst] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

		#endregion

		#region Constructors

		public StdMst()
		{ }

		public StdMst(int sID)
		{
			Load(sID);
		}

		public StdMst(int sID, SqlConnection objConnection)
		{
			Load(sID, objConnection);
		}

		public StdMst(int sID, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(sID, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(int sID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(sID, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_SID = Convert.ToInt32(drRecord["SID"]);

				if (drRecord["StdName"] != DBNull.Value)
					this.f_StdName = Convert.ToString(drRecord["StdName"]);


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

		public static DataRow GetDataByKey(int sID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@SID", sID);

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

		public static int GetNewSID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
				SqlParameter[] sqlparams = new SqlParameter[3];
				sqlparams[0] = new SqlParameter("@SID", f_SID);

				if(!string.IsNullOrEmpty(f_StdName))
					sqlparams[1] = new SqlParameter("@StdName", f_StdName);
				else
					sqlparams[1] = new SqlParameter("@StdName", DBNull.Value);

				if(f_EDate.HasValue)
					sqlparams[2] = new SqlParameter("@EDate", f_EDate.Value);
				else
					sqlparams[2] = new SqlParameter("@EDate", DBNull.Value);

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
				SqlParameter[] sqlparams = new SqlParameter[3];
				sqlparams[0] = new SqlParameter("@SID", f_SID);

				if(!string.IsNullOrEmpty(f_StdName))
					sqlparams[1] = new SqlParameter("@StdName", f_StdName);
				else
					sqlparams[1] = new SqlParameter("@StdName", DBNull.Value);

				if(f_EDate.HasValue)
					sqlparams[2] = new SqlParameter("@EDate", f_EDate.Value);
				else
					sqlparams[2] = new SqlParameter("@EDate", DBNull.Value);

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

        public int Delete(int Id, SqlTransaction objTransaction = null)
        {
            int result = 0;

            const string Query_Delete = @" Delete FROM StdMst where SID =@SID";

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@SID", Id);

                if (objTransaction != null)
                    result = SqlHelper.ExecuteNonQuery(objTransaction, CommandType.Text, Query_Delete, sqlparams);
                else
                    result = SqlHelper.ExecuteNonQuery(CommandType.Text, Query_Delete, sqlparams);

                return result;
            }
            catch (Exception ex)
            {
                SqlHelper.LogException(ex);
                throw ex;
            }

        }
        #endregion
    }
}

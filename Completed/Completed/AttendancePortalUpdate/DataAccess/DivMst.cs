using DataAccess;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
	public class DivMst : BaseClass
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

        private StdMst f_StdId_StdMst;

        private int f_DID;
        private string f_DivName;
        private string f_StdName;
        private Nullable<int> f_Seat;
        private Nullable<DateTime> f_EDate;
        private Nullable<int> f_StdId;

        //	Not database fields
        private bool f_IsDataLoaded;

        #endregion

        #region Properties

        public StdMst StdId_StdMst
        {
            get
            {
                if (this.f_StdId.HasValue)
                {
                    if (this.f_StdId_StdMst == null)
                        this.f_StdId_StdMst = new StdMst(this.f_StdId.Value);

                    return this.f_StdId_StdMst;
                }
                else
                    return null;
            }
        }

        public int DID
        {
            get { return f_DID; }
            set { f_DID = value; }
        }

        public string DivName
        {
            get { return f_DivName; }
            set { f_DivName = value; }
        }

        public string StdName
        {
            get { return f_StdName; }
            set { f_StdName = value; }
        }

        public Nullable<int> Seat
        {
            get { return f_Seat; }
            set { f_Seat = value; }
        }

        public Nullable<DateTime> EDate
        {
            get { return f_EDate; }
            set { f_EDate = value; }
        }

        public Nullable<int> StdId
        {
            get { return f_StdId; }
            set { f_StdId = value; }
        }

        public bool IsDataLoaded
        {
            get { return f_IsDataLoaded; }
        }

        #endregion

        #region Constant Strings

        const string Query_SelectAll = @" SELECT * FROM [DivMst] ";

        const string Query_SelectAll_WithPaging = @" SELECT [DID], [DivName], [StdName], [Seat], [EDate], [StdId], ROW_NUMBER() Over (Order by DID) as 'Row' FROM [DivMst] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [DID], [DivName], [StdName], [Seat], [EDate], [StdId] FROM [DivMst] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(DID), 0) + 1 as DID FROM DivMst";

        const string Query_GetData_byKey = @" SELECT * FROM [DivMst] WHERE [DID] = @DID ";

        const string Query_Insert = @" INSERT INTO [DivMst] ([DID], [DivName], [StdName], [Seat], [EDate], [StdId])
		VALUES(@DID, @DivName, @StdName, @Seat, @EDate, @StdId) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [DivMst] SET [DivName] = @DivName, [StdName] = @StdName, [Seat] = @Seat, [EDate] = @EDate, [StdId] = @StdId
		WHERE [DID] = @DID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [DivMst] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

        #endregion

        #region Constructors

        public DivMst()
        { }

        public DivMst(int dID)
        {
            Load(dID);
        }

        public DivMst(int dID, SqlConnection objConnection)
        {
            Load(dID, objConnection);
        }

        public DivMst(int dID, SqlConnection objConnection, SqlTransaction objTransaction)
        {
            Load(dID, objConnection, objTransaction);
        }

        #endregion

        #region Loading Methods

        private void Load(int dID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow drRecord = GetDataByKey(dID, objConnection, objTransaction);

            if (drRecord != null)
            {
                this.f_IsDataLoaded = true;
                this.f_DID = Convert.ToInt32(drRecord["DID"]);

                if (drRecord["DivName"] != DBNull.Value)
                    this.f_DivName = Convert.ToString(drRecord["DivName"]);


                if (drRecord["StdName"] != DBNull.Value)
                    this.f_StdName = Convert.ToString(drRecord["StdName"]);


                if (drRecord["Seat"] != DBNull.Value)
                    this.f_Seat = Convert.ToInt32(drRecord["Seat"]);


                if (drRecord["EDate"] != DBNull.Value)
                    this.f_EDate = Convert.ToDateTime(drRecord["EDate"]);


                if (drRecord["StdId"] != DBNull.Value)
                    this.f_StdId = Convert.ToInt32(drRecord["StdId"]);

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

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_SelectAll);
                else if (objConnection != null)
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

        public static DataRow GetDataByKey(int dID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@DID", dID);

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byKey, sqlparams);
                else if (objConnection != null)
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

        public static int GetNewDID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
        {
            int ID = 0;

            try
            {
                if (objTransaction != null)
                    ID = Convert.ToInt32(SqlHelper.ExecuteScalar(objTransaction, CommandType.Text, Query_Get_MaxId));
                else if (objConnection != null)
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

        public bool Insert(SqlTransaction objTransaction = null)
        {
            bool result = false;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[6];
                sqlparams[0] = new SqlParameter("@DID", f_DID);

                if (!string.IsNullOrEmpty(f_DivName))
                    sqlparams[1] = new SqlParameter("@DivName", f_DivName);
                else
                    sqlparams[1] = new SqlParameter("@DivName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[2] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[2] = new SqlParameter("@StdName", DBNull.Value);

                if (f_Seat.HasValue)
                    sqlparams[3] = new SqlParameter("@Seat", f_Seat.Value);
                else
                    sqlparams[3] = new SqlParameter("@Seat", DBNull.Value);

                if (f_EDate.HasValue)
                    sqlparams[4] = new SqlParameter("@EDate", f_EDate.Value);
                else
                    sqlparams[4] = new SqlParameter("@EDate", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[5] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[5] = new SqlParameter("@StdId", DBNull.Value);

                if (objTransaction == null)
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
                SqlParameter[] sqlparams = new SqlParameter[6];
                sqlparams[0] = new SqlParameter("@DID", f_DID);

                if (!string.IsNullOrEmpty(f_DivName))
                    sqlparams[1] = new SqlParameter("@DivName", f_DivName);
                else
                    sqlparams[1] = new SqlParameter("@DivName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[2] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[2] = new SqlParameter("@StdName", DBNull.Value);

                if (f_Seat.HasValue)
                    sqlparams[3] = new SqlParameter("@Seat", f_Seat.Value);
                else
                    sqlparams[3] = new SqlParameter("@Seat", DBNull.Value);

                if (f_EDate.HasValue)
                    sqlparams[4] = new SqlParameter("@EDate", f_EDate.Value);
                else
                    sqlparams[4] = new SqlParameter("@EDate", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[5] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[5] = new SqlParameter("@StdId", DBNull.Value);

                if (objTransaction == null)
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

        public static int Delete(int Id, SqlTransaction objTransaction = null)
        {
            int result = 0;

            const string Query_Delete = @" Delete FROM [DivMst] where DID =@DID";

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@DID", Id);

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

        public static DataTable GetDataByStandardId(int StdId, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                const string Query_GetData_byKey = @" SELECT * FROM [DivMst] WHERE [StdId] = @StdId ";

                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@StdId", StdId);

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byKey, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetData_byKey, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetData_byKey, sqlparams);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                SqlHelper.LogException(ex);
                throw ex;
            }

            return dt;
        }
    }
}

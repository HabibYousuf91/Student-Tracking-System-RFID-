using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace Models
{
	public partial class LeaveMst : BaseClass
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

        private StudentMst f_StudentId_StudentMst;

        private int f_LID;
        private string f_Rollno;
        private string f_Name;
        private string f_StdName;
        private string f_Message;
        private Nullable<int> f_Nodays;
        private string f_Reply;
        private Nullable<DateTime> f_Edate;
        private Nullable<int> f_StudentId;

        //	Not database fields
        private bool f_IsDataLoaded;

        #endregion

        #region Properties

        public StudentMst StudentId_StudentMst
        {
            get
            {
                if (this.f_StudentId.HasValue)
                {
                    if (this.f_StudentId_StudentMst == null)
                        this.f_StudentId_StudentMst = new StudentMst(this.f_StudentId.Value);

                    return this.f_StudentId_StudentMst;
                }
                else
                    return null;
            }
        }

        public int LID
        {
            get { return f_LID; }
            set { f_LID = value; }
        }

        public string Rollno
        {
            get { return f_Rollno; }
            set { f_Rollno = value; }
        }

        public string Name
        {
            get { return f_Name; }
            set { f_Name = value; }
        }

        public string StdName
        {
            get { return f_StdName; }
            set { f_StdName = value; }
        }

        public string Message
        {
            get { return f_Message; }
            set { f_Message = value; }
        }

        public Nullable<int> Nodays
        {
            get { return f_Nodays; }
            set { f_Nodays = value; }
        }

        public string Reply
        {
            get { return f_Reply; }
            set { f_Reply = value; }
        }

        public Nullable<DateTime> Edate
        {
            get { return f_Edate; }
            set { f_Edate = value; }
        }

        public Nullable<int> StudentId
        {
            get { return f_StudentId; }
            set { f_StudentId = value; }
        }

        public bool IsDataLoaded
        {
            get { return f_IsDataLoaded; }
        }

        #endregion

        #region Constant Strings

        const string Query_SelectAll = @" SELECT * FROM [LeaveMst] ";

        const string Query_SelectAll_WithPaging = @" SELECT [LID], [Rollno], [Name], [StdName], [Message], [Nodays], [Reply], [Edate], [StudentId], ROW_NUMBER() Over (Order by LID) as 'Row' FROM [LeaveMst] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [LID], [Rollno], [Name], [StdName], [Message], [Nodays], [Reply], [Edate], [StudentId] FROM [LeaveMst] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(LID), 0) + 1 as LID FROM LeaveMst";

        const string Query_GetData_byKey = @" SELECT * FROM [LeaveMst] WHERE [LID] = @LID ";

        const string Query_Insert = @" INSERT INTO [LeaveMst] ([LID], [Rollno], [Name], [StdName], [Message], [Nodays], [Reply], [Edate], [StudentId])
		VALUES(@LID, @Rollno, @Name, @StdName, @Message, @Nodays, @Reply, @Edate, @StudentId) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [LeaveMst] SET [Rollno] = @Rollno, [Name] = @Name, [StdName] = @StdName, [Message] = @Message, [Nodays] = @Nodays, [Reply] = @Reply, [Edate] = @Edate, [StudentId] = @StudentId
		WHERE [LID] = @LID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [LeaveMst] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

        #endregion

        #region Constructors

        public LeaveMst()
        { }

        public LeaveMst(int lID)
        {
            Load(lID);
        }

        public LeaveMst(int lID, SqlConnection objConnection)
        {
            Load(lID, objConnection);
        }

        public LeaveMst(int lID, SqlConnection objConnection, SqlTransaction objTransaction)
        {
            Load(lID, objConnection, objTransaction);
        }

        #endregion

        #region Loading Methods

        private void Load(int lID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow drRecord = GetDataByKey(lID, objConnection, objTransaction);

            if (drRecord != null)
            {
                this.f_IsDataLoaded = true;
                this.f_LID = Convert.ToInt32(drRecord["LID"]);

                if (drRecord["Rollno"] != DBNull.Value)
                    this.f_Rollno = Convert.ToString(drRecord["Rollno"]);


                if (drRecord["Name"] != DBNull.Value)
                    this.f_Name = Convert.ToString(drRecord["Name"]);


                if (drRecord["StdName"] != DBNull.Value)
                    this.f_StdName = Convert.ToString(drRecord["StdName"]);


                if (drRecord["Message"] != DBNull.Value)
                    this.f_Message = Convert.ToString(drRecord["Message"]);


                if (drRecord["Nodays"] != DBNull.Value)
                    this.f_Nodays = Convert.ToInt32(drRecord["Nodays"]);


                if (drRecord["Reply"] != DBNull.Value)
                    this.f_Reply = Convert.ToString(drRecord["Reply"]);


                if (drRecord["Edate"] != DBNull.Value)
                    this.f_Edate = Convert.ToDateTime(drRecord["Edate"]);


                if (drRecord["StudentId"] != DBNull.Value)
                    this.f_StudentId = Convert.ToInt32(drRecord["StudentId"]);

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

        public static DataRow GetDataByKey(int lID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@LID", lID);

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

        public static int GetNewLID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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

                    if (objTransaction != null)
                        dt = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, string.Format(Paging, Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty)), sqlparams).Tables[0];
                    else
                        dt = SqlHelper.ExecuteDataset(CommandType.Text, string.Format(Paging, Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty), Query_SelectAll_WithPaging + strWhere + (!string.IsNullOrEmpty(OrderBy) ? OrderBy : string.Empty)), sqlparams).Tables[0];

                    ReturnTotalRows = int.Parse(sqlparamTotalRows.Value.ToString());
                }
                else
                {
                    if (objTransaction != null)
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
                SqlParameter[] sqlparams = new SqlParameter[9];
                sqlparams[0] = new SqlParameter("@LID", f_LID);

                if (!string.IsNullOrEmpty(f_Rollno))
                    sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
                else
                    sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[2] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[3] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[3] = new SqlParameter("@StdName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Message))
                    sqlparams[4] = new SqlParameter("@Message", f_Message);
                else
                    sqlparams[4] = new SqlParameter("@Message", DBNull.Value);

                if (f_Nodays.HasValue)
                    sqlparams[5] = new SqlParameter("@Nodays", f_Nodays.Value);
                else
                    sqlparams[5] = new SqlParameter("@Nodays", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Reply))
                    sqlparams[6] = new SqlParameter("@Reply", f_Reply);
                else
                    sqlparams[6] = new SqlParameter("@Reply", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[7] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[7] = new SqlParameter("@Edate", DBNull.Value);

                if (f_StudentId.HasValue)
                    sqlparams[8] = new SqlParameter("@StudentId", f_StudentId.Value);
                else
                    sqlparams[8] = new SqlParameter("@StudentId", DBNull.Value);

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
                SqlParameter[] sqlparams = new SqlParameter[9];
                sqlparams[0] = new SqlParameter("@LID", f_LID);

                if (!string.IsNullOrEmpty(f_Rollno))
                    sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
                else
                    sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[2] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[3] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[3] = new SqlParameter("@StdName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Message))
                    sqlparams[4] = new SqlParameter("@Message", f_Message);
                else
                    sqlparams[4] = new SqlParameter("@Message", DBNull.Value);

                if (f_Nodays.HasValue)
                    sqlparams[5] = new SqlParameter("@Nodays", f_Nodays.Value);
                else
                    sqlparams[5] = new SqlParameter("@Nodays", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Reply))
                    sqlparams[6] = new SqlParameter("@Reply", f_Reply);
                else
                    sqlparams[6] = new SqlParameter("@Reply", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[7] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[7] = new SqlParameter("@Edate", DBNull.Value);

                if (f_StudentId.HasValue)
                    sqlparams[8] = new SqlParameter("@StudentId", f_StudentId.Value);
                else
                    sqlparams[8] = new SqlParameter("@StudentId", DBNull.Value);

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

        public static DataTable GetAllDataRecordByStId(string status,int? StdId = null, int? StudentId = null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;
            string sb = "";
            try
            {
                const string Query_GetAllRDataByStId = @" SELECT  LeaveMst.[Message]
      ,LeaveMst.[Nodays]
      ,LeaveMst.[Reply]
      ,LeaveMst.[Edate]
      ,LeaveMst.[StudentId],StudentMst.* FROM LeaveMst
INNER JOIN StudentMst ON StudentMst.SID =LeaveMst.StudentId
WHERE ( StudentMst.StdId = @StdId  OR @StdId IS NULL )
AND  ( StudentMst.SID = @StudentId  OR @StudentId IS NULL )";
                SqlParameter[] sqlparams = new SqlParameter[2];
                if (StdId != null)
                    sqlparams[0] = new SqlParameter("@StdId", StdId);
                else
                    sqlparams[0] = new SqlParameter("@StdId", DBNull.Value);
              
                if (StudentId != null)
                    sqlparams[1] = new SqlParameter("@StudentId", StudentId);
                else
                    sqlparams[1] = new SqlParameter("@StudentId", DBNull.Value);
                DataSet ds = null;
                if (status != "")
                {
                    sb = " AND Reply='" + status + "'";
                }

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetAllRDataByStId + sb, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetAllRDataByStId + sb, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetAllRDataByStId + sb, sqlparams);

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
	}
}

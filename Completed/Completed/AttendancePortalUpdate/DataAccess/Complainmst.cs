using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
	public class Complainmst : BaseClass
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

        private int f_CID;
        private string f_Rollno;
        private string f_Name;
        private string f_Subject;
        private string f_Message;
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

        public int CID
        {
            get { return f_CID; }
            set { f_CID = value; }
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

        public string Subject
        {
            get { return f_Subject; }
            set { f_Subject = value; }
        }

        public string Message
        {
            get { return f_Message; }
            set { f_Message = value; }
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

        const string Query_SelectAll = @" SELECT * FROM [Complainmst] ";

        const string Query_SelectAll_WithPaging = @" SELECT [CID], [Rollno], [Name], [Subject], [Message], [Reply], [Edate], [StudentId], ROW_NUMBER() Over (Order by CID) as 'Row' FROM [Complainmst] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [CID], [Rollno], [Name], [Subject], [Message], [Reply], [Edate], [StudentId] FROM [Complainmst] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(CID), 0) + 1 as CID FROM Complainmst";

        const string Query_GetData_byKey = @" SELECT * FROM [Complainmst] WHERE [CID] = @CID ";

        const string Query_Insert = @" INSERT INTO [Complainmst] ([CID], [Rollno], [Name], [Subject], [Message], [Reply], [Edate], [StudentId])
		VALUES(@CID, @Rollno, @Name, @Subject, @Message, @Reply, @Edate, @StudentId) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [Complainmst] SET [Rollno] = @Rollno, [Name] = @Name, [Subject] = @Subject, [Message] = @Message, [Reply] = @Reply, [Edate] = @Edate, [StudentId] = @StudentId
		WHERE [CID] = @CID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [Complainmst] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

        #endregion

        #region Constructors

        public Complainmst()
        { }

        public Complainmst(int cID)
        {
            Load(cID);
        }

        public Complainmst(int cID, SqlConnection objConnection)
        {
            Load(cID, objConnection);
        }

        public Complainmst(int cID, SqlConnection objConnection, SqlTransaction objTransaction)
        {
            Load(cID, objConnection, objTransaction);
        }

        #endregion

        #region Loading Methods

        private void Load(int cID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow drRecord = GetDataByKey(cID, objConnection, objTransaction);

            if (drRecord != null)
            {
                this.f_IsDataLoaded = true;
                this.f_CID = Convert.ToInt32(drRecord["CID"]);

                if (drRecord["Rollno"] != DBNull.Value)
                    this.f_Rollno = Convert.ToString(drRecord["Rollno"]);


                if (drRecord["Name"] != DBNull.Value)
                    this.f_Name = Convert.ToString(drRecord["Name"]);


                if (drRecord["Subject"] != DBNull.Value)
                    this.f_Subject = Convert.ToString(drRecord["Subject"]);


                if (drRecord["Message"] != DBNull.Value)
                    this.f_Message = Convert.ToString(drRecord["Message"]);


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

        public static DataRow GetDataByKey(int cID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@CID", cID);

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

        public static int GetNewCID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
                SqlParameter[] sqlparams = new SqlParameter[8];
                sqlparams[0] = new SqlParameter("@CID", f_CID);

                if (!string.IsNullOrEmpty(f_Rollno))
                    sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
                else
                    sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[2] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Subject))
                    sqlparams[3] = new SqlParameter("@Subject", f_Subject);
                else
                    sqlparams[3] = new SqlParameter("@Subject", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Message))
                    sqlparams[4] = new SqlParameter("@Message", f_Message);
                else
                    sqlparams[4] = new SqlParameter("@Message", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Reply))
                    sqlparams[5] = new SqlParameter("@Reply", f_Reply);
                else
                    sqlparams[5] = new SqlParameter("@Reply", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[6] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[6] = new SqlParameter("@Edate", DBNull.Value);

                if (f_StudentId.HasValue)
                    sqlparams[7] = new SqlParameter("@StudentId", f_StudentId.Value);
                else
                    sqlparams[7] = new SqlParameter("@StudentId", DBNull.Value);

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
                SqlParameter[] sqlparams = new SqlParameter[8];
                sqlparams[0] = new SqlParameter("@CID", f_CID);

                if (!string.IsNullOrEmpty(f_Rollno))
                    sqlparams[1] = new SqlParameter("@Rollno", f_Rollno);
                else
                    sqlparams[1] = new SqlParameter("@Rollno", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[2] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[2] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Subject))
                    sqlparams[3] = new SqlParameter("@Subject", f_Subject);
                else
                    sqlparams[3] = new SqlParameter("@Subject", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Message))
                    sqlparams[4] = new SqlParameter("@Message", f_Message);
                else
                    sqlparams[4] = new SqlParameter("@Message", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Reply))
                    sqlparams[5] = new SqlParameter("@Reply", f_Reply);
                else
                    sqlparams[5] = new SqlParameter("@Reply", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[6] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[6] = new SqlParameter("@Edate", DBNull.Value);

                if (f_StudentId.HasValue)
                    sqlparams[7] = new SqlParameter("@StudentId", f_StudentId.Value);
                else
                    sqlparams[7] = new SqlParameter("@StudentId", DBNull.Value);

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

        public static DataTable GetDataByStaffId(int StaffId, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@StaffId", StaffId);

                const string Query_GetData_byKey = @" SELECT StudentMst.RollNo,StudentMst.Name,Complainmst.* FROM StaffMst  

INNER JOIN StudentMst ON StudentMst.StdId = StaffMst.StdId
INNER JOIN Complainmst ON Complainmst.StudentId= StudentMst.StdId
Where StaffMst.SID = @StaffId";

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

        public static DataTable GetAllDataRecordByStId(string status, int? StdId = null, int? StudentId = null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;
            string sb = "";
            try
            {
                const string Query_GetAllRDataByStId = @" SELECT [Complainmst].[CID]
      ,[Complainmst].[Subject]
      ,[Complainmst].[Message]
      ,[Complainmst].[Reply]
      ,[Complainmst].[Edate]
      ,[Complainmst].[StudentId],StudentMst.* FROM Complainmst
INNER JOIN StudentMst ON StudentMst.SID =Complainmst.StudentId
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
                    sb = " AND Replay='" + status + "'";
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

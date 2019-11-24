using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    public class StudentResult : BaseClass
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
        private ExamMst f_ExamID_ExamMst;

        private int f_ResultID;
        private int f_StudentId;
        private int f_ExamID;
        private Nullable<decimal> f_TotalMarks;
        private Nullable<decimal> f_ObtainMarks;

        //	Not database fields
        private bool f_IsDataLoaded;

        #endregion

        #region Properties

        public StudentMst StudentId_StudentMst
        {
            get
            {
                if (this.f_StudentId_StudentMst == null)
                    this.f_StudentId_StudentMst = new StudentMst(this.f_StudentId);

                return this.f_StudentId_StudentMst;
            }
        }

        public ExamMst ExamID_ExamMst
        {
            get
            {
                if (this.f_ExamID_ExamMst == null)
                    this.f_ExamID_ExamMst = new ExamMst(this.f_ExamID);

                return this.f_ExamID_ExamMst;
            }
        }

        public int ResultID
        {
            get { return f_ResultID; }
            set { f_ResultID = value; }
        }

        public int StudentId
        {
            get { return f_StudentId; }
            set { f_StudentId = value; }
        }

        public int ExamID
        {
            get { return f_ExamID; }
            set { f_ExamID = value; }
        }

        public Nullable<decimal> TotalMarks
        {
            get { return f_TotalMarks; }
            set { f_TotalMarks = value; }
        }

        public Nullable<decimal> ObtainMarks
        {
            get { return f_ObtainMarks; }
            set { f_ObtainMarks = value; }
        }

        public bool IsDataLoaded
        {
            get { return f_IsDataLoaded; }
        }

        #endregion

        #region Constant Strings

        const string Query_SelectAll = @" SELECT * FROM [StudentResult] ";

        const string Query_SelectAll_WithPaging = @" SELECT [ResultID], [StudentId], [ExamID], [TotalMarks], [ObtainMarks], ROW_NUMBER() Over (Order by ResultID) as 'Row' FROM [StudentResult] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [ResultID], [StudentId], [ExamID], [TotalMarks], [ObtainMarks] FROM [StudentResult] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(ResultID), 0) + 1 as ResultID FROM StudentResult";

        const string Query_GetData_byKey = @" SELECT * FROM [StudentResult] WHERE [ResultID] = @ResultID ";

        const string Query_Insert = @" INSERT INTO [StudentResult] ([ResultID], [StudentId], [ExamID], [TotalMarks], [ObtainMarks])
		VALUES(@ResultID, @StudentId, @ExamID, @TotalMarks, @ObtainMarks) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [StudentResult] SET [StudentId] = @StudentId, [ExamID] = @ExamID, [TotalMarks] = @TotalMarks, [ObtainMarks] = @ObtainMarks
		WHERE [ResultID] = @ResultID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [StudentResult] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

        #endregion

        #region Constructors

        public StudentResult()
        { }

        public StudentResult(int resultID)
        {
            Load(resultID);
        }

        public StudentResult(int resultID, SqlConnection objConnection)
        {
            Load(resultID, objConnection);
        }

        public StudentResult(int resultID, SqlConnection objConnection, SqlTransaction objTransaction)
        {
            Load(resultID, objConnection, objTransaction);
        }

        #endregion

        #region Loading Methods

        private void Load(int resultID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow drRecord = GetDataByKey(resultID, objConnection, objTransaction);

            if (drRecord != null)
            {
                this.f_IsDataLoaded = true;
                this.f_ResultID = Convert.ToInt32(drRecord["ResultID"]);
                this.f_StudentId = Convert.ToInt32(drRecord["StudentId"]);
                this.f_ExamID = Convert.ToInt32(drRecord["ExamID"]);

                if (drRecord["TotalMarks"] != DBNull.Value)
                    this.f_TotalMarks = Convert.ToDecimal(drRecord["TotalMarks"]);


                if (drRecord["ObtainMarks"] != DBNull.Value)
                    this.f_ObtainMarks = Convert.ToDecimal(drRecord["ObtainMarks"]);

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

        public static DataRow GetDataByKey(int resultID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@ResultID", resultID);

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

        public static int GetNewResultID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@ResultID", f_ResultID);
                sqlparams[1] = new SqlParameter("@StudentId", f_StudentId);
                sqlparams[2] = new SqlParameter("@ExamID", f_ExamID);

                if (f_TotalMarks.HasValue)
                    sqlparams[3] = new SqlParameter("@TotalMarks", f_TotalMarks.Value);
                else
                    sqlparams[3] = new SqlParameter("@TotalMarks", DBNull.Value);

                if (f_ObtainMarks.HasValue)
                    sqlparams[4] = new SqlParameter("@ObtainMarks", f_ObtainMarks.Value);
                else
                    sqlparams[4] = new SqlParameter("@ObtainMarks", DBNull.Value);

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
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@ResultID", f_ResultID);
                sqlparams[1] = new SqlParameter("@StudentId", f_StudentId);
                sqlparams[2] = new SqlParameter("@ExamID", f_ExamID);

                if (f_TotalMarks.HasValue)
                    sqlparams[3] = new SqlParameter("@TotalMarks", f_TotalMarks.Value);
                else
                    sqlparams[3] = new SqlParameter("@TotalMarks", DBNull.Value);

                if (f_ObtainMarks.HasValue)
                    sqlparams[4] = new SqlParameter("@ObtainMarks", f_ObtainMarks.Value);
                else
                    sqlparams[4] = new SqlParameter("@ObtainMarks", DBNull.Value);

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

        public static DataTable GetAllResultDataByStId(int? StdId = null, int? StudentId = null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                if (StdId != null)
                    sqlparams[0] = new SqlParameter("@StdId", StdId);
                else
                    sqlparams[0] = new SqlParameter("@StdId", DBNull.Value);

                if (StudentId != null)
                    sqlparams[1] = new SqlParameter("@StudentId", StudentId);
                else
                    sqlparams[1] = new SqlParameter("@StudentId", DBNull.Value);

                const string Query_GetAllResultDataByStId = @" SELECT ResultID,ExamMst.Name as ExamName,[StudentResult].*,StudentMst.* FROM [StudentResult]
INNER JOIN ExamMst ON ExamMst.ExamId =[StudentResult].ExamId
INNER JOIN StudentMst ON StudentMst.SID =[StudentResult].StudentId
WHERE ( StudentMst.StdId = @StdId  OR @StdId IS NULL )
AND  ( StudentMst.SID = @StudentId  OR @StudentId IS NULL )";
                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetAllResultDataByStId, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetAllResultDataByStId, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetAllResultDataByStId, sqlparams);

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

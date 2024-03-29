using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
	public class StudentMst : BaseClass
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

        private int f_SID;
        private string f_RollNo;
        private string f_Name;
        private string f_StdName;
        private string f_DivName;
        private string f_Email;
        private string f_Mobile;
        private string f_DOB;
        private string f_Image;
        private string f_Add;
        private string f_City;
        private string f_Pincode;
        private string f_Uname;
        private string f_Pass;
        private Nullable<DateTime> f_EDate;
        private Nullable<int> f_MachineCode;
        private Nullable<int> f_DivId;
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

        public int SID
        {
            get { return f_SID; }
            set { f_SID = value; }
        }

        public string RollNo
        {
            get { return f_RollNo; }
            set { f_RollNo = value; }
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

        public string DivName
        {
            get { return f_DivName; }
            set { f_DivName = value; }
        }

        public string Email
        {
            get { return f_Email; }
            set { f_Email = value; }
        }

        public string Mobile
        {
            get { return f_Mobile; }
            set { f_Mobile = value; }
        }

        public string DOB
        {
            get { return f_DOB; }
            set { f_DOB = value; }
        }

        public string Image
        {
            get { return f_Image; }
            set { f_Image = value; }
        }

        public string Add
        {
            get { return f_Add; }
            set { f_Add = value; }
        }

        public string City
        {
            get { return f_City; }
            set { f_City = value; }
        }

        public string Pincode
        {
            get { return f_Pincode; }
            set { f_Pincode = value; }
        }

        public string Uname
        {
            get { return f_Uname; }
            set { f_Uname = value; }
        }

        public string Pass
        {
            get { return f_Pass; }
            set { f_Pass = value; }
        }

        public Nullable<DateTime> EDate
        {
            get { return f_EDate; }
            set { f_EDate = value; }
        }

        public Nullable<int> MachineCode
        {
            get { return f_MachineCode; }
            set { f_MachineCode = value; }
        }

        public Nullable<int> DivId
        {
            get { return f_DivId; }
            set { f_DivId = value; }
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

        const string Query_SelectAll = @" SELECT * FROM [StudentMst] ";

        const string Query_SelectAll_WithPaging = @" SELECT [SID], [RollNo], [Name], [StdName], [DivName], [Email], [Mobile], [DOB], [Image], [Add], [City], [Pincode], [Uname], [Pass], [EDate], [MachineCode], [DivId], [StdId], ROW_NUMBER() Over (Order by SID) as 'Row' FROM [StudentMst] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [SID], [RollNo], [Name], [StdName], [DivName], [Email], [Mobile], [DOB], [Image], [Add], [City], [Pincode], [Uname], [Pass], [EDate], [MachineCode], [DivId], [StdId] FROM [StudentMst] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(SID), 0) + 1 as SID FROM StudentMst";

        const string Query_GetData_byKey = @" SELECT * FROM [StudentMst] WHERE [SID] = @SID ";

        const string Query_Insert = @" INSERT INTO [StudentMst] ([SID], [RollNo], [Name], [StdName], [DivName], [Email], [Mobile], [DOB], [Image], [Add], [City], [Pincode], [Uname], [Pass], [EDate], [MachineCode], [DivId], [StdId])
		VALUES(@SID, @RollNo, @Name, @StdName, @DivName, @Email, @Mobile, @DOB, @Image, @Add, @City, @Pincode, @Uname, @Pass, @EDate, @MachineCode, @DivId, @StdId) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [StudentMst] SET [RollNo] = @RollNo, [Name] = @Name, [StdName] = @StdName, [DivName] = @DivName, [Email] = @Email, [Mobile] = @Mobile, [DOB] = @DOB, [Image] = @Image, [Add] = @Add, [City] = @City, [Pincode] = @Pincode, [Uname] = @Uname, [Pass] = @Pass, [EDate] = @EDate, [MachineCode] = @MachineCode, [DivId] = @DivId, [StdId] = @StdId
		WHERE [SID] = @SID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [StudentMst] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";


        const string Query_GetData_byNameandPass = @" SELECT * FROM [StudentMst] WHERE [Uname] = @Uname and Pass =@Pass ";

        const string Query_Delete = @" Delete FROM StudentMst where SID =@SID";




        #endregion

        #region Constructors

        public StudentMst()
        { }

        public StudentMst(int sID)
        {
            Load(sID);
        }

        public StudentMst(int sID, SqlConnection objConnection)
        {
            Load(sID, objConnection);
        }

        public StudentMst(int sID, SqlConnection objConnection, SqlTransaction objTransaction)
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

                if (drRecord["RollNo"] != DBNull.Value)
                    this.f_RollNo = Convert.ToString(drRecord["RollNo"]);

                this.f_Name = Convert.ToString(drRecord["Name"]);

                if (drRecord["StdName"] != DBNull.Value)
                    this.f_StdName = Convert.ToString(drRecord["StdName"]);


                if (drRecord["DivName"] != DBNull.Value)
                    this.f_DivName = Convert.ToString(drRecord["DivName"]);


                if (drRecord["Email"] != DBNull.Value)
                    this.f_Email = Convert.ToString(drRecord["Email"]);


                if (drRecord["Mobile"] != DBNull.Value)
                    this.f_Mobile = Convert.ToString(drRecord["Mobile"]);


                if (drRecord["DOB"] != DBNull.Value)
                    this.f_DOB = Convert.ToString(drRecord["DOB"]);


                if (drRecord["Image"] != DBNull.Value)
                    this.f_Image = Convert.ToString(drRecord["Image"]);


                if (drRecord["Add"] != DBNull.Value)
                    this.f_Add = Convert.ToString(drRecord["Add"]);


                if (drRecord["City"] != DBNull.Value)
                    this.f_City = Convert.ToString(drRecord["City"]);


                if (drRecord["Pincode"] != DBNull.Value)
                    this.f_Pincode = Convert.ToString(drRecord["Pincode"]);


                if (drRecord["Uname"] != DBNull.Value)
                    this.f_Uname = Convert.ToString(drRecord["Uname"]);


                if (drRecord["Pass"] != DBNull.Value)
                    this.f_Pass = Convert.ToString(drRecord["Pass"]);


                if (drRecord["EDate"] != DBNull.Value)
                    this.f_EDate = Convert.ToDateTime(drRecord["EDate"]);


                if (drRecord["MachineCode"] != DBNull.Value)
                    this.f_MachineCode = Convert.ToInt32(drRecord["MachineCode"]);


                if (drRecord["DivId"] != DBNull.Value)
                    this.f_DivId = Convert.ToInt32(drRecord["DivId"]);


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

        public static DataRow GetDataByKey(int sID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@SID", sID);

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

        public static int GetNewSID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
                SqlParameter[] sqlparams = new SqlParameter[18];
                sqlparams[0] = new SqlParameter("@SID", f_SID);

                if (!string.IsNullOrEmpty(f_RollNo))
                    sqlparams[1] = new SqlParameter("@RollNo", f_RollNo);
                else
                    sqlparams[1] = new SqlParameter("@RollNo", DBNull.Value);

                sqlparams[2] = new SqlParameter("@Name", f_Name);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[3] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[3] = new SqlParameter("@StdName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_DivName))
                    sqlparams[4] = new SqlParameter("@DivName", f_DivName);
                else
                    sqlparams[4] = new SqlParameter("@DivName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Email))
                    sqlparams[5] = new SqlParameter("@Email", f_Email);
                else
                    sqlparams[5] = new SqlParameter("@Email", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Mobile))
                    sqlparams[6] = new SqlParameter("@Mobile", f_Mobile);
                else
                    sqlparams[6] = new SqlParameter("@Mobile", DBNull.Value);

                if (!string.IsNullOrEmpty(f_DOB))
                    sqlparams[7] = new SqlParameter("@DOB", f_DOB);
                else
                    sqlparams[7] = new SqlParameter("@DOB", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Image))
                    sqlparams[8] = new SqlParameter("@Image", f_Image);
                else
                    sqlparams[8] = new SqlParameter("@Image", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Add))
                    sqlparams[9] = new SqlParameter("@Add", f_Add);
                else
                    sqlparams[9] = new SqlParameter("@Add", DBNull.Value);

                if (!string.IsNullOrEmpty(f_City))
                    sqlparams[10] = new SqlParameter("@City", f_City);
                else
                    sqlparams[10] = new SqlParameter("@City", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pincode))
                    sqlparams[11] = new SqlParameter("@Pincode", f_Pincode);
                else
                    sqlparams[11] = new SqlParameter("@Pincode", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Uname))
                    sqlparams[12] = new SqlParameter("@Uname", f_Uname);
                else
                    sqlparams[12] = new SqlParameter("@Uname", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pass))
                    sqlparams[13] = new SqlParameter("@Pass", f_Pass);
                else
                    sqlparams[13] = new SqlParameter("@Pass", DBNull.Value);

                if (f_EDate.HasValue)
                    sqlparams[14] = new SqlParameter("@EDate", f_EDate.Value);
                else
                    sqlparams[14] = new SqlParameter("@EDate", DBNull.Value);

                if (f_MachineCode.HasValue)
                    sqlparams[15] = new SqlParameter("@MachineCode", f_MachineCode.Value);
                else
                    sqlparams[15] = new SqlParameter("@MachineCode", DBNull.Value);

                if (f_DivId.HasValue)
                    sqlparams[16] = new SqlParameter("@DivId", f_DivId.Value);
                else
                    sqlparams[16] = new SqlParameter("@DivId", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[17] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[17] = new SqlParameter("@StdId", DBNull.Value);

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
                SqlParameter[] sqlparams = new SqlParameter[18];
                sqlparams[0] = new SqlParameter("@SID", f_SID);

                if (!string.IsNullOrEmpty(f_RollNo))
                    sqlparams[1] = new SqlParameter("@RollNo", f_RollNo);
                else
                    sqlparams[1] = new SqlParameter("@RollNo", DBNull.Value);

                sqlparams[2] = new SqlParameter("@Name", f_Name);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[3] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[3] = new SqlParameter("@StdName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_DivName))
                    sqlparams[4] = new SqlParameter("@DivName", f_DivName);
                else
                    sqlparams[4] = new SqlParameter("@DivName", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Email))
                    sqlparams[5] = new SqlParameter("@Email", f_Email);
                else
                    sqlparams[5] = new SqlParameter("@Email", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Mobile))
                    sqlparams[6] = new SqlParameter("@Mobile", f_Mobile);
                else
                    sqlparams[6] = new SqlParameter("@Mobile", DBNull.Value);

                if (!string.IsNullOrEmpty(f_DOB))
                    sqlparams[7] = new SqlParameter("@DOB", f_DOB);
                else
                    sqlparams[7] = new SqlParameter("@DOB", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Image))
                    sqlparams[8] = new SqlParameter("@Image", f_Image);
                else
                    sqlparams[8] = new SqlParameter("@Image", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Add))
                    sqlparams[9] = new SqlParameter("@Add", f_Add);
                else
                    sqlparams[9] = new SqlParameter("@Add", DBNull.Value);

                if (!string.IsNullOrEmpty(f_City))
                    sqlparams[10] = new SqlParameter("@City", f_City);
                else
                    sqlparams[10] = new SqlParameter("@City", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pincode))
                    sqlparams[11] = new SqlParameter("@Pincode", f_Pincode);
                else
                    sqlparams[11] = new SqlParameter("@Pincode", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Uname))
                    sqlparams[12] = new SqlParameter("@Uname", f_Uname);
                else
                    sqlparams[12] = new SqlParameter("@Uname", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pass))
                    sqlparams[13] = new SqlParameter("@Pass", f_Pass);
                else
                    sqlparams[13] = new SqlParameter("@Pass", DBNull.Value);

                if (f_EDate.HasValue)
                    sqlparams[14] = new SqlParameter("@EDate", f_EDate.Value);
                else
                    sqlparams[14] = new SqlParameter("@EDate", DBNull.Value);

                if (f_MachineCode.HasValue)
                    sqlparams[15] = new SqlParameter("@MachineCode", f_MachineCode.Value);
                else
                    sqlparams[15] = new SqlParameter("@MachineCode", DBNull.Value);

                if (f_DivId.HasValue)
                    sqlparams[16] = new SqlParameter("@DivId", f_DivId.Value);
                else
                    sqlparams[16] = new SqlParameter("@DivId", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[17] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[17] = new SqlParameter("@StdId", DBNull.Value);

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


        public static DataRow StdLogin(string name, string pass, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@Uname", name);
                sqlparams[1] = new SqlParameter("@Pass", pass);

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byNameandPass, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetData_byNameandPass, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetData_byNameandPass, sqlparams);

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


        public static int Delete(int Id, SqlTransaction objTransaction = null)
        {
            int result = 0;

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

            public static DataRow GetMaxRow( SqlConnection objConnection = null, SqlTransaction objTransaction = null)
            {
                DataRow dr = null;

                try
                {
                const string Query_GetMaxRow_byKey = @" SELECT TOP 1 * FROM [StudentMst]  ORder by SID desc ";
                SqlParameter[] sqlparams = new SqlParameter[0];

                    DataSet ds = null;

                    if (objTransaction != null)
                        ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetMaxRow_byKey, sqlparams);
                    else if (objConnection != null)
                        ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetMaxRow_byKey, sqlparams);
                    else
                        ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetMaxRow_byKey, sqlparams);

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

            public static DataTable GetAllResultDataByDivID(int DivId, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
            {
                DataTable dt = null;

                try
                {
                    SqlParameter[] sqlparams = new SqlParameter[1];
                    if (DivId != null)
                        sqlparams[0] = new SqlParameter("@DivId", DivId);
                    else
                        sqlparams[0] = new SqlParameter("@DivId", DBNull.Value);


                    const string Query_GetAllResultDataByStId = @" SELECT * FROM [StudentMst]  WHERE [DivId] = @DivId";
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

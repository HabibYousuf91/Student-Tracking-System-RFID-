using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace Models
{
	public class StaffMst : BaseClass
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
        private string f_Name;
        private string f_StdName;
        private Nullable<int> f_StdId;
        private string f_Email;
        private string f_Mobile;
        private string f_Image;
        private string f_Qualification;
        private string f_Add;
        private string f_City;
        private string f_Pincode;
        private string f_Uname;
        private string f_Pass;
        private string f_Gender;
        private Nullable<DateTime> f_Edate;

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

        public Nullable<int> StdId
        {
            get { return f_StdId; }
            set { f_StdId = value; }
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

        public string Image
        {
            get { return f_Image; }
            set { f_Image = value; }
        }

        public string Qualification
        {
            get { return f_Qualification; }
            set { f_Qualification = value; }
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

        public string Gender
        {
            get { return f_Gender; }
            set { f_Gender = value; }
        }

        public Nullable<DateTime> Edate
        {
            get { return f_Edate; }
            set { f_Edate = value; }
        }

        public bool IsDataLoaded
        {
            get { return f_IsDataLoaded; }
        }

        #endregion

        #region Constant Strings

        const string Query_SelectAll = @" SELECT * FROM [StaffMst] ";

        const string Query_SelectAll_WithPaging = @" SELECT [SID], [Name], [StdName], [StdId], [Email], [Mobile], [Image], [Qualification], [Add], [City], [Pincode], [Uname], [Pass], [Gender], [Edate], ROW_NUMBER() Over (Order by SID) as 'Row' FROM [StaffMst] ";

        const string Query_SelectAll_WithoutPaging = @" SELECT [SID], [Name], [StdName], [StdId], [Email], [Mobile], [Image], [Qualification], [Add], [City], [Pincode], [Uname], [Pass], [Gender], [Edate] FROM [StaffMst] ";

        const string Query_Get_MaxId = @" SELECT IsNull(max(SID), 0) + 1 as SID FROM StaffMst";

        const string Query_GetData_byKey = @" SELECT * FROM [StaffMst] WHERE [SID] = @SID ";

        const string Query_Insert = @" INSERT INTO [StaffMst] ([SID], [Name], [StdName], [StdId], [Email], [Mobile], [Image], [Qualification], [Add], [City], [Pincode], [Uname], [Pass], [Gender], [Edate])
		VALUES(@SID, @Name, @StdName, @StdId, @Email, @Mobile, @Image, @Qualification, @Add, @City, @Pincode, @Uname, @Pass, @Gender, @Edate) ";

        const string Query_Update_By_PrimaryKey = @" UPDATE [StaffMst] SET [Name] = @Name, [StdName] = @StdName, [StdId] = @StdId, [Email] = @Email, [Mobile] = @Mobile, [Image] = @Image, [Qualification] = @Qualification, [Add] = @Add, [City] = @City, [Pincode] = @Pincode, [Uname] = @Uname, [Pass] = @Pass, [Gender] = @Gender, [Edate] = @Edate
		WHERE [SID] = @SID ";

        const string Query_Select_Field = @" SELECT {0} [{1}] FROM [StaffMst] WHERE [{2}] like '%' + @Value + '%'";

        const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

        #endregion

        #region Constructors

        public StaffMst()
        { }

        public StaffMst(int sID)
        {
            Load(sID);
        }

        public StaffMst(int sID, SqlConnection objConnection)
        {
            Load(sID, objConnection);
        }

        public StaffMst(int sID, SqlConnection objConnection, SqlTransaction objTransaction)
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

                if (drRecord["Name"] != DBNull.Value)
                    this.f_Name = Convert.ToString(drRecord["Name"]);


                if (drRecord["StdName"] != DBNull.Value)
                    this.f_StdName = Convert.ToString(drRecord["StdName"]);


                if (drRecord["StdId"] != DBNull.Value)
                    this.f_StdId = Convert.ToInt32(drRecord["StdId"]);


                if (drRecord["Email"] != DBNull.Value)
                    this.f_Email = Convert.ToString(drRecord["Email"]);


                if (drRecord["Mobile"] != DBNull.Value)
                    this.f_Mobile = Convert.ToString(drRecord["Mobile"]);


                if (drRecord["Image"] != DBNull.Value)
                    this.f_Image = Convert.ToString(drRecord["Image"]);


                if (drRecord["Qualification"] != DBNull.Value)
                    this.f_Qualification = Convert.ToString(drRecord["Qualification"]);


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


                if (drRecord["Gender"] != DBNull.Value)
                    this.f_Gender = Convert.ToString(drRecord["Gender"]);


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
                SqlParameter[] sqlparams = new SqlParameter[15];
                sqlparams[0] = new SqlParameter("@SID", f_SID);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[1] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[1] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[2] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[2] = new SqlParameter("@StdName", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[3] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[3] = new SqlParameter("@StdId", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Email))
                    sqlparams[4] = new SqlParameter("@Email", f_Email);
                else
                    sqlparams[4] = new SqlParameter("@Email", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Mobile))
                    sqlparams[5] = new SqlParameter("@Mobile", f_Mobile);
                else
                    sqlparams[5] = new SqlParameter("@Mobile", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Image))
                    sqlparams[6] = new SqlParameter("@Image", f_Image);
                else
                    sqlparams[6] = new SqlParameter("@Image", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Qualification))
                    sqlparams[7] = new SqlParameter("@Qualification", f_Qualification);
                else
                    sqlparams[7] = new SqlParameter("@Qualification", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Add))
                    sqlparams[8] = new SqlParameter("@Add", f_Add);
                else
                    sqlparams[8] = new SqlParameter("@Add", DBNull.Value);

                if (!string.IsNullOrEmpty(f_City))
                    sqlparams[9] = new SqlParameter("@City", f_City);
                else
                    sqlparams[9] = new SqlParameter("@City", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pincode))
                    sqlparams[10] = new SqlParameter("@Pincode", f_Pincode);
                else
                    sqlparams[10] = new SqlParameter("@Pincode", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Uname))
                    sqlparams[11] = new SqlParameter("@Uname", f_Uname);
                else
                    sqlparams[11] = new SqlParameter("@Uname", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pass))
                    sqlparams[12] = new SqlParameter("@Pass", f_Pass);
                else
                    sqlparams[12] = new SqlParameter("@Pass", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Gender))
                    sqlparams[13] = new SqlParameter("@Gender", f_Gender);
                else
                    sqlparams[13] = new SqlParameter("@Gender", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[14] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[14] = new SqlParameter("@Edate", DBNull.Value);

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
                SqlParameter[] sqlparams = new SqlParameter[15];
                sqlparams[0] = new SqlParameter("@SID", f_SID);

                if (!string.IsNullOrEmpty(f_Name))
                    sqlparams[1] = new SqlParameter("@Name", f_Name);
                else
                    sqlparams[1] = new SqlParameter("@Name", DBNull.Value);

                if (!string.IsNullOrEmpty(f_StdName))
                    sqlparams[2] = new SqlParameter("@StdName", f_StdName);
                else
                    sqlparams[2] = new SqlParameter("@StdName", DBNull.Value);

                if (f_StdId.HasValue)
                    sqlparams[3] = new SqlParameter("@StdId", f_StdId.Value);
                else
                    sqlparams[3] = new SqlParameter("@StdId", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Email))
                    sqlparams[4] = new SqlParameter("@Email", f_Email);
                else
                    sqlparams[4] = new SqlParameter("@Email", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Mobile))
                    sqlparams[5] = new SqlParameter("@Mobile", f_Mobile);
                else
                    sqlparams[5] = new SqlParameter("@Mobile", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Image))
                    sqlparams[6] = new SqlParameter("@Image", f_Image);
                else
                    sqlparams[6] = new SqlParameter("@Image", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Qualification))
                    sqlparams[7] = new SqlParameter("@Qualification", f_Qualification);
                else
                    sqlparams[7] = new SqlParameter("@Qualification", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Add))
                    sqlparams[8] = new SqlParameter("@Add", f_Add);
                else
                    sqlparams[8] = new SqlParameter("@Add", DBNull.Value);

                if (!string.IsNullOrEmpty(f_City))
                    sqlparams[9] = new SqlParameter("@City", f_City);
                else
                    sqlparams[9] = new SqlParameter("@City", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pincode))
                    sqlparams[10] = new SqlParameter("@Pincode", f_Pincode);
                else
                    sqlparams[10] = new SqlParameter("@Pincode", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Uname))
                    sqlparams[11] = new SqlParameter("@Uname", f_Uname);
                else
                    sqlparams[11] = new SqlParameter("@Uname", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Pass))
                    sqlparams[12] = new SqlParameter("@Pass", f_Pass);
                else
                    sqlparams[12] = new SqlParameter("@Pass", DBNull.Value);

                if (!string.IsNullOrEmpty(f_Gender))
                    sqlparams[13] = new SqlParameter("@Gender", f_Gender);
                else
                    sqlparams[13] = new SqlParameter("@Gender", DBNull.Value);

                if (f_Edate.HasValue)
                    sqlparams[14] = new SqlParameter("@Edate", f_Edate.Value);
                else
                    sqlparams[14] = new SqlParameter("@Edate", DBNull.Value);

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

        #region
        const string Query_GetData_byNameandPass = @" SELECT * FROM [StaffMst] WHERE [UName] = @UName and Pass =@Pass ";

        const string Query_GetData_byUName = @" SELECT * FROM [StaffMst] WHERE [UName] = @UName";

        const string Query_GetData_byUNameandPassword = @" SELECT * FROM [StaffMst] WHERE [UName] = @UName and Pass =@Pass";
        public static DataRow StaffLogin(string name, string pass, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@UName", name);
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

        public static DataTable Select_UNAME(string name, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@UName", name);

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byUName, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetData_byUName, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetData_byUName, sqlparams);

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
        public static DataRow GetNameandPassword(string name, string pass, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataRow dr = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@Name", name);
                sqlparams[1] = new SqlParameter("@Pass", pass);

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetData_byUNameandPassword, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetData_byUNameandPassword, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetData_byUNameandPassword, sqlparams);

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
        #endregion

        public static int Delete(int Id, SqlTransaction objTransaction = null)
        {
            int result = 0;

            const string Query_Delete = @" Delete FROM StaffMst where SID =@SID";

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
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
	public partial class CHECKINOUT : BaseClass
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

		private int f_USERID;
		private DateTime f_CHECKTIME;
		private string f_CHECKTYPE;
		private Nullable<int> f_VERIFYCODE;
		private string f_SENSORID;
		private string f_Memoinfo;
		private string f_WorkCode;
		private string f_sn;
		private Nullable<short> f_UserExtFmt;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int USERID
		{
			get{ return f_USERID; }
			set{ f_USERID = value; }
		}

		public DateTime CHECKTIME
		{
			get{ return f_CHECKTIME; }
			set{ f_CHECKTIME = value; }
		}

		public string CHECKTYPE
		{
			get{ return f_CHECKTYPE; }
			set{ f_CHECKTYPE = value; }
		}

		public Nullable<int> VERIFYCODE
		{
			get{ return f_VERIFYCODE; }
			set{ f_VERIFYCODE = value; }
		}

		public string SENSORID
		{
			get{ return f_SENSORID; }
			set{ f_SENSORID = value; }
		}

		public string Memoinfo
		{
			get{ return f_Memoinfo; }
			set{ f_Memoinfo = value; }
		}

		public string WorkCode
		{
			get{ return f_WorkCode; }
			set{ f_WorkCode = value; }
		}

		public string sn
		{
			get{ return f_sn; }
			set{ f_sn = value; }
		}

		public Nullable<short> UserExtFmt
		{
			get{ return f_UserExtFmt; }
			set{ f_UserExtFmt = value; }
		}

		public bool IsDataLoaded
		{
			get{ return f_IsDataLoaded; }
		}

		#endregion

		#region Constant Strings

		const string Query_SelectAll = @" SELECT * FROM [CHECKINOUT] WHERE ([CHECKTIME] = @CHECKTIME OR @CHECKTIME is null) ";

		const string Query_SelectAll_WithPaging = @" SELECT [USERID], [CHECKTIME], [CHECKTYPE], [VERIFYCODE], [SENSORID], [Memoinfo], [WorkCode], [sn], [UserExtFmt], ROW_NUMBER() Over (Order by CHECKTIME, USERID) as 'Row' FROM [CHECKINOUT] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [USERID], [CHECKTIME], [CHECKTYPE], [VERIFYCODE], [SENSORID], [Memoinfo], [WorkCode], [sn], [UserExtFmt] FROM [CHECKINOUT] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(USERID), 0) + 1 as USERID FROM CHECKINOUT WHERE [CHECKTIME] = @CHECKTIME ";

		const string Query_GetData_byKey = @" SELECT * FROM [CHECKINOUT] WHERE [CHECKTIME] = @CHECKTIME and [USERID] = @USERID ";

		const string Query_Insert = @" INSERT INTO [CHECKINOUT] ([USERID], [CHECKTIME], [CHECKTYPE], [VERIFYCODE], [SENSORID], [Memoinfo], [WorkCode], [sn], [UserExtFmt])
		VALUES(@USERID, @CHECKTIME, @CHECKTYPE, @VERIFYCODE, @SENSORID, @Memoinfo, @WorkCode, @sn, @UserExtFmt) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [CHECKINOUT] SET [CHECKTYPE] = @CHECKTYPE, [VERIFYCODE] = @VERIFYCODE, [SENSORID] = @SENSORID, [Memoinfo] = @Memoinfo, [WorkCode] = @WorkCode, [sn] = @sn, [UserExtFmt] = @UserExtFmt
		WHERE [CHECKTIME] = @CHECKTIME and [USERID] = @USERID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [CHECKINOUT] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";


        const string Query_GetAttendanceRecord = @" Select MachineAlias,RollNo, StudentMst.Name,Email,CHECKTIME from StudentMst
INNER JOIN CHECKINOUT ON StudentMst.MachineCode = CHECKINOUT.USERID
INNER JOIN Machines ON Machines.MachineNumber= CHECKINOUT.SENSORID

WHERE ([SID] = @StudentId OR @StudentId is null) ";


		#endregion

		#region Constructors

		public CHECKINOUT()
		{ }

		public CHECKINOUT(DateTime cHECKTIME, int uSERID)
		{
			Load(cHECKTIME, uSERID);
		}

		public CHECKINOUT(DateTime cHECKTIME, int uSERID, SqlConnection objConnection)
		{
			Load(cHECKTIME, uSERID, objConnection);
		}

		public CHECKINOUT(DateTime cHECKTIME, int uSERID, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(cHECKTIME, uSERID, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(DateTime cHECKTIME, int uSERID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(cHECKTIME, uSERID, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_USERID = Convert.ToInt32(drRecord["USERID"]);
				this.f_CHECKTIME = Convert.ToDateTime(drRecord["CHECKTIME"]);

				if (drRecord["CHECKTYPE"] != DBNull.Value)
					this.f_CHECKTYPE = Convert.ToString(drRecord["CHECKTYPE"]);


				if (drRecord["VERIFYCODE"] != DBNull.Value)
					this.f_VERIFYCODE = Convert.ToInt32(drRecord["VERIFYCODE"]);


				if (drRecord["SENSORID"] != DBNull.Value)
					this.f_SENSORID = Convert.ToString(drRecord["SENSORID"]);


				if (drRecord["Memoinfo"] != DBNull.Value)
					this.f_Memoinfo = Convert.ToString(drRecord["Memoinfo"]);


				if (drRecord["WorkCode"] != DBNull.Value)
					this.f_WorkCode = Convert.ToString(drRecord["WorkCode"]);


				if (drRecord["sn"] != DBNull.Value)
					this.f_sn = Convert.ToString(drRecord["sn"]);


				if (drRecord["UserExtFmt"] != DBNull.Value)
					this.f_UserExtFmt = Convert.ToInt16(drRecord["UserExtFmt"]);

			}
		}

		#endregion

		#region Class Methods

		public static DataTable GetAllActiveData(Nullable<DateTime> cHECKTIME =null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataTable dt = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@CHECKTIME", cHECKTIME);

				DataSet ds = null;

				if(objTransaction != null)
					ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_SelectAll, sqlparams);
				else if(objConnection != null)
					ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_SelectAll, sqlparams);
				else
					ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_SelectAll, sqlparams);

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

		public static DataRow GetDataByKey(DateTime cHECKTIME, int uSERID, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[2];
				sqlparams[0] = new SqlParameter("@CHECKTIME", cHECKTIME);
				sqlparams[1] = new SqlParameter("@USERID", uSERID);

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

		public static int GetNewUSERID(DateTime cHECKTIME, SqlTransaction objTransaction = null, SqlConnection objConnection = null)
		{
			int ID = 0;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@CHECKTIME", cHECKTIME);

				if(objTransaction != null)
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(objTransaction, CommandType.Text, Query_Get_MaxId, sqlparams));
				else if(objConnection != null)
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(objConnection, CommandType.Text, Query_Get_MaxId, sqlparams));
				else
					ID = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, Query_Get_MaxId, sqlparams));
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
				SqlParameter[] sqlparams = new SqlParameter[9];
				sqlparams[0] = new SqlParameter("@USERID", f_USERID);
				sqlparams[1] = new SqlParameter("@CHECKTIME", f_CHECKTIME);

				if(!string.IsNullOrEmpty(f_CHECKTYPE))
					sqlparams[2] = new SqlParameter("@CHECKTYPE", f_CHECKTYPE);
				else
					sqlparams[2] = new SqlParameter("@CHECKTYPE", DBNull.Value);

				if(f_VERIFYCODE.HasValue)
					sqlparams[3] = new SqlParameter("@VERIFYCODE", f_VERIFYCODE.Value);
				else
					sqlparams[3] = new SqlParameter("@VERIFYCODE", DBNull.Value);

				if(!string.IsNullOrEmpty(f_SENSORID))
					sqlparams[4] = new SqlParameter("@SENSORID", f_SENSORID);
				else
					sqlparams[4] = new SqlParameter("@SENSORID", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Memoinfo))
					sqlparams[5] = new SqlParameter("@Memoinfo", f_Memoinfo);
				else
					sqlparams[5] = new SqlParameter("@Memoinfo", DBNull.Value);

				if(!string.IsNullOrEmpty(f_WorkCode))
					sqlparams[6] = new SqlParameter("@WorkCode", f_WorkCode);
				else
					sqlparams[6] = new SqlParameter("@WorkCode", DBNull.Value);

				if(!string.IsNullOrEmpty(f_sn))
					sqlparams[7] = new SqlParameter("@sn", f_sn);
				else
					sqlparams[7] = new SqlParameter("@sn", DBNull.Value);

				if(f_UserExtFmt.HasValue)
					sqlparams[8] = new SqlParameter("@UserExtFmt", f_UserExtFmt.Value);
				else
					sqlparams[8] = new SqlParameter("@UserExtFmt", DBNull.Value);

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
				SqlParameter[] sqlparams = new SqlParameter[9];
				sqlparams[0] = new SqlParameter("@USERID", f_USERID);
				sqlparams[1] = new SqlParameter("@CHECKTIME", f_CHECKTIME);

				if(!string.IsNullOrEmpty(f_CHECKTYPE))
					sqlparams[2] = new SqlParameter("@CHECKTYPE", f_CHECKTYPE);
				else
					sqlparams[2] = new SqlParameter("@CHECKTYPE", DBNull.Value);

				if(f_VERIFYCODE.HasValue)
					sqlparams[3] = new SqlParameter("@VERIFYCODE", f_VERIFYCODE.Value);
				else
					sqlparams[3] = new SqlParameter("@VERIFYCODE", DBNull.Value);

				if(!string.IsNullOrEmpty(f_SENSORID))
					sqlparams[4] = new SqlParameter("@SENSORID", f_SENSORID);
				else
					sqlparams[4] = new SqlParameter("@SENSORID", DBNull.Value);

				if(!string.IsNullOrEmpty(f_Memoinfo))
					sqlparams[5] = new SqlParameter("@Memoinfo", f_Memoinfo);
				else
					sqlparams[5] = new SqlParameter("@Memoinfo", DBNull.Value);

				if(!string.IsNullOrEmpty(f_WorkCode))
					sqlparams[6] = new SqlParameter("@WorkCode", f_WorkCode);
				else
					sqlparams[6] = new SqlParameter("@WorkCode", DBNull.Value);

				if(!string.IsNullOrEmpty(f_sn))
					sqlparams[7] = new SqlParameter("@sn", f_sn);
				else
					sqlparams[7] = new SqlParameter("@sn", DBNull.Value);

				if(f_UserExtFmt.HasValue)
					sqlparams[8] = new SqlParameter("@UserExtFmt", f_UserExtFmt.Value);
				else
					sqlparams[8] = new SqlParameter("@UserExtFmt", DBNull.Value);

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

        public static DataTable GetAllDataRecord( int? StudentId =null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                if (StudentId != null)
                    sqlparams[0] = new SqlParameter("@StudentId", StudentId);
                else
                    sqlparams[0] = new SqlParameter("@StudentId", DBNull.Value);
                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_GetAttendanceRecord, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_GetAttendanceRecord, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_GetAttendanceRecord, sqlparams);

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


        public static DataTable GetAllMonthlyAttendanceRecord(int? StudentId = null, int? month =null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                if (StudentId != null)
                    sqlparams[0] = new SqlParameter("@StudentId", StudentId);
                else
                    sqlparams[0] = new SqlParameter("@StudentId", DBNull.Value);

                sqlparams[1] = new SqlParameter("@month", month);

                const string Query_AllMonthlyAttendanceRecord = @"Select Distinct theDate,RollNo, StudentMst.Name,Email,
CASE  WHEN isholiday =1 then 'Holiday'  WHEN CHECKTIME is not null then  'Present' else 'Abscent' end  status from [SET_DateTime]
INNER JOIN StudentMst ON 1 = 1

LEFT JOIN  CHECKINOUT ON convert(date,CHECKINOUT.CHECKTIME) = convert(date,[SET_DateTime].thedate)
AND StudentMst.MachineCode = CHECKINOUT.USERID

LEFT JOIN Machines ON Machines.MachineNumber= CHECKINOUT.SENSORID

where YEAR([SET_DateTime].thedate) = YEAR(GETDATE()) and  MONTH([SET_DateTime].thedate)=@month

AND ([SID] = @StudentId OR @StudentId is null) 
 ";

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_AllMonthlyAttendanceRecord, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_AllMonthlyAttendanceRecord, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_AllMonthlyAttendanceRecord, sqlparams);

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


        public static DataTable GetAllMonthlyAttendanceRecordByDivId(int? month = null, int? divId = null, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
        {
            DataTable dt = null;

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@month", month);

                if (divId != null)
                    sqlparams[1] = new SqlParameter("@divId", divId);
                else
                    sqlparams[1] = new SqlParameter("@divId", DBNull.Value);

                const string Query_AllMonthlyAttendanceRecordByDivId = @"Select Distinct theDate,RollNo, StudentMst.Name,Email,
CASE  WHEN isholiday =1 then 'Holiday'  WHEN CHECKTIME is not null then  'Present' else 'Abscent' end  status from [SET_DateTime]
LEFT JOIN StudentMst ON 1 = 1

LEFT JOIN  CHECKINOUT ON convert(date,CHECKINOUT.CHECKTIME) = convert(date,[SET_DateTime].thedate)
AND StudentMst.MachineCode = CHECKINOUT.USERID

LEFT JOIN Machines ON Machines.MachineNumber= CHECKINOUT.SENSORID

where YEAR([SET_DateTime].thedate) = YEAR(GETDATE()) and  MONTH([SET_DateTime].thedate)=@month

AND ([divId] = @divId OR @divId is null) ";

                DataSet ds = null;

                if (objTransaction != null)
                    ds = SqlHelper.ExecuteDataset(objTransaction, CommandType.Text, Query_AllMonthlyAttendanceRecordByDivId, sqlparams);
                else if (objConnection != null)
                    ds = SqlHelper.ExecuteDataset(objConnection, CommandType.Text, Query_AllMonthlyAttendanceRecordByDivId, sqlparams);
                else
                    ds = SqlHelper.ExecuteDataset(CommandType.Text, Query_AllMonthlyAttendanceRecordByDivId, sqlparams);

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
		#endregion
	}
}

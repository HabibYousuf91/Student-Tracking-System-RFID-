using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Models
{ 
public class Machines : BaseClass
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

		private int f_ID;
		private string f_MachineAlias;
		private int f_ConnectType;
		private string f_IP;
		private Nullable<int> f_SerialPort;
		private Nullable<int> f_Port;
		private Nullable<int> f_Baudrate;
		private int f_MachineNumber;
		private Nullable<bool> f_IsHost;
		private Nullable<bool> f_Enabled;
		private string f_CommPassword;
		private Nullable<short> f_UILanguage;
		private Nullable<short> f_DateFormat;
		private Nullable<short> f_InOutRecordWarn;
		private Nullable<short> f_Idle;
		private Nullable<short> f_Voice;
		private Nullable<short> f_managercount;
		private Nullable<short> f_usercount;
		private Nullable<short> f_fingercount;
		private Nullable<short> f_SecretCount;
		private string f_FirmwareVersion;
		private string f_ProductType;
		private Nullable<short> f_LockControl;
		private Nullable<short> f_Purpose;
		private Nullable<int> f_ProduceKind;
		private string f_sn;
		private string f_PhotoStamp;
		private Nullable<int> f_IsIfChangeConfigServer2;
		private string f_IsAndroid;

		//	Not database fields
		private bool f_IsDataLoaded;

		#endregion

		#region Properties

		public int ID
		{
			get{ return f_ID; }
			set{ f_ID = value; }
		}

		public string MachineAlias
		{
			get{ return f_MachineAlias; }
			set{ f_MachineAlias = value; }
		}

		public int ConnectType
		{
			get{ return f_ConnectType; }
			set{ f_ConnectType = value; }
		}

		public string IP
		{
			get{ return f_IP; }
			set{ f_IP = value; }
		}

		public Nullable<int> SerialPort
		{
			get{ return f_SerialPort; }
			set{ f_SerialPort = value; }
		}

		public Nullable<int> Port
		{
			get{ return f_Port; }
			set{ f_Port = value; }
		}

		public Nullable<int> Baudrate
		{
			get{ return f_Baudrate; }
			set{ f_Baudrate = value; }
		}

		public int MachineNumber
		{
			get{ return f_MachineNumber; }
			set{ f_MachineNumber = value; }
		}

		public Nullable<bool> IsHost
		{
			get{ return f_IsHost; }
			set{ f_IsHost = value; }
		}

		public Nullable<bool> Enabled
		{
			get{ return f_Enabled; }
			set{ f_Enabled = value; }
		}

		public string CommPassword
		{
			get{ return f_CommPassword; }
			set{ f_CommPassword = value; }
		}

		public Nullable<short> UILanguage
		{
			get{ return f_UILanguage; }
			set{ f_UILanguage = value; }
		}

		public Nullable<short> DateFormat
		{
			get{ return f_DateFormat; }
			set{ f_DateFormat = value; }
		}

		public Nullable<short> InOutRecordWarn
		{
			get{ return f_InOutRecordWarn; }
			set{ f_InOutRecordWarn = value; }
		}

		public Nullable<short> Idle
		{
			get{ return f_Idle; }
			set{ f_Idle = value; }
		}

		public Nullable<short> Voice
		{
			get{ return f_Voice; }
			set{ f_Voice = value; }
		}

		public Nullable<short> managercount
		{
			get{ return f_managercount; }
			set{ f_managercount = value; }
		}

		public Nullable<short> usercount
		{
			get{ return f_usercount; }
			set{ f_usercount = value; }
		}

		public Nullable<short> fingercount
		{
			get{ return f_fingercount; }
			set{ f_fingercount = value; }
		}

		public Nullable<short> SecretCount
		{
			get{ return f_SecretCount; }
			set{ f_SecretCount = value; }
		}

		public string FirmwareVersion
		{
			get{ return f_FirmwareVersion; }
			set{ f_FirmwareVersion = value; }
		}

		public string ProductType
		{
			get{ return f_ProductType; }
			set{ f_ProductType = value; }
		}

		public Nullable<short> LockControl
		{
			get{ return f_LockControl; }
			set{ f_LockControl = value; }
		}

		public Nullable<short> Purpose
		{
			get{ return f_Purpose; }
			set{ f_Purpose = value; }
		}

		public Nullable<int> ProduceKind
		{
			get{ return f_ProduceKind; }
			set{ f_ProduceKind = value; }
		}

		public string sn
		{
			get{ return f_sn; }
			set{ f_sn = value; }
		}

		public string PhotoStamp
		{
			get{ return f_PhotoStamp; }
			set{ f_PhotoStamp = value; }
		}

		public Nullable<int> IsIfChangeConfigServer2
		{
			get{ return f_IsIfChangeConfigServer2; }
			set{ f_IsIfChangeConfigServer2 = value; }
		}

		public string IsAndroid
		{
			get{ return f_IsAndroid; }
			set{ f_IsAndroid = value; }
		}

		public bool IsDataLoaded
		{
			get{ return f_IsDataLoaded; }
		}

		#endregion

		#region Constant Strings

		const string Query_SelectAll = @" SELECT * FROM [Machines] ";

		const string Query_SelectAll_WithPaging = @" SELECT [ID], [MachineAlias], [ConnectType], [IP], [SerialPort], [Port], [Baudrate], [MachineNumber], [IsHost], [Enabled], [CommPassword], [UILanguage], [DateFormat], [InOutRecordWarn], [Idle], [Voice], [managercount], [usercount], [fingercount], [SecretCount], [FirmwareVersion], [ProductType], [LockControl], [Purpose], [ProduceKind], [sn], [PhotoStamp], [IsIfChangeConfigServer2], [IsAndroid], ROW_NUMBER() Over (Order by ID) as 'Row' FROM [Machines] "; 

		const string Query_SelectAll_WithoutPaging = @" SELECT [ID], [MachineAlias], [ConnectType], [IP], [SerialPort], [Port], [Baudrate], [MachineNumber], [IsHost], [Enabled], [CommPassword], [UILanguage], [DateFormat], [InOutRecordWarn], [Idle], [Voice], [managercount], [usercount], [fingercount], [SecretCount], [FirmwareVersion], [ProductType], [LockControl], [Purpose], [ProduceKind], [sn], [PhotoStamp], [IsIfChangeConfigServer2], [IsAndroid] FROM [Machines] "; 

		const string Query_Get_MaxId = @" SELECT IsNull(max(ID), 0) + 1 as ID FROM Machines";

		const string Query_GetData_byKey = @" SELECT * FROM [Machines] WHERE [ID] = @ID ";

		const string Query_Insert = @" INSERT INTO [Machines] ([ID], [MachineAlias], [ConnectType], [IP], [SerialPort], [Port], [Baudrate], [MachineNumber], [IsHost], [Enabled], [CommPassword], [UILanguage], [DateFormat], [InOutRecordWarn], [Idle], [Voice], [managercount], [usercount], [fingercount], [SecretCount], [FirmwareVersion], [ProductType], [LockControl], [Purpose], [ProduceKind], [sn], [PhotoStamp], [IsIfChangeConfigServer2], [IsAndroid])
		VALUES(@ID, @MachineAlias, @ConnectType, @IP, @SerialPort, @Port, @Baudrate, @MachineNumber, @IsHost, @Enabled, @CommPassword, @UILanguage, @DateFormat, @InOutRecordWarn, @Idle, @Voice, @managercount, @usercount, @fingercount, @SecretCount, @FirmwareVersion, @ProductType, @LockControl, @Purpose, @ProduceKind, @sn, @PhotoStamp, @IsIfChangeConfigServer2, @IsAndroid) ";

		const string Query_Update_By_PrimaryKey = @" UPDATE [Machines] SET [MachineAlias] = @MachineAlias, [ConnectType] = @ConnectType, [IP] = @IP, [SerialPort] = @SerialPort, [Port] = @Port, [Baudrate] = @Baudrate, [MachineNumber] = @MachineNumber, [IsHost] = @IsHost, [Enabled] = @Enabled, [CommPassword] = @CommPassword, [UILanguage] = @UILanguage, [DateFormat] = @DateFormat, [InOutRecordWarn] = @InOutRecordWarn, [Idle] = @Idle, [Voice] = @Voice, [managercount] = @managercount, [usercount] = @usercount, [fingercount] = @fingercount, [SecretCount] = @SecretCount, [FirmwareVersion] = @FirmwareVersion, [ProductType] = @ProductType, [LockControl] = @LockControl, [Purpose] = @Purpose, [ProduceKind] = @ProduceKind, [sn] = @sn, [PhotoStamp] = @PhotoStamp, [IsIfChangeConfigServer2] = @IsIfChangeConfigServer2, [IsAndroid] = @IsAndroid
		WHERE [ID] = @ID ";

		const string Query_Select_Field = @" SELECT {0} [{1}] FROM [Machines] WHERE [{2}] like '%' + @Value + '%'";

		const string Paging = @"WITH ResultSet AS({0})
			SELECT * FROM ResultSet WHERE Row between (@PageIndex - 1) * @PageSize + 1 and @PageIndex * @PageSize;
			SET @TotalRows = (SELECT COUNT(*) FROM({1}) as Query)";

		#endregion

		#region Constructors

		public Machines()
		{ }

		public Machines(int iD)
		{
			Load(iD);
		}

		public Machines(int iD, SqlConnection objConnection)
		{
			Load(iD, objConnection);
		}

		public Machines(int iD, SqlConnection objConnection, SqlTransaction objTransaction)
		{
			Load(iD, objConnection, objTransaction);
		}

		#endregion

		#region Loading Methods

		private void Load(int iD, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow drRecord = GetDataByKey(iD, objConnection, objTransaction);

			if (drRecord != null)
			{
				this.f_IsDataLoaded = true;
				this.f_ID = Convert.ToInt32(drRecord["ID"]);
				this.f_MachineAlias = Convert.ToString(drRecord["MachineAlias"]);
				this.f_ConnectType = Convert.ToInt32(drRecord["ConnectType"]);

				if (drRecord["IP"] != DBNull.Value)
					this.f_IP = Convert.ToString(drRecord["IP"]);


				if (drRecord["SerialPort"] != DBNull.Value)
					this.f_SerialPort = Convert.ToInt32(drRecord["SerialPort"]);


				if (drRecord["Port"] != DBNull.Value)
					this.f_Port = Convert.ToInt32(drRecord["Port"]);


				if (drRecord["Baudrate"] != DBNull.Value)
					this.f_Baudrate = Convert.ToInt32(drRecord["Baudrate"]);

				this.f_MachineNumber = Convert.ToInt32(drRecord["MachineNumber"]);

				if (drRecord["IsHost"] != DBNull.Value)
					this.f_IsHost = Convert.ToBoolean(drRecord["IsHost"]);


				if (drRecord["Enabled"] != DBNull.Value)
					this.f_Enabled = Convert.ToBoolean(drRecord["Enabled"]);


				if (drRecord["CommPassword"] != DBNull.Value)
					this.f_CommPassword = Convert.ToString(drRecord["CommPassword"]);


				if (drRecord["UILanguage"] != DBNull.Value)
					this.f_UILanguage = Convert.ToInt16(drRecord["UILanguage"]);


				if (drRecord["DateFormat"] != DBNull.Value)
					this.f_DateFormat = Convert.ToInt16(drRecord["DateFormat"]);


				if (drRecord["InOutRecordWarn"] != DBNull.Value)
					this.f_InOutRecordWarn = Convert.ToInt16(drRecord["InOutRecordWarn"]);


				if (drRecord["Idle"] != DBNull.Value)
					this.f_Idle = Convert.ToInt16(drRecord["Idle"]);


				if (drRecord["Voice"] != DBNull.Value)
					this.f_Voice = Convert.ToInt16(drRecord["Voice"]);


				if (drRecord["managercount"] != DBNull.Value)
					this.f_managercount = Convert.ToInt16(drRecord["managercount"]);


				if (drRecord["usercount"] != DBNull.Value)
					this.f_usercount = Convert.ToInt16(drRecord["usercount"]);


				if (drRecord["fingercount"] != DBNull.Value)
					this.f_fingercount = Convert.ToInt16(drRecord["fingercount"]);


				if (drRecord["SecretCount"] != DBNull.Value)
					this.f_SecretCount = Convert.ToInt16(drRecord["SecretCount"]);


				if (drRecord["FirmwareVersion"] != DBNull.Value)
					this.f_FirmwareVersion = Convert.ToString(drRecord["FirmwareVersion"]);


				if (drRecord["ProductType"] != DBNull.Value)
					this.f_ProductType = Convert.ToString(drRecord["ProductType"]);


				if (drRecord["LockControl"] != DBNull.Value)
					this.f_LockControl = Convert.ToInt16(drRecord["LockControl"]);


				if (drRecord["Purpose"] != DBNull.Value)
					this.f_Purpose = Convert.ToInt16(drRecord["Purpose"]);


				if (drRecord["ProduceKind"] != DBNull.Value)
					this.f_ProduceKind = Convert.ToInt32(drRecord["ProduceKind"]);


				if (drRecord["sn"] != DBNull.Value)
					this.f_sn = Convert.ToString(drRecord["sn"]);


				if (drRecord["PhotoStamp"] != DBNull.Value)
					this.f_PhotoStamp = Convert.ToString(drRecord["PhotoStamp"]);


				if (drRecord["IsIfChangeConfigServer2"] != DBNull.Value)
					this.f_IsIfChangeConfigServer2 = Convert.ToInt32(drRecord["IsIfChangeConfigServer2"]);


				if (drRecord["IsAndroid"] != DBNull.Value)
					this.f_IsAndroid = Convert.ToString(drRecord["IsAndroid"]);

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

		public static DataRow GetDataByKey(int iD, SqlConnection objConnection = null, SqlTransaction objTransaction = null)
		{
			DataRow dr = null;

			try
			{
				SqlParameter[] sqlparams = new SqlParameter[1];
				sqlparams[0] = new SqlParameter("@ID", iD);

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

		public static int GetNewID(SqlTransaction objTransaction = null, SqlConnection objConnection = null)
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
				SqlParameter[] sqlparams = new SqlParameter[29];
				sqlparams[0] = new SqlParameter("@ID", f_ID);
				sqlparams[1] = new SqlParameter("@MachineAlias", f_MachineAlias);
				sqlparams[2] = new SqlParameter("@ConnectType", f_ConnectType);

				if(!string.IsNullOrEmpty(f_IP))
					sqlparams[3] = new SqlParameter("@IP", f_IP);
				else
					sqlparams[3] = new SqlParameter("@IP", DBNull.Value);

				if(f_SerialPort.HasValue)
					sqlparams[4] = new SqlParameter("@SerialPort", f_SerialPort.Value);
				else
					sqlparams[4] = new SqlParameter("@SerialPort", DBNull.Value);

				if(f_Port.HasValue)
					sqlparams[5] = new SqlParameter("@Port", f_Port.Value);
				else
					sqlparams[5] = new SqlParameter("@Port", DBNull.Value);

				if(f_Baudrate.HasValue)
					sqlparams[6] = new SqlParameter("@Baudrate", f_Baudrate.Value);
				else
					sqlparams[6] = new SqlParameter("@Baudrate", DBNull.Value);

				sqlparams[7] = new SqlParameter("@MachineNumber", f_MachineNumber);

				if(f_IsHost.HasValue)
					sqlparams[8] = new SqlParameter("@IsHost", f_IsHost.Value);
				else
					sqlparams[8] = new SqlParameter("@IsHost", DBNull.Value);

				if(f_Enabled.HasValue)
					sqlparams[9] = new SqlParameter("@Enabled", f_Enabled.Value);
				else
					sqlparams[9] = new SqlParameter("@Enabled", DBNull.Value);

				if(!string.IsNullOrEmpty(f_CommPassword))
					sqlparams[10] = new SqlParameter("@CommPassword", f_CommPassword);
				else
					sqlparams[10] = new SqlParameter("@CommPassword", DBNull.Value);

				if(f_UILanguage.HasValue)
					sqlparams[11] = new SqlParameter("@UILanguage", f_UILanguage.Value);
				else
					sqlparams[11] = new SqlParameter("@UILanguage", DBNull.Value);

				if(f_DateFormat.HasValue)
					sqlparams[12] = new SqlParameter("@DateFormat", f_DateFormat.Value);
				else
					sqlparams[12] = new SqlParameter("@DateFormat", DBNull.Value);

				if(f_InOutRecordWarn.HasValue)
					sqlparams[13] = new SqlParameter("@InOutRecordWarn", f_InOutRecordWarn.Value);
				else
					sqlparams[13] = new SqlParameter("@InOutRecordWarn", DBNull.Value);

				if(f_Idle.HasValue)
					sqlparams[14] = new SqlParameter("@Idle", f_Idle.Value);
				else
					sqlparams[14] = new SqlParameter("@Idle", DBNull.Value);

				if(f_Voice.HasValue)
					sqlparams[15] = new SqlParameter("@Voice", f_Voice.Value);
				else
					sqlparams[15] = new SqlParameter("@Voice", DBNull.Value);

				if(f_managercount.HasValue)
					sqlparams[16] = new SqlParameter("@managercount", f_managercount.Value);
				else
					sqlparams[16] = new SqlParameter("@managercount", DBNull.Value);

				if(f_usercount.HasValue)
					sqlparams[17] = new SqlParameter("@usercount", f_usercount.Value);
				else
					sqlparams[17] = new SqlParameter("@usercount", DBNull.Value);

				if(f_fingercount.HasValue)
					sqlparams[18] = new SqlParameter("@fingercount", f_fingercount.Value);
				else
					sqlparams[18] = new SqlParameter("@fingercount", DBNull.Value);

				if(f_SecretCount.HasValue)
					sqlparams[19] = new SqlParameter("@SecretCount", f_SecretCount.Value);
				else
					sqlparams[19] = new SqlParameter("@SecretCount", DBNull.Value);

				if(!string.IsNullOrEmpty(f_FirmwareVersion))
					sqlparams[20] = new SqlParameter("@FirmwareVersion", f_FirmwareVersion);
				else
					sqlparams[20] = new SqlParameter("@FirmwareVersion", DBNull.Value);

				if(!string.IsNullOrEmpty(f_ProductType))
					sqlparams[21] = new SqlParameter("@ProductType", f_ProductType);
				else
					sqlparams[21] = new SqlParameter("@ProductType", DBNull.Value);

				if(f_LockControl.HasValue)
					sqlparams[22] = new SqlParameter("@LockControl", f_LockControl.Value);
				else
					sqlparams[22] = new SqlParameter("@LockControl", DBNull.Value);

				if(f_Purpose.HasValue)
					sqlparams[23] = new SqlParameter("@Purpose", f_Purpose.Value);
				else
					sqlparams[23] = new SqlParameter("@Purpose", DBNull.Value);

				if(f_ProduceKind.HasValue)
					sqlparams[24] = new SqlParameter("@ProduceKind", f_ProduceKind.Value);
				else
					sqlparams[24] = new SqlParameter("@ProduceKind", DBNull.Value);

				if(!string.IsNullOrEmpty(f_sn))
					sqlparams[25] = new SqlParameter("@sn", f_sn);
				else
					sqlparams[25] = new SqlParameter("@sn", DBNull.Value);

				if(!string.IsNullOrEmpty(f_PhotoStamp))
					sqlparams[26] = new SqlParameter("@PhotoStamp", f_PhotoStamp);
				else
					sqlparams[26] = new SqlParameter("@PhotoStamp", DBNull.Value);

				if(f_IsIfChangeConfigServer2.HasValue)
					sqlparams[27] = new SqlParameter("@IsIfChangeConfigServer2", f_IsIfChangeConfigServer2.Value);
				else
					sqlparams[27] = new SqlParameter("@IsIfChangeConfigServer2", DBNull.Value);

				if(!string.IsNullOrEmpty(f_IsAndroid))
					sqlparams[28] = new SqlParameter("@IsAndroid", f_IsAndroid);
				else
					sqlparams[28] = new SqlParameter("@IsAndroid", DBNull.Value);

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
				SqlParameter[] sqlparams = new SqlParameter[29];
				sqlparams[0] = new SqlParameter("@ID", f_ID);
				sqlparams[1] = new SqlParameter("@MachineAlias", f_MachineAlias);
				sqlparams[2] = new SqlParameter("@ConnectType", f_ConnectType);

				if(!string.IsNullOrEmpty(f_IP))
					sqlparams[3] = new SqlParameter("@IP", f_IP);
				else
					sqlparams[3] = new SqlParameter("@IP", DBNull.Value);

				if(f_SerialPort.HasValue)
					sqlparams[4] = new SqlParameter("@SerialPort", f_SerialPort.Value);
				else
					sqlparams[4] = new SqlParameter("@SerialPort", DBNull.Value);

				if(f_Port.HasValue)
					sqlparams[5] = new SqlParameter("@Port", f_Port.Value);
				else
					sqlparams[5] = new SqlParameter("@Port", DBNull.Value);

				if(f_Baudrate.HasValue)
					sqlparams[6] = new SqlParameter("@Baudrate", f_Baudrate.Value);
				else
					sqlparams[6] = new SqlParameter("@Baudrate", DBNull.Value);

				sqlparams[7] = new SqlParameter("@MachineNumber", f_MachineNumber);

				if(f_IsHost.HasValue)
					sqlparams[8] = new SqlParameter("@IsHost", f_IsHost.Value);
				else
					sqlparams[8] = new SqlParameter("@IsHost", DBNull.Value);

				if(f_Enabled.HasValue)
					sqlparams[9] = new SqlParameter("@Enabled", f_Enabled.Value);
				else
					sqlparams[9] = new SqlParameter("@Enabled", DBNull.Value);

				if(!string.IsNullOrEmpty(f_CommPassword))
					sqlparams[10] = new SqlParameter("@CommPassword", f_CommPassword);
				else
					sqlparams[10] = new SqlParameter("@CommPassword", DBNull.Value);

				if(f_UILanguage.HasValue)
					sqlparams[11] = new SqlParameter("@UILanguage", f_UILanguage.Value);
				else
					sqlparams[11] = new SqlParameter("@UILanguage", DBNull.Value);

				if(f_DateFormat.HasValue)
					sqlparams[12] = new SqlParameter("@DateFormat", f_DateFormat.Value);
				else
					sqlparams[12] = new SqlParameter("@DateFormat", DBNull.Value);

				if(f_InOutRecordWarn.HasValue)
					sqlparams[13] = new SqlParameter("@InOutRecordWarn", f_InOutRecordWarn.Value);
				else
					sqlparams[13] = new SqlParameter("@InOutRecordWarn", DBNull.Value);

				if(f_Idle.HasValue)
					sqlparams[14] = new SqlParameter("@Idle", f_Idle.Value);
				else
					sqlparams[14] = new SqlParameter("@Idle", DBNull.Value);

				if(f_Voice.HasValue)
					sqlparams[15] = new SqlParameter("@Voice", f_Voice.Value);
				else
					sqlparams[15] = new SqlParameter("@Voice", DBNull.Value);

				if(f_managercount.HasValue)
					sqlparams[16] = new SqlParameter("@managercount", f_managercount.Value);
				else
					sqlparams[16] = new SqlParameter("@managercount", DBNull.Value);

				if(f_usercount.HasValue)
					sqlparams[17] = new SqlParameter("@usercount", f_usercount.Value);
				else
					sqlparams[17] = new SqlParameter("@usercount", DBNull.Value);

				if(f_fingercount.HasValue)
					sqlparams[18] = new SqlParameter("@fingercount", f_fingercount.Value);
				else
					sqlparams[18] = new SqlParameter("@fingercount", DBNull.Value);

				if(f_SecretCount.HasValue)
					sqlparams[19] = new SqlParameter("@SecretCount", f_SecretCount.Value);
				else
					sqlparams[19] = new SqlParameter("@SecretCount", DBNull.Value);

				if(!string.IsNullOrEmpty(f_FirmwareVersion))
					sqlparams[20] = new SqlParameter("@FirmwareVersion", f_FirmwareVersion);
				else
					sqlparams[20] = new SqlParameter("@FirmwareVersion", DBNull.Value);

				if(!string.IsNullOrEmpty(f_ProductType))
					sqlparams[21] = new SqlParameter("@ProductType", f_ProductType);
				else
					sqlparams[21] = new SqlParameter("@ProductType", DBNull.Value);

				if(f_LockControl.HasValue)
					sqlparams[22] = new SqlParameter("@LockControl", f_LockControl.Value);
				else
					sqlparams[22] = new SqlParameter("@LockControl", DBNull.Value);

				if(f_Purpose.HasValue)
					sqlparams[23] = new SqlParameter("@Purpose", f_Purpose.Value);
				else
					sqlparams[23] = new SqlParameter("@Purpose", DBNull.Value);

				if(f_ProduceKind.HasValue)
					sqlparams[24] = new SqlParameter("@ProduceKind", f_ProduceKind.Value);
				else
					sqlparams[24] = new SqlParameter("@ProduceKind", DBNull.Value);

				if(!string.IsNullOrEmpty(f_sn))
					sqlparams[25] = new SqlParameter("@sn", f_sn);
				else
					sqlparams[25] = new SqlParameter("@sn", DBNull.Value);

				if(!string.IsNullOrEmpty(f_PhotoStamp))
					sqlparams[26] = new SqlParameter("@PhotoStamp", f_PhotoStamp);
				else
					sqlparams[26] = new SqlParameter("@PhotoStamp", DBNull.Value);

				if(f_IsIfChangeConfigServer2.HasValue)
					sqlparams[27] = new SqlParameter("@IsIfChangeConfigServer2", f_IsIfChangeConfigServer2.Value);
				else
					sqlparams[27] = new SqlParameter("@IsIfChangeConfigServer2", DBNull.Value);

				if(!string.IsNullOrEmpty(f_IsAndroid))
					sqlparams[28] = new SqlParameter("@IsAndroid", f_IsAndroid);
				else
					sqlparams[28] = new SqlParameter("@IsAndroid", DBNull.Value);

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

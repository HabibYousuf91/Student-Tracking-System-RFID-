using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Models
{

    public class SqlHelper
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MyProjectConnectionString"].ConnectionString;

        public static Int32 ExecuteNonQuery(CommandType commandType, String commandText,
           params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect  
                    // type is only for OLE DB.   
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static Int32 ExecuteNonQuery(SqlTransaction objTransaction, CommandType commandType, String commandText,
           params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect  
                    // type is only for OLE DB.   
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command and only return one value. 
        /// </summary> 
        public static Object ExecuteScalar(CommandType commandType, String commandText,
             params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static Object ExecuteScalar(SqlTransaction objTransaction, CommandType commandType, String commandText,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static Object ExecuteScalar(SqlConnection objConnection, CommandType commandType, String commandText,
           params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary> 
        public static SqlDataReader ExecuteReader(CommandType commandType, String commandText,
             params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the  
                // IDataReader is closed. 
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }


        public static DataSet ExecuteDataset(CommandType commandType, String commandText,
          params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(commandText))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = commandType;
                        cmd.Parameters.AddRange(parameters);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                    }
                }
                return ds;
            }
        }
        public static DataSet ExecuteDataset(SqlTransaction objTransaction, CommandType commandType, String commandText,
        params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(commandText))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = commandType;
                        cmd.Parameters.AddRange(parameters);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                    }
                }
                return ds;
            }
        }
        public static DataSet ExecuteDataset(SqlConnection objConnection, CommandType commandType, String commandText,
        params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(commandText))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = commandType;
                        cmd.Parameters.AddRange(parameters);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                    }
                }
                return ds;
            }
        }
        public static void LogException(Exception ex)
        {

        }

        public static string GetConn()
        {
            string Password = "";
            string Connection = connectionString;
            Password = Connection.Substring(Connection.LastIndexOf(';') + 10, Connection.Length - (Connection.LastIndexOf(';') + 10));
            Connection = Connection.Remove(Connection.LastIndexOf(';') + 10, Connection.Length - (Connection.LastIndexOf(';') + 10));
            string ActualPassword = Password;
            Connection = Connection += ActualPassword;

            return Connection;
        }

        public static SqlTransaction GetTransaction(string TransactionName)
        {
            SqlTransaction sqltrans;
            SqlConnection sqlcon = new SqlConnection(GetConn());
            sqlcon.Open();
            sqltrans = sqlcon.BeginTransaction(TransactionName);
            return sqltrans;
        }

        public static void CommitTransaction(SqlTransaction transaction)
        {
            transaction.Commit();
            if (transaction.Connection != null)
                if (transaction.Connection.State == ConnectionState.Open)
                    transaction.Connection.Close();
        }

        public static void RollBackTransaction(SqlTransaction transaction, string TransactionName)
        {
            transaction.Rollback(TransactionName);
            if (transaction.Connection != null)
                if (transaction.Connection.State == ConnectionState.Open)
                    transaction.Connection.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
//using System.Data.OracleClient;
using System.IO;
namespace RTLM
{
    public class DbHelper : IDisposable
    {
        private string strConnectionString;
        private DbConnection objConnection;
        private DbCommand objCommand;
        private DbProviderFactory objFactory = null;
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;
        private string strLogFile;

        public DbHelper(string connectionString, Providers provider)
        {
            strConnectionString = connectionString;

            switch (provider)
            {
                case Providers.SqlServer:
                    objFactory = SqlClientFactory.Instance;
                    break;
                case Providers.OleDb:
                    objFactory = OleDbFactory.Instance;
                    break;
                case Providers.Oracle:
                    //objFactory = OracleClientFactory.Instance;
                    break;
                case Providers.ODBC:
                    objFactory = OdbcFactory.Instance;
                    break;
                case Providers.ConfigDefined:
                    string providername = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
                    switch (providername)
                    {
                        case "System.Data.SqlClient":
                            objFactory = SqlClientFactory.Instance;
                            break;
                        case "System.Data.OleDb":
                            objFactory = OleDbFactory.Instance;
                            break;
                        case "System.Data.OracleClient":
                            //objFactory = OracleClientFactory.Instance;
                            break;
                        case "System.Data.Odbc":
                            objFactory = OdbcFactory.Instance;
                            break;
                    }
                    break;
            }
            objConnection = objFactory.CreateConnection();
            objCommand = objFactory.CreateCommand();
            objConnection.ConnectionString = strConnectionString;
            objCommand.Connection = objConnection;
        }

        public DbHelper(Providers provider)
            : this(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, provider)
        {
        }

        public DbHelper(string connectionString)
            : this(connectionString, Providers.SqlServer)
        {
        }
        public DbHelper()
            : this(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, Providers.ConfigDefined)
        {
        }

        public bool HandleErrors
        {
            get
            {
                return boolHandleErrors;
            }
            set
            {
                boolHandleErrors = value;
            }
        }

        public string LastError
        {
            get
            {
                return strLastError;
            }
        }

        public bool LogErrors
        {
            get
            {
                return boolLogError;
            }
            set
            {
                boolLogError = value;
            }
        }

        public string LogFile
        {
            get
            {
                return strLogFile;
            }
            set
            {
                strLogFile = value;
            }
        }

        public int AddParameter(string name, object value)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return objCommand.Parameters.Add(p);
        }

        public int AddParameter(DbParameter parameter)
        {
            return objCommand.Parameters.Add(parameter);
        }

        public int AddParameter(DbParameter[] parameter)
        {
            int i = 1;
            foreach (DbParameter p in parameter)
                i *= objCommand.Parameters.Add(p);
            return i;
        }
        public void ClearParameters()
        {
            objCommand.Parameters.Clear();
        }
        public DbCommand Command
        {
            get
            {
                return objCommand;
            }
        }

        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            objConnection.Close();
        }

        public void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            objConnection.Close();
        }

        public int ExecuteNonQuery(string query, CommandType commandType, DBHelperConnectionState connectionState)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandType;
            int i = 0;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionState == DBHelperConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }
            return i;
        }

        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, DBHelperConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query, CommandType commandType)
        {
            return ExecuteNonQuery(query, commandType, DBHelperConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query, DBHelperConnectionState connectionState)
        {
            return ExecuteNonQuery(query, CommandType.Text, connectionState);
        }

        public object ExecuteScalar(string query, CommandType commandType, DBHelperConnectionState connectionState)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandType;
            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                    objConnection.Open();
                o = objCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionState == DBHelperConnectionState.CloseOnExit)
                    objConnection.Close();
            }
            return o;
        }

        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, DBHelperConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query, CommandType commandType)
        {
            return ExecuteScalar(query, commandType, DBHelperConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query, DBHelperConnectionState connectionState)
        {
            return ExecuteScalar(query, CommandType.Text, connectionState);
        }

        public DbDataReader ExecuteReader(string query, CommandType commandType, DBHelperConnectionState connectionState)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandType;
            DbDataReader reader = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                    objConnection.Open();
                if (connectionState == DBHelperConnectionState.CloseOnExit)
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    reader = objCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
            }
            return reader;
        }

        public DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, DBHelperConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query, CommandType commandType)
        {
            return ExecuteReader(query, commandType, DBHelperConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query, DBHelperConnectionState connectionState)
        {
            return ExecuteReader(query, CommandType.Text, connectionState);
        }

        public DataSet ExecuteDataSet(string query, CommandType commandType, DBHelperConnectionState connectionState)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandType;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionState == DBHelperConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
            return ds;
        }

        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, DBHelperConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query, CommandType commandType)
        {
            return ExecuteDataSet(query, commandType, DBHelperConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query, DBHelperConnectionState connectionState)
        {
            return ExecuteDataSet(query, CommandType.Text, connectionState);
        }

        private void HandleExceptions(Exception ex)
        {
            if (LogErrors)
            {
                WriteToLog(ex.Message);
            }
            if (HandleErrors)
            {
                strLastError = ex.Message;
            }
            else
            {
                throw ex;
            }
        }

        private void WriteToLog(string msg)
        {
            StreamWriter writer = File.AppendText(LogFile);
            writer.WriteLine(DateTime.Now.ToString() + "-" + msg);
            writer.Close();
        }

        public void Dispose()
        {
            objConnection.Close();
            objConnection.Dispose();
            objCommand.Dispose();
        }


    }

    public enum Providers
    {
        SqlServer, OleDb, Oracle, ODBC, ConfigDefined
    }
    public enum DBHelperConnectionState
    {
        KeepOpen, CloseOnExit
    }

}
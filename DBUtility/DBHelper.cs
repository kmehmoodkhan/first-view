using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MSSQL.DBHelper
{
    public class DBHelper : IDisposable
    {
        private DbConnection objConn;
        private DbCommand objComm;
        private DbProviderFactory objFactory = null;

        public DBHelper(ConnectionStr ConnStr)
        {
            objFactory = SqlClientFactory.Instance;
            objConn = objFactory.CreateConnection();
            objComm = objFactory.CreateCommand();
            if (ConnStr == ConnectionStr.DefaultConnection)
            {
                objConn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
            objComm.Connection = objConn;
        }

        public int AddParameter(string name, object value)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return objComm.Parameters.Add(p);
        }

        public int ExecuteNonQuery(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            objComm.CommandTimeout = 0;
            int i = -1;
            try
            {
                if (objConn.State == ConnectionState.Closed)
                {
                    objConn.Open();
                }
                i = objComm.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return i;
        }

        public object ExecuteScalar(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            object o = null;
            try
            {
                if (objConn.State == ConnectionState.Closed)
                {
                    objConn.Open();
                }
                o = objComm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }

            return o;
        }

        public DbDataReader ExecuteReader(string SQLQuery, CommandType CommType)
        {
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            DbDataReader reader = null;
            try
            {
                if (objConn.State == ConnectionState.Closed)
                {
                    objConn.Open();
                }
                reader = objComm.ExecuteReader(CommandBehavior.CloseConnection);
            }            
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
            }
            return reader;
        }

        public DataSet ExecuteDataSet(string SQLQuery, CommandType CommType)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            objComm.CommandTimeout = 0;
            adapter.SelectCommand = objComm;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return ds;
        }

        public  DataView ExecuteDataView(string SQLQuery, CommandType CommType)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objComm.CommandText = SQLQuery;
            objComm.CommandType = CommType;
            objComm.CommandTimeout = 0;
            adapter.SelectCommand = objComm;
            DataSet ds = new DataSet();
            DataView dv = new DataView();
            try
            {
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dv.Table = ds.Tables[0];
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                LogErrors(ex);
            }
            finally
            {
                objComm.Parameters.Clear();
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            return dv;
        }

        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            string myMsg = e.Message;
        }

        private void LogErrors(Exception ex)
        {
            throw ex;       
        }

        public enum ConnectionStr
        {
            DefaultConnection
        }

        public void Dispose()
        {
            objConn.Close();
            objConn.Dispose();
            objComm.Dispose();
        }
    }
}
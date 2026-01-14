using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
    public enum  DataProvider
    {
        Oracle, SqlServer, OleDb, Odbc
    }
    public interface IDBManager
    {

        DataProvider ProviderType
        {
            get;
            set;
        }

        string ConnectionString
        {
            get;
            set;
        }

        IDbConnection Connection
        {
            get;
        }
        IDbTransaction Transaction
        {
            get;
        }

        IDataReader DataReader
        {
            get;
        }
        IDbCommand Command
        {
            get;
        }

        IDbDataParameter[] Parameters
        {
            get;
        }

        void Open();
        void BeginTransaction();
        void CommitTransaction();
        void CreateParameters(int paramsCount);
        void AddParameters(int index, string paramName, object objValue);
        IDataReader ExecuteReader(CommandType commandType, string
        commandText);
        DataSet ExecuteDataSet(CommandType commandType, string
        commandText);
        object ExecuteScalar(CommandType commandType, string commandText);
        int ExecuteNonQuery(CommandType commandType, string commandText);
        void CloseReader();
        void Close();
        void Dispose();
        //int ExecuteNonQuery(CommandType commandType, string commandText, System.Data.SqlClient.SqlTransaction Tran) 
    }

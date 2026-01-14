using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    class CTSDB
    {
        #region  Attributes

        //private String m_sTicketNo = String.Empty;

        //private Int32 m_nReporterID = 0;

        //private Int32 m_nBureauID = 0;

        //private String m_sDuration = String.Empty;

        //private String m_sJobType = String.Empty;

        //private Int32 m_nCreatedBy = 0;

        #endregion

        #region  Constructors

        public CTSDB()
        {

        }

        #endregion

        //#region  Properties

        //public String TicketNo
        //{
        //    get
        //    {
        //        return m_sTicketNo;

        //    }
        //    set
        //    {
        //        m_sTicketNo = value;

        //    }
        //}

        //public Int32 ReporterID
        //{
        //    get
        //    {
        //        return m_nReporterID;

        //    }
        //    set
        //    {
        //        m_nReporterID = value;

        //    }
        //}

        //public Int32 BureauID
        //{
        //    get
        //    {
        //        return m_nBureauID;

        //    }
        //    set
        //    {
        //        m_nBureauID = value;

        //    }
        //}

        //public String Duration
        //{
        //    get
        //    {
        //        return m_sDuration;

        //    }
        //    set
        //    {
        //        m_sDuration = value;

        //    }
        //}

        //public String JobType
        //{
        //    get
        //    {
        //        return m_sJobType;

        //    }
        //    set
        //    {
        //        m_sJobType = value;

        //    }
        //}

        //public Int32 CreatedBy
        //{
        //    get
        //    {
        //        return m_nCreatedBy;

        //    }
        //    set
        //    {
        //        m_nCreatedBy = value;

        //    }
        //}

        //#endregion

        #region  Methods
        

        public DataTable GetAllCTSData(object TageName)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetCTSDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLCTSData", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (TageName != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTagNo", TageName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTagNo", System.DBNull.Value);
            }

            DataTable dt = new DataTable("CTSData");

            dbAdapter.Fill(dt);

            return dt;
        }

        public DataTable GetAllReporterFromJTS()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLRepoterFromJTS", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable("Reporter");

            dbAdapter.Fill(dt);

            return dt;
        }

        public DataTable GetAllProducerFromJTS()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLProducerFromJTS", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable("Producer");

            dbAdapter.Fill(dt);

            return dt;
        }


        #endregion
    }
}

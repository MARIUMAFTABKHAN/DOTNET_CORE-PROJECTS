/***************************************************************************************
'*
'*      Class Name         :    OurceDB
'*      Class Description  :    Provides the Entity Level Functionality of table OurceDB
'*      Task Owned By      :    CRISS
'*      Creation Date      :   2/25/2009 12:00:00 AM
'***************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    public class SUSourceDB
    {
        #region  Attributes

        private Int32 m_nSourceID = 0;

        private String m_strSource = String.Empty;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditedBy = 0;

        private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

        #endregion 

        #region  Constructors

        public SUSourceDB()
        {

        }

        #endregion

        #region  Properties

        public Int32 SourceID
        {
            get
            {
                return m_nSourceID;

            }
            set
            {
                m_nSourceID = value;

            }
        }

        public String Source
        {
            get
            {
                return m_strSource;

            }
            set
            {
                m_strSource = value;

            }
        }

        public Int32 CreatedBy
        {
            get
            {
                return m_nCreatedBy;

            }
            set
            {
                m_nCreatedBy = value;

            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return m_dtCreatedOn;

            }
            set
            {
                m_dtCreatedOn = value;

            }
        }

        public Int32 EditedBy
        {
            get
            {
                return m_nEditedBy;

            }
            set
            {
                m_nEditedBy = value;

            }
        }

        public DateTime EditedOn
        {
            get
            {
                return m_dtEditedOn;

            }
            set
            {
                m_dtEditedOn = value;

            }
        }

        #endregion 

        #region  Methods

        public Int32 InsertSource(SUSourceDB objource)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_InsertSource", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_strSource", objource.Source);
            dbCom.Parameters.Add("@p_nCreatedBy", objource.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objource.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objource.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objource.EditedOn);

            /*Output Parameters*/
            SqlParameter pSourceID = new SqlParameter();
            pSourceID.ParameterName = "@p_nSourceID";
            pSourceID.SqlDbType = SqlDbType.Int;
            pSourceID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pSourceID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Int32.Parse(pSourceID.Value.ToString());
        }

        public SUSourceDB GetSource(Int32 sourceID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_GetSource", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nSourceID", sourceID);

            /*Output Parameters*/
            SqlParameter pSource = new SqlParameter();
            pSource.ParameterName = "@p_strSource";
            pSource.SqlDbType = SqlDbType.VarChar;
            pSource.Size = 30;
            pSource.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pSource);

            SqlParameter pCreatedBy = new SqlParameter();
            pCreatedBy.ParameterName = "@p_nCreatedBy";
            pCreatedBy.SqlDbType = SqlDbType.Int;
            pCreatedBy.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pCreatedBy);

            SqlParameter pCreatedOn = new SqlParameter();
            pCreatedOn.ParameterName = "@p_dtCreatedOn";
            pCreatedOn.SqlDbType = SqlDbType.DateTime;
            pCreatedOn.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pCreatedOn);

            SqlParameter pEditedBy = new SqlParameter();
            pEditedBy.ParameterName = "@p_nEditedBy";
            pEditedBy.SqlDbType = SqlDbType.Int;
            pEditedBy.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pEditedBy);

            SqlParameter pEditedOn = new SqlParameter();
            pEditedOn.ParameterName = "@p_dtEditedOn";
            pEditedOn.SqlDbType = SqlDbType.DateTime;
            pEditedOn.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pEditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            SUSourceDB objOurce = new SUSourceDB();

            objOurce.Source = pSource.Value.ToString();
            objOurce.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
            objOurce.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
            objOurce.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
            objOurce.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objOurce.SourceID = sourceID;

            return objOurce;
        }

        public DataTable GetAllSource(object sourceID, object source, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllSource", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (sourceID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", sourceID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", System.DBNull.Value);
            }
            if (source != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSource", source);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSource", System.DBNull.Value);
            }
            if (createdBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", createdBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (editedBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", editedBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", System.DBNull.Value);
            }
            if (editedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", editedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", System.DBNull.Value);
            }

            DataTable dtOurce = new DataTable("Source");

            dbAdapter.Fill(dtOurce);

            return dtOurce;
        }

        public void DeleteSource(Int32 sourceID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_DeleteSource", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nSourceID", sourceID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public void UpdateSource(SUSourceDB objource)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateSource", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nSourceID", objource.SourceID);
            dbCom.Parameters.Add("@p_strSource", objource.Source);
            dbCom.Parameters.Add("@p_nCreatedBy", objource.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objource.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objource.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objource.EditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        #endregion

    }
}
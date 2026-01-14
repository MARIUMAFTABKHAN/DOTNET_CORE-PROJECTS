using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    public class ChannelDB
    {
        #region  Attributes

        private Int32 m_shChannelID = 0;

        private String m_strChannel = String.Empty;

        private Boolean m_bIsActive = false;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditedBy = 0;

        private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

        #endregion

        #region  Constructors

        public ChannelDB()
        {

        }

        #endregion

        #region  Properties

        public Int32 ChannelID
        {
            get
            {
                return m_shChannelID;

            }
            set
            {
                m_shChannelID = value;

            }
        }

        public String Channel
        {
            get
            {
                return m_strChannel;

            }
            set
            {
                m_strChannel = value;

            }
        }

        public Boolean IsActive
        {
            get
            {
                return m_bIsActive;

            }
            set
            {
                m_bIsActive = value;

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

        public Int32 InsertChannel(ChannelDB objhannel)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlCommand dbCom = new SqlCommand("usp_InsertChannel", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_strChannel", objhannel.Channel);
            dbCom.Parameters.Add("@p_bIsActive", objhannel.IsActive);
            dbCom.Parameters.Add("@p_nCreatedBy", objhannel.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objhannel.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objhannel.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objhannel.EditedOn);

            /*Output Parameters*/
            SqlParameter pChannelID = new SqlParameter();
            pChannelID.ParameterName = "@p_nChannelID";
            pChannelID.SqlDbType = SqlDbType.SmallInt;
            pChannelID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pChannelID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Int32.Parse(pChannelID.Value.ToString());
        }

        public ChannelDB GetChannel(Int32 channelID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_GetChannel", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nChannelID", channelID);

            /*Output Parameters*/
            SqlParameter pChannel = new SqlParameter();
            pChannel.ParameterName = "@p_strChannel";
            pChannel.SqlDbType = SqlDbType.VarChar;
            pChannel.Size = 20;
            pChannel.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pChannel);

            SqlParameter pIsActive = new SqlParameter();
            pIsActive.ParameterName = "@p_bIsActive";
            pIsActive.SqlDbType = SqlDbType.Bit;
            pIsActive.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pIsActive);

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

            ChannelDB objHannel = new ChannelDB();

            objHannel.Channel = pChannel.Value.ToString();
            objHannel.IsActive = Boolean.Parse(pIsActive.Value.ToString());
            objHannel.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
            objHannel.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
            objHannel.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
            objHannel.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objHannel.ChannelID = channelID;

            return objHannel;
        }

        public DataTable GetAllChannel(object channelID, object channel, object isActive, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllChannel", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (channelID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", channelID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", System.DBNull.Value);
            }
            if (channel != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strChannel", channel);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strChannel", System.DBNull.Value);
            }
            if (isActive != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive", isActive);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive", System.DBNull.Value);
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

            DataTable dtHannel = new DataTable("Channel");

            dbAdapter.Fill(dtHannel);

            return dtHannel;
        }

        public void DeleteChannel(Int32 channelID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_DeleteChannel", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nChannelID", channelID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public void UpdateChannel(ChannelDB objhannel)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateChannel", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nChannelID", objhannel.ChannelID);
            dbCom.Parameters.Add("@p_strChannel", objhannel.Channel);
            dbCom.Parameters.Add("@p_bIsActive", objhannel.IsActive);
            dbCom.Parameters.Add("@p_nCreatedBy", objhannel.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objhannel.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objhannel.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objhannel.EditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        #endregion

    }
}
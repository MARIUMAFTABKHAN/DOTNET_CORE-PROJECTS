using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Altodownloading.DataAccessLayer
{
    class DBArchiveKeyDetailDb
    {

        #region  Attributes

        private Int64 m_lArchiveKeyDetailID = 0;

        private Int64 m_lArchiveID = 0;

        private String m_strDetail = String.Empty;

        private String m_strStartTime = String.Empty;

        private String m_strEndTime = String.Empty;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn =     Helper.GetDefaultDateTime();

        private Int32 m_nEditedBy = 0;

        private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

        #endregion

        #region  Constructors



        #endregion

        #region  Properties

        public Int64 ArchiveKeyDetailID
        {
            get
            {
                return m_lArchiveKeyDetailID;

            }
            set
            {
                m_lArchiveKeyDetailID = value;

            }
        }

        public Int64 ArchiveID
        {
            get
            {
                return m_lArchiveID;

            }
            set
            {
                m_lArchiveID = value;

            }
        }

        public String Detail
        {
            get
            {
                return m_strDetail;

            }
            set
            {
                m_strDetail = value;

            }
        }

        public String StartTime
        {
            get
            {
                return m_strStartTime;

            }
            set
            {
                m_strStartTime = value;

            }
        }

        public String EndTime
        {
            get
            {
                return m_strEndTime;

            }
            set
            {
                m_strEndTime = value;

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

        public Int64 InsertArchiveKeyDetail( DBManager dbCom,  DBArchiveKeyDetailDb objArchiveKeyDetail)
        {

            //SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            //SqlCommand dbCom = new SqlCommand("usp_InsertArchiveKeyDetail", dbConn);
            //dbCom.CommandType = CommandType.StoredProcedure;
            try
            {
            
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(9);

            /*Input Parameters*/
            dbCom.AddParameters(0,"@p_nArchiveID", objArchiveKeyDetail.ArchiveID,0);
            dbCom.AddParameters(1,"@p_strDetail", objArchiveKeyDetail.Detail,0);
            dbCom.AddParameters(2,"@p_strStartTime", objArchiveKeyDetail.StartTime,0);
            dbCom.AddParameters(3,"@p_strEndTime", objArchiveKeyDetail.EndTime,0);
            dbCom.AddParameters(4,"@p_nCreatedBy", objArchiveKeyDetail.CreatedBy,0);
            dbCom.AddParameters(5,"@p_dtCreatedOn", objArchiveKeyDetail.CreatedOn,0);
            dbCom.AddParameters(6,"@p_nEditedBy", objArchiveKeyDetail.EditedBy,0);
            dbCom.AddParameters(7,"@p_dtEditedOn", objArchiveKeyDetail.EditedOn,0);
            dbCom.AddParameters(8, "@p_nArchiveKeyDetailID", objArchiveKeyDetail.ArchiveKeyDetailID, 1);
            dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertArchiveKeyDetailForDesktop]");

            }
            catch (Exception ex)
            {

                string str = ex.Message;
            }
            return Int64.Parse(dbCom.Parameters[8].Value.ToString ());
        }

        public ArchiveKeyDetailDB GetArchiveKeyDetail(Int64 archiveKeyDetailID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_GetArchiveKeyDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveKeyDetailID", archiveKeyDetailID);

            /*Output Parameters*/
            SqlParameter pArchiveID = new SqlParameter();
            pArchiveID.ParameterName = "@p_nArchiveID";
            pArchiveID.SqlDbType = SqlDbType.BigInt;
            pArchiveID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pArchiveID);

            SqlParameter pDetail = new SqlParameter();
            pDetail.ParameterName = "@p_strDetail";
            pDetail.SqlDbType = SqlDbType.NVarChar;
            pDetail.Size = 4000;
            pDetail.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pDetail);

            SqlParameter pStartTime = new SqlParameter();
            pStartTime.ParameterName = "@p_strStartTime";
            pStartTime.SqlDbType = SqlDbType.VarChar;
            pStartTime.Size = 11;
            pStartTime.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pStartTime);

            SqlParameter pEndTime = new SqlParameter();
            pEndTime.ParameterName = "@p_strEndTime";
            pEndTime.SqlDbType = SqlDbType.VarChar;
            pEndTime.Size = 11;
            pEndTime.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pEndTime);

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

            ArchiveKeyDetailDB objArchiveKeyDetail = new ArchiveKeyDetailDB();

            objArchiveKeyDetail.ArchiveID = Int64.Parse(pArchiveID.Value.ToString());
            objArchiveKeyDetail.Detail = pDetail.Value.ToString();
            objArchiveKeyDetail.StartTime = pStartTime.Value.ToString();
            objArchiveKeyDetail.EndTime = pEndTime.Value.ToString();
            objArchiveKeyDetail.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
            objArchiveKeyDetail.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
            objArchiveKeyDetail.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
            objArchiveKeyDetail.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objArchiveKeyDetail.ArchiveKeyDetailID = archiveKeyDetailID;

            return objArchiveKeyDetail;
        }

        public DataTable GetAllArchiveKeyDetail(object archiveKeyDetailID, object archiveID, object detail, object startTime, object endTime, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllArchiveKeyDetail", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (archiveKeyDetailID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", archiveKeyDetailID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", System.DBNull.Value);
            }
            if (archiveID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", archiveID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", System.DBNull.Value);
            }
            if (detail != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDetail", detail);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDetail", System.DBNull.Value);
            }
            if (startTime != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strStartTime", startTime);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strStartTime", System.DBNull.Value);
            }
            if (endTime != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEndTime", endTime);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEndTime", System.DBNull.Value);
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

            DataTable dtArchiveKeyDetail = new DataTable("ArchiveKeyDetailDB");

            dbAdapter.Fill(dtArchiveKeyDetail);

            return dtArchiveKeyDetail;
        }

        public void DeleteArchiveKeyDetail(Int64 archiveID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_DeleteArchiveKeyDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveID", archiveID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public bool  DeleteArchiveKeyDetail(DBManager dbCom, Int64 archiveID)
        {
            bool result =true ;

            //SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            //SqlCommand dbCom = new SqlCommand("usp_DeleteArchiveKeyDetail", dbConn);
            //dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0,"@p_nArchiveID", archiveID);
            try
            {
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_DeleteArchiveKeyDetail");
            }
            catch (Exception ex)
            {
                result = false;
            
            }
            return result;           
            
        }


        public void UpdateArchiveKeyDetail(ArchiveKeyDetailDB objArchiveKeyDetail)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateArchiveKeyDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveKeyDetailID", objArchiveKeyDetail.ArchiveKeyDetailID);
            dbCom.Parameters.Add("@p_nArchiveID", objArchiveKeyDetail.ArchiveID);
            dbCom.Parameters.Add("@p_strDetail", objArchiveKeyDetail.Detail);
            dbCom.Parameters.Add("@p_strStartTime", objArchiveKeyDetail.StartTime);
            dbCom.Parameters.Add("@p_strEndTime", objArchiveKeyDetail.EndTime);
            dbCom.Parameters.Add("@p_nCreatedBy", objArchiveKeyDetail.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objArchiveKeyDetail.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objArchiveKeyDetail.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objArchiveKeyDetail.EditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        #endregion

       
    }
}
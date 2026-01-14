using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    class JTSDB
    {
         #region  Attributes

        private String m_sTicketNo = String.Empty;
        
        private Int32 m_nReporterID = 0;

        private Int32 m_nBureauID = 0;

        private String m_sDuration = String.Empty;

        private String m_sJobType = String.Empty;

        private Int32 m_nCreatedBy = 0;
             
        #endregion

	    #region  Constructors 

        public JTSDB()
		{

		}

	#endregion 

        #region  Properties

        public String TicketNo
        {
            get
            {
                return m_sTicketNo;

            }
            set
            {
                m_sTicketNo = value;

            }
        }

        public Int32 ReporterID
        {
            get
            {
                return m_nReporterID;

            }
            set
            {
                m_nReporterID = value;

            }
        }

        public Int32 BureauID
        {
            get
            {
                return m_nBureauID;

            }
            set
            {
                m_nBureauID = value;

            }
        }

        public String Duration
        {
            get
            {
                return m_sDuration;

            }
            set
            {
                m_sDuration = value;

            }
        }

        public String JobType
        {
            get
            {
                return m_sJobType;

            }
            set
            {
                m_sJobType = value;

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
                
        #endregion

	    #region  Methods 

  
 //       public SUMediaTypeDB GetMediaType(Int32 mediaTypeID)
 //       {

 //           SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
 //           SqlCommand dbCom=new SqlCommand("usp_GetALLJTSData", dbConn);
 //           dbCom.CommandType=CommandType.StoredProcedure;


 ///*Input Parameters*/
 //           dbCom.Parameters.Add("@p_nMediaTypeID",mediaTypeID);

 ///*Output Parameters*/
 //           SqlParameter pMediaType=new SqlParameter();
 //           pMediaType.ParameterName="@p_strMediaType";
 //           pMediaType.SqlDbType = SqlDbType.VarChar;
 //           pMediaType.Size = 10;
 //           pMediaType.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMediaType);

 //           SqlParameter pMinimumLevel=new SqlParameter();
 //           pMinimumLevel.ParameterName="@p_nMinimumLevel";
 //           pMinimumLevel.SqlDbType = SqlDbType.Int;
 //           pMinimumLevel.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMinimumLevel);

 //           SqlParameter pIsActive=new SqlParameter();
 //           pIsActive.ParameterName="@p_bIsActive";
 //           pIsActive.SqlDbType = SqlDbType.Bit;
 //           pIsActive.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pIsActive);

 //           SqlParameter pCreatedBy=new SqlParameter();
 //           pCreatedBy.ParameterName="@p_nCreatedBy";
 //           pCreatedBy.SqlDbType = SqlDbType.Int;
 //           pCreatedBy.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCreatedBy);

 //           SqlParameter pCreatedOn=new SqlParameter();
 //           pCreatedOn.ParameterName="@p_dtCreatedOn";
 //           pCreatedOn.SqlDbType = SqlDbType.DateTime;
 //           pCreatedOn.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCreatedOn);

 //           SqlParameter pEditBy=new SqlParameter();
 //           pEditBy.ParameterName="@p_nEditBy";
 //           pEditBy.SqlDbType = SqlDbType.Int;
 //           pEditBy.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEditBy);

 //           SqlParameter pEditOn=new SqlParameter();
 //           pEditOn.ParameterName="@p_dtEditOn";
 //           pEditOn.SqlDbType = SqlDbType.DateTime;
 //           pEditOn.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEditOn);

 //               dbConn.Open();
 //               dbCom.ExecuteNonQuery();
 //               dbConn.Close();

 //               SUMediaTypeDB objEdiaType = new SUMediaTypeDB();

 //           objEdiaType.MediaType = pMediaType.Value.ToString();
 //           objEdiaType.MinimumLevel = Int32.Parse(pMinimumLevel.Value.ToString());
 //           objEdiaType.IsActive = Boolean.Parse(pIsActive.Value.ToString());
 //           objEdiaType.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
 //           objEdiaType.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
 //           objEdiaType.EditBy = Int32.Parse(pEditBy.Value.ToString());
 //           objEdiaType.EditOn = DateTime.Parse(pEditOn.Value.ToString());
 //           objEdiaType.MediaTypeID = mediaTypeID;

 //           return objEdiaType;
 //       }

		public DataTable GetAllJTSData(object fileName)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            //SqlDataAdapter dbAdapter = new SqlDataAdapter("[dbo].[usp_GetALLJTSDataWithScript]", dbConn);
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLJTSData", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

            if (fileName != null)
			{
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
			}
			else
			{
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
			}			

			DataTable dt=new DataTable("JTSData");

			dbAdapter.Fill(dt);

			return dt;
		}

        //public DataTable GetScript(string fileName)
        //{ 
        //     SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
        //    SqlDataAdapter dbAdapter = new SqlDataAdapter("[dbo].[usp_GetALLJTSDataWithScript]", dbConn);
        //   // SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLJTSData", dbConn);
        //    dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

        //    if (fileName != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
        //    }			

        //    DataTable dt=new DataTable("JTSData");

        //    dbAdapter.Fill(dt);

        //    return dt;
        //}
      
        public DataTable GetAllJTSDataByQry(string fileName)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter(fileName  , dbConn);
            //SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLJTSData", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.Text ;

            //if (fileName != null)
            //{
            //    dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
            //}
            //else
            //{
            //    dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
            //}

            DataTable dt = new DataTable("JTSData");

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

        public DataTable GetAllUserByBureauID(Int32 BureauId)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllUserByBureauId", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.Add("@BureauId", BureauId);

            DataTable dt = new DataTable("User");

            dbAdapter.Fill(dt);

            return dt;
        }
          public DataTable GetAllPhotographerByBureauID(Int32 BureauId)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllPhotograhperByBureauId", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.Add("@BureauId", BureauId);

            DataTable dt = new DataTable("User");

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



        public DataTable GetUserByDepartmentId(
                                               object DepartmentID,                                               
                                               object IsActive
                                            )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetUserByDepartmentId", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (DepartmentID != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", System.DBNull.Value);
            }

            if (IsActive != null)
            {

                dbAdapter.SelectCommand.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(IsActive));
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@IsActive", System.DBNull.Value);
            }
            DataTable dtGetUserByDepartment = new DataTable("GetUserByDepartment");

            dbAdapter.Fill(dtGetUserByDepartment);

            return dtGetUserByDepartment;
        }

	#endregion 
    }
}

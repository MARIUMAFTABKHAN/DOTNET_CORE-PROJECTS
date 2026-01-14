using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
	public class SUMediaTypeDB
	{
        #region  Attributes

        private Int32 m_shMediaTypeID = 0;

        private String m_strMediaType = String.Empty;

        private Int32 m_nMinimumLevel = 0;

        private Boolean m_bIsActive = false;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditBy = 0;

        private DateTime m_dtEditOn = Helper.GetDefaultDateTime();

        #endregion

	    #region  Constructors 

        public SUMediaTypeDB()
		{

		}

	#endregion 

        #region  Properties

        public Int32 MediaTypeID
        {
            get
            {
                return m_shMediaTypeID;

            }
            set
            {
                m_shMediaTypeID = value;

            }
        }

        public String MediaType
        {
            get
            {
                return m_strMediaType;

            }
            set
            {
                m_strMediaType = value;

            }
        }

        public Int32 MinimumLevel
        {
            get
            {
                return m_nMinimumLevel;

            }
            set
            {
                m_nMinimumLevel = value;

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

        public Int32 EditBy
        {
            get
            {
                return m_nEditBy;

            }
            set
            {
                m_nEditBy = value;

            }
        }

        public DateTime EditOn
        {
            get
            {
                return m_dtEditOn;

            }
            set
            {
                m_dtEditOn = value;

            }
        }

        #endregion

	    #region  Methods 

        public Int32 InsertMediaType(SUMediaTypeDB objediaType)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertMediaType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_strMediaType",objediaType.MediaType);
			dbCom.Parameters.Add("@p_nMinimumLevel",objediaType.MinimumLevel);
			dbCom.Parameters.Add("@p_bIsActive",objediaType.IsActive);
			dbCom.Parameters.Add("@p_nCreatedBy",objediaType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objediaType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objediaType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objediaType.EditOn);

 /*Output Parameters*/
			SqlParameter pMediaTypeID=new SqlParameter();
			pMediaTypeID.ParameterName="@p_nMediaTypeID";
			pMediaTypeID.SqlDbType = SqlDbType.SmallInt;
			pMediaTypeID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pMediaTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int32.Parse(pMediaTypeID.Value.ToString());
		}

        public SUMediaTypeDB GetMediaType(Int32 mediaTypeID)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetMediaType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nMediaTypeID",mediaTypeID);

 /*Output Parameters*/
			SqlParameter pMediaType=new SqlParameter();
			pMediaType.ParameterName="@p_strMediaType";
			pMediaType.SqlDbType = SqlDbType.VarChar;
			pMediaType.Size = 10;
			pMediaType.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pMediaType);

			SqlParameter pMinimumLevel=new SqlParameter();
			pMinimumLevel.ParameterName="@p_nMinimumLevel";
			pMinimumLevel.SqlDbType = SqlDbType.Int;
			pMinimumLevel.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pMinimumLevel);

			SqlParameter pIsActive=new SqlParameter();
			pIsActive.ParameterName="@p_bIsActive";
			pIsActive.SqlDbType = SqlDbType.Bit;
			pIsActive.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pIsActive);

			SqlParameter pCreatedBy=new SqlParameter();
			pCreatedBy.ParameterName="@p_nCreatedBy";
			pCreatedBy.SqlDbType = SqlDbType.Int;
			pCreatedBy.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pCreatedBy);

			SqlParameter pCreatedOn=new SqlParameter();
			pCreatedOn.ParameterName="@p_dtCreatedOn";
			pCreatedOn.SqlDbType = SqlDbType.DateTime;
			pCreatedOn.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pCreatedOn);

			SqlParameter pEditBy=new SqlParameter();
			pEditBy.ParameterName="@p_nEditBy";
			pEditBy.SqlDbType = SqlDbType.Int;
			pEditBy.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pEditBy);

			SqlParameter pEditOn=new SqlParameter();
			pEditOn.ParameterName="@p_dtEditOn";
			pEditOn.SqlDbType = SqlDbType.DateTime;
			pEditOn.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pEditOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                SUMediaTypeDB objEdiaType = new SUMediaTypeDB();

			objEdiaType.MediaType = pMediaType.Value.ToString();
			objEdiaType.MinimumLevel = Int32.Parse(pMinimumLevel.Value.ToString());
			objEdiaType.IsActive = Boolean.Parse(pIsActive.Value.ToString());
			objEdiaType.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objEdiaType.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objEdiaType.EditBy = Int32.Parse(pEditBy.Value.ToString());
			objEdiaType.EditOn = DateTime.Parse(pEditOn.Value.ToString());
            objEdiaType.MediaTypeID = mediaTypeID;

			return objEdiaType;
		}

		public DataTable GetAllMediaType(object mediaTypeID, object mediaType, object minimumLevel, object isActive, object createdBy, object createdOn, object editBy, object editOn)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllMediaType", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(mediaTypeID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",mediaTypeID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",System.DBNull.Value);
			}
			if(mediaType!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strMediaType",mediaType);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strMediaType",System.DBNull.Value);
			}
			if(minimumLevel!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMinimumLevel",minimumLevel);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMinimumLevel",System.DBNull.Value);
			}
			if(isActive!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive",isActive);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive",System.DBNull.Value);
			}
			if(createdBy!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy",createdBy);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy",System.DBNull.Value);
			}
			if(createdOn!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn",createdOn);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn",System.DBNull.Value);
			}
			if(editBy!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nEditBy",editBy);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nEditBy",System.DBNull.Value);
			}
			if(editOn!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtEditOn",editOn);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtEditOn",System.DBNull.Value);
			}

			DataTable dtEdiaType=new DataTable("EdiaType");

			dbAdapter.Fill(dtEdiaType);

			return dtEdiaType;
		}

        public DataTable GetAllMediaType()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllMediaTypeForDesktop", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;            

            DataTable dtEdiaType = new DataTable("EdiaType");

            dbAdapter.Fill(dtEdiaType);

            return dtEdiaType;
        }

		public void DeleteMediaType(Int32 mediaTypeID)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteMediaType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nMediaTypeID",mediaTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

        public void UpdateMediaType(SUMediaTypeDB objediaType)
		{

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateMediaType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nMediaTypeID",objediaType.MediaTypeID);
			dbCom.Parameters.Add("@p_strMediaType",objediaType.MediaType);
			dbCom.Parameters.Add("@p_nMinimumLevel",objediaType.MinimumLevel);
			dbCom.Parameters.Add("@p_bIsActive",objediaType.IsActive);
			dbCom.Parameters.Add("@p_nCreatedBy",objediaType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objediaType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objediaType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objediaType.EditOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

        public DataTable AlreadyExistCode(String ID)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_AlreadyExistCode", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@Code", Convert.ToInt32(ID));
            DataTable dt = new DataTable("Code");
            dbAdapter.Fill(dt);
            return dt;
        }


	#endregion 

	}
}
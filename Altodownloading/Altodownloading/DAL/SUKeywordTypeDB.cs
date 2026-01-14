using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
	public class SUKeywordTypeDB
	{
        #region  Attributes

        private Int32 m_nKeywordTypeID = 0;

        private String m_strKeywordType = String.Empty;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditBy = 0;

        private DateTime m_dtEditOn = Helper.GetDefaultDateTime();

        #endregion 

	#region  Constructors 

        public SUKeywordTypeDB()
		{

		}

	#endregion 

        #region  Properties

        public Int32 KeywordTypeID
        {
            get
            {
                return m_nKeywordTypeID;

            }
            set
            {
                m_nKeywordTypeID = value;

            }
        }

        public String KeywordType
        {
            get
            {
                return m_strKeywordType;

            }
            set
            {
                m_strKeywordType = value;

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

		public Int32 InsertKeywordType(SUKeywordTypeDB objeywordType)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertKeywordType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_strKeywordType",objeywordType.KeywordType);
			dbCom.Parameters.Add("@p_nCreatedBy",objeywordType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objeywordType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objeywordType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objeywordType.EditOn);

 /*Output Parameters*/
			SqlParameter pKeywordTypeID=new SqlParameter();
			pKeywordTypeID.ParameterName="@p_nKeywordTypeID";
			pKeywordTypeID.SqlDbType = SqlDbType.Int;
			pKeywordTypeID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pKeywordTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int32.Parse(pKeywordTypeID.Value.ToString());
		}

		public SUKeywordTypeDB GetKeywordType(Int32 keywordTypeID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetKeywordType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.AddWithValue("@p_nKeywordTypeID",keywordTypeID);

 /*Output Parameters*/
			SqlParameter pKeywordType=new SqlParameter();
			pKeywordType.ParameterName="@p_strKeywordType";
			pKeywordType.SqlDbType = SqlDbType.VarChar;
			pKeywordType.Size = 50;
			pKeywordType.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pKeywordType);

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

			SUKeywordTypeDB objEywordType = new SUKeywordTypeDB();

			objEywordType.KeywordType = pKeywordType.Value.ToString();
			objEywordType.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objEywordType.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objEywordType.EditBy = Int32.Parse(pEditBy.Value.ToString());
			objEywordType.EditOn = DateTime.Parse(pEditOn.Value.ToString());
            objEywordType.KeywordTypeID = keywordTypeID;

			return objEywordType;
		}

		public DataTable GetAllKeywordType(object keywordTypeID, object keywordType, object createdBy, object createdOn, object editBy, object editOn)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllKeywordType", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(keywordTypeID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordTypeID",keywordTypeID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordTypeID",System.DBNull.Value);
			}
			if(keywordType!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strKeywordType",keywordType);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strKeywordType",System.DBNull.Value);
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

			DataTable dtEywordType=new DataTable("SUKeywordTypeDB");

			dbAdapter.Fill(dtEywordType);

			return dtEywordType;
		}
        
		public void DeleteKeywordType(Int32 keywordTypeID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteKeywordType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nKeywordTypeID",keywordTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

		public void UpdateKeywordType(SUKeywordTypeDB objeywordType)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateKeywordType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nKeywordTypeID",objeywordType.KeywordTypeID);
			dbCom.Parameters.Add("@p_strKeywordType",objeywordType.KeywordType);
			dbCom.Parameters.Add("@p_nCreatedBy",objeywordType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objeywordType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objeywordType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objeywordType.EditOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

        public DataTable GetAllKeyword(Object KeywordTypeID,Object CategoryID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllKeywordForArchiveCombo", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0; 

            if (KeywordTypeID != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@KeywordTypeID", KeywordTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@KeywordTypeID", System.DBNull.Value);
            }

            if (CategoryID != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@CategoryID", CategoryID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@CategoryID", System.DBNull.Value);
            }
            DataTable dtEywordType = new DataTable("SUKeywordTypeDB");

            dbAdapter.Fill(dtEywordType);

            return dtEywordType;
        }

	#endregion 

	}
}



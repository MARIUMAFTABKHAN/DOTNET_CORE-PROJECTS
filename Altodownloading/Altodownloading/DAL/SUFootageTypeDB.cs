using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
	public class SUFootageTypeDB
	{
        #region  Attributes

        private Int32 m_nFootageTypeID = 0;

        private String m_strFootageType = String.Empty;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditBy = 0;

        private DateTime m_dtEditOn = Helper.GetDefaultDateTime();

        #endregion

	    #region  Constructors 

        public SUFootageTypeDB()
		{

		}

	#endregion 

        #region  Properties

        public Int32 FootageTypeID
        {
            get
            {
                return m_nFootageTypeID;

            }
            set
            {
                m_nFootageTypeID = value;

            }
        }

        public String FootageType
        {
            get
            {
                return m_strFootageType;

            }
            set
            {
                m_strFootageType = value;

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

		public Int32 InsertFootageType(SUFootageTypeDB objootageType)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertFootageType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_strFootageType",objootageType.FootageType);
			dbCom.Parameters.Add("@p_nCreatedBy",objootageType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objootageType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objootageType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objootageType.EditOn);

 /*Output Parameters*/
			SqlParameter pFootageTypeID=new SqlParameter();
			pFootageTypeID.ParameterName="@p_nFootageTypeID";
			pFootageTypeID.SqlDbType = SqlDbType.Int;
			pFootageTypeID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pFootageTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int32.Parse(pFootageTypeID.Value.ToString());
		}

		public SUFootageTypeDB GetFootageType(Int32 footageTypeID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetFootageType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nFootageTypeID",footageTypeID);

 /*Output Parameters*/
			SqlParameter pFootageType=new SqlParameter();
			pFootageType.ParameterName="@p_strFootageType";
			pFootageType.SqlDbType = SqlDbType.VarChar;
			pFootageType.Size = 50;
			pFootageType.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pFootageType);

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

			SUFootageTypeDB objOotageType = new SUFootageTypeDB();

			objOotageType.FootageType = pFootageType.Value.ToString();
			objOotageType.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objOotageType.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objOotageType.EditBy = Int32.Parse(pEditBy.Value.ToString());
			objOotageType.EditOn = DateTime.Parse(pEditOn.Value.ToString());
            objOotageType.FootageTypeID = footageTypeID;

			return objOotageType;
		}

		public DataTable GetAllFootageType(object footageTypeID, object footageType, object createdBy, object createdOn, object editBy, object editOn)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllFootageType", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(footageTypeID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID",footageTypeID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID",System.DBNull.Value);
			}
			if(footageType!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strFootageType",footageType);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strFootageType",System.DBNull.Value);
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

			DataTable dtOotageType=new DataTable("SUFootageTypeDB");

			dbAdapter.Fill(dtOotageType);

			return dtOotageType;
		}

		public void DeleteFootageType(Int32 footageTypeID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteFootageType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nFootageTypeID",footageTypeID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

		public void UpdateFootageType(SUFootageTypeDB objootageType)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateFootageType", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nFootageTypeID",objootageType.FootageTypeID);
			dbCom.Parameters.Add("@p_strFootageType",objootageType.FootageType);
			dbCom.Parameters.Add("@p_nCreatedBy",objootageType.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objootageType.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objootageType.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objootageType.EditOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

	#endregion 

	}
}


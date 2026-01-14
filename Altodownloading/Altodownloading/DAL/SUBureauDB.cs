using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    class SUBureauDB
	{
	#region  Attributes 

		private Int32 m_nBureauId = 0;

		private String m_strName = String.Empty;

		private String m_strCode = String.Empty;

		private Boolean m_bIsActive = false;

		private Int32 m_nCreatedBy = 0;

		private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

		private Int32 m_nEditBy = 0;

		private DateTime m_dtEditOn = Helper.GetDefaultDateTime();

	#endregion 

	#region  Constructors 

        public SUBureauDB()
		{

		}

	#endregion 

	#region  Properties 

		public Int32 BureauId
		{
			get
			{
				return m_nBureauId;

			}
			set
			{
				m_nBureauId = value;

			}
		}

		public String Name
		{
			get
			{
				return m_strName;

			}
			set
			{
				m_strName = value;

			}
		}

		public String Code
		{
			get
			{
				return m_strCode;

			}
			set
			{
				m_strCode = value;

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

		public Int32 InsertBureau(SUBureauDB objBureau)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertBureau", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_strName",objBureau.Name);
			dbCom.Parameters.Add("@p_strCode",objBureau.Code);
			dbCom.Parameters.Add("@p_bIsActive",objBureau.IsActive);
			dbCom.Parameters.Add("@p_nCreatedBy",objBureau.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objBureau.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objBureau.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objBureau.EditOn);

 /*Output Parameters*/
			SqlParameter pBureauId=new SqlParameter();
			pBureauId.ParameterName="@p_nBureauId";
			pBureauId.SqlDbType = SqlDbType.Int;
			pBureauId.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pBureauId);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

			return Int32.Parse(pBureauId.Value.ToString());
		}

		public SUBureauDB GetBureau(Int32 bureauId)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetBureau", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nBureauId",bureauId);

 /*Output Parameters*/
			SqlParameter pName=new SqlParameter();
			pName.ParameterName="@p_strName";
			pName.SqlDbType = SqlDbType.VarChar;
			pName.Size = 50;
			pName.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pName);

			SqlParameter pCode=new SqlParameter();
			pCode.ParameterName="@p_strCode";
			pCode.SqlDbType = SqlDbType.VarChar;
			pCode.Size = 3;
			pCode.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pCode);

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

			SUBureauDB objBureau = new SUBureauDB();

			objBureau.Name = pName.Value.ToString();
			objBureau.Code = pCode.Value.ToString();
			objBureau.IsActive = Boolean.Parse(pIsActive.Value.ToString());
			objBureau.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objBureau.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objBureau.EditBy = Int32.Parse(pEditBy.Value.ToString());
			objBureau.EditOn = DateTime.Parse(pEditOn.Value.ToString());
            objBureau.BureauId = bureauId;

			return objBureau;
		}

		public DataTable GetAllBureau(object bureauId, object name, object code, object isActive, object createdBy, object createdOn, object editBy, object editOn)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllBureau", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(bureauId!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nBureauId",bureauId);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nBureauId",System.DBNull.Value);
			}
			if(name!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strName",name);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strName",System.DBNull.Value);
			}
			if(code!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strCode",code);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strCode",System.DBNull.Value);
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

			DataTable dtBureau=new DataTable("SUBureauDB");

			dbAdapter.Fill(dtBureau);

			return dtBureau;
		}

		public void DeleteBureau(Int32 bureauId)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteBureau", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nBureauId",bureauId);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

		public void UpdateBureau(SUBureauDB objBureau)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateBureau", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nBureauId",objBureau.BureauId);
			dbCom.Parameters.Add("@p_strName",objBureau.Name);
			dbCom.Parameters.Add("@p_strCode",objBureau.Code);
			dbCom.Parameters.Add("@p_bIsActive",objBureau.IsActive);
			dbCom.Parameters.Add("@p_nCreatedBy",objBureau.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objBureau.CreatedOn);
			dbCom.Parameters.Add("@p_nEditBy",objBureau.EditBy);
			dbCom.Parameters.Add("@p_dtEditOn",objBureau.EditOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

	#endregion 

	}
}
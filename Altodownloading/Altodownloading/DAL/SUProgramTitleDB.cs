using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{   

    public class SUProgramTitleDB
	{
	#region  Attributes 
        
        private Int64 m_lProgramTitleID = 0;

		private String m_strProgramTitle = String.Empty;

		private Int32 m_nCreatedBy = 0;

		private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

		private Int32 m_nEditedBy = 0;

		private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

		private SqlConnection m_strconnection = null;

	#endregion 

	#region  Constructors 

        public SUProgramTitleDB()
		{

		}

	#endregion 

	#region  Properties 

		public Int64 ProgramTitleID
		{
			get
			{
				return m_lProgramTitleID;

			}
			set
			{
				m_lProgramTitleID = value;

			}
		}

		public String ProgramTitle
		{
			get
			{
				return m_strProgramTitle;

			}
			set
			{
				m_strProgramTitle = value;

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

		public Int64 InsertProgramTitle(SUProgramTitleDB objrogramTitle)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertProgramTitle", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_strProgramTitle",objrogramTitle.ProgramTitle);
			dbCom.Parameters.Add("@p_nCreatedBy",objrogramTitle.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objrogramTitle.CreatedOn);
			dbCom.Parameters.Add("@p_nEditedBy",objrogramTitle.EditedBy);
			dbCom.Parameters.Add("@p_dtEditedOn",objrogramTitle.EditedOn);

 /*Output Parameters*/
			SqlParameter pProgramTitleID=new SqlParameter();
			pProgramTitleID.ParameterName="@p_nProgramTitleID";
			pProgramTitleID.SqlDbType = SqlDbType.BigInt;
			pProgramTitleID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pProgramTitleID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int64.Parse(pProgramTitleID.Value.ToString());
		}

		public SUProgramTitleDB GetProgramTitle(Int64 programTitleID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetProgramTitle", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nProgramTitleID",programTitleID);

 /*Output Parameters*/
			SqlParameter pProgramTitle=new SqlParameter();
			pProgramTitle.ParameterName="@p_strProgramTitle";
			pProgramTitle.SqlDbType = SqlDbType.VarChar;
			pProgramTitle.Size = 1000;
			pProgramTitle.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pProgramTitle);

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

			SqlParameter pEditedBy=new SqlParameter();
			pEditedBy.ParameterName="@p_nEditedBy";
			pEditedBy.SqlDbType = SqlDbType.Int;
			pEditedBy.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pEditedBy);

			SqlParameter pEditedOn=new SqlParameter();
			pEditedOn.ParameterName="@p_dtEditedOn";
			pEditedOn.SqlDbType = SqlDbType.DateTime;
			pEditedOn.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pEditedOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

			SUProgramTitleDB objRogramTitle = new SUProgramTitleDB();

			objRogramTitle.ProgramTitle = pProgramTitle.Value.ToString();
			objRogramTitle.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objRogramTitle.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objRogramTitle.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
			objRogramTitle.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objRogramTitle.ProgramTitleID = programTitleID;

			return objRogramTitle;
		}

		public DataTable GetAllProgramTitle()
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetProgramTitleOrName", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;
			DataTable dtRogramTitle=new DataTable("SUProgramTitleDB");
			dbAdapter.Fill(dtRogramTitle);
			return dtRogramTitle;
		}

		public void DeleteProgramTitle(Int64 programTitleID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteProgramTitle", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nProgramTitleID",programTitleID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

		public void UpdateProgramTitle(SUProgramTitleDB objrogramTitle)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateProgramTitle", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nProgramTitleID",objrogramTitle.ProgramTitleID);
			dbCom.Parameters.Add("@p_strProgramTitle",objrogramTitle.ProgramTitle);
			dbCom.Parameters.Add("@p_nCreatedBy",objrogramTitle.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objrogramTitle.CreatedOn);
			dbCom.Parameters.Add("@p_nEditedBy",objrogramTitle.EditedBy);
			dbCom.Parameters.Add("@p_dtEditedOn",objrogramTitle.EditedOn);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

	#endregion 

	}
}


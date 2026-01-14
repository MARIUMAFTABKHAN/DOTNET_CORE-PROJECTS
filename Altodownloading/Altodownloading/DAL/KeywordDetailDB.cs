using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    public class KeywordDetailDB
    {
        #region  Attributes 

		private Int64 m_lKeywordDetailID = 0;

		private Int64 m_lArchiveKeyDetailID = 0;

		private Int32 m_nKeywordTypeID = 0;

		private String m_strKeyword = String.Empty;

		private Int32 m_nCreatedBy = 0;

		private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

		private Int32 m_nEditedBy = 0;

		private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

	#endregion 

        #region  Constructors

        public KeywordDetailDB()
        {

        }

        #endregion

        #region  Properties 

		public Int64 KeywordDetailID
		{
			get
			{
				return m_lKeywordDetailID;

			}
			set
			{
				m_lKeywordDetailID = value;

			}
		}

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

		public String Keyword
		{
			get
			{
				return m_strKeyword;

			}
			set
			{
				m_strKeyword = value;

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

        public Int64 InsertKeywordDetail(DBManager dbCom, KeywordDetailDB objKeywordDetail)
        {

            //SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            //SqlCommand dbCom = new SqlCommand("usp_InsertKeywordDetail", dbConn);
             //dbCom.CommandType = CommandType.StoredProcedure;
            dbCom.Command.Parameters.Clear ();
            dbCom.CreateParameters(8);

            /*Input Parameters*/
            dbCom.AddParameters(0, "@p_nArchiveKeyDetailID", objKeywordDetail.ArchiveKeyDetailID, 0);
            dbCom.AddParameters(1, "@p_nKeywordTypeID", objKeywordDetail.KeywordTypeID, 0);
            dbCom.AddParameters(2, "@p_strKeyword", objKeywordDetail.Keyword, 0);
            dbCom.AddParameters(3, "@p_nCreatedBy", objKeywordDetail.CreatedBy, 0);
            dbCom.AddParameters(4, "@p_dtCreatedOn", objKeywordDetail.CreatedOn, 0);
            dbCom.AddParameters(5, "@p_nEditedBy", objKeywordDetail.EditedBy, 0);
            dbCom.AddParameters(6, "@p_dtEditedOn", objKeywordDetail.EditedOn, 0);
            dbCom.AddParameters(7, "@p_nKeywordDetailID", objKeywordDetail.KeywordDetailID, 1);

            /*Output Parameters*/
            dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_InsertKeywordDetail");
         

            return Int64.Parse(dbCom.Parameters [7].Value.ToString());
        }
        public Int64 InsertKeywordDetail(KeywordDetailDB objKeywordDetail)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_InsertKeywordDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveKeyDetailID", objKeywordDetail.ArchiveKeyDetailID);
            dbCom.Parameters.Add("@p_nKeywordTypeID", objKeywordDetail.KeywordTypeID);
            dbCom.Parameters.Add("@p_strKeyword", objKeywordDetail.Keyword);
            dbCom.Parameters.Add("@p_nCreatedBy", objKeywordDetail.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objKeywordDetail.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objKeywordDetail.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objKeywordDetail.EditedOn);

            /*Output Parameters*/
            SqlParameter pKeywordDetailID = new SqlParameter();
            pKeywordDetailID.ParameterName = "@p_nKeywordDetailID";
            pKeywordDetailID.SqlDbType = SqlDbType.BigInt;
            pKeywordDetailID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pKeywordDetailID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Int64.Parse(pKeywordDetailID.Value.ToString());
        }

        public KeywordDetailDB GetKeywordDetail(Int64 keywordDetailID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_GetKeywordDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nKeywordDetailID", keywordDetailID);

            /*Output Parameters*/
            SqlParameter pArchiveKeyDetailID = new SqlParameter();
            pArchiveKeyDetailID.ParameterName = "@p_nArchiveKeyDetailID";
            pArchiveKeyDetailID.SqlDbType = SqlDbType.BigInt;
            pArchiveKeyDetailID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pArchiveKeyDetailID);

            SqlParameter pKeywordTypeID = new SqlParameter();
            pKeywordTypeID.ParameterName = "@p_nKeywordTypeID";
            pKeywordTypeID.SqlDbType = SqlDbType.Int;
            pKeywordTypeID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pKeywordTypeID);

            SqlParameter pKeyword = new SqlParameter();
            pKeyword.ParameterName = "@p_strKeyword";
            pKeyword.SqlDbType = SqlDbType.VarChar;
            pKeyword.Size = 50;
            pKeyword.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pKeyword);

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

            KeywordDetailDB objKeywordDetail = new KeywordDetailDB();

            objKeywordDetail.ArchiveKeyDetailID = Int64.Parse(pArchiveKeyDetailID.Value.ToString());
            objKeywordDetail.KeywordTypeID = Int32.Parse(pKeywordTypeID.Value.ToString());
            objKeywordDetail.Keyword = pKeyword.Value.ToString();
            objKeywordDetail.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
            objKeywordDetail.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
            objKeywordDetail.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
            objKeywordDetail.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objKeywordDetail.KeywordDetailID = keywordDetailID;

            return objKeywordDetail;
        }
        public DataTable GetDistinctKeywords(object archiveKeyDetailID)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetDistinctKeywords", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (archiveKeyDetailID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", archiveKeyDetailID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", System.DBNull.Value);
            }

            DataTable dtKeywordDetail = new DataTable("KeywordDetailDB");

            dbAdapter.Fill(dtKeywordDetail);

            return dtKeywordDetail;

        }
        public DataTable GetAllKeywordDetail(object archiveKeyDetailID)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("[usp_GetAllKeywordDetailForDeskTop]", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (archiveKeyDetailID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", archiveKeyDetailID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", System.DBNull.Value);
            }
            DataTable dtKeywordDetail = new DataTable("KeywordDetailDB");

            dbAdapter.Fill(dtKeywordDetail);

            return dtKeywordDetail;
        }

        public DataTable GetAllKeywordDetail(object keywordDetailID, object archiveKeyDetailID, object keywordTypeID, object keyword, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllKeywordDetail", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (keywordDetailID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordDetailID", keywordDetailID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordDetailID", System.DBNull.Value);
            }
            if (archiveKeyDetailID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", archiveKeyDetailID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", System.DBNull.Value);
            }
            if (keywordTypeID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordTypeID", keywordTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordTypeID", System.DBNull.Value);
            }
            if (keyword != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strKeyword", keyword);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strKeyword", System.DBNull.Value);
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

            DataTable dtKeywordDetail = new DataTable("KeywordDetailDB");

            dbAdapter.Fill(dtKeywordDetail);

            return dtKeywordDetail;
        }

        public void DeleteKeywordDetail(Int64 keywordDetailID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_DeleteKeywordDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nKeywordDetailID", keywordDetailID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public void UpdateKeywordDetail(KeywordDetailDB objKeywordDetail)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateKeywordDetail", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nKeywordDetailID", objKeywordDetail.KeywordDetailID);
            dbCom.Parameters.Add("@p_nArchiveKeyDetailID", objKeywordDetail.ArchiveKeyDetailID);
            dbCom.Parameters.Add("@p_nKeywordTypeID", objKeywordDetail.KeywordTypeID);
            dbCom.Parameters.Add("@p_strKeyword", objKeywordDetail.Keyword);
            dbCom.Parameters.Add("@p_nCreatedBy", objKeywordDetail.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objKeywordDetail.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objKeywordDetail.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objKeywordDetail.EditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        #endregion

    }
}
using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    public class SUClassificationDB
    {
        #region  Attributes 

		private Int32 m_nClassificationID = 0;

		private String m_strClassificationCode = String.Empty;

		private String m_strClassification = String.Empty;

		private Int32 m_nCreatedBy = 0;

		private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

		private Int32 m_nEditedBy = 0;

		private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

	#endregion 

        #region  Constructors

        public SUClassificationDB()
        {

        }

        #endregion

        #region  Properties 

		public Int32 ClassificationID
		{
			get
			{
				return m_nClassificationID;

			}
			set
			{
				m_nClassificationID = value;

			}
		}

		public String ClassificationCode
		{
			get
			{
				return m_strClassificationCode;

			}
			set
			{
				m_strClassificationCode = value;

			}
		}

		public String Classification
		{
			get
			{
				return m_strClassification;

			}
			set
			{
				m_strClassification = value;

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

        public Int32 InsertClassification(SUClassificationDB objlassification)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_InsertClassification", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_strClassificationCode", objlassification.ClassificationCode);
            dbCom.Parameters.Add("@p_strClassification", objlassification.Classification);
            dbCom.Parameters.Add("@p_nCreatedBy", objlassification.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objlassification.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objlassification.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objlassification.EditedOn);

            /*Output Parameters*/
            SqlParameter pClassificationID = new SqlParameter();
            pClassificationID.ParameterName = "@p_nClassificationID";
            pClassificationID.SqlDbType = SqlDbType.Int;
            pClassificationID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pClassificationID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Int32.Parse(pClassificationID.Value.ToString());
        }

        public SUClassificationDB GetClassification(Int32 classificationID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_GetClassification", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nClassificationID", classificationID);

            /*Output Parameters*/
            SqlParameter pClassificationCode = new SqlParameter();
            pClassificationCode.ParameterName = "@p_strClassificationCode";
            pClassificationCode.SqlDbType = SqlDbType.VarChar;
            pClassificationCode.Size = 10;
            pClassificationCode.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pClassificationCode);

            SqlParameter pClassification = new SqlParameter();
            pClassification.ParameterName = "@p_strClassification";
            pClassification.SqlDbType = SqlDbType.VarChar;
            pClassification.Size = 20;
            pClassification.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pClassification);

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

            SUClassificationDB objLassification = new SUClassificationDB();

            objLassification.ClassificationCode = pClassificationCode.Value.ToString();
            objLassification.Classification = pClassification.Value.ToString();
            objLassification.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
            objLassification.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
            objLassification.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
            objLassification.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
            objLassification.ClassificationID = classificationID;

            return objLassification;
        }

        public DataTable GetAllClassification(object classificationID, object classificationCode, object classification, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllClassification", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (classificationID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", classificationID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", System.DBNull.Value);
            }
            if (classificationCode != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClassificationCode", classificationCode);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClassificationCode", System.DBNull.Value);
            }
            if (classification != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClassification", classification);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClassification", System.DBNull.Value);
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

            DataTable dtLassification = new DataTable("Lassification");

            dbAdapter.Fill(dtLassification);

            return dtLassification;
        }

        public DataTable GetAllClassification()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllClassificationForDeskTop", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtLassification = new DataTable("Lassification");
            dbAdapter.Fill(dtLassification);
            return dtLassification;
        }

        public void DeleteClassification(Int32 classificationID)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_DeleteClassification", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nClassificationID", classificationID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public void UpdateClassification(SUClassificationDB objlassification)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateClassification", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nClassificationID", objlassification.ClassificationID);
            dbCom.Parameters.Add("@p_strClassificationCode", objlassification.ClassificationCode);
            dbCom.Parameters.Add("@p_strClassification", objlassification.Classification);
            dbCom.Parameters.Add("@p_nCreatedBy", objlassification.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objlassification.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objlassification.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objlassification.EditedOn);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        #endregion

    }
}
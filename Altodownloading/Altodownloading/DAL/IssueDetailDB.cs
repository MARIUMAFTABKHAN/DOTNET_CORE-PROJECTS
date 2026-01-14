using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
	public class IssueDetailDB
	{
        #region  Attributes

        private Int64 m_lIssueDetailID = 0;

        private Int64 m_lIssueID = 0;

        private Int32 m_nMediaTypeID = 0;

        private String m_strSlug = String.Empty;

        private Int64 m_lMediaNo = 0;

        private Boolean m_bIsReturned = false;

        private Int32 m_nReturnFrom = 0;

        private Int32 m_nReturnedBy = 0;

        private DateTime m_dtReturnDate = Helper.GetDefaultDateTime();

        private String m_strAssignment = String.Empty;

        private String m_strFootageDetails = String.Empty;

        private Int32 m_nCreatedBy = 0;

        private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

        private Int32 m_nEditedBy = 0;

        private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

        private Boolean m_bIsActive = false;

        #endregion 
        
        #region  Constructors 

		public IssueDetailDB()
		{

		}

	#endregion 

        #region  Properties

        public Int64 IssueDetailID
        {
            get
            {
                return m_lIssueDetailID;

            }
            set
            {
                m_lIssueDetailID = value;

            }
        }

        public Int64 IssueID
        {
            get
            {
                return m_lIssueID;

            }
            set
            {
                m_lIssueID = value;

            }
        }

        public Int32 MediaTypeID
        {
            get
            {
                return m_nMediaTypeID;

            }
            set
            {
                m_nMediaTypeID = value;

            }
        }

        public String Slug
        {
            get
            {
                return m_strSlug;

            }
            set
            {
                m_strSlug = value;

            }
        }

        public Int64 MediaNo
        {
            get
            {
                return m_lMediaNo;

            }
            set
            {
                m_lMediaNo = value;

            }
        }

        public Boolean IsReturned
        {
            get
            {
                return m_bIsReturned;

            }
            set
            {
                m_bIsReturned = value;

            }
        }

        public Int32 ReturnFrom
        {
            get
            {
                return m_nReturnFrom;

            }
            set
            {
                m_nReturnFrom = value;

            }
        }

        public Int32 ReturnedBy
        {
            get
            {
                return m_nReturnedBy;

            }
            set
            {
                m_nReturnedBy = value;

            }
        }

        public DateTime ReturnDate
        {
            get
            {
                return m_dtReturnDate;

            }
            set
            {
                m_dtReturnDate = value;

            }
        }

        public String Assignment
        {
            get
            {
                return m_strAssignment;

            }
            set
            {
                m_strAssignment = value;

            }
        }

        public String FootageDetails
        {
            get
            {
                return m_strFootageDetails;

            }
            set
            {
                m_strFootageDetails = value;

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

        #endregion 

       #region  Methods 

		public Int64 InsertIssueDetail(IssueDetailDB objIssueDetail)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlCommand dbCom = new SqlCommand("usp_InsertIssueDetail", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;
 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nIssueID",objIssueDetail.IssueID);
			dbCom.Parameters.Add("@p_nMediaTypeID",objIssueDetail.MediaTypeID);
			dbCom.Parameters.Add("@p_strSlug",objIssueDetail.Slug);
			dbCom.Parameters.Add("@p_nMediaNo",objIssueDetail.MediaNo);			
			dbCom.Parameters.Add("@p_nCreatedBy",objIssueDetail.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objIssueDetail.CreatedOn);
		
 /*Output Parameters*/
			SqlParameter pIssueDetailID=new SqlParameter();
            pIssueDetailID.ParameterName = "@p_nIssueDetailID";
            pIssueDetailID.SqlDbType = SqlDbType.BigInt;
            pIssueDetailID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pIssueDetailID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int64.Parse(pIssueDetailID.Value.ToString());
		}

        public Int64 InsertIssueDetailBlankMedia(IssueDetailDB objIssueDetail)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlCommand dbCom = new SqlCommand("usp_InsertIssueDetailBlankMedia", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nIssueID", objIssueDetail.IssueID);
            dbCom.Parameters.Add("@p_nMediaTypeID", objIssueDetail.MediaTypeID);
            dbCom.Parameters.Add("@p_nMediaNo", objIssueDetail.MediaNo);
            dbCom.Parameters.Add("@p_bIsReturned", objIssueDetail.IsReturned);
            dbCom.Parameters.Add("@p_nCreatedBy", objIssueDetail.CreatedBy);
            dbCom.Parameters.Add("@p_dtCreatedOn", objIssueDetail.CreatedOn);
            dbCom.Parameters.Add("@p_nEditedBy", objIssueDetail.EditedBy);
            dbCom.Parameters.Add("@p_dtEditedOn", objIssueDetail.EditedOn);
            dbCom.Parameters.Add("@p_bIsActive", objIssueDetail.IsActive);

            /*Output Parameters*/
            SqlParameter pIssueDetailID = new SqlParameter();
            pIssueDetailID.ParameterName = "@p_nIssueDetailID";
            pIssueDetailID.SqlDbType = SqlDbType.BigInt;
            pIssueDetailID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pIssueDetailID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Int64.Parse(pIssueDetailID.Value.ToString());
        }
                
		public IssueDetailDB GetIssueDetail(Int64 issueDetailID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_GetIssueDetail", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nIssueDetailID",issueDetailID);

 /*Output Parameters*/
			SqlParameter pIssueID=new SqlParameter();
			pIssueID.ParameterName="@p_nIssueID";
			pIssueID.SqlDbType = SqlDbType.BigInt;
			pIssueID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pIssueID);

			SqlParameter pMediaTypeID=new SqlParameter();
			pMediaTypeID.ParameterName="@p_nMediaTypeID";
			pMediaTypeID.SqlDbType = SqlDbType.Int;
			pMediaTypeID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pMediaTypeID);

			SqlParameter pSlug=new SqlParameter();
			pSlug.ParameterName="@p_strSlug";
			pSlug.SqlDbType = SqlDbType.VarChar;
			pSlug.Size = 50;
			pSlug.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pSlug);

			SqlParameter pMediaNo=new SqlParameter();
			pMediaNo.ParameterName="@p_nMediaNo";
			pMediaNo.SqlDbType = SqlDbType.BigInt;
			pMediaNo.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pMediaNo);

			SqlParameter pIsReturned=new SqlParameter();
			pIsReturned.ParameterName="@p_bIsReturned";
			pIsReturned.SqlDbType = SqlDbType.Bit;
			pIsReturned.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pIsReturned);

			SqlParameter pReturnFrom=new SqlParameter();
			pReturnFrom.ParameterName="@p_nReturnFrom";
			pReturnFrom.SqlDbType = SqlDbType.Int;
			pReturnFrom.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pReturnFrom);

			SqlParameter pReturnedBy=new SqlParameter();
			pReturnedBy.ParameterName="@p_nReturnedBy";
			pReturnedBy.SqlDbType = SqlDbType.Int;
			pReturnedBy.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pReturnedBy);

			SqlParameter pReturnDate=new SqlParameter();
			pReturnDate.ParameterName="@p_dtReturnDate";
			pReturnDate.SqlDbType = SqlDbType.DateTime;
			pReturnDate.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pReturnDate);

			SqlParameter pAssignment=new SqlParameter();
			pAssignment.ParameterName="@p_strAssignment";
			pAssignment.SqlDbType = SqlDbType.VarChar;
			pAssignment.Size = 30;
			pAssignment.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pAssignment);

			SqlParameter pFootageDetails=new SqlParameter();
			pFootageDetails.ParameterName="@p_strFootageDetails";
			pFootageDetails.SqlDbType = SqlDbType.VarChar;
			pFootageDetails.Size = 50;
			pFootageDetails.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pFootageDetails);

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

			SqlParameter pIsActive=new SqlParameter();
			pIsActive.ParameterName="@p_bIsActive";
			pIsActive.SqlDbType = SqlDbType.Bit;
			pIsActive.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pIsActive);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                IssueDetailDB objIssueDetail = new IssueDetailDB();

			objIssueDetail.IssueID = Int64.Parse(pIssueID.Value.ToString());
			objIssueDetail.MediaTypeID = Int32.Parse(pMediaTypeID.Value.ToString());
			objIssueDetail.Slug = pSlug.Value.ToString();
			objIssueDetail.MediaNo = Int64.Parse(pMediaNo.Value.ToString());
			objIssueDetail.IsReturned = Boolean.Parse(pIsReturned.Value.ToString());
			objIssueDetail.ReturnFrom = Int32.Parse(pReturnFrom.Value.ToString());
			objIssueDetail.ReturnedBy = Int32.Parse(pReturnedBy.Value.ToString());
			objIssueDetail.ReturnDate = DateTime.Parse(pReturnDate.Value.ToString());
			objIssueDetail.Assignment = pAssignment.Value.ToString();
			objIssueDetail.FootageDetails = pFootageDetails.Value.ToString();
			objIssueDetail.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
			objIssueDetail.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
			objIssueDetail.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
			objIssueDetail.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
			objIssueDetail.IsActive = Boolean.Parse(pIsActive.Value.ToString());
            objIssueDetail.IssueDetailID = issueDetailID;

			return objIssueDetail;
		}

		public DataTable GetAllIssueDetail(object issueDetailID, object issueID, object mediaTypeID, object slug, object mediaNo, object isReturned, object returnFrom, object returnedBy, object returnDate, object assignment, object footageDetails, object createdBy, object createdOn, object editedBy, object editedOn, object isActive)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
			SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllIssueDetail", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(issueDetailID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nIssueDetailID",issueDetailID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nIssueDetailID",System.DBNull.Value);
			}
			if(issueID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nIssueID",issueID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nIssueID",System.DBNull.Value);
			}
			if(mediaTypeID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",mediaTypeID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",System.DBNull.Value);
			}
			if(slug!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strSlug",slug);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strSlug",System.DBNull.Value);
			}
			if(mediaNo!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo",mediaNo);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo",System.DBNull.Value);
			}
			if(isReturned!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsReturned",isReturned);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsReturned",System.DBNull.Value);
			}
			if(returnFrom!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nReturnFrom",returnFrom);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nReturnFrom",System.DBNull.Value);
			}
			if(returnedBy!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nReturnedBy",returnedBy);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nReturnedBy",System.DBNull.Value);
			}
			if(returnDate!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtReturnDate",returnDate);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtReturnDate",System.DBNull.Value);
			}
			if(assignment!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strAssignment",assignment);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strAssignment",System.DBNull.Value);
			}
			if(footageDetails!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetails",footageDetails);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetails",System.DBNull.Value);
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
			if(editedBy!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy",editedBy);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy",System.DBNull.Value);
			}
			if(editedOn!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn",editedOn);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn",System.DBNull.Value);
			}
			if(isActive!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive",isActive);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_bIsActive",System.DBNull.Value);
			}

			DataTable dtIssueDetail=new DataTable("IssueDetail");

			dbAdapter.Fill(dtIssueDetail);

			return dtIssueDetail;
		}

        public void UpdateIssueDetail(IssueDetailDB objIssueDetail)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateIssueDetail", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nIssueDetailID",objIssueDetail.IssueDetailID);
			dbCom.Parameters.Add("@p_nIssueID",objIssueDetail.IssueID);
			dbCom.Parameters.Add("@p_nMediaTypeID",objIssueDetail.MediaTypeID);
			dbCom.Parameters.Add("@p_strSlug",objIssueDetail.Slug);
			dbCom.Parameters.Add("@p_nMediaNo",objIssueDetail.MediaNo);
			dbCom.Parameters.Add("@p_bIsReturned",objIssueDetail.IsReturned);
			dbCom.Parameters.Add("@p_nReturnFrom",objIssueDetail.ReturnFrom);
			dbCom.Parameters.Add("@p_nReturnedBy",objIssueDetail.ReturnedBy);
			dbCom.Parameters.Add("@p_dtReturnDate",objIssueDetail.ReturnDate);
			dbCom.Parameters.Add("@p_strAssignment",objIssueDetail.Assignment);
			dbCom.Parameters.Add("@p_strFootageDetails",objIssueDetail.FootageDetails);
			dbCom.Parameters.Add("@p_nCreatedBy",objIssueDetail.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objIssueDetail.CreatedOn);
			dbCom.Parameters.Add("@p_nEditedBy",objIssueDetail.EditedBy);
			dbCom.Parameters.Add("@p_dtEditedOn",objIssueDetail.EditedOn);
			dbCom.Parameters.Add("@p_bIsActive",objIssueDetail.IsActive);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

        public DataTable GetMaxNum()
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetMaxMediaNotblIssueDetail", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable("tblIssueDetail");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable GetRecycleStockValues(Int32 MediaTypeId)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetRecycleStockValues", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaTypeId", MediaTypeId);
            DataTable dt = new DataTable("StockValues");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable GetRecycleStockValuesForIssue(Int32 MediaTypeId)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetRecycleStockValuesForIssue", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaTypeId", MediaTypeId);
            DataTable dt = new DataTable("StockValuesForIssue");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable GetAssignmentandFootageDetailsOnlyReturnMedia(Int64 MediaNo)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAssignmentsAndFootageDetailsOnlyReturnMedia", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaNo", MediaNo);
            DataTable dt = new DataTable("GetAFD");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable UpdateIsReturned(Int64 MediaNo, Int32 ReturnBy, DateTime ReturnDate)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_UpdateIsReturned", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaNo", MediaNo);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@ReturnBy", ReturnBy);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@ReturnDate", ReturnDate);
            DataTable dt = new DataTable("UIR");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable UpdateIsReturnedForEmployee(Int64 MediaNo, Int32 ReturnBy, DateTime ReturnDate, String Assignment, String FootageDetails)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_UpdateIsReturnedForEmployee", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaNo", MediaNo);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@ReturnBy", ReturnBy);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@ReturnDate", ReturnDate);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@Assignment", Assignment);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@FootageDetails", FootageDetails);
            DataTable dt = new DataTable("UIR");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable GetIsDispatched(Int64 MediaNo)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetIsDispatched", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaNo", MediaNo);
            DataTable dt = new DataTable("ID");
            dbAdapter.Fill(dt);
            return dt;
        }

        public DataTable DeleteIssuedRecordsFromRecycleStock(Int64 MediaNo)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_DeleteIssuedRecordsFromRecycleStock", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.Parameters.AddWithValue("@MediaNo", MediaNo);
            DataTable dt = new DataTable("UIR");
            dbAdapter.Fill(dt);
            return dt;
        }

	#endregion 

	}
}
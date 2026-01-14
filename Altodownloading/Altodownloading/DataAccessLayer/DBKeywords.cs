using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.Data.SqlClient;

namespace Altodownloading.DataAccessLayer
{
    class DBKeywords
    {

        private Int32 mkeywordId;
        private Int32 mKeyTypeID;
        private string mKeyword;
        private int mCreatedBy;
        private int mEditedBy;
        private DateTime mCreatedOn;
        private DateTime mEditedOn;

        public Int32 keywordId
        {
            get { return mkeywordId; }
            set { mkeywordId = value; }
        }
        public Int32 KeyTypeID
        {
            get { return mKeyTypeID; }
            set { mKeyTypeID = value; }
        }
        public string Keyword
        {
            get { return mKeyword; }
            set { mKeyword = value; }
        }
        public Int32 CratedBy
        {
            get { return mCreatedBy; }
            set { mCreatedBy = value; }
        }
        public Int32 EditedBy
        {
            get { return mEditedBy; }
            set { mEditedBy = value; }
        }
        public DateTime CreatedOn
        {
            get { return mCreatedOn; }
            set { mCreatedOn = value; }
        }
        public DateTime EditedOn
        {
            get { return mEditedOn; }
            set { mEditedOn = value; }
        }



        public bool InsertKeyword( )
        {
            bool result = true ;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(7);
            dbCom.AddParameters(0, "@KeyWordId", keywordId, 1);
            dbCom.AddParameters(1, "@KeywordTypeID", KeyTypeID, 0);
            dbCom.AddParameters(2, "@Keyword",Keyword, 0);
            dbCom.AddParameters(3, "@CreatedBy", CratedBy, 0);
            dbCom.AddParameters(4, "@CreatedOn", CreatedOn, 0);
            dbCom.AddParameters(5, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(6, "@EditedOn", EditedOn, 0);
            try
            {
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertKeyword]");
                this.keywordId =  Convert.ToInt32(dbCom.Parameters[0].Value);
                dbCom.Close();
            }


            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }
        public bool UpdateKeyword()
        {
            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(5);
            dbCom.AddParameters(0, "@KeyWordId", keywordId, 0);
            dbCom.AddParameters(1, "@KeywordTypeID", KeyTypeID, 0);
            dbCom.AddParameters(2, "@Keyword", Keyword, 0);          
            dbCom.AddParameters(3, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(4, "@EditedOn", EditedOn, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdateKeyWorddetailForDesktop]");                
                dbCom.Close();
            }


            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteKeyword()
        {

            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@p_nArchiveKeyDetailID", keywordId, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_DeletetKeywords]");
                if (i >= 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }

            catch (Exception ex)
            {
                result = false;
            }
            return result;          
        }

        public DataTable GetListByKeyTypeId()
        {
          
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetDistinctKeywords", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (KeyTypeID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", KeyTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveKeyDetailID", System.DBNull.Value);
            }

            DataTable dtKeywordDetail = new DataTable("KeywordDetailDB");

            dbAdapter.Fill(dtKeywordDetail);

            return dtKeywordDetail;        
        }
        public DataTable GetRecordByID()
        {
            DataTable dt = null;

            return dt;
        }
      
        public bool updateArchiveScript(DBManager dbCom)
        {
            bool result = true;
            int i = -1;
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(3);
            dbCom.AddParameters(0, "@KeyWordId", keywordId  , 0);
            dbCom.AddParameters(1, "@KeywordTypeID", KeyTypeID, 0);
            dbCom.AddParameters(2, "@Keyword", Keyword, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdateKeyWorddetailForDesktop]");
                if (i >= 0)
                {                 
                   result = false;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }







    }
}

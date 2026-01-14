using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.Data.SqlClient;

namespace Altodownloading.DataAccessLayer
{
    class DBAuthority
    {
        private int mAuthorityID;
        private string mAuthority;
        private string mSearchString;

        private int mCreatedBy;
        private int mEditedBy;
        private DateTime mCreatedOn;
        private DateTime mEditedOn;

        public int AuthorityID
        {
            get { return mAuthorityID ;}
            set { mAuthorityID = value ;}
        }

        public string  Authority
        {
            get { return mAuthority; }
            set { mAuthority = value.Trim (); }
        }
        public string SearchString
        {
            get { return mSearchString; }
            set { mSearchString = value.Trim(); }
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

        public DataTable GetSearchedData()
        {          
          
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetSerachedAuthority", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (SearchString != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@PhasreTxt", @SearchString );
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@PhasreTxt", System.DBNull.Value);
            }

            DataTable dtKeywordDetail = new DataTable("SearchedData");

            dbAdapter.Fill(dtKeywordDetail);

            return dtKeywordDetail;        
        
        }
        public bool InsertAuthority()
        {
            bool result = true;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.BeginTransaction();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(6);
            dbCom.AddParameters(0, "@AuthorityID", AuthorityID,  1);
            dbCom.AddParameters(1, "@Authority",Authority , 0);
            dbCom.AddParameters(2, "@CreatedBy", CratedBy, 0);
            dbCom.AddParameters(3, "@CreatedOn", CreatedOn, 0);
            dbCom.AddParameters(4, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(5, "@EditedOn", EditedOn, 0);
            try
            {
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertAuthority]");
                this.AuthorityID = Convert.ToInt32(dbCom.Parameters[0].Value);
                dbCom.CommitTransaction();
                dbCom.Close();
            }
            catch (Exception ex)
            {
                dbCom.Transaction.Rollback();
                dbCom.Close();
                result = false;
            }
            return result;

        }
        public bool UpdateAuthority()
        {
            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(4);
            dbCom.AddParameters(0, "@AuthorityID", AuthorityID , 0);
            dbCom.AddParameters(1, "@Authority", Authority, 0);
            dbCom.AddParameters(2, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(3, "@EditedOn", EditedOn, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdateAuthority]");
                dbCom.Close();
            }


            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteAuthority()
        {

            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@AughorityID", AuthorityID, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_DeleteAuthority]");
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

        //public DataTable GetList()
        //{
        //    DataSet dt;
        //    DBManager dbCom = new DBManager();
        //    dbCom.Open();
        //    dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetGuestList");
        //    dbCom.Close();
        //    return dt.Tables[0];
        //}

        //public DataTable GetSearchedList()
        //{
        //    DataSet dt;
        //    DBManager dbCom = new DBManager();
        //    dbCom.Open();
        //    dbCom.CreateParameters(1);
        //    dbCom.AddParameters(0, "@PhasreTxt", SearchString, 0);
        //    dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetSearchedGuestList");
        //    dbCom.Close();
        //    return dt.Tables[0];
        //}


    }
}

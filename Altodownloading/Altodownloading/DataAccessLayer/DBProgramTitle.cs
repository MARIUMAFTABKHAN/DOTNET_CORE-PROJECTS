using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Altodownloading.DataAccessLayer
{
    class DBProgramTitle
    {
        private Int32 mPGTitleID;
        private string mPGTitle;
        private int mCreatedBy;
        private int mEditedBy;
        private DateTime mCreatedOn;
        private DateTime mEditedOn;
        private string mSearchString;
        private string mProgramShortcode;

        public Int32 PGTitleID
        {
            get { return mPGTitleID; }
            set { mPGTitleID = value; }
        }

        public string SearchString
        {
            get { return mSearchString; }
            set { mSearchString = value; }
        }
        public string ProgramShortCode
        {
            get { return mProgramShortcode; }
            set { mProgramShortcode = value; }
        }
        public string PGTitle
        {
            get { return mPGTitle; }
            set { mPGTitle = value; }
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



        public bool InsertPGTitle( )
        {
            bool result = true;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.BeginTransaction();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(7);
            //dbCom.AddParameters(0, "@PGTitleID", PGTitleID, 1);
            //dbCom.AddParameters(1, "@PGTitle", PGTitle, 0);
            //dbCom.AddParameters(2, "@CreatedBy", CratedBy, 0);
            //dbCom.AddParameters(3, "@CreatedOn", CreatedOn, 0);
            //dbCom.AddParameters(4, "@EditedBy", EditedBy, 0);
            //dbCom.AddParameters(5, "@EditedOn", EditedOn, 0);
            dbCom.AddParameters(0, "@ProgramTitleID", PGTitleID, 1);
            dbCom.AddParameters(1, "@ProgramTitle", PGTitle, 0);
            dbCom.AddParameters(2, "@CreatedBy", CratedBy, 0);
            dbCom.AddParameters(3, "@CreatedOn", CreatedOn, 0);
            dbCom.AddParameters(4, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(5, "@EditedOn", EditedOn, 0);
            dbCom.AddParameters(6, "@ProgramshortCode", ProgramShortCode, 0);
            try
            {
                int i = -1;
                //i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertProgramTitleOrName]");
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertProgramTitleOrName_WithShortCode]");
                
                    //dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertPGTitle]");
               // MessageBox.Show (i.ToString ());
                this.PGTitleID  = Convert.ToInt32(dbCom.Parameters[0].Value);
                dbCom.CommitTransaction(); 
                dbCom.Close();
                
            }


            catch (Exception ex)
            {
                dbCom.Transaction.Rollback();
                result = false;
               
            }
            return result;

        }
        public bool UpdatePGTitle()
        {
            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(5);
            dbCom.AddParameters(0, "@PGTitleID", PGTitleID, 0);
            dbCom.AddParameters(1, "@PGTitle", PGTitle, 0);           
            dbCom.AddParameters(2, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(3, "@EditedOn", EditedOn, 0);
            dbCom.AddParameters(4, "@ProgramshortCode", ProgramShortCode, 0);
            try
            {
               // i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdatePGTitle]");
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdatePGTitle_WithShortCode]");
                
                dbCom.Close();
            }


            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeletePGTitle()
        {

            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@p_nProgramTitleID", PGTitleID, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_DeleteProgramTitleOrName]");
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

        public DataTable GetList()
        {
            DataSet dt;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@PhasreTxt", SearchString);
            //dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetPGTitleList");
            dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetPGTitleList_WithShortCode");
            
            dbCom.Close();
            return dt.Tables[0];
        }
        public DataTable GetRecordByID()
        {
            DataTable dt = null;

            return dt;
        }
    }
}

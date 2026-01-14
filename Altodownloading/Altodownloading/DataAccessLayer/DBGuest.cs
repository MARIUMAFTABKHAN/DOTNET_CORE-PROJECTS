using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Altodownloading.DataAccessLayer
{
    class DBGuest
    {
        private Int32 mGuestID;        
        private string mGuest;
        private int mCreatedBy;
        private int mEditedBy;
        private DateTime mCreatedOn;
        private DateTime mEditedOn;
        private string mSearchString;

        public Int32 GuestID
        {
            get { return mGuestID; }
            set { mGuestID = value; }
        }
     
        public string Guest
        {
            get { return mGuest; }
            set { mGuest = value; }
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



        public bool InsertGuest()
        {
            bool result = true;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.BeginTransaction();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(6);
            dbCom.AddParameters(0, "@GuestID", GuestID , 1);            
            dbCom.AddParameters(1, "@Guest", Guest, 0);
            dbCom.AddParameters(2, "@CreatedBy", CratedBy, 0);
            dbCom.AddParameters(3, "@CreatedOn", CreatedOn, 0);
            dbCom.AddParameters(4, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(5, "@EditedOn", EditedOn, 0);
            try
            {
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertGuest]");
                this.GuestID = Convert.ToInt32(dbCom.Parameters[0].Value);
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
        public bool UpdateGuest()
        {
            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(4);
            dbCom.AddParameters(0, "@GuestID", GuestID,0);
            dbCom.AddParameters(1, "@Guest", Guest, 0);            
            dbCom.AddParameters(2, "@EditedBy", EditedBy, 0);
            dbCom.AddParameters(3, "@EditedOn", EditedOn, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdateGuest]");
                dbCom.Close();
            }


            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteGuest()
        {

            bool result = true;
            int i = -1;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@GuestID", GuestID, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_DeleteSUGuest]");
                if (i >= 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
                 catch (Exception exc )
                 {
                     ExceptionHandling.Application_ThreadException(exc );
                     result = false;
                        //if ((exc.Number == 2601)||(exc.Number ==2627))
                        //{
                        //    MessageBox.Show("Record is using in Archival data","Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    result = false;
                        //}
                        //else
                        //{
                        //    MessageBox.Show(exc.Number.ToString() + exc.Message);
                        //}
                }


            
            return result;
        }

        public DataTable GetList()
        {
            DataSet dt;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetGuestList");
            dbCom.Close();            
            return dt.Tables[0];
        }
        public DataTable GetRecordByID()
        {
            DataTable dt = null;

            return dt;
        }
        public DataTable GetSearchedList()
        {
            DataSet dt;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@PhasreTxt",SearchString,0 );
            dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetSearchedGuestList");
            dbCom.Close();            
            return dt.Tables[0];
        }
        
        





    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ABMS
{
    public class DBRegister
    {
        public int ID { get; set; }
        public DateTime InsertionDate { get; set; }
        public DateTime BookingDate { get; set; }
        public int PublicationId { get; set; }
        public string UserID { get; set; }
        public int SubCategoryID { get; set; }
        public int ClientID { get; set; }
        public int AgencyID { get; set; }
        public string RO { get; set; }
        public byte CM { get; set; }
        public byte COL { get; set; }
        public string Caption { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public DateTime ? AsOnDate { get; set; }
        public string Remarks { get; set; }
        public int StationID { get; set; }
        public int GroupBaseStationID { get; set; }
        public bool DummyGeneration { get; set; }
        public string RecAddedby { get; set; }
        public DateTime RecAddedDate { get; set; }
        public string RecEditedby { get; set; }
        public DateTime RecEditedDate { get; set; }
        public string WindowsID { get; set; }
        public string SystemName { get; set; }
        public string  BookingExecutiveID { get; set; }
        public bool IsConfirm { get; set; }
        public int  CMMeasurement { get; set; }
        public string LogAction { get; set; }
        public int PageID { get; set; }
        public int LogID { get; set; }
        public string ActionTaken { get; set; }

        public string StrSql { get; set; }

        public string ClientName { get; set; }
        public int ClientMainCategoryID { get; set; }
        public int ClientSubCategoryID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }



        public DataTable ExecuteDataTable(DBManager db, string sp)
        {
            
            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, sp);
            db.Command.Parameters.Clear();
            db.CreateParameters(0);
            return ds.Tables[0];
        }
        public bool InsertLog(DBManager db)
        {
            bool Result = false;
            db.Command.Parameters.Clear();
            db.CreateParameters(4);
            db.AddParameters(0, "ID", LogID);
            db.AddParameters(1, "UserID", UserID);
            db.AddParameters(2, "BookingID", ID);            
            db.AddParameters(3, "ActionTaken", LogAction);
            try
            {
                db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_InsertLog");
                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }
        public bool CancelBooking(DBManager db)
        {
            bool Result = false;
            db.Command.Parameters.Clear(); 
            db.CreateParameters(1);
            db.AddParameters(0, "BookingID", ID);
            try
            {
                db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_CancelBooking");
                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }
        public bool Insert(DBManager db)
        {                  
            bool Result = false;
            db.CreateParameters(24);
            db.AddParameters(0, "ID", ID);
            db.AddParameters(1, "@InsertionDate", InsertionDate);
            db.AddParameters(2, "@BookingDate", BookingDate);
            db.AddParameters(3, "@PublicationId", PublicationId);
            db.AddParameters(4, "@UserID", UserID);
            db.AddParameters(5, "@SubCategoryID", SubCategoryID  );
            db.AddParameters(6, "@ClientID", ClientID);
            db.AddParameters(7, "@AgencyID", AgencyID);
            db.AddParameters(8, "@RO", RO);
            db.AddParameters(9, "@CM", CM);
            db.AddParameters(10, "@COL", COL );
            db.AddParameters(11, "@Caption", Caption);
            db.AddParameters(12, "@Color", Color);
            db.AddParameters(13, "@Material", Material);
            if (AsOnDate == null)
            {
                db.AddParameters(14, "@AsOnDate", DBNull.Value);
            }
            else
            {
                db.AddParameters(14, "@AsOnDate", AsOnDate);
            }
            db.AddParameters(15, "@Remarks", Remarks);
            db.AddParameters(16, "@StationID", StationID );
            db.AddParameters(17, "@GroupBaseStationID", GroupBaseStationID);
            db.AddParameters(18, "@DummyGeneration", false);
            db.AddParameters(19, "@RecAddedby", RecAddedby);
            db.AddParameters(20, "@RecAddedDate", DateTime.Now);
            db.AddParameters(21, "@WindowsID", Helper.GetIpAddress());
            db.AddParameters(22, "@SystemName", Helper.GetCompCode());
            db.AddParameters(23, "@IsConfirm", IsConfirm);
            

            try
            {
                db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_InsertBookingRegister");
                
                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }         
            return Result;
        }
        public bool CheckRecordDate(DBManager db)
        {
            bool Result = false;
                    db.Command.Parameters.Clear ();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@InsertionDate",InsertionDate);
                    DataSet ds= db.ExecuteDataSet (CommandType.StoredProcedure, "sp_CheckExistingDate");
                    if (ds.Tables[0].Rows.Count > 0)
                        Result = true;
            return Result;
        }
        public  bool InsertDetails(DBManager db, int PID,DataTable dt)
        {
            bool Result = true;
            try
            {
                int RecID =0;
                db.Command.Parameters.Clear();               
                foreach (DataRow dr in dt.Rows)
                {
                    db.CreateParameters(1);
                    db.AddParameters(0, "@For_Table", "Stations");
                    RecID  = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));

                    db.Command.Parameters.Clear();
                    db.CreateParameters(5);
                    db.AddParameters(0, "@ID", RecID);
                    db.AddParameters(1, "@PID",PID);
                    db.AddParameters(2, "@PageID", Convert.ToInt32 (dr[5]));
                    db.AddParameters(3, "@GroupcompID", Convert.ToInt32 (dr[6]));
                    db.AddParameters(4, "@PositionID", dr[7].ToString());
                    db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_InsertBookingDetails");
                }
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;

        }
        public bool CheckSpace(DBManager db )
        {
            bool Result = true;
            db.Command.Parameters.Clear ();
            db.CreateParameters (1);
            db.AddParameters(0, "@InsertionDate", InsertionDate);            
            int i =  Convert.ToInt32 (  db.ExecuteScalar (CommandType.StoredProcedure,"sp_GetConfirmSpaceByDate"));
            CMMeasurement =  (i + CM);
            if ((i + CM) > 54)
                Result = false;
            // sp_GetConfirmSpaceByDate
            return Result;
        }

        public bool DeleteDetails(DBManager db)
        {
            bool Result = false;
            string strsql = "Delete from Stations where BookingRegister_ID =" + ID;
            db.Command.Parameters.Clear();
            db.CreateParameters(0);
            try
            {
                db.ExecuteNonQuery(CommandType.Text, strsql);
                Result = true;
            }
            catch (Exception)
            {
            }
            return Result;
        }

        public bool UpDate(DBManager db)
        {
            bool Result = false;
            db.CreateParameters(22);
            db.AddParameters(0, "ID", ID);
            db.AddParameters(1, "@InsertionDate", InsertionDate);
            db.AddParameters(2, "@BookingDate", BookingDate);
            db.AddParameters(3, "@PublicationId", PublicationId);            
            db.AddParameters(4, "@SubCategoryID", SubCategoryID);
            db.AddParameters(5, "@ClientID", ClientID);
            db.AddParameters(6, "@AgencyID", AgencyID);
            db.AddParameters(7, "@RO", RO);
            db.AddParameters(8, "@CM", CM);
            db.AddParameters(9, "@COL", COL);
            db.AddParameters(10, "@Caption", Caption);
            db.AddParameters(11, "@Color", Color);
            db.AddParameters(12, "@Material", Material);
            if (AsOnDate == null)
            {
                db.AddParameters(13, "@AsOnDate", DBNull.Value);
            }
            else
            {
                db.AddParameters(13, "@AsOnDate", AsOnDate);
            }
            db.AddParameters(14, "@Remarks", Remarks);
            db.AddParameters(15, "@StationID", StationID);
            db.AddParameters(16, "@GroupBaseStationID", GroupBaseStationID);            
            db.AddParameters(17, "@RecEditedby", RecAddedby);
            db.AddParameters(18, "@RecEditedDate", DateTime.Now);
            db.AddParameters(19, "@WindowsID", Helper.GetIpAddress());
            db.AddParameters(20, "@SystemName", Helper.GetCompCode());
            db.AddParameters(21, "@IsConfirm", IsConfirm);


            try
            {
                db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_UpdateBookingRegister");

                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }
        public DataTable GetCitiesByPubID(DBManager db)
        { 
            db.Command.Parameters.Clear();
            db.CreateParameters(1);
            db.AddParameters(0, "@RecID", ID);            

            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetCityListByPublicationByInsertionDate");
            return ds.Tables [0];            
        }
        public DataTable BookingRegisterByPublicationInsertionDate(DBManager db)
        {
            db.Command.Parameters.Clear();
            db.CreateParameters(2);
            db.AddParameters(0, "@InsertionDate", InsertionDate);
            db.AddParameters(1, "@PublicationID", PublicationId);
            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetBookingregisterbyInsertionDate");
            return ds.Tables[0];
        }
        public DataTable AdGraphicView_Confirm(DBManager db)
        {
            db.Command.Parameters.Clear();
            db.CreateParameters(5);
            db.AddParameters(0, "@InsertionDate", InsertionDate);
            db.AddParameters(1, "@IsConfirm", IsConfirm);
            db.AddParameters(2, "@PublicationID", PublicationId);
            db.AddParameters(3, "@StationId", GroupBaseStationID);
            db.AddParameters(4, "@PageID", PageID);
            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetAdGraphicView_Confirm");
            return ds.Tables [0];            
        }
        public DataTable AdGraphicView_UnConfirm(DBManager db)
        {
            db.Command.Parameters.Clear();
            db.CreateParameters(5);
            db.AddParameters(0, "@InsertionDate", InsertionDate);
            db.AddParameters(1, "@IsConfirm", IsConfirm);
            db.AddParameters(2, "@PublicationID", PublicationId);
            db.AddParameters(3, "@StationId", GroupBaseStationID);
            db.AddParameters(4, "@PageID", PageID);
            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetAdGraphicView_UnConfirm");
            return ds.Tables[0];
        }
        public DataTable GetDataTableByText(DBManager db)
        {
            db.Command.Parameters.Clear();            
            DataSet ds = db.ExecuteDataSet(CommandType.Text, StrSql);
            return ds.Tables[0];
        }
        public bool GetInsertClient(DBManager db)
        {
            bool Result = false;
            db.Command.Parameters.Clear();
            try
            {
               db.ExecuteNonQuery(CommandType.Text, StrSql);
               Result = true;
            }
            catch (Exception)
            {
            }
            return Result;
        }

        public bool InsertClient(DBManager db)
        {
            bool Result = false;
            db.Command.Parameters.Clear();
            db.CreateParameters(9);
            db.AddParameters(0, "@ID",ID);
            db.AddParameters(1, "@ClientName",ClientName);
            db.AddParameters(2, "@MainCategoryID",ClientMainCategoryID);
            db.AddParameters(3, "@SubCategoryID",SubCategoryID);
            db.AddParameters(4, "@Addres1",Address1 );
            db.AddParameters(5, "@Addres2",Address2);
            db.AddParameters(6, "@Addres3",Address3);
            db.AddParameters(7, "@Addres4",Address4 );
            db.AddParameters(8, "@UserID",UserID);
            try
            {
                db.ExecuteNonQuery(CommandType.StoredProcedure, "sp_InsertClient");
                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }
    }
}
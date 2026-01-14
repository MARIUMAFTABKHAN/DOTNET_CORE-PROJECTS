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
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

public static class Helper
{

    private static Random random = new Random((int)DateTime.Now.Ticks);
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    //public static DateTime SetDateFormat(string txt)
    //{
    //    DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
    //    try
    //    {
    //        string[] str = txt.Split('/');

    //        string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

    //        // dt       
    //        dt = Convert.ToDateTime(a);
    //    }
    //    catch (Exception)
    //    {
    //        SetDateFormat(DateTime.Now.ToString("dd/MM/yyy"));
    //    }
    //    return dt;

    //}
    public static string SetDateFormatString(string txt)
    {
        if (string.IsNullOrEmpty(txt))
            txt = DateTime.Now.ToString("dd/MM/yyyy");
        string[] str = txt.Substring(0, 10).Split('/');

        string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

        return a;

    }
    public static bool IsEmail(string Email)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }  

    public static string GetDBConnectionString()
    {
        //return System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");
        return System.Configuration.ConfigurationManager.AppSettings["ConStr"];
    }
   

   
    public static int GetCounter(DBManager db , string Fortable)
    {
        //BookingRegister
        Int32 RecCounter = 0;
        db.Command.Parameters.Clear();
        db.CreateParameters(2);
        db.AddParameters(0, "@fortable", Fortable, 0);
        db.AddParameters(1, "@IdColumn", 0, 1);
        DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "usp_GetCounter"); 
        RecCounter = Convert.ToInt32(ds.Tables[0].Rows[0][0]);        
        return RecCounter;
    }
    public static DateTime  SetDateFormat(string txt)
    {
        string[] str = txt.Split('-');

        string a = str[1].ToString() + "-" + str[0].ToString() + "-" +str[2].ToString();

        return Convert.ToDateTime (a);

    }
    public static string GetIpAddress()  // Get IP Address
    {
        string ip = "";
        try
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
            IPAddress[] addr = ipEntry.AddressList;
            ip = addr[1].ToString();
        }
        catch (Exception)
        {          
        }
       
        return ip;
    }
    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        strHostName = Dns.GetHostName();
        return strHostName;
    }

    public static DataTable BlankDataTable()
    {
        DataTable dt = new DataTable();        
        dt.Columns.Add("ID");
        dt.Columns.Add("Description");
        DataRow  dr = dt.NewRow();
        dr[0] = "0";
        dr[1] = "Please select me";
        dt.Rows.InsertAt(dr, 0);
        return dt;
    }
}

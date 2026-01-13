using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using EPOMS;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Drawing;
using System.Globalization;

public static class Helper
{
    public static class Messages
    {
        public const string Authorization = "You are not authorised to edit this record ...";
    }

    public enum UploadDocumentType
    {
        Quotation,
        HODApproval,
        ManagerApproval,
        Challan,
        Invoice,
        Bargain
    }

    //I write a method for decimal numbers in this example, you can write some override methods for it to support int, long and double to use it without any casting such as:
    public static string ToKMB(this int num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }
        else
            if (num > 999999 || num < -999999)
            {
                return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }
            else
                if (num > 999 || num < -999)
                {
                    return num.ToString("0,.#K", CultureInfo.InvariantCulture);
                }
                else
                {
                    return num.ToString(CultureInfo.InvariantCulture);
                }
    }
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static DateTime SetDateFormatNew(string txt)
    {
        DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        try
        {
            string[] str = txt.Split('/');
            string a = str[2].ToString() + "/" + str[1].ToString() + "/" + str[0].ToString();
            dt = Convert.ToDateTime(a);
        }
        catch (Exception)
        {
            SetDateFormat(DateTime.Now.ToString("dd/MM/yyy"));
        }

        return dt;
    }

    public static DateTime SetDateFormat(string txt)
    {
        DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        try
        {
            string[] str = txt.Split('/');
            string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();
            dt = Convert.ToDateTime(a);
        }
        catch (Exception)
        {
            SetDateFormat(DateTime.Now.ToString("dd/MM/yyy"));
        }

        return dt;
    }

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
        if (Email == "")
            return false;
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }

    //public static DataTable ToDataTable<T>(List<T> items)
    //{
    //    DataTable dataTable = new DataTable(typeof(T).Name);

    //    //Get all the properties
    //    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //    foreach (PropertyInfo prop in Props)
    //    {
    //        //Setting column names as Property names
    //        dataTable.Columns.Add(prop.Name);
    //    }
    //    foreach (T item in items)
    //    {
    //        var values = new object[Props.Length];
    //        for (int i = 0; i < Props.Length; i++)
    //        {
    //            //inserting property values to datatable rows
    //            values[i] = Props[i].GetValue(item, null);
    //        }
    //        dataTable.Rows.Add(values);
    //    }
    //    //put a breakpoint here and check datatable
    //    return dataTable;
    //}
    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }

        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                try
                {
                    var value = Props[i].GetValue(item, null);
                    values[i] = value ?? DBNull.Value;
                }
                catch (TargetInvocationException ex)
                {
                    throw new Exception($"Error getting value of property '{Props[i].Name}'", ex.InnerException);
                }
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    private static void AddControls(ControlCollection page, ArrayList controlList)
    {
        foreach (Control c in page)
        {
            if (c.ID != null)
            {
                if ((c.ID.Contains("btn")) || (c.ID.Contains("gv")) || (c.ID.Contains("Edit")) || (c.ID.Contains("Dell")))
                {
                    controlList.Add(c.ID);
                }
            }

            if (c.HasControls())
            {

                AddControls(c.Controls, controlList);
            }
        }
    }

    public static string GenerateEmailBody(string title, string bodyText, string senderName, string senderDepartment)
    {
        var sb = new StringBuilder();
        sb.Append("<table>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(title);
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(bodyText);
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("Regards");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(senderName);
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(senderDepartment);
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("This is system generated email message");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        return sb.ToString();
    }
    public static string RemoveCharactersFromString(string originalString)
    {
        char[] chars = originalString.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            int asciiValue = (int)chars[i];

            //if (!((asciiValue >= 48 && asciiValue <= 57) || (asciiValue >= 65 && asciiValue <= 90) || (asciiValue >= 97 && asciiValue <= 122)))
            if (!(asciiValue >= 32 && asciiValue <= 125))
            {
                chars[i] = ' '; // Replace characters outside the specified range with a space
            }
        }

        string replacedString = new string(chars);

        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options);
        replacedString = regex.Replace(replacedString, " ");

        return replacedString.Trim();
    }
}

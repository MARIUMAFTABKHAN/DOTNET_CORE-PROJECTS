using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using System.Globalization;

namespace ExpressDigital
{
    public static class Helper
    {
        public static string Page { get; set; }
        public static int UID { get; set; }



        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static DateTime SetDateFormat(string txt)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            try
            {
                string[] str = txt.Split('/');

                string a = str[2].ToString() + "/" + str[1].ToString() + "/" + str[0].ToString();

                // dt       
                dt = Convert.ToDateTime(Convert.ToDateTime(a).ToShortDateString()); ;
            }
            catch (Exception ex)
            {
                dt = DateTime.Now;
            }
            return dt;

        }
        public static string SetDateFormatString(string txt,bool val)
        {
            string a = "";
            try
            {
                if (string.IsNullOrEmpty(txt))
                    txt = DateTime.Now.ToString("dd/MM/yyyy");
                string[] str = txt.Substring(0, 10).Split('/');

                a = str[2].ToString() + "-" + str[1].ToString() + "-" + str[0].ToString();

            }
            catch (Exception)
            {
                throw;
            }
            return a;

        }
        public static string SetDateFormatString(string txt)
        {
            string a = "";
            try
            {
                if (string.IsNullOrEmpty(txt))
                    txt = DateTime.Now.ToString("dd/MM/yyyy");
                string[] str = txt.Substring(0, 10).Split('/');

                a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

            }
            catch (Exception)
            {
                throw;
            }
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
        public static DataSet ToDataSetNew<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }
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
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static Boolean CheckValidPage(DbDigitalEntities db)
        {
            bool Result = false;

            var lst = db.ExamptedForms.Where(x => x.FormName.Contains(Page)).SingleOrDefault();
            if (lst != null)
            {
                //if ((Page == "home.aspx" || (Page == "rptreleaseorder.aspx") || (Page == "releaseorderdetails.aspx") 
                ///  || (Page == "suspendresotregroupagency.aspx") 
                // || (Page == "suspendresotreagency.aspx") || (Page == "suspendresotreclient.aspx") || (Page == "documentsearchreport.aspx")
                //||(Page == "documentacknowledgereport.aspx")||
                //(Page == "reportdebitcredit.aspx")
                //))                
                Result = true;
            }

            else
            {
                db = new DbDigitalEntities();
                var s = db.usp_CheckValidPage(Page, UID).ToList().Count();
                if (s > 0)
                    Result = true;
            }
            return Result;
        }
        public static byte[] PrepImageForUpload(AjaxControlToolkit.AsyncFileUpload FileData, int _height, int _width)
        {
            using (Bitmap origImage = new Bitmap(FileData.FileContent))
            {
                //int maxWidth = _width;

                int newWidth = _height;// origImage.Width;
                int newHeight = _width;// origImage.Height;
                                       //if (origImage.Width < newWidth) //Force to max width
                                       //{
                                       //    newWidth = maxWidth;
                                       //    newHeight = origImage.Height * maxWidth / origImage.Width;
                                       //}

                using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                {
                    using (Graphics gr = Graphics.FromImage(newImage))
                    {
                        gr.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(origImage, new Rectangle(0, 0, newWidth, newHeight));

                        MemoryStream ms = new MemoryStream();
                        newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
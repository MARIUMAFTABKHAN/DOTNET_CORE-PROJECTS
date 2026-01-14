using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;


namespace Altodownloading
{
    public static class Helper
    {
        public static string Page { get; set; }
        public static int UID { get; set; }

        public static Int32 User_Id = 0;
        public static String UserFullName = "";
        /// <returns>Setting with all settings value</returns>
        public static SystemConfigurationData GetSettings()
        {
            SystemConfigurationData objscd = new SystemConfigurationData();

            objscd.LocationCode = System.Configuration.ConfigurationSettings.AppSettings["LocationCode"];
            objscd.LocationName = System.Configuration.ConfigurationSettings.AppSettings["LocationName"];
            objscd.DueDays = System.Configuration.ConfigurationSettings.AppSettings["DueDays"];
            objscd.HighResPath = System.Configuration.ConfigurationSettings.AppSettings["HighResPath"];
            objscd.LowResPath = System.Configuration.ConfigurationSettings.AppSettings["LowResPath"];
            objscd.DVDTempFolder = System.Configuration.ConfigurationSettings.AppSettings["DVDTempFolder"];
            objscd.DVDTempFolderSize = System.Configuration.ConfigurationSettings.AppSettings["DVDTempFolderSize"];
            objscd.M2TempFolder = System.Configuration.ConfigurationSettings.AppSettings["M2TempFolder"];
            objscd.JTSTempFolder = System.Configuration.ConfigurationSettings.AppSettings["JTSTempFolder"];

            return objscd;
        }
        public static string GetDownloadsPath()
        {
            string currentMonth = DateTime.Now.ToString("dd-MM-yyyy");
            string path = null;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                IntPtr pathPtr;
                int hr = SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                if (hr == 0)
                {
                    path = System.Runtime.InteropServices.Marshal.PtrToStringUni(pathPtr);
                    Marshal.FreeCoTaskMem(pathPtr);
                    return path + "\\" + currentMonth;
                }
            }
            path = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            path = Path.Combine(path, "Downloads");
            
            return path +"\\"+ currentMonth;
        }
        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);
        public static bool WriteSettings(SystemConfigurationData objscd)
        {
            AppSettingsSection objAPPSettings;

            try
            {
                string exePath = System.Windows.Forms.Application.ExecutablePath;// System.IO.Path.Combine(Environment.CurrentDirectory, "TMNCallDialingSystem.exe");

                Configuration objConfiguration = ConfigurationManager.OpenExeConfiguration(exePath);

                objAPPSettings = objConfiguration.AppSettings;
                objAPPSettings.Settings["LocationCode"].Value = objscd.LocationCode;
                objAPPSettings.Settings["LocationName"].Value = objscd.LocationName;
                objAPPSettings.Settings["DueDays"].Value = objscd.DueDays;
                objAPPSettings.Settings["HighResPath"].Value = objscd.HighResPath;
                objAPPSettings.Settings["LowResPath"].Value = objscd.LowResPath;
                objAPPSettings.Settings["DVDTempFolder"].Value = objscd.DVDTempFolder;
                objAPPSettings.Settings["DVDTempFolderSize"].Value = objscd.DVDTempFolderSize;
                objAPPSettings.Settings["M2TempFolder"].Value = objscd.M2TempFolder;
                objAPPSettings.Settings["JTSTempFolder"].Value = objscd.JTSTempFolder;

                //objAPPSettings.Settings["AreaCode"].Value = cmbArea.SelectedValue.ToString();
                //objAPPSettings.Settings["AreaName"].Value = ((DataRowView)cmbArea.SelectedItem)["AreaName"].ToString();
                //objAPPSettings.Settings["COMPort"].Value = cmbCOM.SelectedItem.ToString();
                objConfiguration.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                ShowMsg(ex);
                return false;
            }

            return true;
        }

        public static bool WriteConfigFiles(SystemConfigurationData objscd)
        {
            AppSettingsSection objAPPSettings;

            try
            {
                string exePath = System.Windows.Forms.Application.ExecutablePath;// System.IO.Path.Combine(Environment.CurrentDirectory, "TMNCallDialingSystem.exe");

                Configuration objConfiguration = ConfigurationManager.OpenExeConfiguration(exePath);

                objAPPSettings = objConfiguration.AppSettings;
                objAPPSettings.Settings["LocationCode"].Value = objscd.LocationCode;
                objAPPSettings.Settings["LocationName"].Value = objscd.LocationName;
                objAPPSettings.Settings["DueDays"].Value = objscd.DueDays;
                objAPPSettings.Settings["HighResPath"].Value = objscd.HighResPath;
                objAPPSettings.Settings["LowResPath"].Value = objscd.LowResPath;
                objAPPSettings.Settings["DVDTempFolder"].Value = objscd.DVDTempFolder;
                objAPPSettings.Settings["DVDTempFolderSize"].Value = objscd.DVDTempFolderSize;
                objAPPSettings.Settings["M2TempFolder"].Value = objscd.M2TempFolder;
                objAPPSettings.Settings["JTSTempFolder"].Value = objscd.JTSTempFolder;

                //objAPPSettings.Settings["AreaCode"].Value = cmbArea.SelectedValue.ToString();
                //objAPPSettings.Settings["AreaName"].Value = ((DataRowView)cmbArea.SelectedItem)["AreaName"].ToString();
                //objAPPSettings.Settings["COMPort"].Value = cmbCOM.SelectedItem.ToString();
                objConfiguration.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                ShowMsg(ex);
                return false;
            }

            return true;
        }

        public static void ShowMsg(Exception ex)
        {
            MessageBox.Show(ex.Message + "\nError occured during the operation.", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        }

        public static string GetJTSDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.AppSettings["JTSConnectionString"].ToString();
        }

        public static string GetCTSDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CTSConnectionString"].ToString();
        }
        public static string GetEXPARCSDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        }
        public static DateTime GetDefaultDateTime()
        {
            //return Convert.ToDateTime("1/1/1900");

            return Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }

        public static String RemoveSpaces(String Text)
        {

            String[] str;

            str = Text.Split((char)32);
            String lastVal = String.Empty;
            List<String> lst = new List<string>();
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if ((lastVal == "") & (str[i] == ""))
                {
                    lastVal = str[i];
                    str[i] = "-";
                }
                else
                {
                    lst.Add(str[i]);
                    lastVal = str[i];
                }

            }

            lst.TrimExcess();

            lst.RemoveAll(EmptyString);



            return String.Join(" ", lst.ToArray());




        }
        private static bool EmptyString(String s)
        {
            if (s == "")
                return true;
            else
                return false;

        }




        private static Random random = new Random((int)DateTime.Now.Ticks);


        public static class Messages
        {
            public const string Authorization = "You are not authorised to edit this record ...";

        }

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

                string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

                // dt       
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

    }
}
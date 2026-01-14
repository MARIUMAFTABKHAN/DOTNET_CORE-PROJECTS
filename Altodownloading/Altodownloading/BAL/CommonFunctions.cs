using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Altodownloading.BAL
{
    public static class CommonFunctions
    {
        private static ToolStrip mSTP;

        public static ToolStrip STP
        {
            get
            {
                return mSTP;

            }
            set
            {
                mSTP = value;

            }

        }

        public static bool CheckPath()
        {
             bool result = true;
             string Folderpath ="";
             Folderpath = System.Configuration.ConfigurationManager.AppSettings["DVDTempFolder"];
             if (! Directory.Exists(Folderpath))
             {
                 MessageBox.Show("DVDTempFolder not exits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                 return false ;
             }
             Folderpath = System.Configuration.ConfigurationManager.AppSettings["HighResPath"];
             if (!Directory.Exists(Folderpath))
             {
                 MessageBox.Show("DVDTempFolder not exits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
             }
             

             Folderpath = System.Configuration.ConfigurationManager.AppSettings["JTSTempFolder"];
             if (!Directory.Exists(Folderpath))
             {
                 MessageBox.Show("DVDTempFolder not exits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
             }
             Folderpath = System.Configuration.ConfigurationManager.AppSettings["LowResPath"];
             if (!Directory.Exists(Folderpath))
             {
                 MessageBox.Show("DVDTempFolder not exits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
             }
             Folderpath = System.Configuration.ConfigurationManager.AppSettings["M2TempFolder"];
             if (!Directory.Exists(Folderpath))
             {
                 MessageBox.Show("DVDTempFolder not exits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
             } 

            return result;
        }

        public static void SetButtons(bool btnnew, bool btnedit, bool btnsave, bool btnsaveclose, bool btncancel)
        {
            foreach (ToolStripItem itm in STP.Items)
            {
                int tg = itm.Tag == null ? 0 : Convert.ToInt32(itm.Tag);
                if (tg == 1)
                {
                    switch (itm.Name)
                    {
                        case "btnNew":
                            itm.Enabled = btnnew;
                            break;
                        case "btnEdit":
                            itm.Enabled = btnedit;
                            break;
                        case "btnSave":
                            itm.Enabled = btnsave;
                            break;
                        case "btnSavenClose":
                            itm.Enabled = btnsaveclose;
                            break;
                        case "btnCancel":
                            itm.Enabled = btncancel;
                            break;
                        //case "btnDelete":
                        //    itm.Enabled = btndelete;
                        //    break;
                    }
                    //MessageBox.Show(itm.Name);
                }
            }
        }
        public static bool isOpened(string fname)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == fname)//"FrmDirectoryLiting")
                {
                    return true;
                }
            }
            return false;
        }
        public static bool FindAndKillProcess()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Length >= 7)
                {

                    if (clsProcess.ProcessName.Contains("Altodownloading"))
                    {
                        clsProcess.Kill();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }
            }
            return true;
        }

        public static bool checkTime(string txt)
        {
            bool result = true;
            string[] str = txt.Split(':');
            for (int a = 0; a < str.Length; a++)
            {
                try
                {
                    if (Convert.ToInt32(str[a]) >= 0)
                    {
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                    return result;
                }
            }
            return result;

        }
        public static Form DynamicallyLoadedObject(string AssemblyName, string objectName)
        {
            object returnObj = null;
            try
            {
                Type type = Assembly.GetExecutingAssembly().GetType(AssemblyName + "." + objectName, true);
                if (type != null)
                {
                    try
                    {
                        returnObj = (Activator.CreateInstance(type, null));
                    }
                    catch (Exception)
                    {
                        returnObj = Activator.CreateInstance(type, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return (Form)returnObj;
        }
        public static string GetDuration(string mTime)
        {
            string m_Time = "0";
            string[] t = mTime.Split(':');
            switch (t.Length)
            {
                case 0:
                    m_Time = "0";
                    break;
                case 1:
                    m_Time = mTime;
                    break;
                case 2:
                    m_Time = Convert.ToString((Convert.ToInt32(t[0]) * 60) + Convert.ToInt32(t[1]));
                    break;
                case 3:
                    m_Time = Convert.ToString((Convert.ToInt32(t[0]) * 3600) + (Convert.ToInt32(t[1]) * 60) + Convert.ToInt32(t[2]));
                    break;
            }
            return m_Time;

        }
        public static decimal CalculateTargetFolderSize(string folder)
        {
            decimal folderSize = 0.0m;
            try
            {
                //Checks if the path is valid or not
                if (!Directory.Exists(folder))
                    return folderSize;
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(folder))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                folderSize += (finfo.Length / 1024);
                            }
                        }

                        foreach (string dir in Directory.GetDirectories(folder))
                            folderSize += CalculateTargetFolderSize(dir);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return Math.Round((folderSize / 1024), 2);
        }

        public static decimal CalculateFolderSize(string folder)
        {
            decimal folderSize = 0.0m;
            try
            {
                //Checks if the path is valid or not
                if (!Directory.Exists(folder))
                    return folderSize;
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(folder))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                folderSize += (finfo.Length / 1024);
                            }
                        }

                        foreach (string dir in Directory.GetDirectories(folder))
                            folderSize += CalculateFolderSize(dir);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            //decimal val = 0.0m;
            //val = Convert.ToDecimal(folderSize) / 1024;
            return Math.Round((folderSize / 1024), 2);
        }
        public static string DefaultPrinterName()
        {
            string functionReturnValue = null;
            System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings();

            try
            {
                functionReturnValue = oPS.PrinterName;
            }
            catch (System.Exception ex)
            {
                functionReturnValue = "";
            }
            finally
            {
                oPS = null;
            }
            return functionReturnValue;
        }


        public static  bool CheckEntryInArchive(string sql)
        {
            DBManager db = new DBManager();
            bool result = false; ;
            db.Open();
            int i =-1;
            i = db.ExecuteNonQuery(System.Data.CommandType.Text, sql);
            if (i > 0)
            {
                result = true;
            }
            db.Close();
            return result;
        }
    }
}
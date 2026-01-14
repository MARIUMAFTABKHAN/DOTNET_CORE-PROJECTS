using System;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Windows.Forms;

namespace Altodownloading
{
    public class  SystemConfigurationData
    {
        private string _DBConnectionString;
        private string _CTSConnectionString;
        private string _JTSConnectionString;

        public SystemConfigurationData()
        {
        }

        [XmlElement]
        public string LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        private string _LocationCode;

        [XmlElement]
        public string LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        private string _LocationName;

        [XmlElement]
        public string DueDays
        {
            get { return _DueDays; }
            set { _DueDays = value; }
        }
        private string _DueDays;

        [XmlElement]
        public string HighResPath
        {
            get { return _HighResPath; }
            set { _HighResPath = value; }
        }
        private string _HighResPath;

        [XmlElement]
        public string LowResPath
        {
            get { return _LowResPath; }
            set { _LowResPath = value; }
        }
        private string _LowResPath;

        [XmlElement]
        public string DVDTempFolder
        {
            get { return _DVDTempFolder; }
            set { _DVDTempFolder = value; }
        }
        private string _DVDTempFolder;

        [XmlElement]
        public string DVDTempFolderSize
        {
            get { return _DVDTempFolderSize; }
            set { _DVDTempFolderSize = value; }
        }
        private string _DVDTempFolderSize;

        [XmlElement]
        public string M2TempFolder
        {
            get { return _M2TempFolder; }
            set { _M2TempFolder = value; }
        }
        private string _M2TempFolder;

        [XmlElement]
        public string JTSTempFolder
        {
            get { return _JTSTempFolder; }
            set { _JTSTempFolder = value; }
        }

        [XmlElement]
        public string CTSConnectionString
        {
            get { return _CTSConnectionString; }
            set { _CTSConnectionString = value; }
        }
        public string JTSConnectionString
        {
            get { return _JTSConnectionString; }
            set { _JTSConnectionString = value; }
        }
        public string DBConnectionString
        {
            get { return _DBConnectionString; }
            set { _DBConnectionString = value; }
        }
        private string _JTSTempFolder;

        public bool UpdateConfig()
        {
            bool result = true;
            System.Configuration.AppSettingsSection objAPPSettings;

            try
            {
                string exePath = System.Windows.Forms.Application.ExecutablePath;// System.IO.Path.Combine(Environment.CurrentDirectory, "TMNCallDialingSystem.exe");
                Configuration objConfiguration = ConfigurationManager.OpenExeConfiguration(exePath);
                objAPPSettings = objConfiguration.AppSettings;
                objAPPSettings.Settings["CTSConnectionString"].Value = CTSConnectionString;
                objAPPSettings.Settings["JTSConnectionString"].Value = JTSConnectionString ;
                objAPPSettings.Settings["ConnectionString"].Value = DBConnectionString;
                objConfiguration.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
                result = false;
            }          
            return result;
        }
    }
}

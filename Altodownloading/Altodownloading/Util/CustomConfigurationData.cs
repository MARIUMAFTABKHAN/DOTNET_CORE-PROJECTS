using System;
using System.Drawing;
using System.Xml;

namespace Altodownloading
{

    public class CustomConfigurationData
    {

        public CustomConfigurationData()
        {
        }
       
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private string _server;


        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        private string _database;

        public string UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        private string _userid;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _password;

        public string LoginSave
        {
            get { return _LoginSave; }
            set { _LoginSave = value; }
        }
        private string _LoginSave;

        public string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }
        private string _LoginId;

        public string LoginPw
        {
            get { return _LoginPw; }
            set { _LoginPw = value; }
        }
        private string _LoginPw;
    }
    
}

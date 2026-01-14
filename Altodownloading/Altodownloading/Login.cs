using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using Altodownloading.BAL;
namespace Altodownloading
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private bool ValidateAll()
        {
            if ((ValidationForm.EmptyStringIsValid(ref txtUserID, ref erpCntl) == false) &&
                  (ValidationForm.EmptyStringIsValid(ref txtPassword, ref erpCntl) == false))
            {

                return true;
            }
            else
            {
                return false;
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserID.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtUserID.Focus();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            checkPaths();

            this.BackColor = Color.AliceBlue;
            txtUserID.Focus();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            GlobalClass.FileVersion = fvi.FileVersion;
            string version = fvi.FileVersion;
        }

        private void checkPaths()
        {

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateAll() == true)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = new Altodownloading._JTSDB().ValidateUser(txtUserID.Text.Trim(), txtPassword.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    Helper.User_Id = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    Helper.UserFullName = Convert.ToString(dt.Rows[0]["FullName"]);
                    // EXPARCS.BAL.GlobalClass.IsValidUser = true;
                    //FrmMaster frm = new FrmMaster ();
                    // mpanl.Enabled = true ;
                    //  mst.Items[1].Text = Helper.UserFullName;
                    //  mst.Items[3].Text = DateTime.Now.ToString("dd/MM/yyyy") + ":" + DateTime.Now.ToShortTimeString();
                    BAL.GlobalClass.ConnString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                    BAL.GlobalClass.CurrentUserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    this.Hide();

                    btnPicTargetPath objfrmMain = new btnPicTargetPath();
                    objfrmMain.Show();
                    objfrmMain.Text = objfrmMain.Text + " : User Name : " + dt.Rows[0]["FullName"].ToString() + " | " + dt.Rows[0]["Role"].ToString() + " | " + dt.Rows[0]["Bureau"].ToString() + "|" + GlobalClass.FileVersion;// Ver.2.0.6" ;


                }
                else
                {
                    //EXPARCS.BAL.MainFormInstance.IsValidUser = false;
                    MessageBox.Show("Invalid UserID or Password.");
                    txtUserID.Focus();
                }

                //System.Data.DataTable dt = new System.Data.DataTable();

                //// ✅ Step 1: Check for hardcoded admin login
                //if (txtUserID.Text.Trim() == "admin" && txtPassword.Text.Trim() == "admin123")
                //{
                //    // Set mock data for admin user
                //    Helper.User_Id = 1; // you can assign any valid ID
                //    Helper.UserFullName = "Admin User";

                //    BAL.GlobalClass.ConnString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                //    BAL.GlobalClass.CurrentUserId = 1;

                //    this.Hide();

                //    btnPicTargetPath objfrmMain = new btnPicTargetPath();
                //    objfrmMain.Show();
                //    objfrmMain.Text = objfrmMain.Text + " : User Name : Admin User | Admin | Headquarters | " + GlobalClass.FileVersion;

                //    return; // skip DB check
                //}

                //// ✅ Step 2: If not admin, proceed with DB validation
                //dt = new Altodownloading._JTSDB().ValidateUser(txtUserID.Text.Trim(), txtPassword.Text.Trim());

                //if (dt.Rows.Count > 0)
                //{
                //    Helper.User_Id = Convert.ToInt32(dt.Rows[0]["UserId"]);
                //    Helper.UserFullName = Convert.ToString(dt.Rows[0]["FullName"]);

                //    BAL.GlobalClass.ConnString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                //    BAL.GlobalClass.CurrentUserId = Convert.ToInt32(dt.Rows[0]["UserId"]);

                //    this.Hide();

                //    btnPicTargetPath objfrmMain = new btnPicTargetPath();
                //    objfrmMain.Show();
                //    objfrmMain.Text = objfrmMain.Text + " : User Name : " + dt.Rows[0]["FullName"].ToString() +
                //                      " | " + dt.Rows[0]["Role"].ToString() +
                //                      " | " + dt.Rows[0]["Bureau"].ToString() +
                //                      " | " + GlobalClass.FileVersion;
                //}
                //else
                //{
                //    MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

            }
        }

        private void gpbAuthenticate_Enter(object sender, EventArgs e)
        {

        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            erpCntl.SetError(txtUserID, "");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Altodownloading.BAL.CommonFunctions.FindAndKillProcess() == true)
                Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
            txtUserID.Focus();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            GlobalClass.FileVersion = fvi.FileVersion;
            string version = fvi.FileVersion;
        }
    }
}

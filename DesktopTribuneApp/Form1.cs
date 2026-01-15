using System.Data;
using System.Data.SqlClient;

namespace DesktopTribuneApp
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, string> fileIndexCache = new(StringComparer.OrdinalIgnoreCase);
        private readonly string networkRoot = @"\\61.5.152.140\VFS"; // ✅ Updated for local access

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=61.5.152.140\\MSSQLSERVER;Initial Catalog=EXPARCS;User ID=NcUser;Password=New#Contact_DB_user_2003;Connect Timeout=3";

            lblDB.Text = "Connecting...";
            lblDB.ForeColor = Color.Black;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        lblDB.Text = $"✅ Connected to database 'EXPARCS' successfully.";
                        lblDB.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDB.Text = $"❌ Could not connect to database.";
                        lblDB.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblDB.Text = $"❌ Error: {ex.Message}";
                lblDB.ForeColor = Color.Red;
            }
        }
    }
}

using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace NetworkCheckApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            string ipToPing = "61.5.152.140";
            lblStatus.Text = "Checking...";

            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipToPing, 2000); // 2-second timeout

                    if (reply.Status == IPStatus.Success)
                    {
                        lblStatus.Text = $"✅ Server {ipToPing} is reachable.";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = $"❌ Server {ipToPing} is not reachable.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"❌ Error: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnCheckDb_Click(object sender, EventArgs e)
        {
            string serverIp = "61.5.152.140";
            string sqlInstance = "EXP-ARCH"; // Named instance
            string database = "EXPARCS";
            string username = "NcUser"; // use SQL Auth (replace)
            string password = "New#Contact_DB_user_2003"; // replace with actual password

            // Build the connection string for SQL Server named instance
            string connectionString = $"Data Source={serverIp}\\{sqlInstance};Initial Catalog={database};User ID={username};Password={password};Connect Timeout=3";

            lblDbStatus.Text = "Connecting...";
            lblDbStatus.ForeColor = System.Drawing.Color.Black;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        lblDbStatus.Text = $"✅ Connected to database '{database}' successfully.";
                        lblDbStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblDbStatus.Text = $"❌ Could not connect to database.";
                        lblDbStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblDbStatus.Text = $"❌ Error: {ex.Message}";
                lblDbStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;

namespace NetworkCheckApp
{
    public partial class Form1 : Form
    {
        // ✅ Cache for fast lookups
        private readonly Dictionary<string, string> fileIndexCache = new(StringComparer.OrdinalIgnoreCase);
        private readonly string networkRoot = @"\\172.18.11.9\vfs";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            string serverIp = "61.5.152.140";
            string sqlInstance = "EXP-ARCH"; // Named instance
            string database = "EXPARCS";
            string username = "NcUser"; // use SQL Auth (replace)
            string password = "New#Contact_DB_user_2003"; // replace with actual password

            // Build the connection string for SQL Server named instance
            string connectionString = $"Data Source={sqlInstance};Initial Catalog={database};User ID={username};Password={password};Connect Timeout=3";

            lblDB.Text = "Connecting...";
            lblDB.ForeColor = System.Drawing.Color.Black;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        lblDB.Text = $"✅ Connected to database '{database}' successfully.";
                        lblDB.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblDB.Text = $"❌ Could not connect to database.";
                        lblDB.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblDB.Text = $"❌ Error: {ex.Message}";
                lblDB.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnchecknetwork_Click(object sender, EventArgs e)
        {
            string networkPath = @"\\172.18.11.9\vfs";

            lblNetworkStatus.Text = "Checking access...";
            lblNetworkStatus.ForeColor = Color.Black;

            try
            {
                // Check network availability using Directory.Exists
                if (System.IO.Directory.Exists(networkPath))
                {
                    lblNetworkStatus.Text = $"✅ Network share is reachable: {networkPath}";
                    lblNetworkStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblNetworkStatus.Text = $"❌ Network share not reachable or access denied: {networkPath}";
                    lblNetworkStatus.ForeColor = Color.Red;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                lblNetworkStatus.Text = $"❌ Access denied to {networkPath}. {ex.Message}";
                lblNetworkStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                lblNetworkStatus.Text = $"❌ Error checking network share: {ex.Message}";
                lblNetworkStatus.ForeColor = Color.Red;
            }
        }

        private void btnCheckFile_Click(object sender, EventArgs e)
        {
            // This is the UNC path to the shared file (mapped from Z:\)
            string filePath = @"\\172.18.11.9\vfs\NEWS\Chaupal-NEWS\INT\2025\Package\October\12-10-2025\121672363-01-PKG_INT_MIDDLE_EAST_PEACE_DEAL_PUNJABI_12102025.avi";

            lblFileStatus.Text = "Checking file...";
            lblFileStatus.ForeColor = Color.Black;

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    lblFileStatus.Text = $"✅ File exists and is reachable:\n{Path.GetFileName(filePath)}";
                    lblFileStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblFileStatus.Text = $"❌ File does NOT exist or is inaccessible:\n{Path.GetFileName(filePath)}";
                    lblFileStatus.ForeColor = Color.Red;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                lblFileStatus.Text = $"❌ Access denied to file.\n{ex.Message}";
                lblFileStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                lblFileStatus.Text = $"❌ Error checking file:\n{ex.Message}";
                lblFileStatus.ForeColor = Color.Red;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(txtArchiveId.Text.Trim(), out long archiveId))
            {
                lblStatus.Text = "❌ Invalid ArchiveID.";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            // Step 1: Get relative path from stored procedure
            string relativePath = GetRelativeVideoPathFromProc(archiveId);
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                lblStatus.Text = $"❌ No file found for ArchiveID {archiveId}.";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            // Step 2: Combine with UNC root path
            string fullPath = Path.Combine(@"\\172.18.11.9\VFS", relativePath);

            lblStatus.Text = $"🔍 Checking file at: {fullPath}";
            lblStatus.ForeColor = Color.Black;

            if (File.Exists(fullPath))
            {
                lblStatus.Text = $"✅ File found. Playing video...";
                lblStatus.ForeColor = Color.Green;

                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = fullPath,
                        UseShellExecute = true
                    };

                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"❌ Could not open video.\n{ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                lblStatus.Text = $"❌ File not found at path: {fullPath}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private string GetRelativeVideoPathFromProc(long archiveId)
        {
            try
            {
                string connStr = "Data Source=EXP-ARCH;Initial Catalog=EXPARCS;User ID=NcUser;Password=New#Contact_DB_user_2003;";
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("uspT_VideoSearching", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_ArchiveID", archiveId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader["FileName"]?.ToString(); // this now contains relative path
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error fetching file path: {ex.Message}");
            }
            return null;
        }

       
        private string NormalizeKey(string input)
        {
            return input.Replace(" ", "")
                        .Replace("-", "")
                        .Replace("_", "")
                        .ToLowerInvariant();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string jsonPath = "IndexCache.json";
                if (File.Exists(jsonPath))
                {
                    var content = File.ReadAllText(jsonPath);
                    var loadedIndex = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
                    if (loadedIndex != null)
                    {
                        foreach (var kvp in loadedIndex)
                            fileIndexCache[kvp.Key] = kvp.Value;

                        lblStatus.Text = $"✅ Loaded {fileIndexCache.Count} entries from cache.";
                        lblStatus.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"❌ Failed to load cache: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void btnBuildIndex_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Building index...";
            lblStatus.ForeColor = Color.Black;

            try
            {
                fileIndexCache.Clear();
                string[] allowedExtensions = { ".avi", ".mp4", ".mov", ".wmv" };

                foreach (var file in Directory.EnumerateFiles(networkRoot, "*.*", SearchOption.AllDirectories))
                {
                    string ext = Path.GetExtension(file);
                    if (allowedExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        // normalize the key the same way we normalize lookup keys
                        string nameKey = NormalizeKey(Path.GetFileNameWithoutExtension(file));

                        if (!fileIndexCache.ContainsKey(nameKey))
                            fileIndexCache[nameKey] = file;
                    }
                }

                // --- DEBUG DUMP: write index keys to disk for inspection ---
                try
                {
                    var dumpPath = Path.Combine(Application.StartupPath, "RebuiltIndex.txt");
                    File.WriteAllLines(dumpPath, fileIndexCache
                        .Select(kvp => $"{kvp.Key} => {kvp.Value}"));
                    // optional: show location in UI for convenience
                    lblStatus.Text = $"✅ Index built successfully. {fileIndexCache.Count} files indexed. Dump: {dumpPath}";
                }
                catch (Exception exDump)
                {
                    // if dumping fails, still continue but report it
                    lblStatus.Text = $"✅ Index built ({fileIndexCache.Count}). Dump failed: {exDump.Message}";
                }

                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"❌ Error building index: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }
    }
}

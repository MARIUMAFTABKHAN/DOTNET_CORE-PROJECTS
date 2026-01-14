using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Altodownloading
{
    public partial class btnPicTargetPath : Form
    {
        JTSEntities dbJTS = new JTSEntities();
        EXPARCSEntities db = new EXPARCSEntities();
        //Boolean LowRD = false;
        bool IsLoad = false;
        public btnPicTargetPath()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Helper.UserFullName;
            lblDateTime.Text = Helper.GetDefaultDateTime().ToShortDateString();
            this.Text = "AMS-V-" + Altodownloading.BAL.GlobalClass.FileVersion;
            dpFromDate.Value = DateTime.Now;
            dpToDate.Value = DateTime.Now;
            chkDates.Checked = false;
            BindControl();

            txtEntTargetPath.Text = Helper.GetDownloadsPath();
            txtNewsTargetPath.Text = Helper.GetDownloadsPath();
            txtPicTargetPath.Text = Helper.GetDownloadsPath(); ;// txtNewsTargetPath.Text;
            tabControl1.SelectedIndex = 1;
            chkPicSet.Checked = false;
        }

        private void BindControl()
        {
            bindnewsshorttitle();
            bindcmbChannel();
            CreateProgramList();
            LoadDepartment();
            cmbDepartment_SelectedIndexChanged(null, null);

            CreateNewsList();
            CreateComList();
            CreatePicist();
            bindNewsCategory();
            bindNewsSource();
            bindNewsBureau();
            bindNewsFootage();
            bindNewsReporter();
            //bindNewsReporter();
            bindComCategory();

            bindkeywords();


            //bindComCategory();
            //bindComBureau();
            // bindcmbProducer();

        }


        private void bindnewsshorttitle()
        {
            // var s = db.SUProgramTitleOrNames.Where(x => x.ProgramTitleShortCode != null).ToList().Distinct();

            var k = (from u in db.SUProgramTitleOrNames.Where(x => x.ProgramTitleShortCode != null)
                     select new { u.ProgramTitleShortCode }).Distinct().ToList();

            DataTable dt = Helper.ToDataTable(k);
            DataRow dr = dt.NewRow();
            dr[0] = "-- Please Select --";
            //// dr["RoleId"] = "31";
            dt.Rows.InsertAt(dr, 0);
            cmbEnterShortTitle.DisplayMember = "ProgramTitleShortCode";
            // cmbNewsKeyword.ValueMember = "KeyWords";
            cmbEnterShortTitle.DataSource = dt;
        }
        private void bindkeywords()
        {
            var k = (from u in db.SUKeyWordDetails
                     select new { u.KeyWords }).Distinct().ToList();

            DataTable dt = Helper.ToDataTable(k);
            DataRow dr = dt.NewRow();
            dr[0] = "-- Please Select --";
            //// dr["RoleId"] = "31";
            dt.Rows.InsertAt(dr, 0);


            cmbNewsKeyword.DisplayMember = "KeyWords";
            //cmbNewsKeyword.ValueMember = "KeyWords";
            cmbNewsKeyword.DataSource = dt;

            cmbPicKeywords.DisplayMember = "KeyWords";
            // cmbPicKeywords.ValueMember = "KeyWords";
            cmbPicKeywords.DataSource = dt;

            cmbComKeywords.DisplayMember = "KeyWords";
            //cmbComKeywords.ValueMember = "KeyWords";
            cmbComKeywords.DataSource = dt;



        }
        private void LoadDepartment()
        {
            IsLoad = false;
            var d = dbJTS.tblDepartments.Where(x => x.IsActive == true).ToList();
            DataTable dt = Helper.ToDataTable(d);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "-- Please Select --";
            //// dr["RoleId"] = "31";
            dt.Rows.InsertAt(dr, 0);
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.DisplayMember = "Department";
            cmbDepartment.DataSource = dt;
            cmbDepartment.SelectedIndex = 0;
            IsLoad = true;
        }

        private void bindcmbProducer()
        {
            //if (IsLoad == true)
            {
                Int32 departmentid = Convert.ToInt32(cmbDepartment.SelectedValue);
                //DataTable dt = (new JTSDB()).GetUserByDepartmentId(cmbDepartment.SelectedValue, true);
                var d = dbJTS.usp_GetUserByDepartmentId(departmentid, true).ToList();
                DataTable dt = Helper.ToDataTable(d.ToList());
                DataRow dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = "-- Please Select --";
                dt.Rows.InsertAt(dr, 0);

                cmbProducer.DisplayMember = "FullName";
                cmbProducer.ValueMember = "UserID";
                cmbProducer.DataSource = dt;////dv;
                //cmbReporter.Items.Insert(0, "-- Please Select --");
                cmbProducer.SelectedIndex = 0;
                IsLoad = false;
            }
        }


        private void bindcmbChannel()
        {
            DataTable dt = (new ChannelDB()).GetAllChannel(null, null, true, null, null, null, null);
            DataRow dr = dt.NewRow();
            dr["ChannelID"] = 0;
            dr["Name"] = "-- Please Select --";
            dt.Rows.InsertAt(dr, 0);
            cmbChannel.DataSource = dt;
            cmbChannel.DisplayMember = "Name";
            cmbChannel.ValueMember = "ChannelID";
            cmbChannel.SelectedIndex = 0;
        }
        private void CreateProgramList()
        {
            lstProgramDetails.Items.Clear();
            ColumnHeader header1, header2, header3, header4, header5, header6, header7, header8, header9, header10, header11, header12, header13, header14, header15, header16, header17;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();
            header7 = new ColumnHeader();
            header8 = new ColumnHeader();
            header9 = new ColumnHeader();
            header10 = new ColumnHeader();
            header11 = new ColumnHeader();
            header12 = new ColumnHeader();
            header13 = new ColumnHeader();
            header14 = new ColumnHeader();
            //header15 = new ColumnHeader();
            //header16 = new ColumnHeader();
            //header17 = new ColumnHeader();
            // Set the text, alignment and width for each column header.
            header1.Text = "#.";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 50;

            header2.Text = "ArchiveID.";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 80;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Program Name";
            header3.Width = 110;

            header4.TextAlign = HorizontalAlignment.Left;
            header4.Text = "Sub Title";
            header4.Width = 210;

            header5.TextAlign = HorizontalAlignment.Left;
            header5.Text = "Topic";
            header5.Width = 200;

            header6.Text = "EP.#.";
            header6.TextAlign = HorizontalAlignment.Left;
            header6.Width = 40;

            header7.TextAlign = HorizontalAlignment.Left;
            header7.Text = "Part";
            header7.Width = 40;

            //header6.TextAlign = HorizontalAlignment.Left;
            //header6.Text = "ShootDate";
            //header6.Width = 75;

            header8.Text = "TeleDate";
            header8.TextAlign = HorizontalAlignment.Left;
            header8.Width = 75;

            header9.TextAlign = HorizontalAlignment.Left;
            header9.Text = "Producer";
            header9.Width = 90;

            header10.TextAlign = HorizontalAlignment.Left;
            header10.Text = "Master";
            header10.Width = 60;

            //header10.Text = "OnSite";
            //header10.TextAlign = HorizontalAlignment.Left;
            //header10.Width = 50;

            //header11.TextAlign = HorizontalAlignment.Left;
            //header11.Text = "OffSite";
            //header11.Width = 50;

            //header12.TextAlign = HorizontalAlignment.Left;
            //header12.Text = "PG.#";
            //header12.Width = 45;

            header11.Text = "M-Type";
            header11.TextAlign = HorizontalAlignment.Left;
            header11.Width = 60;

            //header14.TextAlign = HorizontalAlignment.Left;
            //header14.Text = "Seechange";
            //header14.Width = 70;

            header12.TextAlign = HorizontalAlignment.Left;
            header12.Text = "Low";
            header12.Width = 40;

            header13.TextAlign = HorizontalAlignment.Left;
            header13.Text = "High";
            header13.Width = 40;

            header14.TextAlign = HorizontalAlignment.Left;
            header14.Text = "File Name";
            header14.Width = -2;

            lstProgramDetails.Columns.Add(header1);
            lstProgramDetails.Columns.Add(header2);
            lstProgramDetails.Columns.Add(header3);
            lstProgramDetails.Columns.Add(header4);
            lstProgramDetails.Columns.Add(header5);
            lstProgramDetails.Columns.Add(header6);
            lstProgramDetails.Columns.Add(header7);
            lstProgramDetails.Columns.Add(header8);
            lstProgramDetails.Columns.Add(header9);
            lstProgramDetails.Columns.Add(header10);
            lstProgramDetails.Columns.Add(header11);
            lstProgramDetails.Columns.Add(header12);
            lstProgramDetails.Columns.Add(header13);
            lstProgramDetails.Columns.Add(header14);
            //lstProgramDetails.Columns.Add(header15);
            //lstProgramDetails.Columns.Add(header16);
            //lstProgramDetails.Columns.Add(header17);
        }

        private void chkDates_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDates.Checked)
            {
                dpFromDate.Enabled = false;
                dpToDate.Enabled = false;
            }
            else
            {
                dpFromDate.Enabled = true;
                dpToDate.Enabled = true;
            }

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstProgramDetails.Items.Clear();
            // lbltotal.BackColor = Color.Red;
            lbltotal.ForeColor = Color.Red;
            lbltotal.Text = "Working ! please wait";
            lbltotal.Refresh();
            //lbltotal.BackColor = Color.Blue;
            lbltotal.ForeColor = Color.Blue;

            //LowRD = false;
            btnSearch.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            if (cmbEnterShortTitle.Text.Length == 0 && txtTopic.Text.Length == 0 && txtArchiveID.Text.Length == 0 && txtProgramName.Text.Length == 0 && txtEpisodeNo.Text.Length == 0 &&
               txtDetails.Text.Length == 0 && cmbChannel.SelectedIndex == 0 && cmbDepartment.SelectedIndex == 0 && cmbProducer.SelectedIndex == 0 && chkDates.Checked == false)
            {
                MessageBox.Show("Please provide atleast single criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSearch.Enabled = true;
                Cursor.Current = Cursors.Default;
                return;
            }
            progressBar1.Value = 0;
            Int32 i = 1;
            DataTable dt = EntertainmentSearch();
            lbltotal.Text = "Record Found : " + dt.Rows.Count.ToString();
            progressBar1.Maximum = dt.Rows.Count;
            lstProgramDetails.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(i.ToString());
                lvi.UseItemStyleForSubItems = true;
                lvi.SubItems.Add(row["ArchiveId"].ToString());
                lvi.Tag = row["FileName"].ToString();
                lvi.SubItems.Add(row["ProgramTitle"].ToString());
                lvi.SubItems.Add(row["Caption"].ToString());
                lvi.SubItems.Add(row["Detail"].ToString());
                // lvi.SubItems.Add(row["Caption"].ToString());              
                lvi.SubItems.Add(row["EpisodeNo"].ToString());
                lvi.SubItems.Add(row["PartNo"].ToString());
                //  lvi.SubItems.Add(row["ShootDate"].ToString());
                lvi.SubItems.Add(row["TDate"].ToString());
                lvi.SubItems.Add(row["Producer"].ToString());
                lvi.SubItems.Add(row["MediaNo"].ToString());
                lvi.SubItems.Add(row["MediaType"].ToString());

                lvi.SubItems.Add(row["IsLowClip"].ToString());
                lvi.SubItems.Add(row["IsHighClip"].ToString());
                lvi.SubItems.Add(row["FileName"].ToString());
                // lvi.. .en. SubItems[0]. .en . .en.Checked = false;
                //if (Convert.ToBoolean(row["IsLowClip"]) == true)
                //{

                //    lowResolutionToolStripMenuItem.Enabled = true;

                //}
                //else
                //{
                //      lowResolutionToolStripMenuItem.Enabled = false;
                //}
                //if (Convert.ToBoolean(row["IsHighClip"]) == true)
                //{

                //     highResolutionToolStripMenuItem.Enabled = true;

                //}
                //else
                //{
                //     highResolutionToolStripMenuItem.Enabled = false;
                //}
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems[0].BackColor = Color.LightSkyBlue;
                //  highResolutionToolStripMenuItem.Enabled = false;
                //   lowResolutionToolStripMenuItem.Enabled = false;

                if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                {

                    //  highResolutionToolStripMenuItem.Enabled = true;
                    //  lowResolutionToolStripMenuItem.Enabled = true;


                    // lowResToolStripMenuItem.Enabled = true;
                    lvi.SubItems[11].BackColor = Color.Blue;
                    lvi.SubItems[11].ForeColor = Color.White;
                    lvi.SubItems[12].BackColor = Color.Green;
                    lvi.SubItems[12].ForeColor = Color.White;
                }
                else if ((Convert.ToBoolean(row["IsHighClip"]) == false) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                {
                    // lvi.UseItemStyleForSubItems = false;
                    //highResolutionToolStripMenuItem.Enabled = false;
                    //   lowResolutionToolStripMenuItem.Enabled = true;

                    // lvi.UseItemStyleForSubItems = false;
                    // lowResToolStripMenuItem.Enabled = true;
                    lvi.SubItems[11].BackColor = Color.Blue;
                    lvi.SubItems[11].ForeColor = Color.White;

                }
                if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == false))
                {
                    //   lvi.UseItemStyleForSubItems = false;
                    //   highResolutionToolStripMenuItem.Enabled = true;
                    // lowResolutionToolStripMenuItem.Enabled = false;                    
                    // lowResToolStripMenuItem.Enabled = true;
                    lvi.SubItems[12].BackColor = Color.Green;
                    lvi.SubItems[12].ForeColor = Color.White;

                    // lowResToolStripMenuItem.Enabled = true;                   

                }
                lstProgramDetails.Items.Add(lvi);
                progressBar1.Value = i++;
            }
            btnSearch.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindcmbProducer();
        }
        private DataTable EntertainmentSearch()
        {
            DataTable dt = null;
            try
            {
                DataTable dtSearchResult;

                long ArID = 0;
                string telefrom = "";
                string teleto = "";
                Boolean? isHeigh;
                if (chkDates.Checked)
                {
                    telefrom = dpFromDate.Value.ToShortDateString();
                    teleto = dpToDate.Value.ToShortDateString();
                }
                if (chkhighres.Checked)
                    isHeigh = true;
                else
                    isHeigh = null;
                txtDetails.Text = txtDetails.Text.Replace("%", " ");
                txtDetails.Text = txtDetails.Text.Replace("  ", " ");
                String[] arrSearch = txtDetails.Text.Trim().Split(' ');
                String strDetail = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strDetail += arrSearch[iIndex] + "%";
                    }
                }
                if (strDetail.EndsWith("%"))
                {
                    strDetail = strDetail.Substring(0, strDetail.LastIndexOf("%"));
                }
                string shorttitlecode = "";
                if (cmbEnterShortTitle.SelectedIndex > 0)
                    shorttitlecode = cmbEnterShortTitle.Text.Trim();
                dt = (new Altodownloading.ArchiveDB()).GetProgramSearch(
                    txtArchiveID.Text.Replace("_", "").Trim(),
                    cmbChannel.SelectedValue,
                    null,
                    cmbProducer.SelectedValue,
                    isHeigh,
                    txtProgramName.Text.Trim(),
                    "",
                   shorttitlecode,
                    "",
                    "",
                    "",
                    cmbDepartment.SelectedValue,
                    strDetail.Trim(),
                    txtEpisodeNo.Text.Replace("_", "").Trim(),
                    "",
                    null,
                    "",
                    "",
                    "",
                    telefrom,
                    teleto,
                    "");

            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ep.Clear();
            try
            {
                DirectoryInfo inf = new DirectoryInfo(txtEntTargetPath.Text);
                if (!inf.Exists)
                {
                    System.IO.Directory.CreateDirectory(inf.FullName);
                }               
                Downloadfiles();
            }
            catch (Exception ex)
            {
                ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                return;
            }
            btnDownload.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        private void Downloadfiles()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["targetip"];
            string file2 = "";
            Int32 ID = 0;
            // progressBar1.Value = 0;
            foreach (ListViewItem row in lstProgramDetails.Items)
            {
                try
                {
                    bool isSelected = Convert.ToBoolean(row.Checked);
                    if (isSelected)
                    {
                        ID = Convert.ToInt32(row.SubItems[1].Text);
                        var s = db.ArchiveFileDetails.Where(x => x.ArchiveID == ID).ToList();
                        foreach (var ss in s)
                        {
                            if (s != null)
                            {
                                string MediaType = row.SubItems[10].Text;
                                if (MediaType.ToUpper() == "ALTO")
                                {
                                    string pathAndFilename = (path + "\\" + ss.FileFullPath);
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
                                    System.IO.FileInfo filetarget;
                                    //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
                                    System.IO.DirectoryInfo dirinfo;
                                    try
                                    {

                                        if (fileInfo.Exists)
                                        {
                                            WebClient wc = new WebClient();
                                            string targetpath;
                                            string[] str = fileInfo.DirectoryName.Split('\\');
                                            string strtxt = "";
                                            strtxt = str[str.Count() - 1];
                                            targetpath = txtEntTargetPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;

                                            filetarget = new FileInfo(targetpath);
                                            if (!filetarget.Exists)
                                                System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

                                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                                            wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
                                        }
                                        else
                                        {
                                            throw new Exception("File not found");
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        //private void Downloadfiles()
        //{
        //    string path = System.Configuration.ConfigurationManager.AppSettings["targetip"];
        //    string file2 = "";
        //    Int32 ID = 0;
        //    // progressBar1.Value = 0;
        //    foreach (ListViewItem row in lstProgramDetails.Items)
        //    {
        //        try
        //        {
        //            bool isSelected = Convert.ToBoolean(row.Checked);
        //            if (isSelected)
        //            {
        //                //ID = Convert.ToInt32(row.SubItems[0].Text);
        //                string MediaType = row.SubItems[12].Text;
        //                if (MediaType.ToUpper() == "ALTO")
        //                {
        //                    string pathAndFilename = (path + "\\" + row.SubItems[16].Text);
        //                    MessageBox.Show(pathAndFilename);
        //                    Console.Write(pathAndFilename);
        //                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
        //                    System.IO.FileInfo filetarget;
        //                    //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
        //                    System.IO.DirectoryInfo dirinfo;
        //                    try
        //                    {

        //                        if (fileInfo.Exists)
        //                        {
        //                            WebClient wc = new WebClient();
        //                            string targetpath;
        //                            string[] str = fileInfo.DirectoryName.Split('\\');
        //                            string strtxt = "";
        //                            strtxt = str[str.Count() - 1];
        //                            targetpath = txtEntTargetPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;
        //                            filetarget = new FileInfo(targetpath);
        //                            if (!filetarget.Exists)
        //                                System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

        //                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
        //                            wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
        //                        }
        //                        else
        //                        {

        //                            MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            return;
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                    finally
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}
        public void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                progressBar2.Value = e.ProgressPercentage;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                PBNewsDownload.Value = e.ProgressPercentage;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                PBPicDownloading.Value = e.ProgressPercentage;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                PBComDownloadBar.Value = e.ProgressPercentage;
            }

        }
        private void btnOpenDialouge_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {

                fbd.SelectedPath = Helper.GetDownloadsPath();// Environment.SpecialFolder.MyDocuments;
                // fbd.SelectedPath = 
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtEntTargetPath.Text = fbd.SelectedPath;
                }
            }
        }
        private void Downloadfiles(string path)
        {
            string FileName = "";

            if (tabControl1.SelectedIndex == 0)
            {
                FileName = lstProgramDetails.SelectedItems[0].SubItems[13].Text;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                FileName = lstNews.SelectedItems[0].SubItems[12].Text;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                FileName = lstPicList.SelectedItems[0].SubItems[12].Text;
            }

            string pathAndFilename = (path + "\\" + FileName);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
            System.IO.FileInfo filetarget;
            //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
            System.IO.DirectoryInfo dirinfo;
            try
            {

                if (fileInfo.Exists)
                {
                    WebClient wc = new WebClient();
                    string targetpath;
                    string[] str = fileInfo.DirectoryName.Split('\\');
                    string strtxt = "";
                    strtxt = str[str.Count() - 1];
                    targetpath = txtEntTargetPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;

                    filetarget = new FileInfo(targetpath);
                    if (!filetarget.Exists)
                        System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
                }
                else
                {
                    MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            finally
            {

            }
            //   \\172.18.11.3\AMS_Lowres
            //  \\172.18.11.3\AMS_Highres
        }
        private void Playfile(string pathAndFilename)
        {

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
            try
            {
                if (fileInfo.Exists)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        wmp.URL = fileInfo.FullName;
                        WMPLib.IWMPMedia m = wmp.newMedia(wmp.URL);
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {
                        WMPNews.URL = fileInfo.FullName;
                        WMPLib.IWMPMedia m = WMPNews.newMedia(WMPNews.URL);
                    }
                    else if (tabControl1.SelectedIndex == 2)
                    {
                        WMPPicture.URL = fileInfo.FullName;
                        WMPLib.IWMPMedia m = WMPPicture.newMedia(WMPPicture.URL);
                    }
                    else if (tabControl1.SelectedIndex == 3)
                    {
                        WMPCom.URL = fileInfo.FullName;
                        WMPLib.IWMPMedia m = WMPCom.newMedia(WMPCom.URL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }


        private void btnOpenTargetENT_Click(object sender, EventArgs e)
        {
            OFDENT.Title = "Select File to Upload";

            if (Directory.Exists(txtEntTargetPath.Text))
            {
                OFDENT.InitialDirectory = txtEntTargetPath.Text;
            }
            else
            {
                OFDENT.InitialDirectory = @"C:\";
            }
            if (OFDENT.ShowDialog() == DialogResult.OK)
            {
                OFDENT.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                OFDENT.FilterIndex = 1;

            }
            //OFDENT.RestoreDirectory = true;  
        }

        private void lowResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // LowRD = true;
            // News
            if (tabControl1.SelectedIndex == 0)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Lowres"];
                string FileName = lstProgramDetails.SelectedItems[0].SubItems[13].Text;
                string pathAndFilename = (path + "\\" + FileName);
                //nDownload.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                ep.Clear();
                try
                {
                    FileInfo inf = new FileInfo(pathAndFilename);
                    if (inf.Exists)
                        Playfile(pathAndFilename);
                    else
                    {
                        MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //    ep.SetError(txtEntTargetPath, "Directory not exits");
                }
                catch (Exception ex)
                {
                    //ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                    return;
                }
                ///nDownload.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Lowres"];
                string FileName = lstNews.SelectedItems[0].SubItems[12].Text;
                string pathAndFilename = (path + "\\" + FileName);
                //nDownload.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                ep.Clear();
                try
                {
                    FileInfo inf = new FileInfo(pathAndFilename);
                    if (inf.Exists)
                        Playfile(pathAndFilename);
                    else
                    {
                        MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //    ep.SetError(txtEntTargetPath, "Directory not exits");
                }
                catch (Exception ex)
                {
                    //ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                    return;
                }
                ///nDownload.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Lowres"];
                string FileName = lstPicList.SelectedItems[0].SubItems[12].Text;
                string pathAndFilename = (path + "\\" + FileName);
                //nDownload.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                ep.Clear();
                try
                {
                    FileInfo inf = new FileInfo(pathAndFilename);
                    if (inf.Exists)
                        Playfile(pathAndFilename);
                    else
                    {
                        MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //    ep.SetError(txtEntTargetPath, "Directory not exits");
                }
                catch (Exception ex)
                {
                    //ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                    return;
                }
                ///nDownload.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Lowres"];
                string FileName = lstComList.SelectedItems[0].SubItems[12].Text;
                string pathAndFilename = (path + "\\" + FileName);
                //nDownload.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                ep.Clear();
                try
                {
                    FileInfo inf = new FileInfo(pathAndFilename);
                    if (inf.Exists)
                        Playfile(pathAndFilename);
                    else
                    {
                        MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //    ep.SetError(txtEntTargetPath, "Directory not exits");
                }
                catch (Exception ex)
                {
                    //ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                    return;
                }
                ///nDownload.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void highResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ArchiveID = 0;
            string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Highres"];

            if (tabControl1.SelectedIndex == 0)
            {
                ArchiveID = Convert.ToInt32(lstProgramDetails.SelectedItems[0].SubItems[1].Text);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                ArchiveID = Convert.ToInt32(lstNews.SelectedItems[0].SubItems[1].Text);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                ArchiveID = Convert.ToInt32(lstPicList.SelectedItems[0].SubItems[1].Text);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                ArchiveID = Convert.ToInt32(lstComList.SelectedItems[0].SubItems[1].Text);
            }
            Downloadfiles(path);
        }

        private void lstProgramDetails_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    if (lstProgramDetails.SelectedItems[0].SubItems[11].Text == "True")
            //    {

            //    }
            //}
        }

        private void txtShortTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtProgramName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtTopic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtEpisodeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void txtArchiveID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }
        #region News
        private void cmbNewsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(cmbNewsCategory.SelectedValue);
            var lst = (from u in dbJTS.ChannelCategoryDetails.Where(x => x.CategoryID == ID && x.IsActive == true)
                       select new { u.ChannelID, u.tblChannel.Name, u.tblChannel.orderbyID }).OrderBy(x => x.orderbyID).ToList();
            DataTable dt = Helper.ToDataTable(lst);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Please Select";
            dt.Rows.InsertAt(dr, 0);

            cmbNewsChannel.DisplayMember = "Name";
            cmbNewsChannel.ValueMember = "ChannelID";
            cmbNewsChannel.DataSource = dt;
            cmbNewsChannel.SelectedIndex = 0;
        }
        private void bindNewsCategory()
        {
            var c = dbJTS.ChannelCategories.Where(x => x.IsActive == true).ToList();
            cmbNewsCategory.ValueMember = "ID";
            cmbNewsCategory.DisplayMember = "CatagoryName";
            cmbNewsCategory.DataSource = c;
            int idx = cmbNewsCategory.FindString("News");
            cmbNewsCategory.SelectedIndex = idx;

            cmbNewsCategory_SelectedIndexChanged(null, null);
            cmbNewsCategory.Enabled = false;
            cmbPicCategory.ValueMember = "ID";
            cmbPicCategory.DisplayMember = "CatagoryName";
            cmbPicCategory.DataSource = c;
            cmbPicCategory_SelectedIndexChanged(null, null);
        }
        private void bindComCategory()
        {
            var lst = (from u in dbJTS.ChannelCategoryDetails.Where(x => x.IsActive == true)
                       select new { u.ChannelID, u.tblChannel.Name, u.tblChannel.orderbyID }).Distinct().OrderBy(x => x.orderbyID).ToList();
            DataTable dt = Helper.ToDataTable(lst);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Please Select";
            dt.Rows.InsertAt(dr, 0);

            cmbComChannel.DisplayMember = "Name";
            cmbComChannel.ValueMember = "ChannelID";
            cmbComChannel.DataSource = dt;
            cmbComChannel.SelectedIndex = 0;




        }
        private void bindNewsSource()
        {
            var c = db.SUSources.ToList();
            DataTable dt = Helper.ToDataTable(c);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Please Select";
            dt.Rows.InsertAt(dr, 0);

            cmbNewsSource.ValueMember = "SourceID";
            cmbNewsSource.DisplayMember = "Source";
            cmbNewsSource.DataSource = dt;
            cmbNewsSource.SelectedIndex = 0;

            cmbPicSource.ValueMember = "SourceID";
            cmbPicSource.DisplayMember = "Source";
            cmbPicSource.DataSource = dt;
            cmbPicSource.SelectedIndex = 0;


            cmbPicSource.ValueMember = "SourceID";
            cmbPicSource.DisplayMember = "Source";
            cmbPicSource.DataSource = dt;
            cmbPicSource.SelectedIndex = 0;



        }
        private void bindNewsBureau()
        {
            var c = dbJTS.tblBureaux.ToList();
            DataTable dt = Helper.ToDataTable(c);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Please Select";
            dt.Rows.InsertAt(dr, 0);



            cmbNewsBureau.ValueMember = "BureauID";
            cmbNewsBureau.DisplayMember = "Name";
            cmbNewsBureau.DataSource = dt;
            cmbNewsBureau.SelectedIndex = 0;


            cmbComBureau.ValueMember = "BureauID";
            cmbComBureau.DisplayMember = "Name";
            cmbComBureau.DataSource = dt;
            cmbComBureau.SelectedIndex = 0;

            //cmbPicBureau.ValueMember = "BureauID";
            //cmbPicBureau.DisplayMember = "Name";
            //cmbPicBureau.DataSource = dt;
            //cmbPicBureau.SelectedIndex = 0;




        }
        private void bindNewsFootage()
        {
            DataTable dt = (new SUFootageTypeDB()).GetAllFootageType(null, null, null, null, null, null);

            DataView dv = dt.DefaultView;
            dv.RowFilter = "FootageType <> 'Image' AND FootageType <> 'Map'";
            dv.Sort = "FootageType ASC";


            DataRow dr = dt.NewRow();
            dr["FootageTypeID"] = 0;
            dr["FootageType"] = "-- Please Select --";
            dt.Rows.Add(dr);
            //cmbFootage.DataSource = dt;
            cmbNewsfootageType.DataSource = dv;
            cmbNewsfootageType.DisplayMember = "FootageType";
            cmbNewsfootageType.ValueMember = "FootageTypeID";
            //cmbFootage.DataBind();
            //cmbFootage.Items.Insert(0, "-- Please Select --");
            cmbNewsfootageType.SelectedValue = "0";

            cmbPicImage.DataSource = dt;
            cmbPicImage.DisplayMember = "FootageType";
            cmbPicImage.ValueMember = "FootageTypeID";
            cmbPicImage.SelectedIndex = 0;

        }
        private void bindNewsReporter()
        {
            DataTable dt = (new JTSDB()).GetAllReporterFromJTS();

            DataRow dr = dt.NewRow();
            dr["UserID"] = 0;
            dr["FullName"] = "-- Please Select --";
            ////   dr["RoleId"] = "9";
            dt.Rows.Add(dr);

            ////    DataView dv = dt.DefaultView;
            ////    dv.RowFilter = "RoleId = 9 ";        

            cmbNewsReporter.DataSource = dt;////dv;
            cmbNewsReporter.DisplayMember = "FullName";
            cmbNewsReporter.ValueMember = "UserID";
            cmbNewsReporter.SelectedValue = "0";
        }
        private void bindPhotographers()
        {
            try
            {
                int id = Convert.ToInt32(cmbPicBureau.SelectedValue);
                DataTable dt = (new JTSDB()).GetAllPhotographerByBureauID(id);

                DataRow dr = dt.NewRow();
                dr["UserID"] = 0;
                dr["FullName"] = "-- Please Select --";
                ////   dr["RoleId"] = "9";
                //dt.Rows.Add(dr);

                ////    DataView dv = dt.DefaultView;
                ////    dv.RowFilter = "RoleId = 9 ";        

                cmb_PicPhotographer.DataSource = dt;////dv;
                cmb_PicPhotographer.DisplayMember = "FullName";
                cmb_PicPhotographer.ValueMember = "UserID";
                dt.Rows.InsertAt(dr, 0);
                cmb_PicPhotographer.SelectedValue = "0";
            }
            catch (Exception)
            {

            }

        }
        private void btnNewsSearch_Click(object sender, EventArgs e)
        {
            lstNews.Items.Clear();
            if (txtNewsSlug.Text.Trim() != String.Empty || txtNewsDetails.Text.Trim() != String.Empty || txtNewArchiveID.Text.Trim() != String.Empty
                   || cmbChannel.SelectedValue != "0"
                   || cmbNewsBureau.SelectedValue != "0" || cmbNewsBureau.SelectedValue != "0"
                   || cmbNewsKeyword.Text.Trim() != String.Empty
                   || cmbNewsBureau.SelectedValue != "0"
                   || dtNewsDtFrom.Text.Trim() != String.Empty || dtNewsDtTo.Text.Trim() != String.Empty
                   )
            {
                Search();
            }
        }
        protected void Search()
        {
            try
            {
                string telefrom = "";
                string teleto = "";

                if (chkNewDate.Checked)
                {
                    telefrom = dtNewsDtFrom.Value.ToShortDateString();
                    teleto = dtNewsDtTo.Value.ToShortDateString();
                }
                DataTable dtSearchResult;
                DataTable dt;
                long ArID = 0;
                Object isHighClip = null;
                Object isLowClip = null;
                Object exactwordsearch = null;
                Object NewsArchiveID = null;
                try
                {
                    if (Convert.ToInt32(txtNewArchiveID.Text) > 0)
                    {
                        NewsArchiveID = txtNewArchiveID.Text;
                    }
                }
                catch (Exception)
                {
                    NewsArchiveID = null;
                }
                if (chkNewsHighRes.Checked == true)
                    isHighClip = true;
                if (chkNewsExact.Checked == true)
                    exactwordsearch = true;
                else
                    exactwordsearch = false;
                txtNewsSlug.Text = txtNewsSlug.Text.Replace("%", " ");
                txtNewsSlug.Text = txtNewsSlug.Text.Replace("  ", " ");
                String[] arrSearch = txtNewsSlug.Text.Trim().Split(' ');
                String strSlug = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strSlug += arrSearch[iIndex] + "%";
                    }
                }
                if (strSlug.EndsWith("%"))
                {
                    strSlug = strSlug.Substring(0, strSlug.LastIndexOf("%"));
                }

                arrSearch = null;
                txtNewsDetails.Text = txtNewsDetails.Text.Replace("%", " ");
                txtNewsDetails.Text = txtNewsDetails.Text.Replace("  ", " ");
                arrSearch = txtNewsDetails.Text.Trim().Split(' ');
                String strDetail = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strDetail += arrSearch[iIndex] + "%";
                    }
                }
                if (strDetail.EndsWith("%"))
                {
                    strDetail = strDetail.Substring(0, strDetail.LastIndexOf("%"));
                }
                string stringkeyword = "";
                if (cmbNewsKeyword.SelectedIndex > 0)
                    stringkeyword = cmbNewsKeyword.Text.Trim();

                ArchiveDB obj = new ArchiveDB();
                dt = obj.GetAllQuickSearch(NewsArchiveID, null, cmbNewsChannel.SelectedValue, null, 0,
                                               0, null, null, cmbNewsSource.SelectedValue,
                                               cmbNewsfootageType.SelectedValue, null, null, null, cmbNewsReporter.SelectedValue,
                                               null, cmbNewsBureau.SelectedValue, null, null, strSlug, null, null, null, null, null, null, null,
                                               null, null, null, null, null, null, null, null, null, null, null, null, null, strDetail, null,
                                               null, null, null, null, null, null, null, null, null, isHighClip, isLowClip, null, null, null, null,
                                               null, null, null, null, null, 0, stringkeyword, exactwordsearch,
                                               telefrom, teleto
                                               );

                dtSearchResult = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt64(dr["ArchiveID"]) != ArID)
                        {
                            dtSearchResult.ImportRow(dr);
                            ArID = Convert.ToInt64(dr["ArchiveID"]);
                        }

                    }
                }
                if (dtSearchResult.Rows.Count > 0)
                {
                    lblNewTotal.ForeColor = Color.Red;
                    lblNewTotal.Text = "Working ! please wait";
                    lblNewTotal.Refresh();
                    //lbltotal.BackColor = Color.Blue;
                    lblNewTotal.ForeColor = Color.Blue;

                    //LowRD = false;
                    btnNewsSearch.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    BPNewsSearch.Value = 0;
                    Int32 i = 1;
                    //   DataTable dt = dtSearchResult;
                    lblNewTotal.Text = "Record Found : " + dtSearchResult.Rows.Count.ToString();
                    BPNewsSearch.Maximum = dtSearchResult.Rows.Count;
                    lstNews.Items.Clear();
                    foreach (DataRow row in dtSearchResult.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(i.ToString());
                        lvi.UseItemStyleForSubItems = true;
                        lvi.SubItems.Add(row["ArchiveId"].ToString());
                        lvi.Tag = row["FileName"].ToString();
                        lvi.SubItems.Add(row["Slug"].ToString());
                        lvi.SubItems.Add(row["Detail"].ToString());
                        //lvi.SubItems.Add(row["Source"].ToString());
                        lvi.SubItems.Add(row["FootageType"].ToString());
                        lvi.SubItems.Add(row["MediaType"].ToString());
                        lvi.SubItems.Add(row["ShootDate"].ToString());
                        lvi.SubItems.Add(row["Reporter"].ToString());
                        lvi.SubItems.Add(row["Bureau"].ToString());
                        lvi.SubItems.Add(row["MediaNo"].ToString());
                        //lvi.SubItems.Add(row["JTSTicketNo"].ToString());
                        lvi.SubItems.Add(row["IsLowClip"].ToString());
                        lvi.SubItems.Add(row["IsHighClip"].ToString());
                        lvi.SubItems.Add(row["FileName"].ToString());

                        lvi.UseItemStyleForSubItems = false;
                        lvi.SubItems[0].BackColor = Color.LightSkyBlue;
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;
                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;
                        }
                        else if ((Convert.ToBoolean(row["IsHighClip"]) == false) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;

                        }
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == false))
                        {

                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;

                        }
                        lstNews.Items.Add(lvi);
                        BPNewsSearch.Value = i++;
                    }
                    btnNewsSearch.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    lblNewTotal.Text = "Total Record Found : " + dtSearchResult.Rows.Count;

                }
                else
                {

                    lblNewTotal.Text = "No Results Found";
                }

            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Specified argument was out of the range of valid values"))
                {
                    lblNewTotal.Visible = true;
                }
            }
        }


        private void btnNewTarget_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {

                fbd.SelectedPath = Helper.GetDownloadsPath();// Environment.SpecialFolder.MyDocuments;
                // fbd.SelectedPath = 
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtNewsTargetPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnNewsDownload_Click(object sender, EventArgs e)
        {

            btnNewsDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ep.Clear();
            try
            {
                DirectoryInfo inf = new DirectoryInfo(txtNewsTargetPath.Text);

                 if (!inf.Exists)
                {
                    System.IO.Directory.CreateDirectory(inf.FullName);
                }                
                DownloadNewsfiles();
                               
            }
            catch (Exception ex)
            {
                ep.SetError(txtNewsTargetPath, "Invalid path or directory not exits");
                return;
            }
            btnNewsDownload.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        private void DownloadNewsfiles()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["targetip"];
            string file2 = "";
            Int32 ID = 0;
            foreach (ListViewItem row in lstNews.Items)
            {
                try
                {
                    bool isSelected = Convert.ToBoolean(row.Checked);
                    if (isSelected)
                    {
                        ID = Convert.ToInt32(row.SubItems[1].Text);
                        var s = db.ArchiveFileDetails.Where(x => x.ArchiveID == ID).ToList();
                        foreach (var ss in s)
                        {
                            if (s != null)
                            {
                                string MediaType = row.SubItems[5].Text;
                                if (MediaType.ToUpper() == "ALTO")
                                {
                                    string pathAndFilename = (path + "\\" + ss.FileFullPath);
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
                                    System.IO.FileInfo filetarget;
                                    //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
                                    System.IO.DirectoryInfo dirinfo;
                                    try
                                    {

                                        if (fileInfo.Exists)
                                        {
                                            WebClient wc = new WebClient();
                                            string targetpath;
                                            string[] str = fileInfo.DirectoryName.Split('\\');
                                            string strtxt = "";
                                            strtxt = str[str.Count() - 1];
                                            targetpath = txtNewsTargetPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;

                                            filetarget = new FileInfo(targetpath);
                                            if (!filetarget.Exists)

                                                System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

                                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                                            wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
                                        }
                                        else
                                        {
                                            throw new Exception("File not found");
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnNewsOpenTarget_Click(object sender, EventArgs e)
        {
            OFDENT.Title = "Select File to Upload";

            if (Directory.Exists(txtNewsTargetPath.Text))
            {
                OFDENT.InitialDirectory = txtNewsTargetPath.Text;
            }
            else
            {
                OFDENT.InitialDirectory = @"C:\";
            }
            if (OFDENT.ShowDialog() == DialogResult.OK)
            {
                OFDENT.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                OFDENT.FilterIndex = 1;

            }
        }
        private void CreateNewsList()
        {
            lstNews.Items.Clear();
            ColumnHeader header1, header2, header3, header4, header5, header6, header7, header8, header9, header10, header11, header12, header13, header14, header15, header16, header17;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();
            header7 = new ColumnHeader();
            header8 = new ColumnHeader();
            header9 = new ColumnHeader();
            header10 = new ColumnHeader();
            header11 = new ColumnHeader();
            header12 = new ColumnHeader();
            header13 = new ColumnHeader();
            header14 = new ColumnHeader();
            //header15 = new ColumnHeader();

            header1.Text = "#.";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 50;

            header2.Text = "ArchiveID.";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 80;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Slug";
            header3.Width = 250;


            header4.Text = "Detail";
            header4.TextAlign = HorizontalAlignment.Left;
            header4.Width = 120;

            header5.TextAlign = HorizontalAlignment.Left;
            header5.Text = "Footage Type";
            header5.Width = 100;



            //header5.TextAlign = HorizontalAlignment.Left;
            //header5.Text = "Details";
            //header5.Width = 250;


            header6.TextAlign = HorizontalAlignment.Left;
            header6.Text = "Media Type";
            header6.Width = 80;

            header7.Text = "Shoot Date";
            header7.TextAlign = HorizontalAlignment.Left;
            header7.Width = 75;

            header8.TextAlign = HorizontalAlignment.Left;
            header8.Text = "Reporter";
            header8.Width = 90;

            header9.TextAlign = HorizontalAlignment.Left;
            header9.Text = "Bureau";
            header9.Width = 80;

            header10.TextAlign = HorizontalAlignment.Left;
            header10.Text = "Media No";
            header10.Width = 80;

            //header11.Text = "JTSTicketNo";
            //header11.TextAlign = HorizontalAlignment.Left;
            //header11.Width = 60;

            header11.TextAlign = HorizontalAlignment.Left;
            header11.Text = "Low";
            header11.Width = 40;

            header12.TextAlign = HorizontalAlignment.Left;
            header12.Text = "High";
            header12.Width = 40;

            header13.TextAlign = HorizontalAlignment.Left;
            header13.Text = "File Name";
            header13.Width = -2;

            lstNews.Columns.Add(header1);
            lstNews.Columns.Add(header2);
            lstNews.Columns.Add(header3);
            lstNews.Columns.Add(header4);
            lstNews.Columns.Add(header5);
            lstNews.Columns.Add(header6);
            lstNews.Columns.Add(header7);
            lstNews.Columns.Add(header8);
            lstNews.Columns.Add(header9);
            lstNews.Columns.Add(header10);
            lstNews.Columns.Add(header11);
            lstNews.Columns.Add(header12);
            lstNews.Columns.Add(header13);
            //lstNews.Columns.Add(header14);
            //lstNews.Columns.Add(header15);
            //lstProgramDetails.Columns.Add(header16);
            //lstProgramDetails.Columns.Add(header17);
        }


        private void txtNewsSlug_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void txtNewsDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void txtNewsKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsfootageType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsReporter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsBureau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }

        private void cmbNewsSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNewsSearch_Click(null, null);
            }
        }
        #endregion

        private void chkNewDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNewDate.Checked)
            {
                dtNewsDtFrom.Enabled = false;
                dtNewsDtTo.Enabled = false;
            }
            else
            {
                dtNewsDtFrom.Enabled = true;
                dtNewsDtTo.Enabled = true;
            }
        }
        #region Commercial
        private void cmbComCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbComChannel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbComBureau_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbComClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnComSearch_Click(object sender, EventArgs e)
        {
            lstComList.Items.Clear();
            if (txtComName.Text.Trim() != String.Empty || txtComDetails.Text.Trim() != String.Empty || txtComCaption.Text.Trim() != String.Empty
                   || cmbComChannel.SelectedValue != "0" || txtComClients.Text.Trim() != String.Empty
                   || cmbComBureau.SelectedValue != "0"
                   || cmbComKeywords.Text.Trim() != String.Empty
                //|| dtComFrom.Text.Trim() != String.Empty || dtComTo.Text.Trim() != String.Empty
                   )
            {
                ComSearch();
            }
        }
        protected void ComSearch()
        {
            try
            {
                string telefrom = "";
                string teleto = "";

                if (chkComSet.Checked)
                {
                    telefrom = dtComFrom.Value.ToShortDateString();
                    teleto = dtComTo.Value.ToShortDateString();
                }
                DataTable dtSearchResult;
                DataTable dt;
                long ArID = 0;
                Object isHighClip = null;
                Object isLowClip = null;
                Object exactwordsearch = null;

                if (chkComhigh.Checked == true)
                    isHighClip = true;
                if (chkComExact.Checked == true)
                    exactwordsearch = true;
                else
                    exactwordsearch = false;
                txtComName.Text = txtComName.Text.Replace("%", " ");
                txtComCaption.Text = txtComCaption.Text.Replace("%", " ");
                cmbComKeywords.SelectedText = cmbComKeywords.SelectedText.Trim().Replace("%", " ");
                txtComClients.Text = txtComClients.Text.Replace("%", " ");

                String[] arrSearch = txtNewsSlug.Text.Trim().Split(' ');

                arrSearch = null;
                txtComDetails.Text = txtComDetails.Text.Replace("%", " ");
                txtComDetails.Text = txtComDetails.Text.Replace("  ", " ");
                arrSearch = txtComDetails.Text.Trim().Split(' ');
                String strDetail = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strDetail += arrSearch[iIndex] + "%";
                    }
                }
                if (strDetail.EndsWith("%"))
                {
                    strDetail = strDetail.Substring(0, strDetail.LastIndexOf("%"));
                }

                string stringkeyword = "";
                if (cmbComKeywords.SelectedIndex > 0)
                    stringkeyword = cmbComKeywords.Text.Trim();
                ArchiveDB obj = new ArchiveDB();
                dt = obj.GetAllQuickSearch_Comm(cmbComChannel.SelectedValue, cmbComChannel.SelectedValue, txtComName.Text, txtComCaption.Text, txtComClients.Text, txtComTag.Text, isHighClip, stringkeyword, exactwordsearch,
                                               telefrom, teleto, strDetail
                                               );


                dtSearchResult = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt64(dr["ArchiveID"]) != ArID)
                        {
                            dtSearchResult.ImportRow(dr);
                            ArID = Convert.ToInt64(dr["ArchiveID"]);
                        }

                    }
                }
                if (dtSearchResult.Rows.Count > 0)
                {
                    lblComTotal.ForeColor = Color.Red;
                    lblComTotal.Text = "Working ! please wait";
                    lblComTotal.Refresh();
                    //lbltotal.BackColor = Color.Blue;
                    lblComTotal.ForeColor = Color.Blue;

                    //LowRD = false;
                    btnComSearch.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    PBComRowsBar.Value = 0;
                    Int32 i = 1;
                    //   DataTable dt = dtSearchResult;
                    lblComTotal.Text = "Record Found : " + dtSearchResult.Rows.Count.ToString();
                    PBComRowsBar.Maximum = dtSearchResult.Rows.Count;
                    lstComList.Items.Clear();
                    foreach (DataRow row in dtSearchResult.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(i.ToString());
                        lvi.UseItemStyleForSubItems = true;
                        lvi.SubItems.Add(row["ArchiveId"].ToString());
                        lvi.Tag = row["FileName"].ToString();
                        lvi.SubItems.Add(row["Slug"].ToString());
                        lvi.SubItems.Add(row["Detail"].ToString());
                        //lvi.SubItems.Add(row["Source"].ToString());
                        lvi.SubItems.Add(row["FootageType"].ToString());
                        lvi.SubItems.Add(row["MediaType"].ToString());
                        lvi.SubItems.Add(Convert.ToDateTime(row["Shoot"]).ToString("dd/MM/yyyy"));
                        lvi.SubItems.Add(row["Reporter"].ToString());
                        lvi.SubItems.Add(row["Bureau"].ToString());
                        lvi.SubItems.Add(row["MediaNo"].ToString());
                        //lvi.SubItems.Add(row["JTSTicketNo"].ToString());
                        lvi.SubItems.Add(row["IsLowClip"].ToString());
                        lvi.SubItems.Add(row["IsHighClip"].ToString());
                        lvi.SubItems.Add(row["FileName"].ToString());

                        lvi.UseItemStyleForSubItems = false;
                        lvi.SubItems[0].BackColor = Color.LightSkyBlue;
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;
                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;
                        }
                        else if ((Convert.ToBoolean(row["IsHighClip"]) == false) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;

                        }
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == false))
                        {

                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;

                        }
                        lstComList.Items.Add(lvi);
                        PBComRowsBar.Value = i++;
                    }
                    btnComSearch.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    lblComTotal.Text = "Total Recoud Found : " + dtSearchResult.Rows.Count;

                }
                else
                {

                    lblComTotal.Text = "No Results Found";
                }

            }
            catch (Exception ex)
            {
                btnComSearch.Enabled = true;
                if (!ex.Message.Contains("Specified argument was out of the range of valid values"))
                {
                    lblComTotal.Visible = true;
                }
            }
        }

        // }
        //}

        private void chkComSet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComSet.Checked)
            {
                dtComFrom.Enabled = true;
                dtComTo.Enabled = true;
            }
            else
            {
                dtComFrom.Enabled = false;
                dtComTo.Enabled = false;
            }
        }

        private void chkComhigh_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkComExact_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnComDownload_Click(object sender, EventArgs e)
        {
            btnComDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ep.Clear();
            try
            {
                DirectoryInfo inf = new DirectoryInfo(txtComPath.Text);
                if (!inf.Exists)
                {
                    System.IO.Directory.CreateDirectory(inf.FullName);
                }              
                DownloadComfiles();
                
            }
            catch (Exception ex)
            {
                ep.SetError(txtComPath, "Invalid path or directory not exits");
                return;
            }
            btnComDownload.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnComOpenTarget_Click(object sender, EventArgs e)
        {
            OFDENT.Title = "Select File to Upload";

            if (Directory.Exists(txtComPath.Text))
            {
                OFDENT.InitialDirectory = txtComPath.Text;
            }
            else
            {
                OFDENT.InitialDirectory = @"C:\";
            }
            if (OFDENT.ShowDialog() == DialogResult.OK)
            {
                OFDENT.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                OFDENT.FilterIndex = 1;

            }
        }

        private void txtComName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnComSearch_Click(null, null);
        }

        private void txtComCaption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnComSearch_Click(null, null);
        }

        private void txtComKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnComSearch_Click(null, null);
        }

        private void txtComDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnComSearch_Click(null, null);
        }

        private void txtComTag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnComSearch_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Helper.GetDownloadsPath();// Environment.SpecialFolder.MyDocuments;
                // fbd.SelectedPath = 
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtComPath.Text = fbd.SelectedPath;
                }
            }
        }
        private void CreateComList()
        {
            lstComList.Items.Clear();
            ColumnHeader header1, header2, header3, header4, header5, header6, header7, header8, header9, header10, header11, header12, header13, header14, header15, header16, header17;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();
            header7 = new ColumnHeader();
            header8 = new ColumnHeader();
            header9 = new ColumnHeader();
            header10 = new ColumnHeader();
            header11 = new ColumnHeader();
            header12 = new ColumnHeader();
            header13 = new ColumnHeader();
            header14 = new ColumnHeader();
            //header15 = new ColumnHeader();

            header1.Text = "#.";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 50;

            header2.Text = "ArchiveID.";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 80;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Slug";
            header3.Width = 250;


            header4.Text = "Detail";
            header4.TextAlign = HorizontalAlignment.Left;
            header4.Width = 120;

            header5.TextAlign = HorizontalAlignment.Left;
            header5.Text = "Type";
            header5.Width = 100;



            //header5.TextAlign = HorizontalAlignment.Left;
            //header5.Text = "Details";
            //header5.Width = 250;


            header6.TextAlign = HorizontalAlignment.Left;
            header6.Text = "Media";
            header6.Width = 80;

            header7.Text = "Date";
            header7.TextAlign = HorizontalAlignment.Left;
            header7.Width = 75;

            header8.TextAlign = HorizontalAlignment.Left;
            header8.Text = "Reporter";
            header8.Width = 90;

            header9.TextAlign = HorizontalAlignment.Left;
            header9.Text = "Bureau";
            header9.Width = 80;

            header10.TextAlign = HorizontalAlignment.Left;
            header10.Text = "Media No";
            header10.Width = 80;

            //header11.Text = "JTSTicketNo";
            //header11.TextAlign = HorizontalAlignment.Left;
            //header11.Width = 60;

            header11.TextAlign = HorizontalAlignment.Left;
            header11.Text = "Low";
            header11.Width = 40;

            header12.TextAlign = HorizontalAlignment.Left;
            header12.Text = "High";
            header12.Width = 40;

            header13.TextAlign = HorizontalAlignment.Left;
            header13.Text = "File Name";
            header13.Width = -2;

            lstComList.Columns.Add(header1);
            lstComList.Columns.Add(header2);
            lstComList.Columns.Add(header3);
            lstComList.Columns.Add(header4);
            lstComList.Columns.Add(header5);
            lstComList.Columns.Add(header6);
            lstComList.Columns.Add(header7);
            lstComList.Columns.Add(header8);
            lstComList.Columns.Add(header9);
            lstComList.Columns.Add(header10);
            lstComList.Columns.Add(header11);
            lstComList.Columns.Add(header12);
            lstComList.Columns.Add(header13);
            //lstNews.Columns.Add(header14);
            //lstNews.Columns.Add(header15);
            //lstProgramDetails.Columns.Add(header16);
            //lstProgramDetails.Columns.Add(header17);
        }
        private void DownloadComfiles()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["targetip"];
            string file2 = "";
            Int32 ID = 0;
            foreach (ListViewItem row in lstComList.Items)
            {
                try
                {
                    bool isSelected = Convert.ToBoolean(row.Checked);
                    if (isSelected)
                    {
                        ID = Convert.ToInt32(row.SubItems[1].Text);
                        var s = db.ArchiveFileDetails.Where(x => x.ArchiveID == ID).ToList();
                        foreach (var ss in s)
                        {
                            if (s != null)
                            {
                                string MediaType = row.SubItems[5].Text;
                                if (MediaType.ToUpper() == "ALTO")
                                {
                                    string pathAndFilename = (path + "\\" + ss.FileFullPath);
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
                                    System.IO.FileInfo filetarget;
                                    //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
                                    // System.IO.DirectoryInfo dirinfo;
                                    try
                                    {

                                        if (fileInfo.Exists)
                                        {
                                            WebClient wc = new WebClient();
                                            string targetpath;
                                            string[] str = fileInfo.DirectoryName.Split('\\');
                                            string strtxt = "";
                                            strtxt = str[str.Count() - 1];
                                            targetpath = txtComPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;

                                            filetarget = new FileInfo(targetpath);
                                            if (!filetarget.Exists)
                                                System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

                                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                                            wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
                                        }
                                        else
                                        {
                                            throw new Exception("File not found");
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void CreatePicist()
        {
            lstPicList.Items.Clear();
            ColumnHeader header1, header2, header3, header4, header5, header6, header7, header8, header9, header10, header11, header12, header13, header14, header15, header16, header17;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();
            header5 = new ColumnHeader();
            header6 = new ColumnHeader();
            header7 = new ColumnHeader();
            header8 = new ColumnHeader();
            header9 = new ColumnHeader();
            header10 = new ColumnHeader();
            header11 = new ColumnHeader();
            header12 = new ColumnHeader();
            header13 = new ColumnHeader();
            header14 = new ColumnHeader();
            //header15 = new ColumnHeader();

            header1.Text = "#.";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 50;

            header2.Text = "ArchiveID.";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 80;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Slug";
            header3.Width = 250;


            header4.Text = "Detail";
            header4.TextAlign = HorizontalAlignment.Left;
            header4.Width = 120;

            header5.TextAlign = HorizontalAlignment.Left;
            header5.Text = "Type";
            header5.Width = 100;



            //header5.TextAlign = HorizontalAlignment.Left;
            //header5.Text = "Details";
            //header5.Width = 250;


            header6.TextAlign = HorizontalAlignment.Left;
            header6.Text = "Media";
            header6.Width = 80;

            header7.Text = "Date";
            header7.TextAlign = HorizontalAlignment.Left;
            header7.Width = 75;

            header8.TextAlign = HorizontalAlignment.Left;
            header8.Text = "Photographer";
            header8.Width = 90;

            header9.TextAlign = HorizontalAlignment.Left;
            header9.Text = "Bureau";
            header9.Width = 80;

            header10.TextAlign = HorizontalAlignment.Left;
            header10.Text = "Media No";
            header10.Width = 80;

            //header11.Text = "JTSTicketNo";
            //header11.TextAlign = HorizontalAlignment.Left;
            //header11.Width = 60;

            header11.TextAlign = HorizontalAlignment.Left;
            header11.Text = "Low";
            header11.Width = 40;

            header12.TextAlign = HorizontalAlignment.Left;
            header12.Text = "High";
            header12.Width = 40;

            header13.TextAlign = HorizontalAlignment.Left;
            header13.Text = "File Name";
            header13.Width = -2;

            lstPicList.Columns.Add(header1);
            lstPicList.Columns.Add(header2);
            lstPicList.Columns.Add(header3);
            lstPicList.Columns.Add(header4);
            lstPicList.Columns.Add(header5);
            lstPicList.Columns.Add(header6);
            lstPicList.Columns.Add(header7);
            lstPicList.Columns.Add(header8);
            lstPicList.Columns.Add(header9);
            lstPicList.Columns.Add(header10);
            lstPicList.Columns.Add(header11);
            lstPicList.Columns.Add(header12);
            lstPicList.Columns.Add(header13);
            //lstNews.Columns.Add(header14);
            //lstNews.Columns.Add(header15);
            //lstProgramDetails.Columns.Add(header16);
            //lstProgramDetails.Columns.Add(header17);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Lowres"];
            string FileName = lstComList.SelectedItems[0].SubItems[12].Text;
            string pathAndFilename = (path + "\\" + FileName);
            //nDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ep.Clear();
            try
            {
                FileInfo inf = new FileInfo(pathAndFilename);
                if (inf.Exists)
                    Playfile(pathAndFilename);
                else
                {
                    MessageBox.Show("File Not Found", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //    ep.SetError(txtEntTargetPath, "Directory not exits");
            }
            catch (Exception ex)
            {
                //ep.SetError(txtEntTargetPath, "Invalid path or directory not exits");
                return;
            }
            ///nDownload.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int ArchiveID = 0;
            string path = System.Configuration.ConfigurationManager.AppSettings["AMS_Highres"];

            try
            {
                ArchiveID = Convert.ToInt32(lstComList.SelectedItems[0].SubItems[1].Text);
                Downloadfiles(path);
            }
            catch (Exception)
            {

                throw;
            }


        }


        #region Commmethods
        //private void bindComCategory()
        //{
        //    var c = dbJTS.ChannelCategories.Where(x => x.IsActive == true).ToList();


        //}

        //private void bindComBureau()
        //{
        //    var c = dbJTS.tblBureaux.ToList();
        //    DataTable dt = Helper.ToDataTable(c);
        //    DataRow dr = dt.NewRow();
        //    dr[0] = 0;
        //    dr[1] = "Please Select";
        //    dt.Rows.InsertAt(dr, 0);
        //    cmbComCategory.ValueMember = "BureauID";
        //    cmbComCategory.DisplayMember = "Name";
        //    cmbComCategory.DataSource = dt;
        //    cmbComCategory.SelectedIndex = 0;
        //}
        #endregion
        #endregion

        #region Pictures
        private void txtPicSlug_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void txtPicDetails_MouseDown(object sender, MouseEventArgs e)
        {

        }



        private void txtPicKeywords_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbPicCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(cmbPicCategory.SelectedValue);
            var lst = (from u in dbJTS.ChannelCategoryDetails.Where(x => x.CategoryID == ID && x.IsActive == true)
                       select new { u.ChannelID, u.tblChannel.Name, u.tblChannel.orderbyID }).OrderBy(x => x.orderbyID).ToList();
            DataTable dt = Helper.ToDataTable(lst);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Please Select";
            dt.Rows.InsertAt(dr, 0);

            cmbPicChannel.DisplayMember = "Name";
            cmbPicChannel.ValueMember = "ChannelID";
            cmbPicChannel.DataSource = dt;
            cmbPicChannel.SelectedIndex = 0;
        }

        private void btnPicSearch_Click(object sender, EventArgs e)
        {
            lstPicList.Items.Clear();
            if (txtPicSlug.Text.Trim() != String.Empty || txtPicDetails.Text.Trim() != String.Empty || cmbPicKeywords.Text.Trim() != String.Empty
                   || cmbPicChannel.SelectedValue != "0" || cmbPicBureau.SelectedValue != "0"
                   || cmb_PicPhotographer.SelectedValue != "0" || cmbPicImage.SelectedValue != "0"
                   || cmbPicSource.SelectedValue != "0"
                 )
            {
                PicSearch();
            }
        }
        protected void PicSearch()
        {
            try
            {
                string telefrom = "";
                string teleto = "";

                if (chkPicSet.Checked)
                {
                    telefrom = dtPicFrom.Value.ToShortDateString();
                    teleto = dtPicto.Value.ToShortDateString();
                }
                DataTable dtSearchResult;
                DataTable dt;
                long ArID = 0;
                Object isHighClip = null;
                Object isLowClip = null;
                Object exactwordsearch = null;

                if (chkPicHigh.Checked == true)
                    isHighClip = true;
                if (chkPicExactKeyword.Checked == true)
                    exactwordsearch = true;
                else
                    exactwordsearch = false;
                txtPicSlug.Text = txtPicSlug.Text.Replace("%", " ");
                txtPicSlug.Text = txtPicSlug.Text.Replace("  ", " ");
                String[] arrSearch = txtPicSlug.Text.Trim().Split(' ');
                String strSlug = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strSlug += arrSearch[iIndex] + "%";
                    }
                }
                if (strSlug.EndsWith("%"))
                {
                    strSlug = strSlug.Substring(0, strSlug.LastIndexOf("%"));
                }

                arrSearch = null;
                txtPicDetails.Text = txtPicDetails.Text.Replace("%", " ");
                txtPicDetails.Text = txtPicDetails.Text.Replace("  ", " ");
                arrSearch = txtPicDetails.Text.Trim().Split(' ');
                String strDetail = String.Empty;
                for (Int32 iIndex = 0; iIndex < arrSearch.Length; iIndex++)
                {
                    if (arrSearch[iIndex] != String.Empty)
                    {
                        strDetail += arrSearch[iIndex] + "%";
                    }
                }
                if (strDetail.EndsWith("%"))
                {
                    strDetail = strDetail.Substring(0, strDetail.LastIndexOf("%"));
                }
                string stringkeyword = "";
                if (cmbPicKeywords.SelectedIndex > 0)
                    stringkeyword = cmbPicKeywords.Text.Trim();
                ArchiveDB obj = new ArchiveDB();
                dt = obj.GetAllQuickSearchPic(null, null, cmbPicChannel.SelectedValue, null, 0,
                                               0, null, null, cmbPicSource.SelectedValue,
                                               0, null, null, null, 0,
                                               null, cmbPicBureau.SelectedValue, null, null, strSlug, null, null, null, null, null, null, null,
                                               null, null, null, null, null, null, null, null, null, null, null, null, null, strDetail, null,
                                               null, null, null, null, null, null, null, null, null, isHighClip, isLowClip, null, null, null, null,
                                               null, null, null, null, null, 0, stringkeyword, exactwordsearch,
                                               telefrom, teleto, cmb_PicPhotographer.SelectedValue
                                               );


                dtSearchResult = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt64(dr["ArchiveID"]) != ArID)
                        {
                            dtSearchResult.ImportRow(dr);
                            ArID = Convert.ToInt64(dr["ArchiveID"]);
                        }

                    }
                }
                if (dtSearchResult.Rows.Count > 0)
                {
                    lblPicTotalItems.ForeColor = Color.Red;
                    lblPicTotalItems.Text = "Working ! please wait";
                    lblPicTotalItems.Refresh();
                    //lbltotal.BackColor = Color.Blue;
                    lblPicTotalItems.ForeColor = Color.Blue;

                    //LowRD = false;
                    btnPicSearch.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    PBPicList.Value = 0;
                    Int32 i = 1;
                    //   DataTable dt = dtSearchResult;
                    lblPicTotalItems.Text = "Record Found : " + dtSearchResult.Rows.Count.ToString();
                    PBPicList.Maximum = dtSearchResult.Rows.Count;
                    lstPicList.Items.Clear();
                    foreach (DataRow row in dtSearchResult.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(i.ToString());
                        lvi.UseItemStyleForSubItems = true;
                        lvi.SubItems.Add(row["ArchiveId"].ToString());
                        lvi.Tag = row["FileName"].ToString();
                        lvi.SubItems.Add(row["Slug"].ToString());
                        lvi.SubItems.Add(row["Detail"].ToString());
                        //lvi.SubItems.Add(row["Source"].ToString());
                        lvi.SubItems.Add(row["FootageType"].ToString());
                        lvi.SubItems.Add(row["MediaType"].ToString());
                        lvi.SubItems.Add(row["ShootDate"].ToString());
                        lvi.SubItems.Add(row["Reporter"].ToString());
                        lvi.SubItems.Add(row["Bureau"].ToString());
                        lvi.SubItems.Add(row["MediaNo"].ToString());
                        //lvi.SubItems.Add(row["JTSTicketNo"].ToString());
                        lvi.SubItems.Add(row["IsLowClip"].ToString());
                        lvi.SubItems.Add(row["IsHighClip"].ToString());
                        lvi.SubItems.Add(row["FileName"].ToString());

                        lvi.UseItemStyleForSubItems = false;
                        lvi.SubItems[0].BackColor = Color.LightSkyBlue;
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;
                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;
                        }
                        else if ((Convert.ToBoolean(row["IsHighClip"]) == false) && (Convert.ToBoolean(row["IsLowClip"]) == true))
                        {
                            lvi.SubItems[10].BackColor = Color.Blue;
                            lvi.SubItems[10].ForeColor = Color.White;

                        }
                        if ((Convert.ToBoolean(row["IsHighClip"]) == true) && (Convert.ToBoolean(row["IsLowClip"]) == false))
                        {

                            lvi.SubItems[11].BackColor = Color.Green;
                            lvi.SubItems[11].ForeColor = Color.White;

                        }
                        lstPicList.Items.Add(lvi);
                        PBPicList.Value = i++;
                    }
                    btnPicSearch.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    lblPicTotalItems.Text = "Total Recoud Found : " + dtSearchResult.Rows.Count;
                }
                else
                {
                    lblPicTotalItems.Text = "No Results Found";
                }

            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Specified argument was out of the range of valid values"))
                {
                    lblPicTotalItems.Visible = true;
                }
            }

        }
        private void chkPicSet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPicSet.Checked)
            {
                dtPicFrom.Enabled = true;
                dtPicto.Enabled = true;
            }
            else
            {
                dtPicFrom.Enabled = false;
                dtPicto.Enabled = false;
            }
        }

        private void btnPicDownload_Click(object sender, EventArgs e)
        {
            btnPicDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ep.Clear();
            try
            {
                DirectoryInfo inf = new DirectoryInfo(txtPicTargetPath.Text);
                if (!inf.Exists)
                {
                    System.IO.Directory.CreateDirectory(inf.FullName);
                }
              
                DownloadPicfiles();
              
               
            }
            catch (Exception ex)
            {
                ep.SetError(txtPicTargetPath, "Invalid path or directory not exits");
                btnPicDownload.Enabled = true;
                return;
            }
            finally
            {
                btnPicDownload.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnPicOpenTargetPath_Click(object sender, EventArgs e)
        {
            OFDENT.Title = "Select File to Upload";

            if (Directory.Exists(txtPicTargetPath.Text))
            {
                OFDENT.InitialDirectory = txtPicTargetPath.Text;
            }
            else
            {
                OFDENT.InitialDirectory = @"C:\";
            }
            if (OFDENT.ShowDialog() == DialogResult.OK)
            {
                OFDENT.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                OFDENT.FilterIndex = 1;

            }
        }

        private void button3_Click(object sender, EventArgs e) //btnPicTargetPath
        {
            using (var fbd = new FolderBrowserDialog())
            {

                fbd.SelectedPath = Helper.GetDownloadsPath();// Environment.SpecialFolder.MyDocuments;
                // fbd.SelectedPath = 
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPicTargetPath.Text = fbd.SelectedPath;
                }
            }
        }



        #region Methods

        //private void bindPicBureau()
        //{
        //    var c = dbJTS.tblBureaux.ToList();
        //    DataTable dt = Helper.ToDataTable(c);
        //    DataRow dr = dt.NewRow();
        //    dr[0] = 0;
        //    dr[1] = "Please Select";
        //    dt.Rows.InsertAt(dr, 0);



        //    cmbNewsBureau.ValueMember = "BureauID";
        //    cmbNewsBureau.DisplayMember = "Name";
        //    cmbNewsBureau.DataSource = dt;
        //    cmbNewsBureau.SelectedIndex = 0;


        //    cmbComBureau.ValueMember = "BureauID";
        //    cmbComBureau.DisplayMember = "Name";
        //    cmbComBureau.DataSource = dt;
        //    cmbComBureau.SelectedIndex = 0;

        //    cmbPicBureau.ValueMember = "BureauId";
        //    cmbPicBureau.DisplayMember = "Name";
        //    cmbPicBureau.DataSource = dt;
        //    cmbPicBureau.SelectedIndex = 0;




        //}

        private void DownloadPicfiles()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["targetip"];
            string file2 = "";
            Int32 ID = 0;
            foreach (ListViewItem row in lstPicList.Items)
            {
                try
                {
                    bool isSelected = Convert.ToBoolean(row.Checked);
                    if (isSelected)
                    {
                        ID = Convert.ToInt32(row.SubItems[1].Text);
                        var s = db.ArchiveFileDetails.Where(x => x.ArchiveID == ID).ToList();
                        foreach (var ss in s)
                        {
                            if (s != null)
                            {
                                string MediaType = row.SubItems[5].Text;
                                if (MediaType.ToUpper() == "ALTO")
                                {
                                    string pathAndFilename = (path + "\\" + ss.FileFullPath);
                                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathAndFilename);
                                    System.IO.FileInfo filetarget;
                                    //   System.IO.FileInfo tempdirectory = new FileInfo(textBox1.Text);
                                    // System.IO.DirectoryInfo dirinfo;
                                    try
                                    {

                                        if (fileInfo.Exists)
                                        {
                                            WebClient wc = new WebClient();
                                            string targetpath;
                                            string[] str = fileInfo.DirectoryName.Split('\\');
                                            string strtxt = "";
                                            strtxt = str[str.Count() - 1];
                                            targetpath = txtPicTargetPath.Text + "\\" + strtxt + "\\" + fileInfo.Name;

                                            filetarget = new FileInfo(targetpath);
                                            if (!filetarget.Exists)
                                                System.IO.Directory.CreateDirectory(filetarget.DirectoryName);

                                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                                            wc.DownloadFileAsync(new Uri(fileInfo.FullName), targetpath);
                                        }
                                        else
                                        {
                                            throw new Exception("File not found");
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        private void cmbPicBureau_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                bindPhotographers();
            }
            catch (Exception)
            {

            }

        }
        #endregion

        private void cmbEnterShortTitle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNewArchiveID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnNewsSearch_Click(null, null);
        }

        private void chkNewsAll_CheckedChanged(object sender, EventArgs e)
        {
            bool Selected = true;
            if (chkNewsAll.Checked)
            {
                Selected = true;
            }
            else
            {
                Selected = false;

            }
            foreach (ListViewItem row in lstNews.Items)
            {
                row.Selected = Selected;
            }
        }
    }
}


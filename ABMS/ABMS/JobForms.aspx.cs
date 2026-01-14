using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
using System.Globalization;
namespace ABMS
{
    public partial class JobForms : System.Web.UI.Page
    {

        dbJobPortalEntities db = new dbJobPortalEntities();
        int CategoryID = 0;
        private void GetCaptcha()
        {
            var c = db.sp_GetCaptcha().SingleOrDefault();
            Session["captcah"] = c.comparename;
            imgCaptcha.ImageUrl = "CaptchImgeHandler.ashx?id=" + c.ImgFileName;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["categorykey"]);
            divtechinical.Visible = Convert.ToBoolean(ViewState["divtechinical"]);// = c.TechnicalEducation; ;
            divtraining.Visible = Convert.ToBoolean(ViewState["divtraining"]);// = c.ParticipatingTraining;
           // CategoryID = 110000002;

          

            if (!Page.IsPostBack)
            {
                FillDropwon();
                Fillpositions();
                //var c = db.sp_GetCaptcha().SingleOrDefault();
                //Session["captcah"] = c.comparename;
                //imgCaptcha.ImageUrl = "CaptchImgeHandler.ashx?id=" + c.ImgFileName;
                GetCaptcha();

                


                //   int Categoryid = Convert.ToInt32(Session["category"]);

                divexpdetails.Visible = true;
                var mylist1 = db.WorkingExperiences.Where(x => x.CategoryID == CategoryID).ToList();
                if (mylist1.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("id1");
                    dt.Columns.Add("desc1");
                    dt.Columns.Add("id2");
                    dt.Columns.Add("desc2");
                    // bool chk = false;
                    DataRow row = dt.NewRow();
                    int i = 0;
                    foreach (var s in mylist1)
                    {
                        if (i == 0)
                        {
                            row[0] = s.ID;
                            row[1] = s.ExpDescritpion;
                            i++;
                        }
                        else
                        {
                            row[2] = s.ID;
                            row[3] = s.ExpDescritpion;
                            i = 0;
                            dt.Rows.Add(row);
                            row = dt.NewRow();
                        }


                        //lst.Add(new ExperienceList() { ID1 = id1, Experience1 = des1, ID2 = id2, Experience2 = des2 });
                    }
                    dt.Rows.Add(row);
                    int total = dt.Rows.Count - 1;
                    if (dt.Rows[total][1].ToString().Trim().Length == 0)
                    {
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    }
                    gv.DataSource = dt;
                    gv.DataBind();
                }
                else
                {
                    divexpdetails.Visible = false;
                }
            }
        }


        #region Methods

        private void Fillpositions()
        {
                      
            var c = db.Categories.Where(x => x.ID == CategoryID && x.Status == true).SingleOrDefault();

            if (c != null)
            {
                //// Session["divexperience"] = c.ID; ;
                //ViewState["divtechinical"] = false; ;
                //ViewState["divtraining"] = false;
                var p = db.JobsNames.Where(x => x.Category_ID == c.ID).OrderBy(x => x.PositionName).Where(x => x.Status == true).ToList();
            //    divtechinical.Visible = Convert.ToBoolean(c.TechnicalEducation);
            //    divtraining.Visible = Convert.ToBoolean(c.ParticipatingTraining);
                lan1.Visible = Convert.ToBoolean(c.LanguageStatus);
           //     lan11.Visible = Convert.ToBoolean(c.LanguageStatus);
                ddlPositionApplied.DataValueField = "ID";
                ddlPositionApplied.DataTextField = "PositionName";
                ddlPositionApplied.DataSource = p;
                ddlPositionApplied.DataBind();
                if (CategoryID == 110000002)
                {
                    lblappliedfor.Text = "Applied for Content Writer";
                    //   changeimage.Src = "Content/Images/positionapplied_C.jpg";
                    ddlPositionApplied.Items.Insert(0, new ListItem("Select Language"));
                 //   lblposition.Text = "Language";
                }
                else if (CategoryID == 110000001)
                {
                    lblappliedfor.Text = "Applied for Teacher";
                    //    changeimage.Src = "Content/Images/positionapplied_T.jpg";
                    ddlPositionApplied.Items.Insert(0, new ListItem("Select Subject"));
              //      lblposition.Text = "Subjects";
                }
                else
                {
                    ddlPositionApplied.Items.Insert(0, new ListItem("Select Position"));
               //     lblposition.Text = "Position Applied";
                }
            }
            else
            {
                ddlPositionApplied.Items.Insert(0, new ListItem("Select Position"));
            }
        }
        private void FillDropwon()
        {
            var months = Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i).ToUpper() }).ToList();

            var c = db.LocationInfoes.OrderBy(x => x.LocationName).ToList();
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "LocationName";
            ddlCity.DataSource = c;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("Select City"));

            ddlexpcomMMS1.DataValueField = "M";
            ddlexpcomMMS1.DataTextField = "M";
            ddlexpcomMMS1.DataSource = months;
            ddlexpcomMMS1.DataBind();
            ddlexpcomMMS1.Items.Insert(0, new ListItem("MM"));


            ddlexpcomMMS2.DataValueField = "M";
            ddlexpcomMMS2.DataTextField = "M";
            ddlexpcomMMS2.DataSource = months;
            ddlexpcomMMS2.DataBind();
            ddlexpcomMMS2.Items.Insert(0, new ListItem("MM"));

            ddlexpcomMMS3.DataValueField = "M";
            ddlexpcomMMS3.DataTextField = "M";
            ddlexpcomMMS3.DataSource = months;
            ddlexpcomMMS3.DataBind();
            ddlexpcomMMS3.Items.Insert(0, new ListItem("MM"));

            ddlexpcomMMS4.DataValueField = "M";
            ddlexpcomMMS4.DataTextField = "M";
            ddlexpcomMMS4.DataSource = months;
            ddlexpcomMMS4.DataBind();
            ddlexpcomMMS4.Items.Insert(0, new ListItem("MM"));

            ddlexpcomMMS5.DataValueField = "M";
            ddlexpcomMMS5.DataTextField = "M";
            ddlexpcomMMS5.DataSource = months;
            ddlexpcomMMS5.DataBind();
            ddlexpcomMMS5.Items.Insert(0, new ListItem("MM"));

            ddlexpcomYYS1.DataValueField = "M";
            ddlexpcomYYS1.DataTextField = "M";
            ddlexpcomYYS1.DataSource = months;
            ddlexpcomYYS1.DataBind();
            ddlexpcomYYS1.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYS1.Items.Insert(0, new ListItem("MM"));

            ddlexpcomYYS2.DataValueField = "M";
            ddlexpcomYYS2.DataTextField = "M";
            ddlexpcomYYS2.DataSource = months;
            ddlexpcomYYS2.DataBind();
            ddlexpcomYYS2.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYS2.Items.Insert(0, new ListItem("MM"));

            ddlexpcomYYS3.DataValueField = "M";
            ddlexpcomYYS3.DataTextField = "M";
            ddlexpcomYYS3.DataSource = months;
            ddlexpcomYYS3.DataBind();
            ddlexpcomYYS3.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYS3.Items.Insert(0, new ListItem("MM"));

            ddlexpcomYYS4.DataValueField = "M";
            ddlexpcomYYS4.DataTextField = "M";
            ddlexpcomYYS4.DataSource = months;
            ddlexpcomYYS4.DataBind();
            ddlexpcomYYS4.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYS4.Items.Insert(0, new ListItem("MM"));

            ddlexpcomYYS5.DataValueField = "M";
            ddlexpcomYYS5.DataTextField = "M";
            ddlexpcomYYS5.DataSource = months;
            ddlexpcomYYS5.DataBind();
            ddlexpcomYYS5.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYS5.Items.Insert(0, new ListItem("MM"));


            List<int> myList = new List<int>();
            myList = Enumerable.Range(1960, 61).ToList();
            var values = myList.OrderByDescending(p => p).ToList();

            ddlexpcomMME1.DataSource = values;
            ddlexpcomMME1.DataBind();
            ddlexpcomMME1.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomMME2.DataSource = values;
            ddlexpcomMME2.DataBind();
            ddlexpcomMME2.Items.Insert(0, new ListItem("YYYY"));


            ddlexpcomMME3.DataSource = values;
            ddlexpcomMME3.DataBind();
            ddlexpcomMME3.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomMME4.DataSource = values;
            ddlexpcomMME4.DataBind();
            ddlexpcomMME4.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomMME5.DataSource = values;
            ddlexpcomMME5.DataBind();
            ddlexpcomMME5.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomYYE1.DataSource = values;
            ddlexpcomYYE1.DataBind();
            ddlexpcomYYE1.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYE1.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomYYE2.DataSource = values;
            ddlexpcomYYE2.DataBind();
            ddlexpcomYYE2.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYE2.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomYYE3.DataSource = values;
            ddlexpcomYYE3.DataBind();
            ddlexpcomYYE3.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYE3.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomYYE4.DataSource = values;
            ddlexpcomYYE4.DataBind();
            ddlexpcomYYE4.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYE4.Items.Insert(0, new ListItem("YYYY"));

            ddlexpcomYYE5.DataSource = values;
            ddlexpcomYYE5.DataBind();
            ddlexpcomYYE5.Items.Insert(0, new ListItem("TILL"));
            ddlexpcomYYE5.Items.Insert(0, new ListItem("YYYY"));

            var g = db.DegreeInfoes.OrderBy(x => x.DegreeName).ToList();
            var s = g.Where(x => x.EdLevel == "S");
            var cl = g.Where(x => x.EdLevel == "C");
            var b = g.Where(x => x.EdLevel == "B");
            var u = g.Where(x => x.EdLevel == "U");
            //var c = g.Where(x => x.EdLevel == "C");

            ddluni1.DataValueField = "ID";
            ddluni1.DataTextField = "DegreeName";
            ddluni1.DataSource = s;
            ddluni1.DataBind();

            ddluni2.DataValueField = "ID";
            ddluni2.DataTextField = "DegreeName";
            ddluni2.DataSource = cl;
            ddluni2.DataBind();

            ddluni3.DataValueField = "ID";
            ddluni3.DataTextField = "DegreeName";
            ddluni3.DataSource = u;
            ddluni3.DataBind();

            ddluni4.DataValueField = "ID";
            ddluni4.DataTextField = "DegreeName";
            ddluni4.DataSource = u;
            ddluni4.DataBind();

            ddluni5.DataValueField = "ID";
            ddluni5.DataTextField = "DegreeName";
            ddluni5.DataSource = u;
            ddluni5.DataBind();

            List<string> LstExpert = new List<string>();
            LstExpert.Add("Proficiency Level");
            LstExpert.Add("Excellent");
            LstExpert.Add("Good");
            LstExpert.Add("Fair");

            ddlRead1.DataSource = LstExpert;
            ddlRead1.DataBind();

            ddlRead2.DataSource = LstExpert;
            ddlRead2.DataBind();

            ddlRead3.DataSource = LstExpert;
            ddlRead3.DataBind();

            ddlRead4.DataSource = LstExpert;
            ddlRead4.DataBind();

            ddlwrite1.DataSource = LstExpert;
            ddlwrite1.DataBind();

            ddlwrite2.DataSource = LstExpert;
            ddlwrite2.DataBind();

            ddlwrite3.DataSource = LstExpert;
            ddlwrite3.DataBind();

            ddlwrite4.DataSource = LstExpert;
            ddlwrite4.DataBind();

            ddlspeak1.DataSource = LstExpert;
            ddlspeak1.DataBind();


            ddlspeak1.DataSource = LstExpert;
            ddlspeak1.DataBind();

            ddlspeak2.DataSource = LstExpert;
            ddlspeak2.DataBind();

            ddlspeak3.DataSource = LstExpert;
            ddlspeak3.DataBind();

            ddlspeak4.DataSource = LstExpert;
            ddlspeak4.DataBind();

            var ll = db.CategoryLanguages.Where(x => x.Isactive == true).ToList();
            ddlLanges.DataValueField = "ID";
            ddlLanges.DataTextField = "LanguageName";
            ddlLanges.DataSource = ll;
            ddlLanges.DataBind();
            ddlLanges.Items.Insert(0, new ListItem("Select Language "));


            var l = db.LocationInfoes.OrderBy(x => x.LocationName).Where(x => x.Status == true).ToList();
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataTextField = "LocationName";
            ddlLocation.DataSource = l;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select Location"));



        }
        #endregion

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //if (Session["location"] == null || Session["Category"] == null || Session["readytoJoin"] == null || Session["jobappplied"] == null || Session["experience"] == null)
            //{
            //    Response.Redirect("AppForm.aspx", true);
            //}
            ErrLocation.Text = string.Empty;
            ErrReady.Text = string.Empty;
            ErrPosition.Text = string.Empty;

            if ((chk1.Checked && (ddlRead1.SelectedIndex == 0 && ddlwrite1.SelectedIndex == 0) && ddlspeak1.SelectedIndex == 0))
            {
                lblMessage.Text = "please select Proficiency Level";
                return;
            }

            if ((chk2.Checked && (ddlRead2.SelectedIndex == 0 && ddlwrite2.SelectedIndex == 0) && ddlspeak2.SelectedIndex == 0))
            {
                lblMessage.Text = "please select Proficiency Level";
                return;
            }

            if ((chk3.Checked && (ddlRead3.SelectedIndex == 0 && ddlwrite3.SelectedIndex == 0 && ddlspeak3.SelectedIndex == 0)) &&
            
                (txtoth3.Text.Trim().Length == 0)) {
                lblMessage.Text = "please select Proficiency Level";
        
            return;}

            if ((chk4.Checked && (ddlRead4.SelectedIndex == 0 && ddlwrite4.SelectedIndex == 0 && ddlspeak4.SelectedIndex == 0)) &&
                (txtoth4.Text.Trim().Length == 0))
            {
                lblMessage.Text = "please select Proficiency Level";
                return;
            }


            if (ddlPositionApplied.SelectedIndex == 0)
            {
                ErrPosition.Text = "Please select Position";
                return;
            }
            if (ddlReadytoJoin.SelectedIndex == 0)
            {
                ErrReady.Text = "Please select Joining Period";
                return;
            }
            if (ddlLocation.SelectedIndex == 0)
            {
                ErrLocation.Text = "Please select Joining City";
                return;
            }
            string captcha = Session["captcah"].ToString().ToUpper();
            if (captcha != txtcaptcha.Text.Trim().ToUpper())
            {
                ErrorCaptcha.Text = "Invalid Captcha information";
                return;
            }
            else
            {
                ErrorCaptcha.Text = "";
                Session["captcah"] = null;
            }

            lblMessage.Text = string.Empty;
            ErrorCity.Text = string.Empty;
            if ((ddlCity.SelectedIndex == 0) && (txtOtherCity.Text.Trim().Length == 0))
            {
                ErrorCity.Text = "Plesae enter/select City";
                return;
            }

            MasterTable obj = new MasterTable();

            try
            {

                obj.Location = Convert.ToString(ddlLocation.SelectedItem.Text);// Session["location"].ToString();
                obj.CategoryID = CategoryID;// Convert.ToInt32(Session["Category"]);
                obj.ReadytoJoinID = ddlReadytoJoin.SelectedItem.Text;//  Session["readytoJoin"].ToString();
                obj.PositionID = Convert.ToInt32(ddlPositionApplied.SelectedValue);
                bool isExperienced = RdoExp.Checked;
                if (isExperienced == true)
                    obj.Experienced = true;
                else
                    obj.Experienced = false;

                obj.ClaimedID = Helper.RandomString(8);
                obj.ApplicantName = txtCanName.Text;
                obj.ApplicantAddress = txtAdddressDetails.Text;
                obj.EmailID = txtEmail.Text;
                obj.Mobile = txtMobile.Text;
                obj.LandLine = txtAlandLine.Text;
                obj.Hobbies = txtHobbies.Text;
                obj.DOB = Helper.SetDateFormat(txtDOB.Text);
                if (RdoMarrid.Checked)
                    obj.Married = true;
                else
                    obj.Married = false;
                if (RdoMale.Checked)
                    obj.Gender = true;
                else
                    obj.Gender = false;

                if (ddlCity.SelectedIndex > 0)
                {
                    obj.City = ddlCity.SelectedItem.Text;
                }
                if ((ddlCity.SelectedIndex == 0) && (txtOtherCity.Text.Trim().Length > 0))
                {
                    obj.City = txtOtherCity.Text;
                }
                obj.PlaceofBirth = "2020-05-21";// txtPlaceofbirth.Text;
                try
                {
                    if (lan1.Visible == true)
                        obj.CategoryLanguageID = Convert.ToInt32(ddlLanges.SelectedValue);// Convert.ToInt32(Session["language"]);
                }
                catch (Exception )
                {
                    
                }
                // string FileFromSession = "";

                //if (Session["imgPath"] != null)
                //{
                //    FileFromSession = Session["imgPath"].ToString();

                //    string filePath = Server.MapPath("~/Documents/" + FileFromSession);
                //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //    BinaryReader br = new BinaryReader(fs);
                //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //    br.Close();
                //    fs.Close();
                //    obj.ApplicantImage = bytes;
                //}
                //else
                //{

                //}
                db.MasterTables.Add(obj);
                db.SaveChanges();
                // Save Academic Qualifications
                AcademicQualification obj2 = new AcademicQualification();
                if (txtschool.Text.Trim().Length > 0 && txtschoolyear.Text.Trim().Length == 4)
                {
                    obj2.RecordID = obj.ID;
                    obj2.AcademicInstitute = txtschool.Text;
                    obj2.DegreeID = Convert.ToInt32(ddluni1.SelectedValue);
                    obj2.PassingYear = txtschoolyear.Text;
                    db.AcademicQualifications.Add(obj2);
                    db.SaveChanges();
                }
                if (txtcollege.Text.Trim().Length > 0 && txtcollegeyear.Text.Trim().Length == 4)
                {

                    obj2 = new AcademicQualification();
                    obj2.RecordID = obj.ID;
                    obj2.AcademicInstitute = txtcollege.Text;
                    obj2.DegreeID = Convert.ToInt32(ddluni2.SelectedValue);
                    obj2.PassingYear = txtcollegeyear.Text;
                    db.AcademicQualifications.Add(obj2);
                    db.SaveChanges();

                }

                if (txtuni1.Text.Trim().Length > 0 && txtuniyear1.Text.Trim().Length == 4)
                {

                    obj2 = new AcademicQualification();
                    obj2.RecordID = obj.ID;
                    obj2.AcademicInstitute = txtuni1.Text;
                    obj2.DegreeID = Convert.ToInt32(ddluni3.SelectedValue);
                    obj2.PassingYear = txtuniyear1.Text;
                    db.AcademicQualifications.Add(obj2);
                    db.SaveChanges();
                }

                if (txtuni2.Text.Trim().Length > 0 && txtuniyear2.Text.Trim().Length == 4)
                {

                    obj2 = new AcademicQualification();
                    obj2.RecordID = obj.ID;
                    obj2.AcademicInstitute = txtuni2.Text;
                    obj2.DegreeID = Convert.ToInt32(ddluni4.SelectedValue);
                    obj2.PassingYear = txtuniyear2.Text;
                    db.AcademicQualifications.Add(obj2);
                    db.SaveChanges();
                }
                if (txtuni3.Text.Trim().Length > 0 && txtuniyear3.Text.Trim().Length == 4)
                {

                    obj2 = new AcademicQualification();
                    obj2.RecordID = obj.ID;
                    obj2.AcademicInstitute = txtuni3.Text;
                    obj2.DegreeID = Convert.ToInt32(ddluni3.SelectedValue);
                    obj2.PassingYear = txtuniyear3.Text;
                    db.AcademicQualifications.Add(obj2);
                    db.SaveChanges();
                }
                //Technical Qualification
                TechnicalQualification obj3 = new TechnicalQualification();
                if (txtins1.Text.Trim().Length > 0 && txtdeg1.Text.Trim().Length > 0 && txtpass1.Text.Trim().Length > 0)
                {
                    obj3 = new TechnicalQualification();
                    obj3.RecordID = obj.ID;
                    obj3.InstituteName = txtins1.Text;
                    obj3.Passingyear = txtpass1.Text;
                    obj3.DegreeName = txtdeg1.Text;
                    db.TechnicalQualifications.Add(obj3);
                    db.SaveChanges();

                }

                if (txtins2.Text.Trim().Length > 0 && txtdeg2.Text.Trim().Length > 0 && txtpass2.Text.Trim().Length > 0)
                {
                    obj3 = new TechnicalQualification();
                    obj3.RecordID = obj.ID;
                    obj3.InstituteName = txtins2.Text;
                    obj3.Passingyear = txtpass2.Text;
                    obj3.DegreeName = txtdeg2.Text;
                    db.TechnicalQualifications.Add(obj3);
                    db.SaveChanges();
                }
                if (txtins3.Text.Trim().Length > 0 && txtdeg3.Text.Trim().Length > 0 && txtpass3.Text.Trim().Length > 0)
                {
                    obj3 = new TechnicalQualification();
                    obj3.RecordID = obj.ID;
                    obj3.InstituteName = txtins3.Text;
                    obj3.Passingyear = txtpass3.Text;
                    obj3.DegreeName = txtdeg3.Text;
                    db.TechnicalQualifications.Add(obj3);
                    db.SaveChanges();
                }
                if (txtins4.Text.Trim().Length > 0 && txtdeg4.Text.Trim().Length > 0 && txtpass4.Text.Trim().Length > 0)
                {
                    obj3 = new TechnicalQualification();
                    obj3.RecordID = obj.ID;
                    obj3.InstituteName = txtins4.Text;
                    obj3.Passingyear = txtpass4.Text;
                    obj3.DegreeName = txtdeg4.Text;
                    db.TechnicalQualifications.Add(obj3);
                    db.SaveChanges();
                }
                if (txtins5.Text.Trim().Length > 0 && txtdeg5.Text.Trim().Length > 0 && txtpass5.Text.Trim().Length > 0)
                {
                    obj3 = new TechnicalQualification();
                    obj3.RecordID = obj.ID;
                    obj3.InstituteName = txtins5.Text;
                    obj3.Passingyear = txtpass5.Text;
                    obj3.DegreeName = txtdeg5.Text;
                    db.TechnicalQualifications.Add(obj3);
                    db.SaveChanges();
                }

                // Training Programmes


                TrainingProgramme obj4 = new TrainingProgramme();
                if (txttrg1.Text.Trim().Length > 0 && txttrgcourse1.Text.Trim().Length > 0 && txttrgyear1.Text.Trim().Length > 0 && txttrgdur1.Text.Trim().Length > 0)
                {
                    obj4.RecordID = obj.ID;
                    obj4.InstituteName = txttrg1.Text;
                    obj4.TrainingName = txttrgcourse1.Text;
                    obj4.TrgYear = txttrgyear1.Text;
                    obj4.TrgDuration = txttrgdur1.Text;
                    db.TrainingProgrammes.Add(obj4);
                    db.SaveChanges();
                }
                if (txttrg1.Text.Trim().Length > 0 && txttrgcourse1.Text.Trim().Length > 0 && txttrgyear1.Text.Trim().Length > 0 && txttrgdur1.Text.Trim().Length > 0)
                {
                    obj4 = new TrainingProgramme();
                    obj4.RecordID = obj.ID;
                    obj4.InstituteName = txttrg2.Text;
                    obj4.TrainingName = txttrgcourse2.Text;
                    obj4.TrgYear = txttrgyear2.Text;
                    obj4.TrgDuration = txttrgdur2.Text;
                    db.TrainingProgrammes.Add(obj4);
                    db.SaveChanges();
                }
                if (txttrg1.Text.Trim().Length > 0 && txttrgcourse1.Text.Trim().Length > 0 && txttrgyear1.Text.Trim().Length > 0 && txttrgdur1.Text.Trim().Length > 0)
                {
                    obj4 = new TrainingProgramme();
                    obj4.RecordID = obj.ID;
                    obj4.InstituteName = txttrg3.Text;
                    obj4.TrainingName = txttrgcourse3.Text;
                    obj4.TrgYear = txttrgyear3.Text;
                    obj4.TrgDuration = txttrgdur3.Text;
                    db.TrainingProgrammes.Add(obj4);
                    db.SaveChanges();
                }
                if (txttrg1.Text.Trim().Length > 0 && txttrgcourse1.Text.Trim().Length > 0 && txttrgyear1.Text.Trim().Length > 0 && txttrgdur1.Text.Trim().Length > 0)
                {
                    obj4 = new TrainingProgramme();
                    obj4.RecordID = obj.ID;
                    obj4.InstituteName = txttrg4.Text;
                    obj4.TrainingName = txttrgcourse4.Text;
                    obj4.TrgYear = txttrgyear4.Text;
                    obj4.TrgDuration = txttrgdur4.Text;
                    db.TrainingProgrammes.Add(obj4);
                    db.SaveChanges();
                }
                //Experience 
                // if (CheckDateRage("01", "2014", "01", "2020") == true)
                // {
                // }
                WorkExperience obj5 = new WorkExperience();
                if (txtexpcom1.Text.Trim().Length > 0 && txtexppos1.Text.Trim().Length > 0 && txtexpBrief1.Text.Trim().Length > 0 &&
                    ddlexpcomMMS1.SelectedIndex > 0 && ddlexpcomMME1.SelectedIndex > 0 && ddlexpcomYYS1.SelectedIndex > 0 && ddlexpcomYYE1.SelectedIndex > 0)
                {
                    obj5.RecordID = obj.ID;
                    obj5.CompanyName = txtexpcom1.Text;
                    obj5.PositioninCompany = txtexppos1.Text;
                    obj5.FromDate = Helper.SetDateFormat(GetMonth(ddlexpcomMMS1.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomMME1.SelectedItem.Text);
                    if (ddlexpcomYYS1.SelectedItem.Text == "Till" || ddlexpcomYYE1.SelectedItem.Text == "TILL")
                        obj5.ToDate = DateTime.Now;
                    else
                    {
                        obj5.ToDate = Helper.SetDateFormat(GetMonth(ddlexpcomYYS1.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomYYE1.SelectedItem.Text);
                    }
                    obj5.BriefDescription = txtexpBrief1.Text;
                    obj5.Duration = 0;
                    db.WorkExperiences.Add(obj5);
                    db.SaveChanges();
                }

                if (txtexpcom2.Text.Trim().Length > 0 && txtexppos2.Text.Trim().Length > 0 && txtexpBrief2.Text.Trim().Length > 0 &&
                    ddlexpcomMMS2.SelectedIndex > 0 && ddlexpcomMME2.SelectedIndex > 0 && ddlexpcomYYS2.SelectedIndex > 0 && ddlexpcomYYE2.SelectedIndex > 0)
                {
                    obj5 = new WorkExperience();
                    obj5.RecordID = obj.ID;
                    obj5.CompanyName = txtexpcom2.Text;
                    obj5.PositioninCompany = txtexppos2.Text;
                    obj5.FromDate = Helper.SetDateFormat(GetMonth(ddlexpcomMMS2.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomMME2.SelectedItem.Text);
                    if (ddlexpcomYYS2.SelectedItem.Text == "Till" || ddlexpcomYYE2.SelectedItem.Text == "TILL")
                        obj5.ToDate = DateTime.Now;
                    else
                    {
                        obj5.ToDate = Helper.SetDateFormat(GetMonth(ddlexpcomYYS2.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomYYE2.SelectedItem.Text);
                    }
                    obj5.BriefDescription = txtexpBrief2.Text;
                    obj5.Duration = 0;
                    db.WorkExperiences.Add(obj5);
                    db.SaveChanges();



                }
                if (txtexpcom3.Text.Trim().Length > 0 && txtexppos3.Text.Trim().Length > 0 && txtexpBrief3.Text.Trim().Length > 0 &&
                     ddlexpcomMMS3.SelectedIndex > 0 && ddlexpcomMME3.SelectedIndex > 0 && ddlexpcomYYS3.SelectedIndex > 0 && ddlexpcomYYE3.SelectedIndex > 0)
                {
                    obj5 = new WorkExperience();
                    obj5.RecordID = obj.ID;
                    obj5.CompanyName = txtexpcom3.Text;
                    obj5.PositioninCompany = txtexppos3.Text;
                    obj5.FromDate = Helper.SetDateFormat(GetMonth(ddlexpcomMMS3.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomMME3.SelectedItem.Text);
                    if (ddlexpcomYYS3.SelectedItem.Text == "Till" || ddlexpcomYYE3.SelectedItem.Text == "TILL")
                        obj5.ToDate = DateTime.Now;
                    else
                    {
                        obj5.ToDate = Helper.SetDateFormat(GetMonth(ddlexpcomYYS3.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomYYE3.SelectedItem.Text);
                    }
                    obj5.BriefDescription = txtexpBrief3.Text;
                    obj5.Duration = 0;
                    db.WorkExperiences.Add(obj5);
                    db.SaveChanges();
                }

                if (txtexpcom4.Text.Trim().Length > 0 && txtexppos4.Text.Trim().Length > 0 && txtexpBrief4.Text.Trim().Length > 0 &&
                     ddlexpcomMMS4.SelectedIndex > 0 && ddlexpcomMME4.SelectedIndex > 0 && ddlexpcomYYS4.SelectedIndex > 0 && ddlexpcomYYE4.SelectedIndex > 0)
                {
                    obj5 = new WorkExperience();
                    obj5.RecordID = obj.ID;
                    obj5.CompanyName = txtexpcom4.Text;
                    obj5.PositioninCompany = txtexppos4.Text;
                    obj5.FromDate = Helper.SetDateFormat(GetMonth(ddlexpcomMMS4.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomMME4.SelectedItem.Text);
                    if (ddlexpcomYYS4.SelectedItem.Text == "Till" || ddlexpcomYYE4.SelectedItem.Text == "TILL")
                        obj5.ToDate = DateTime.Now;
                    else
                    {
                        obj5.ToDate = Helper.SetDateFormat(GetMonth(ddlexpcomYYS4.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomYYE4.SelectedItem.Text);
                    }
                    obj5.BriefDescription = txtexpBrief4.Text;
                    obj5.Duration = 0;
                    db.WorkExperiences.Add(obj5);
                    db.SaveChanges();
                }

                if (txtexpcom5.Text.Trim().Length > 0 && txtexppos5.Text.Trim().Length > 0 && txtexpBrief5.Text.Trim().Length > 0 &&
                     ddlexpcomMMS5.SelectedIndex > 0 && ddlexpcomMME5.SelectedIndex > 0 && ddlexpcomYYS5.SelectedIndex > 0 && ddlexpcomYYE5.SelectedIndex > 0)
                {
                    obj5 = new WorkExperience();
                    obj5.RecordID = obj.ID;
                    obj5.CompanyName = txtexpcom5.Text;
                    obj5.PositioninCompany = txtexppos5.Text;
                    obj5.FromDate = Helper.SetDateFormat(GetMonth(ddlexpcomMMS5.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomMME5.SelectedItem.Text);
                    if (ddlexpcomYYS5.SelectedItem.Text == "Till" || ddlexpcomYYE5.SelectedItem.Text == "TILL")
                        obj5.ToDate = DateTime.Now;
                    else
                    {
                        obj5.ToDate = Helper.SetDateFormat(GetMonth(ddlexpcomYYS5.SelectedItem.Text) + "/" + "01" + "/" + ddlexpcomYYE5.SelectedItem.Text);
                    }
                    obj5.BriefDescription = txtexpBrief5.Text;
                    obj5.Duration = 0;
                    db.WorkExperiences.Add(obj5);
                    db.SaveChanges();
                }
                CategoryWorkingExperience ce = new CategoryWorkingExperience();
                foreach (GridViewRow row in gv.Rows)
                {

                    int idx = row.RowIndex;
                    int ID1 = 0;
                    try
                    {
                        ID1 = Convert.ToInt32(gv.DataKeys[idx].Values[0]);// .DataKeyNam

                    }
                    catch (Exception)
                    {

                        ID1 = 0;
                    }
                    int ID2 = 0;
                    try
                    {
                        ID2 = Convert.ToInt32(gv.DataKeys[idx].Values[1]);//
                    }
                    catch (Exception)
                    {
                        ID2 = 0;
                    }

                    CheckBox chkgv1 = (CheckBox)row.FindControl("chk1");
                    CheckBox chkgv2 = (CheckBox)row.FindControl("chk2");


                    if (chkgv1.Checked)
                    {
                        if (ID1 != 0)
                        {
                            ce = new CategoryWorkingExperience();
                            ce.WorkingExperienceID = ID1;
                            ce.MasterID = obj.ID;
                            db.CategoryWorkingExperiences.Add(ce);
                            db.SaveChanges();
                        }
                    }

                    if (chkgv2.Checked)
                    {
                        if (ID2 != 0)
                        {
                            ce = new CategoryWorkingExperience();
                            ce.WorkingExperienceID = ID2;
                            ce.MasterID = obj.ID;
                            db.CategoryWorkingExperiences.Add(ce);
                            db.SaveChanges();
                        }

                    }

                }

                //Languages

                CandidateLanguage obj6 = new CandidateLanguage();
                if (chk1.Checked)
                {
                    obj6.RecordID = obj.ID;
                    obj6.Language = "Urdu";
                    obj6.LanRead = ddlRead1.SelectedItem.Text;
                    obj6.LanWrite = ddlwrite1.SelectedItem.Text;
                    obj6.LanSpeak = ddlspeak1.SelectedItem.Text;
                    db.CandidateLanguages.Add(obj6);
                    db.SaveChanges();
                }

                if (chk2.Checked)
                {
                    obj6.RecordID = obj.ID;
                    obj6.Language = "English";
                    obj6.LanRead = ddlRead2.SelectedItem.Text;
                    obj6.LanWrite = ddlwrite2.SelectedItem.Text;
                    obj6.LanSpeak = ddlspeak2.SelectedItem.Text;
                    db.CandidateLanguages.Add(obj6);
                    db.SaveChanges();
                }
                if (chk3.Checked && txtoth3.Text.Trim().Length > 0)
                {
                    obj6.RecordID = obj.ID;
                    obj6.Language = txtoth3.Text;
                    obj6.LanRead = ddlRead3.SelectedItem.Text;
                    obj6.LanWrite = ddlwrite3.SelectedItem.Text;
                    obj6.LanSpeak = ddlspeak3.SelectedItem.Text;
                    db.CandidateLanguages.Add(obj6);
                    db.SaveChanges();
                }
                if (chk4.Checked && txtoth4.Text.Trim().Length > 0)
                {
                    obj6.RecordID = obj.ID;
                    obj6.Language = txtoth4.Text;
                    obj6.LanRead = ddlRead4.SelectedItem.Text;
                    obj6.LanWrite = ddlwrite4.SelectedItem.Text;
                    obj6.LanSpeak = ddlspeak4.SelectedItem.Text;
                    db.CandidateLanguages.Add(obj6);
                    db.SaveChanges();
                }

                //Breif Note

                BreiefNote obj7 = new BreiefNote();
                if (txtBriefnote.Text.Trim().Length > 0)
                {
                    obj7.RecordID = obj.ID;
                    obj7.BriefNote = txtBriefnote.Text;
                    db.BreiefNotes.Add(obj7);
                    db.SaveChanges();
                }

                Session["candidatename"] = obj.ApplicantName;
                Session["candidateid"] = obj.ClaimedID.ToString();
                Response.Redirect("Thankyou.aspx", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                try
                {
                    //Session["imgPath"] = null;
                    //lblMessage.Text = ex.InnerException.InnerException.Message;
                }
                catch (Exception exe)
                {
                    //Session["imgPath"] = null;
                    //lblMessage.Text = exe.Message;
                }

            }
            // mase
        }
        private string GetMonth(string txt)
        {
            string mm = "";
            switch (txt)
            {
                case "JAN":
                    mm = "01";
                    break;
                case "FEB":
                    mm = "02";
                    break;
                case "MAR":
                    mm = "03";
                    break;
                case "APR":
                    mm = "04";
                    break;
                case "MAY":
                    mm = "05";
                    break;
                case "JUN":
                    mm = "06";
                    break;
                case "JUL":
                    mm = "07";
                    break;
                case "AUG":
                    mm = "08";
                    break;
                case "SEP":
                    mm = "09";
                    break;
                case "OCT":
                    mm = "10";
                    break;
                case "NOV":
                    mm = "11";
                    break;
                case "DEC":
                    mm = "12";
                    break;
            }
            return mm;
        }
        private int CheckDateRage(string FMM, string FYY, string EMM, string EYY)
        {
            int Result = 0;
            DateTime DT1, DT2;

            try
            {
                string dt1str = FMM + "/" + "01" + "/" + FYY;
                DT1 = Convert.ToDateTime(dt1str);
                if (EMM != "TILL" || EYY != "TILL")
                {
                    if (EMM == "TILL" || EYY == "TILL")
                    {
                        DT2 = DateTime.Now;
                    }
                    string dt2str = EMM + "/" + "01" + "/" + EYY;
                    DT2 = Convert.ToDateTime(dt2str);
                }
                //int DateDiff = DT1.CompareTo(DT2); 
            }
            catch (Exception)
            {

                throw;
            }

            return Result;
        }

        private bool CheckCaptcha()
        {
            bool Result = false;
            string txt = txtcaptcha.Text;
            return Result;
        }
        private Random rand = new Random();

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCanName.Text = string.Empty;
            txtAdddressDetails.Text = string.Empty;
            txtAlandLine.Text = string.Empty;
            txtBriefnote.Text = string.Empty;
            txtcaptcha.Text = string.Empty;
            txtcollege.Text = string.Empty;
            txtcollegeyear.Text = string.Empty;
            txtdeg1.Text = string.Empty;
            txtdeg2.Text = string.Empty;
            txtdeg3.Text = string.Empty;
            txtdeg4.Text = string.Empty;
            txtdeg5.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtexpBrief1.Text = string.Empty;
            txtexpBrief2.Text = string.Empty;
            txtexpBrief3.Text = string.Empty;
            txtexpBrief4.Text = string.Empty;
            txtexpBrief5.Text = string.Empty;
            txtexpcom1.Text = string.Empty;
            txtexpcom2.Text = string.Empty;
            txtexpcom3.Text = string.Empty;
            txtexpcom4.Text = string.Empty;
            txtexpcom5.Text = string.Empty;
            txtexppos1.Text = string.Empty;
            txtexppos2.Text = string.Empty;
            txtexppos3.Text = string.Empty;
            txtexppos4.Text = string.Empty;
            txtexppos5.Text = string.Empty;
            txtHobbies.Text = string.Empty;
            txtins1.Text = string.Empty;
            txtins2.Text = string.Empty;
            txtins3.Text = string.Empty;
            txtins4.Text = string.Empty;
            txtins5.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtoth3.Text = string.Empty;
            txtoth4.Text = string.Empty;
            txtOtherCity.Text = string.Empty;
            txtpass1.Text = string.Empty;
            txtpass2.Text = string.Empty;
            txtpass3.Text = string.Empty;
            txtpass4.Text = string.Empty;
            txtpass5.Text = string.Empty;
            //txtPlaceofbirth.Text = string.Empty;
            txtschool.Text = string.Empty;
            txtschoolyear.Text = string.Empty;
            txttrg1.Text = string.Empty;
            txttrg2.Text = string.Empty;
            txttrg3.Text = string.Empty;
            txttrg4.Text = string.Empty;
            txttrgcourse1.Text = string.Empty;
            txttrgcourse2.Text = string.Empty;
            txttrgcourse3.Text = string.Empty;
            txttrgcourse4.Text = string.Empty;
            txttrgdur1.Text = string.Empty;
            txttrgdur2.Text = string.Empty;
            txttrgdur3.Text = string.Empty;
            txttrgdur4.Text = string.Empty;
            txttrgyear1.Text = string.Empty;
            txttrgyear2.Text = string.Empty;
            txttrgyear3.Text = string.Empty;
            txttrgyear4.Text = string.Empty;
            txtuni1.Text = string.Empty;
            txtuni2.Text = string.Empty;
            txtuni3.Text = string.Empty;
            txtuniyear1.Text = string.Empty;
            txtuniyear2.Text = string.Empty;
            txtuniyear3.Text = string.Empty;
            ddlCity.SelectedIndex = 0;
            ddlexpcomMME1.SelectedIndex = 0;
            ddlexpcomMME2.SelectedIndex = 0;
            ddlexpcomMME3.SelectedIndex = 0;
            ddlexpcomMME4.SelectedIndex = 0;
            ddlexpcomMME5.SelectedIndex = 0;
            ddlexpcomMMS1.SelectedIndex = 0;
            ddlexpcomMMS2.SelectedIndex = 0;
            ddlexpcomMMS3.SelectedIndex = 0;
            ddlexpcomMMS4.SelectedIndex = 0;
            ddlexpcomMMS5.SelectedIndex = 0;
            ddlexpcomYYE1.SelectedIndex = 0;
            ddlexpcomYYE2.SelectedIndex = 0;
            ddlexpcomYYE3.SelectedIndex = 0;
            ddlexpcomYYE4.SelectedIndex = 0;
            ddlexpcomYYE5.SelectedIndex = 0;
            ddlexpcomYYS1.SelectedIndex = 0;
            ddlexpcomYYS2.SelectedIndex = 0;
            ddlexpcomYYS3.SelectedIndex = 0;
            ddlexpcomYYS4.SelectedIndex = 0;
            ddlexpcomYYS5.SelectedIndex = 0;
            ddlRead1.SelectedIndex = 0;
            ddlRead2.SelectedIndex = 0;
            ddlRead3.SelectedIndex = 0;
            ddlRead4.SelectedIndex = 0;
            ddlspeak1.SelectedIndex = 0;
            ddlspeak2.SelectedIndex = 0;
            ddlspeak3.SelectedIndex = 0;
            ddlspeak4.SelectedIndex = 0;
            ddlwrite1.SelectedIndex = 0;
            ddlwrite2.SelectedIndex = 0;
            ddlwrite3.SelectedIndex = 0;
            ddlwrite4.SelectedIndex = 0;
            txtcaptcha.Text = string.Empty;
            txtoth3.Text = string.Empty;
            txtoth4.Text = string.Empty; ;
        }
        //
        protected void ddlPositionApplied_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrOthers.Text = string.Empty;
        }

        protected void ddlReadytoJoin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   ErrReady.Text = string.Empty;
        }

        protected void txtOthers_TextChanged(object sender, EventArgs e)
        {
            ErrOthers.Text = string.Empty;
        }

        protected void txtcaptcha_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnResetCaptcha_Click(object sender, EventArgs e)
        {
            txtcaptcha.Text = string.Empty;
            GetCaptcha();
        }

    }
}

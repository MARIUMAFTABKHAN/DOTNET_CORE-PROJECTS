using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class ContactReport : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtmvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtfvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");

                PopulateMainCatDropdown();

                if (Request.QueryString["ID"] != null && Request.QueryString["Mode"] == "Edit")
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    LoadContactData(id);
                    btnSave.Text = "Update";
                }

            }
        }

        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Category_Title
                }).ToList();

            ddlmaincat.DataSource = maincat;
            ddlmaincat.DataValueField = "Id";
            ddlmaincat.DataTextField = "Category_Title";
            ddlmaincat.DataBind();

            ddlmaincat.Items.Insert(0, new ListItem("Select Main Category", ""));
        }
        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCatDropdown();
        }
        private void PopulateSubCatDropdown()
        {
            int selectedmaincatId = Convert.ToInt32(ddlmaincat.SelectedValue);

            var subcat = db.SubCategories
                .Where(mc => mc.Status == "A" && mc.Main_Category == selectedmaincatId)
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Main_Category,
                    mc.Category_Title
                }).ToList();

            ddlsubcat.DataSource = subcat;
            ddlsubcat.DataValueField = "Id";
            ddlsubcat.DataTextField = "Category_Title";
            ddlsubcat.DataBind();

            ddlsubcat.Items.Insert(0, new ListItem("Select Sub Category", ""));
        }


        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Detect if we're editing an existing record
                bool isEditMode = Request.QueryString["Id"] != null;
                int contactId = isEditMode ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                // 1. Optional: retrieve location data from hidden fields
                double? latitude = 24.83187891581958;
                double? longitude = 67.07940132324741;

                if (double.TryParse(hdnLatitude.Value, out double lat))
                    latitude = lat;
                if (double.TryParse(hdnLongitude.Value, out double lon))
                    longitude = lon;

                string visitToRaw = Request.Form["optionsRadios"];
                string visitTo = visitToRaw == "Agency" ? "A" : visitToRaw == "Client" ? "C" : null;

                if (string.IsNullOrEmpty(visitTo))
                {
                    lblmessage.Text = "Visit To selection is required.";
                    return;
                }

                //string followupVisitTo = Request.Form["optionsRadios2"]; // for Follow-up Visit To
                string visitToRaw2 = Request.Form["optionsRadios2"];
                string followupVisitTo = visitToRaw2 == "Agency" ? "A" : visitToRaw2 == "Client" ? "C" : null;

                if (string.IsNullOrEmpty(followupVisitTo))
                {
                    lblmessage.Text = "Follow up Visit To selection is required.";
                    return;
                }

                //string followupVisitMode = Request.Form["optionsRadios3"]; // for Mode
                string modeRaw = Request.Form["optionsRadios3"];
                string followupmode = modeRaw == "Visit" ? "V" : modeRaw == "Call" ? "C" : modeRaw == "NoFollowUp" ? "N" : null;

                if (string.IsNullOrEmpty(followupVisitTo))
                {
                    lblmessage.Text = "Follow up Mode selection is required.";
                    return;
                }


                TimeSpan? parsedVisitTime = null;
                TimeSpan? parsedFollowTime = null;

                // Convert 12-hour string like "2:15 PM" to TimeSpan
                if (DateTime.TryParseExact(txtmvisisttime.Text.Trim(), "h:mm tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime visitDT))
                {
                    parsedVisitTime = visitDT.TimeOfDay;
                }
                else
                {
                    lblmessage.Text = "Invalid visit time format.";
                    return;
                }

                if (DateTime.TryParseExact(txtfvisisttime.Text.Trim(), "h:mm tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime followDT))
                {
                    parsedFollowTime = followDT.TimeOfDay;
                }
                else
                {
                    lblmessage.Text = "Invalid follow-up time format.";
                    return;
                }

                int agencyClientId;

                if (visitTo == "C")
                {
                    if (!int.TryParse(hiddenClientId.Value, out agencyClientId))
                    {
                        lblmessage.Text = "Invalid or missing Client ID.";
                        return;
                    }
                }
                else if (visitTo == "A")
                {
                    if (!int.TryParse(hiddenAgencyId.Value, out agencyClientId))
                    {
                        lblmessage.Text = "Invalid or missing Agency ID.";
                        return;
                    }
                }
                else
                {
                    lblmessage.Text = "VisitTo is invalid.";
                    return;
                }

                //if (!int.TryParse(hiddenBrandId.Value, out int brandId))
                //{
                //    lblmessage.Text = "Invalid or missing Brand ID.";
                //    return;
                //}

                int? brandId = null;
                //if (int.TryParse(hiddenBrandId.Value, out int parsedBrandId))
                //{
                //    brandId = parsedBrandId;
                //}
                if (string.IsNullOrWhiteSpace(txtBrand.Text))
                {
                    hiddenBrandId.Value = "";
                }
                else if (int.TryParse(hiddenBrandId.Value, out int parsedBrandId))
                {
                    brandId = parsedBrandId;
                }

                // 2. Prepare masterData object
                var masterData = new
                {
                    VisitTo = visitTo, // "C" or "A"
                    MainCat = int.TryParse(ddlmaincat.SelectedValue, out int mc) ? mc : (int?)null,
                    SubCat = int.TryParse(ddlsubcat.SelectedValue, out int sc) ? sc : (int?)null,
                    AgencyClientId = agencyClientId,
                    BrandId = brandId.HasValue ? brandId.Value : (int?)null,
                    VisitDate = txtmvisitdate.Value,
                    VisitTime = parsedVisitTime,
                    MinutesOfMeetings = txtMinutesOfMeeting.Text,
                    PrVisit = chprvisit.Checked,
                    NewExecutive = chnew.Checked,
                    NewexecutiveName = txtnewexename.Text,
                    FollowupVisitTo = followupVisitTo,
                    FollowupVisitMode = followupmode,
                    FollowupVisitTime = parsedFollowTime,
                    FollowupVisitDate = txtfvisitdate.Value,
                    RecAddedBy = Request.Cookies["UserId"]?.Value,
                    RecAddedDatetime = DateTime.Now,
                    WindowsId = Request.Browser.Platform,
                    SystemName = Environment.MachineName,
                    GenerationDateTime = DateTime.Now,
                    Mode="W",
                    Desgn=txtdesgn.Text,
                    PersonName=txtprname.Text

                };

                //using (HttpClient httpClient = new HttpClient())
                //{
                //    string masterJson = JsonConvert.SerializeObject(masterData, new JsonSerializerSettings
                //    {
                //        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                //    });


                //    var masterResponse = await httpClient.PostAsync(
                //        "http://172.17.0.16:89/api/AddContact/add",
                //        new StringContent(masterJson, System.Text.Encoding.UTF8, "application/json")
                //    );

                //    if (!masterResponse.IsSuccessStatusCode)
                //    {
                //        string error = await masterResponse.Content.ReadAsStringAsync();
                //        lblmessage.Text = "Failed to save master data: " + error;
                //        return;
                //    }

                //    dynamic masterResult = JsonConvert.DeserializeObject(await masterResponse.Content.ReadAsStringAsync());
                //    int contactId = masterResult.recId;


                //    // 3. Post detail data
                //    var detailData = new
                //    {
                //        contactreport = contactId,
                //        latitude = latitude,
                //        longitude = longitude
                //    };

                //    string detailJson = JsonConvert.SerializeObject(detailData, new JsonSerializerSettings
                //    {
                //        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                //    });


                //    var detailResponse = await httpClient.PostAsync(
                //        "http://172.17.0.16:89/api/AddContactDetail/add-details",
                //        new StringContent(detailJson, System.Text.Encoding.UTF8, "application/json")
                //    );

                //    if (!detailResponse.IsSuccessStatusCode)
                //    {
                //        lblmessage.Text = "Failed to save location data.";
                //        return;
                //    }

                //    lblmessage.ForeColor = System.Drawing.Color.Green;
                //    lblmessage.Text = "Contact saved successfully.";
                //}

                using (HttpClient httpClient = new HttpClient())
                {
                    string masterJson = JsonConvert.SerializeObject(masterData);

                    // Determine URL and method
                    string masterUrl = isEditMode
                        ? $"http://172.17.0.16:89/api/AddContact/update/{contactId}"
                        : "http://172.17.0.16:89/api/AddContact/add";

                    HttpRequestMessage masterRequest = new HttpRequestMessage
                    {
                        Method = isEditMode ? HttpMethod.Put : HttpMethod.Post,
                        RequestUri = new Uri(masterUrl),
                        Content = new StringContent(masterJson, System.Text.Encoding.UTF8, "application/json")
                    };

                    HttpResponseMessage masterResponse = await httpClient.SendAsync(masterRequest);
                    if (!masterResponse.IsSuccessStatusCode)
                    {
                        string error = await masterResponse.Content.ReadAsStringAsync();
                        lblmessage.Text = "Failed to save master data: " + error;
                        return;
                    }

                    // Get contactId from response
                    if (!isEditMode)
                    {
                        dynamic masterResult = JsonConvert.DeserializeObject(await masterResponse.Content.ReadAsStringAsync());
                        contactId = masterResult.recId;
                    }

                    // 3. Prepare and send detail data
                    var detailData = new
                    {
                        contactreport = contactId,
                        latitude = latitude,
                        longitude = longitude
                    };

                    string detailJson = JsonConvert.SerializeObject(detailData);

                    string detailUrl = isEditMode
                        ? $"http://172.17.0.16:89/api/AddContactDetail/update-details/by-contact/{contactId}"
                        : "http://172.17.0.16:89/api/AddContactDetail/add-details";

                    HttpRequestMessage detailRequest = new HttpRequestMessage
                    {
                        Method = isEditMode ? HttpMethod.Put : HttpMethod.Post,
                        RequestUri = new Uri(detailUrl),
                        Content = new StringContent(detailJson, System.Text.Encoding.UTF8, "application/json")
                    };

                    HttpResponseMessage detailResponse = await httpClient.SendAsync(detailRequest);
                    if (!detailResponse.IsSuccessStatusCode)
                    {
                        lblmessage.Text = "Failed to save/update location data.";
                        return;
                    }

                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    lblmessage.Text = isEditMode ? "Contact updated successfully." : "Contact saved successfully.";
                } 

            }
            catch (Exception ex)
            {
                lblmessage.Text = "Error: " + ex.Message;
            }
        }

        private void LoadContactData(int id)
        {
            var contact = db.contactreport_mobile.FirstOrDefault(x => x.id == id);
            if (contact == null)
            {
                lblmessage.Text = "Record not found.";
                return;
            }

            // Main and Sub category
            if (contact.main_cat != null)
            {
                ddlmaincat.SelectedValue = contact.main_cat.ToString();
                PopulateSubCatDropdown(); // Must call this BEFORE setting subcat
            }

            if (contact.sub_cat != null)
                ddlsubcat.SelectedValue = contact.sub_cat.ToString();

            // VisitTo: Agency (A) or Client (C)
            if (contact.visit_to == "C")
            {
                hiddenClientId.Value = contact.agency_client_id.ToString();
                var client = db.ClientCompanies.FirstOrDefault(c => c.Id == contact.agency_client_id);
                txtClient.Text = client?.Client_Name ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "selectClientRadio", "document.getElementById('Client').checked = true;", true);
            }
            else if (contact.visit_to == "A")
            {
                hiddenAgencyId.Value = contact.agency_client_id.ToString();
                var agency = db.Agencies.FirstOrDefault(a => a.Id == contact.agency_client_id);
                txtAgency.Text = agency?.Agency_Name ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "selectAgencyRadio", "document.getElementById('Agency').checked = true;", true);
            }

            // Brand
            hiddenBrandId.Value = contact.brand_id.ToString();
            var brand = db.Brands.FirstOrDefault(b => b.Id == contact.brand_id);
            txtBrand.Text = brand?.Brand_Name ?? "";

            // Visit Date & Time
            txtmvisitdate.Value = contact.visit_date.ToString("yyyy-MM-dd");

            if (contact.visit_time.HasValue)
            {
                txtmvisisttime.Text = DateTime.Today
                    .Add(contact.visit_time.Value)
                    .ToString("h:mm tt", CultureInfo.InvariantCulture);
            }
            // Minutes of Meeting
            txtMinutesOfMeeting.Text = contact.minutes_of_meetings;

            // PR Visit / New Exec
            chprvisit.Checked = contact.pr_visit.GetValueOrDefault();
            chnew.Checked = contact.new_executive.GetValueOrDefault();

            txtnewexename.Text = contact.newexecutive_name;
            txtdesgn.Text = contact.Desgn;
            txtprname.Text = contact.Person_name;
            // Follow-up
            txtfvisitdate.Value = contact.followup_visit_date?.ToString("yyyy-MM-dd");

            if (contact.followup_visit_time.HasValue)
                txtfvisisttime.Text = DateTime.Today.Add(contact.followup_visit_time.Value).ToString("h:mm tt");

            if (contact.followup_visit_to == "C")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "followClient", "document.getElementById('Client2').checked = true;", true);
            }
            else if (contact.followup_visit_to == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "followAgency", "document.getElementById('Agency2').checked = true;", true);
            }

            switch (contact.followup_visit_mode)
            {
                case "V":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modeVisit", "document.getElementById('Visit').checked = true;", true);
                    break;
                case "C":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modeCall", "document.getElementById('Call').checked = true;", true);
                    break;
                case "N":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modeNone", "document.getElementById('NoFollowUp').checked = true;", true);
                    break;
            }

            // Load Detail (Location data)
            var detail = db.Contactreportmobile_Detail.FirstOrDefault(d => d.contactreport == id);
            if (detail != null)
            {
                hdnLatitude.Value = detail.Latitude?.ToString() ?? string.Empty;
                hdnLongitude.Value = detail.Longitude?.ToString() ?? string.Empty;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            

            //txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            ddlmaincat.SelectedIndex = 0;
            ddlsubcat.SelectedIndex = 0;
            txtClient.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtmvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtMinutesOfMeeting.Text = string.Empty;
            txtnewexename.Text = string.Empty;
            txtfvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //chkmaincat.SelectedIndex = 0;

            //gvfirst.DataSource = null;
            //gvfirst.DataBind();
            //lblMsg.Text = "";
        }
    }
}
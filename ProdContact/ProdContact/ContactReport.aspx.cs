
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

namespace ProdContact
{
    public partial class ContactReport : System.Web.UI.Page
    {
        ExProdEntities1 db = new ExProdEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtmvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtfvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");

                if (Request.QueryString["ID"] != null && Request.QueryString["Mode"] == "Edit")
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    LoadContactData(id);
                    btnSave.Text = "Update";
                }

            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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

                string visitToRaw2 = Request.Form["optionsRadios2"];
                string followupVisitTo = visitToRaw2 == "Agency" ? "A" : visitToRaw2 == "Client" ? "C" : null;

                if (string.IsNullOrEmpty(followupVisitTo))
                {
                    lblmessage.Text = "Follow up Visit To selection is required.";
                    return;
                }

                string modeRaw = Request.Form["optionsRadios3"];
                string followupmode = modeRaw == "Visit" ? "V" : modeRaw == "Call" ? "C" : modeRaw == "NoFollowUp" ? "N" : null;

                if (string.IsNullOrEmpty(followupVisitTo))
                {
                    lblmessage.Text = "Follow up Mode selection is required.";
                    return;
                }
                TimeSpan? parsedVisitTime = null;
                TimeSpan? parsedFollowTime = null;

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

                int? projectId = null;
                
                if (string.IsNullOrWhiteSpace(txtproject.Text))
                {
                    hiddenProjectId.Value = "";
                }
                else if (int.TryParse(hiddenProjectId.Value, out int parsedProjectId))
                {
                    projectId = parsedProjectId;
                }

                // 2. Prepare masterData object
                var masterData = new
                {
                    VisitTo = visitTo, // "C" or "A"
                    AgencyClientId = agencyClientId,
                    ProjectId = projectId.HasValue ? projectId.Value : (int?)null,
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

            
            if (contact.visit_to == "C")
            {
                hiddenClientId.Value = contact.agency_client_id.ToString();
                var client = db.tblClients.FirstOrDefault(c => c.ClientId == contact.agency_client_id);
                txtClient.Text = client?.Name ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "selectClientRadio", "document.getElementById('Client').checked = true;", true);
            }
            else if (contact.visit_to == "A")
            {
                hiddenAgencyId.Value = contact.agency_client_id.ToString();
                var agency = db.tblAgencies.FirstOrDefault(a => a.AgencyId == contact.agency_client_id);
                txtAgency.Text = agency?.Name ?? "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "selectAgencyRadio", "document.getElementById('Agency').checked = true;", true);
            }

            // Brand
            hiddenProjectId.Value = contact.project_id.ToString();
            var brand = db.Projects.FirstOrDefault(b => b.ProjectId == contact.project_id);
            txtproject.Text = brand?.ProjectName ?? "";

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
            txtClient.Text = string.Empty;
            txtproject.Text = string.Empty;
            txtmvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtMinutesOfMeeting.Text = string.Empty;
            txtnewexename.Text = string.Empty;
            txtfvisitdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
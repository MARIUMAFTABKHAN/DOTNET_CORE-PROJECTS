using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class RptInvoice : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();

        public RptInvoice()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/"));
                _isSqlTypesLoaded = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {


                    int InvoiceID = Convert.ToInt32(Request.QueryString[0]);
                    var ss = db.usp_PrintInvoice(InvoiceID).Take(1).SingleOrDefault();
                    var s = db.usp_PrintInvoice(InvoiceID).ToList();
                    string myWords = "";// NumberToWords.ConvertAmount(Convert.ToDouble(ss.NetReceiable));
                                        //    ReportParameter rp = new ReportParameter("pmToWords", myWords);
                                        //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                        //    //set path of the Local report  
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice.rdlc";
                    //creating object of DataSet dsEmployee and filling the DataSet using SQLDataAdapter  
                    //dsEmployee dsemp = new dsEmployee();
                    //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Sample;Integrated Security=true;");
                    //con.Open();
                    //SqlDataAdapter adapt = new SqlDataAdapter("select * from tbl_Employee", con);
                    //adapt.Fill(dsemp, "DataTable1");
                    //con.Close();
                    //Providing DataSource for the Report  
                    ReportDataSource rds = new ReportDataSource("DSInvoice", s);
                    // ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                    ReportViewer1.LocalReport.DataSources.Clear();
                    //Add ReportDataSource  
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch (Exception)
                {

                    //    throw;
                }
            }
        }
    }
}
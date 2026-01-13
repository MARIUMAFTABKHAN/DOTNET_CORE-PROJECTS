using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class RateCardView : BaseClass
    {
        Model1Container db=new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            // Step 1: Retrieve data from the database
            var rateCards = db.RateCards.ToList();
            var publications = db.Publications.ToList();
            var mainCategories = db.MainCategories.ToList();

            //var result = from rateCard in db.RateCards
            //             join publication in db.Publications on Convert.ToInt32(rateCard.Publication.ToString()) equals publication.Id
            //             join mainCategory in db.MainCategories on rateCard.Main_Category equals mainCategory.Id
            //             select new
            //             {
            //                RateCardId = rateCard.Id,
            //                PublicationId = rateCard.Publication,
            //                PublicationName = publication.Publication_Name,
            //                MainCategoryId = rateCard.Main_Category,
            //                CategoryTitle = mainCategory.Category_Title,
            //                rateCard.EffectiveFrom
            //             };

            var result = from rateCard in rateCards
                         join publication in publications
                         on int.Parse(rateCard.Publication.Trim()) equals publication.Id  // Convert the char/str Publication to int in-memory
                         join mainCategory in mainCategories
                         on rateCard.Main_Category equals mainCategory.Id
                         select new
                         {
                             RateCardId = rateCard.Id,
                             PublicationId = rateCard.Publication,
                             PublicationName = publication.Publication_Name,
                             MainCategoryId = rateCard.Main_Category,
                             CategoryTitle = mainCategory.Category_Title,
                             EffectiveFrom = rateCard.EffectiveFrom.HasValue ?
                                     rateCard.EffectiveFrom.Value.ToShortDateString() :
                                     string.Empty // If null, display an empty string
                         };

            // Step 3: Convert the result to a DataTable
            DataTable dt = Helper.ToDataTable(result.ToList());

            ViewState["dt"] = dt;
            if (gv != null)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                lblmessage.Text = "Error: GridView control is not available.";
            }
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        
        protected void btnview_Click(object sender, EventArgs e)
        {
            Response.Redirect("RateCardform.aspx");
        }
    }
}
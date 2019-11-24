using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Admin
{
    public partial class feedback : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                DataTable feedbackdt = FeedBackMst.GetAllActiveData();
                GvFeed.DataSource = feedbackdt;
                GvFeed.DataBind();
                lbl.Text = "Total Record = " + GvFeed.Rows.Count.ToString();
            }
        }
        protected void GvFeed_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //  FeedAdapter.Delete(Convert.ToInt32(GvFeed.DataKeys[e.RowIndex].Value));
            DataTable feedbackdt = FeedBackMst.GetAllActiveData();
            GvFeed.DataSource = feedbackdt;
            GvFeed.DataBind();
            lbl.Text = "Total Record = " + GvFeed.Rows.Count.ToString();
        }
    }
}
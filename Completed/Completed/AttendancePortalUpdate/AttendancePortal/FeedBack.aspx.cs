using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal
{
    public partial class FeedBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";

        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfeed.Text == "")
                {
                    lbl.Text = " feedback can't b blank";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }
                if (txtemail.Text == "")
                {
                    lbl.Text = " email can't b blank";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }
                FeedBackMst feedBack = new FeedBackMst();
                feedBack.Feedback = txtfeed.Text;
                feedBack.Email = txtemail.Text;
                feedBack.Mobile = txtcont.Text;
                feedBack.Insert();
                lbl.Text = "Feedback Sent !!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                txtfeed.Text = "";
                txtemail.Text = "";
                txtcont.Text = "";
            }
            catch (Exception)
            {

                lbl.Text = " Fail to add record";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
            
            

        }
    }
}
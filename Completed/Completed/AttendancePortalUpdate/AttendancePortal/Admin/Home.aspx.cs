using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtuname.Text == "admin" && txtupass.Text == "123")
            {

                Response.Redirect("AddStd.aspx");
            }
            else
            {
                lbl.Text = "Invalid detail";

            }
        }
    }
}
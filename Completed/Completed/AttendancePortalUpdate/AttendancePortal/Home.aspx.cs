using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblstafferror.Text = "";
            lblstuerror.Text = "";
        }
        protected void btnstafflogin_Click(object sender, EventArgs e)
        {
            DataRow StaffDr = StaffMst.StaffLogin(txtstaffuname.Text, txtstaffpass.Text);
            if (StaffDr != null)
            {
                Session["uname"] = txtstaffuname.Text;
                Session["SID"] = StaffDr["SID"].ToString();
                Response.Redirect("Staff/Default.aspx");


            }
            else
            {
                lblstafferror.Text = "Login Error !!";
            }

        }
        protected void btnstudenlogin_Click(object sender, EventArgs e)
        {
            DataRow StuDr = StudentMst.StdLogin(txtstuuname.Text, txtstupass.Text);
            if (StuDr != null)
            {
                Session["sname"] = txtstuuname.Text;
                Session["SID"] = StuDr["SID"].ToString();
                Response.Redirect("Student/Main.aspx");
            }
            else
            {

                lblstuerror.Text = "Login Error !!";
            }
        }
    }
}
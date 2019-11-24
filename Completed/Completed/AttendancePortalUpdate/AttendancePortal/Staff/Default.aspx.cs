using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Staff
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataRow staffdr = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));
                if (staffdr != null)
                {
                    lblname.Text = staffdr["Name"].ToString();
                    lblstd.Text = staffdr["stdname"].ToString();
                    txtemail.Text = staffdr["email"].ToString();
                    txtmobile.Text = staffdr["mobile"].ToString();
                    txtadd.Text = staffdr["add"].ToString();
                    txtcity.Text = staffdr["city"].ToString();
                    txtpin.Text = staffdr["pincode"].ToString();
                    Imgprofile.ImageUrl = staffdr["image"].ToString();
                    ViewState["sid"] = staffdr["SID"].ToString();
                }

            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {

         
            StaffMst staff = new StaffMst(Convert.ToInt32(ViewState["sid"].ToString()));
           /// staff.SID = Convert.ToInt32(ViewState["sid"].ToString());
            staff.StdName = txtadd.Text;
            staff.Email = txtemail.Text;
            staff.Mobile = txtmobile.Text;
            staff.Image = Imgprofile.ImageUrl.ToString();
            staff.Pincode = txtpin.Text;
            staff.City = txtcity.Text;

            staff.UpdateDataByKey();
            Response.Redirect("/Staff/Default.aspx", false);
            }
            catch(Exception ex)
            {
                lblcnt.Text = "Fail to update profile";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }


        }
        protected void btnchange_Click(object sender, EventArgs e)
        {
            FileUpload1.SaveAs(Server.MapPath("~/StaffImg/" + FileUpload1.FileName));
            Imgprofile.ImageUrl = "~/StaffImg/" + FileUpload1.FileName.ToString();

        }
    }
}
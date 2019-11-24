using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace AttendancePortal.Student
{
    public partial class MyProfile : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                int id = 1;
              DataRow Studr = StudentMst.GetDataByKey(id);
                if (Studr != null)
                {
                    lblname.Text = Studr["Name"].ToString();
                    lblroll.Text = Studr["rollno"].ToString();

                    txtemail.Text = Studr["email"].ToString();
                    txtmobile.Text = Studr["mobile"].ToString();
                    txtadd.Text = Studr["add"].ToString();
                    txtcity.Text = Studr["city"].ToString();
                    txtpin.Text = Studr["pincode"].ToString();
                    Imgprofile.ImageUrl = Studr["image"].ToString();
                    ViewState["sid"] = Studr["SID"].ToString();
                }
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
           

            //StuAdapter.Update(Convert.ToInt32(ViewState["sid"].ToString()), txtemail.Text, txtmobile.Text, Imgprofile.ImageUrl.ToString(), txtadd.Text, txtcity.Text, txtpin.Text);




        }
        protected void btnchange_Click(object sender, EventArgs e)
        {

            FileUpload1.SaveAs(Server.MapPath("~/Studentimg/" + FileUpload1.FileName));
            Imgprofile.ImageUrl = "~/Studentimg/" + FileUpload1.FileName.ToString();


        }
    }
}
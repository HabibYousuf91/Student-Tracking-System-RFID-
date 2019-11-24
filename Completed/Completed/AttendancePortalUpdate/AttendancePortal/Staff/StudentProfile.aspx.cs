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
    public partial class StudentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["SID"] != null)
                {
                    DataRow StaffDT = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));


                    if (StaffDT != null && StaffDT["StdId"].ToString() != "")
                    {
                        DataTable DivDT = DivMst.GetDataByStandardId(int.Parse(StaffDT["StdId"].ToString()));
                        lblstd.Text = StaffDT["StdName"].ToString();
                        drpdiv.DataSource = DivDT;
                        drpdiv.DataTextField = "DivName";
                        drpdiv.DataValueField = "DID";
                        drpdiv.DataBind();
                        drpdiv.Items.Insert(0, "SELECT");
                        drpstudent.Items.Insert(0, "SELECT");
                    }
                }
            }
        }
        protected void drpdiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdiv.SelectedIndex > 0)
            {
                DataTable dt = StudentMst.GetAllResultDataByDivID(int.Parse(drpdiv.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    drpstudent.DataSource = dt;
                    drpstudent.DataTextField = "rollno";
                    drpstudent.DataValueField = "sid";
                    drpstudent.DataBind();
                    drpstudent.Items.Insert(0, "SELECT");
                }
            }
        }
        protected void btnsarch_Click(object sender, EventArgs e)
        {
            DataRow StuDr = StudentMst.GetDataByKey(Convert.ToInt32(drpstudent.SelectedValue));
            if (StuDr != null)
            {
                lblroll.Text = StuDr["rollno"].ToString();
                lblname.Text = StuDr["name"].ToString();
                lblemail.Text = StuDr["email"].ToString();
                lblmobile.Text = StuDr["mobile"].ToString();
                lbldob.Text = StuDr["dob"].ToString();
                lbladd.Text = StuDr["add"].ToString();
                lblcity.Text = StuDr["city"].ToString();
                lblpin.Text = StuDr["pincode"].ToString();
                lbluname.Text = StuDr["uname"].ToString();
                //lblpass.Text = StuDr["pass"].ToString();
                imgg.ImageUrl = StuDr["image"].ToString();
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
            }
        }
    }
}
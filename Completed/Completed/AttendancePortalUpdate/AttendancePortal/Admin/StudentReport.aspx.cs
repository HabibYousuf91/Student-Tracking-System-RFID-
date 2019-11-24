using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Admin
{
    public partial class StudentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                DataTable StdDT = StdMst.GetAllActiveData();
                drpstd.DataSource = StdDT;
                drpstd.DataTextField = "StdName";
                drpstd.DataValueField = "Sid";
                drpstd.DataBind();

                drpstd.Items.Insert(0, "SELECT");
                drpstudent.Items.Insert(0, "SELECT");

                DataTable Student_dt = StudentMst.GetAllActiveData();
                    GvStudent.DataSource = Student_dt;
                    GvStudent.DataBind();
            }
        }
        protected void drpdiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            //int rows = 20;
            //DataTable StuDT = StudentMst.et StdMst.GetAllData(sb.ToString(), ref rows, "");

            //drpstudent.DataSource = StuDT;
            //drpstudent.DataTextField = "rollno";
            //drpstudent.DataValueField = "sid";
            //drpstudent.DataBind();
            //drpstudent.Items.Insert(0, "SELECT");
        }
        protected void btnsarch_Click(object sender, EventArgs e)
        {
            DataRow StuDT = StdMst.GetDataByKey(Convert.ToInt32(drpstudent.SelectedValue));
            if (StuDT != null)
            {
                //lblroll.Text = StuDT["rollno"].ToString();
                //lblname.Text = StuDT["name"].ToString();
                //lblemail.Text = StuDT["email"].ToString();
                //lblmobile.Text = StuDT["mobile"].ToString();
                //lbldob.Text = StuDT["dob"].ToString();
                //lbladd.Text = StuDT["add"].ToString();
                //lblcity.Text = StuDT["city"].ToString();
                //lblpin.Text = StuDT["pincode"].ToString();
                //lbluname.Text = StuDT["uname"].ToString();
                //lblpass.Text = StuDT["pass"].ToString();
                //imgg.ImageUrl = StuDT["image"].ToString();
                //MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                //MultiView1.ActiveViewIndex = -1;
            }

        }
        protected void drpstd_SelectedIndexChanged(object sender, EventArgs e)
        {


            DataTable DivDT = DivMst.GetAllActiveData();
            drpdiv.DataSource = DivDT;
            drpdiv.DataTextField = "DivName";
            drpdiv.DataValueField = "DID";
            drpdiv.DataBind();
            drpdiv.Items.Insert(0, "SELECT");

        }

        protected void GvStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            StudentMst.Delete(Convert.ToInt32(GvStudent.DataKeys[e.RowIndex].Value));
            DataTable StudentDT = StudentMst.GetAllActiveData();
            GvStudent.DataSource = StudentDT;
            GvStudent.DataBind();
        }

        protected void GvStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GvStudent.EditIndex = e.NewEditIndex;

                int StaffId = int.Parse((GvStudent.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text);
                Response.Redirect("AddStudent.aspx?SId=" + StaffId, false);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
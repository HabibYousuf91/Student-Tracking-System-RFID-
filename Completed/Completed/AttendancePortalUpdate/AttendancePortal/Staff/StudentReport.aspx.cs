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
               // drpstudent.Items.Insert(0, "SELECT");

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
            try
            {
                DataTable StuDT = StudentMst.GetAllActiveData();
                if (StuDT != null)
                {
                    DataTable Student_dt = StuDT.Select("DivId= " + drpdiv.SelectedValue).CopyToDataTable();

                    if (Student_dt != null && Student_dt.Rows.Count > 0)
                    {
                       

                        GvStudent.DataSource = Student_dt;
                        GvStudent.DataBind();
                    }
                }
            }
            catch
            {
            }
           
            //else
            //{
            //    //MultiView1.ActiveViewIndex = -1;
            //}

        }
        protected void drpstd_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpstd.SelectedIndex > 0)
            {
                DataTable DivDT = DivMst.GetDataByStandardId(int.Parse(drpstd.SelectedValue));
                drpdiv.DataSource = DivDT;
                drpdiv.DataTextField = "DivName";
                drpdiv.DataValueField = "DID";
                drpdiv.DataBind();
                drpdiv.Items.Insert(0, "SELECT");
            }

        }

     
    }
}
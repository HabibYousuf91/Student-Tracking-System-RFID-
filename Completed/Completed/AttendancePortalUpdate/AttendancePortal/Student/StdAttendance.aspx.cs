using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Student
{
    public partial class StdAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                int month = DateTime.Now.Month; 
                //int.Parse(Session["Sid"].ToString())
                DataTable AttDT = CHECKINOUT.GetAllMonthlyAttendanceRecord(int.Parse(Session["Sid"].ToString()), month);
                if (AttDT != null)
                {
                    //AttDT = AttAdapter.Select_By_ROLLNO(StuDT.Rows[0]["rollno"].ToString());

                    GridView1.DataSource = AttDT;
                    GridView1.DataBind();
                    lbl.Text = "Result = " + GridView1.Rows.Count.ToString();
                }
            }
        }
        protected void btnreport_Click(object sender, EventArgs e)
        {
            if (drpmonth.SelectedIndex == 0)
            {
                lbl.Text = "Select month first !!";
            }
            else
            {
                int month = int.Parse(drpmonth.SelectedValue);

                DataTable AttDT = CHECKINOUT.GetAllMonthlyAttendanceRecord(int.Parse(Session["Sid"].ToString()), month);
                if (AttDT != null)
                {
                    //AttDT = AttAdapter.Select_By_ROLLNO(StuDT.Rows[0]["rollno"].ToString());

                    GridView1.DataSource = AttDT;
                    GridView1.DataBind();
                    lbl.Text = "Result = " + GridView1.Rows.Count.ToString();
                }
                lbl.Text = "Result = " + GridView1.Rows.Count.ToString();
            }
        }
    }
}
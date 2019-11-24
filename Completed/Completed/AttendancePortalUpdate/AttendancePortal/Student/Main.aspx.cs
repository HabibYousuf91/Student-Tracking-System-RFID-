using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AttendancePortal.Student
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {

                //  StuDT = StuAdapter.Select_UNAME(Session["sname"].ToString());

                DataTable std = StdMst.GetAllActiveData();

                int month = DateTime.Now.Month;
                DataTable attendancedt = CHECKINOUT.GetAllMonthlyAttendanceRecord(int.Parse(Session["Sid"].ToString()), month);

                if (attendancedt != null && attendancedt.Rows.Count >0)
                {
                lbltotalatt.Text = attendancedt.Rows.Count.ToString();


               // AttDT = AttAdapter.Select_Roll_Atatus(StuDT.Rows[0]["rollno"].ToString(), "Present");
              
                try
                {
                    lblpresent.Text = attendancedt.Select("Status='Present' OR Status='Holiday'").CopyToDataTable().Rows.Count.ToString();
                }
                catch
                {
                    lblpresent.Text = "0";

                }
                try
                {
                    lblabsent.Text = attendancedt.Select("Status='Abscent'").CopyToDataTable().Rows.Count.ToString();
                }
                catch
                {
                    lblabsent.Text = "0";
                }
                // AttDT = AttAdapter.Select_Roll_Atatus(StuDT.Rows[0]["rollno"].ToString(), "Absent");
               
                try
                {
                    lblleave.Text = attendancedt.Select("Status='Leave'").CopyToDataTable().Rows.Count.ToString();
                }
                catch
                {
                    lblleave.Text = "0";
                }


                DataTable LeaveDT = LeaveMst.GetAllDataRecordByStId("",null, int.Parse(Session["Sid"].ToString()));
                lbltotalleave.Text = LeaveDT.Rows.Count.ToString();


                DataTable CompDT = Complainmst.GetAllDataRecordByStId("", null, int.Parse(Session["Sid"].ToString()));
                lbltotalattcompl.Text = CompDT.Rows.Count.ToString();
                }

            }
        }
    }
}
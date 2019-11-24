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
    public partial class Att : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  lblatt.Text = "";
            // lblcnt.Text = "";
            if (Page.IsPostBack == false)
            {
                DataRow staffdr = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));
                if (staffdr != null)
                {
                    //StdDT = StdAdapter.SelectStd();
                    //drpstd.DataSource = StdDT;
                    //drpstd.DataTextField = "STDName";
                    //drpstd.DataValueField = "SID";
                    //drpstd.DataBind();           
                    //  drpstd.Items.Insert(0, "SELECT");
                    BindStandard(int.Parse(staffdr["StdId"].ToString()));
                    lblstd.Text = staffdr["StdName"].ToString();
                }
            }
        }
        protected void BindStandard(int standardId)
        {

            DataTable DivDT = DivMst.GetDataByStandardId(standardId);
            drpdiv.DataSource = DivDT;
            drpdiv.DataTextField = "DivName";
            drpdiv.DataValueField = "DID";
            drpdiv.DataBind();
            drpdiv.Items.Insert(0, "SELECT");
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                int month = DateTime.Now.Month;
                //if (drpmonth.SelectedIndex > 0)
                //{
                //    month = int.Parse(drpmonth.SelectedValue);
                //}
                DataTable Student = CHECKINOUT.GetAllMonthlyAttendanceRecordByDivId(month, int.Parse(drpdiv.SelectedValue));
                GridView1.DataSource = Student;
                GridView1.DataBind();
            }
            catch (Exception)
            {

               // throw;
            }
            // MultiView1.ActiveViewIndex = 0;

        }

    }
}
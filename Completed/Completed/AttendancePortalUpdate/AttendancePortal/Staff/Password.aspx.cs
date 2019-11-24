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
    public partial class Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnchangepass_Click(object sender, EventArgs e)
        {

            // DataTable StaffDT = StaffMst.Select_Current_Pass(txtcurrent.Text, Session["uname"].ToString());

            DataRow StaffDr = StaffMst.GetNameandPassword( Session["uname"].ToString(),txtcurrent.Text);

            if (StaffDr != null)
            {
                StaffMst staffmst = new StaffMst();
                staffmst.SID =Convert.ToInt32( StaffDr["SID"].ToString());
                staffmst.StdName = StaffDr["StdName"].ToString();
                staffmst.Qualification = StaffDr["StdName"].ToString();
                staffmst.Pass = txtnewpass.Text;
                staffmst.Name = StaffDr["Name"].ToString();
                staffmst.Mobile = StaffDr["Mobile"].ToString();
                staffmst.Image = StaffDr["Image"].ToString();
                staffmst.Pincode = StaffDr["PinCode"].ToString();
                staffmst.Uname = StaffDr["Uname"].ToString();
                staffmst.UpdateDataByKey();
                lbl.Text = "Password Changed";

            }
            else
            {

                lbl.Text = "Invalid Current Pass";
            }

        }
    }
}
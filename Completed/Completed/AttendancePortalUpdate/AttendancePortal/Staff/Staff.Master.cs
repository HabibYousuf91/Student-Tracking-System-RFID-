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
    public partial class Staff : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["SID"] != null)
                {

                    DataRow staffsr= StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));
                    if (staffsr != null)
                    {
                        Image4.ImageUrl = staffsr["Image"].ToString();
                        Label1.Text = "Welcome " + staffsr["name"].ToString();
                        Session["std"] = staffsr["stdname"].ToString();
                    }
                }
            }
        }
    }
}
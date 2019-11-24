using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal
{
    public partial class StaffList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
               DataTable StaffDT = StaffMst.GetAllActiveData();
                GvStaff.DataSource = StaffDT;
                GvStaff.DataBind();
                lblstaff.Text = GvStaff.Rows.Count.ToString();
            }
        }
    }
}
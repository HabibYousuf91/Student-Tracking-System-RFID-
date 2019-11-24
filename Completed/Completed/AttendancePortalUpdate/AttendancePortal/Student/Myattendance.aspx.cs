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
    public partial class Myattendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = CHECKINOUT.GetAllDataRecord(int.Parse(Session["Sid"].ToString()));
                GvAttendance.DataSource = dt;
                GvAttendance.DataBind();
            }
        }
    }
}
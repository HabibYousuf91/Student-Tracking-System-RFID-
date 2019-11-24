using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Admin
{
    public partial class Complain : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {

               
              DataTable  CompDT = Complainmst.GetAllDataRecordByStId("");
                GridView1.DataSource = CompDT;
                GridView1.DataBind();

                lblcomplain.Text = GridView1.Rows.Count.ToString();
            }
        }
    }
}
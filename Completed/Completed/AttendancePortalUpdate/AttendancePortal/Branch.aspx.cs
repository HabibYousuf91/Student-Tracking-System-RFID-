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
    public partial class Branch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = DivMst.GetAllActiveData();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            lblstd.Text = GridView1.Rows.Count.ToString();

        }
    }
    }
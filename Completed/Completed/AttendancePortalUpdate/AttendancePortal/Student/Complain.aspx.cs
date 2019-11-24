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
    public partial class Complain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
              DataTable  CompDT = Complainmst.GetAllActiveData();
                GridView1.DataSource = CompDT;
                GridView1.DataBind();
            }
            lbl.Text = "";
        }
        protected void btnsend_Click(object sender, EventArgs e)
        {
            try
            {

           
            Complainmst complainObj = new Complainmst();
            complainObj.CID = Complainmst.GetNewCID();
            complainObj.Edate = DateTime.Now;
            complainObj.Subject = txtmsg.Text;
            complainObj.Message = txtsubj.Text;
            complainObj.StudentId = int.Parse(Session["SID"].ToString());
            //StuDT = StuAdapter.Select_UNAME(Session["sname"].ToString());
            complainObj.Insert();
            DataTable dtcomplain = Complainmst.GetAllActiveData();
            GridView1.DataSource = dtcomplain;
            GridView1.DataBind();
            lbl.Text = "complain Sent";
            txtmsg.Text = "";
            txtsubj.Text = "";
            }
            catch (Exception ex)
            {

                lbl.Text = "Fail to submit complain";
            }
        }

        }
    }
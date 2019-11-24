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
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                bindstd();
            }
        }

        protected void bindstd()
        {
            DataTable StdDT = StdMst.GetAllActiveData();
            drpstd.DataSource = StdDT;
            drpstd.DataTextField = "STDName";
            drpstd.DataValueField = "SID";
            drpstd.DataBind();
            drpstd.Items.Insert(0, "SELECT");
        }
        protected void btnnewleave_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DataTable LeaveDT = LeaveMst.GetAllDataRecordByStId("Pending");
            //.Select_By_STD_and_STATUS(drpstd.SelectedItem.Text, "Pending");

            GridView1.DataSource = LeaveDT;
            GridView1.DataBind();
            lblnew.Text = GridView1.Rows.Count.ToString();
        }
        protected void btnapprove_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            DataTable LeaveDT = LeaveMst.GetAllDataRecordByStId("Approved");
            //LeaveAdapter.Select_By_STD_and_STATUS(drpstd.SelectedItem.Text, "Approve");

            GridView2.DataSource = LeaveDT;
            GridView2.DataBind();
            lblapp.Text = GridView2.Rows.Count.ToString();

        }
        protected void btnreject_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            DataTable LeaveDT = LeaveMst.GetAllDataRecordByStId("Reject");
            //LeaveAdapter.Select_By_STD_and_STATUS(drpstd.SelectedItem.Text, "Reject");

            GridView3.DataSource = LeaveDT;
            GridView3.DataBind();
            lblrej.Text = GridView3.Rows.Count.ToString();
        }
    }
    }
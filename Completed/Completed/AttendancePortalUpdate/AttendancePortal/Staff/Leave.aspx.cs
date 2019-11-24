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
    public partial class Leave : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                MultiView1.ActiveViewIndex = 0;
                Bindgridview("Pending");
                lblnew.Text = GridView1.Rows.Count.ToString();
            }
        }
        protected void btnnewleave_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Bindgridview("Pending");
            lblnew.Text = GridView1.Rows.Count.ToString();
        }
        protected void btnapprove_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Bindgridview("Approve");
            lblapp.Text = GridView2.Rows.Count.ToString();
        }
        protected void btnreject_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            Bindgridview("Reject");
            lblrej.Text = GridView3.Rows.Count.ToString();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                LeaveMst leaveMst = new LeaveMst();
                leaveMst.LID = int.Parse(e.CommandArgument.ToString());
                leaveMst.Reply = "Approve";
                leaveMst.UpdateDataByKey();

               // LeaveAdapter.LeaveMst_UPDATE_STATU(Convert.ToInt32(e.CommandArgument.ToString()), "Approve");
                MultiView1.ActiveViewIndex = 0;
                Bindgridview("Approve");
                lblnew.Text = GridView1.Rows.Count.ToString();
            }
            else
            {

            }

        }
        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {


        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reject")
            {
                LeaveMst leaveMst = new LeaveMst();
                leaveMst.LID = int.Parse(e.CommandArgument.ToString());
                leaveMst.Reply = "Approved";
                leaveMst.UpdateDataByKey();
                //LeaveAdapter.LeaveMst_UPDATE_STATU(Convert.ToInt32(e.CommandArgument.ToString()), "Reject");
                //leaveMst.UpdateDataByKey();
                Bindgridview("Reject");
                lblapp.Text = GridView2.Rows.Count.ToString();
            }

        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Approve")
            {
                LeaveMst leaveMst = new LeaveMst();
                leaveMst.LID = int.Parse(e.CommandArgument.ToString());
                leaveMst.Reply = "Approved";
                leaveMst.UpdateDataByKey();
                MultiView1.ActiveViewIndex = 2;
                Bindgridview("Approved");
                lblrej.Text = GridView3.Rows.Count.ToString();
            }

        }

        protected void Bindgridview(string status)
        {
            try
            {


                if (Session["SID"] != null)
                {
                    int staffId = int.Parse(Session["SID"].ToString());

                    DataRow drstaff = StaffMst.GetDataByKey(staffId);
                    if (drstaff != null)
                    {
                        DataTable LeaveDT = LeaveMst.GetAllDataRecordByStId(status, int.Parse(drstaff["StdId"].ToString()));
                        GridView3.DataSource = LeaveDT;
                        GridView3.DataBind();
                     
                    }
                }
            }
            catch
            {
            }
        }

    }
}
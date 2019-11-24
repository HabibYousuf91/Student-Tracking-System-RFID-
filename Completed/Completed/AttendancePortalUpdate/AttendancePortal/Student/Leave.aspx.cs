using System;
using System.Collections.Generic;
using System.Data;
using Models;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Student
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";

        }
        protected void btnappluleave_Click(object sender, EventArgs e)
        {
            try
            {

          
            DataRow StuDT = StudentMst.GetDataByKey(int.Parse(Session["SID"].ToString()));
            //St StuAdapter.Select_UNAME(Session["sname"].ToString());
            LeaveMst leaveobj = new LeaveMst();
            leaveobj.LID = LeaveMst.GetNewLID();
            leaveobj.Name = StuDT["Name"].ToString();
            leaveobj.StudentId = int.Parse(Session["SID"].ToString());
            leaveobj.Nodays = DropDownList1.SelectedIndex > 0 ? int.Parse(DropDownList1.SelectedValue) : 1;
            leaveobj.Rollno = "";
            leaveobj.Message = txtmsg.Text;
            leaveobj.Insert();
            lbl.Text = "Apply for leave successfully";
            txtmsg.Text = "";
            DropDownList1.SelectedIndex = 0;
            }
            catch (Exception ex )
            {

                lbl.Text = "Apply for leave failed";
            }

        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            DataRow Studr = StudentMst.GetDataByKey(int.Parse(Session["SID"].ToString()));
                //StuAdapter.Select_UNAME(Session["sname"].ToString());
            DataTable LeaveDT = LeaveMst.GetAllActiveData().Select("studentId = '" + Studr["SID"].ToString() + "'").CopyToDataTable();

            GridView1.DataSource = LeaveDT;
            GridView1.DataBind();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

namespace AttendancePortal.Admin
{
    public partial class AddExam : System.Web.UI.Page
    {
        private int? ExamID
        {
            get { return this.hidStdId.Value !=  "" ?(int?)Convert.ToInt32(this.hidStdId.Value) : (int?)null; }
            set { this.hidStdId.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";
            if (Page.IsPostBack == false)
            {
                BindGridStd();
            }
        }

        protected void btnaddstd_Click(object sender, EventArgs e)
        {
            ExamMst Standard = new ExamMst();
            Standard.Name = txtstdname.Text;
            Standard.Total = txt_TotalMarks.Text != "" ? decimal.Parse(txt_TotalMarks.Text) : 100; ;
            if (txtstdname.Text == "")
            {
                lbl.Text = "Exam name is required.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                return;
            }
            if (ExamID != null)
            {
                Standard.ExamID = int.Parse(ExamID.ToString());
                Standard.UpdateDataByKey();
                lbl.Text = "Exam update Successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                BindGridStd();
                txtstdname.Text = "";
                ExamID = 0;
            }

            else
            {

                Standard.ExamID = ExamMst.GetNewExamID();
                Standard.Insert();
                lbl.Text = "Standered update Successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                hidStdId = null;
                BindGridStd();
                txtstdname.Text = "";
            }

            btnaddstd.Text = "ADD"; 
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtstdname.Text = "";
            btnaddstd.Text = "ADD";
            ExamID = 0;
        }

            private void BindGridStd()
        {
            DataTable StdDT = ExamMst.GetAllActiveData();
            GvStd.DataSource = StdDT;
            GvStd.DataBind();

        }
        protected void GvStd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            StdMst std = new StdMst();

            if (std.Delete(Convert.ToInt32(GvStd.DataKeys[e.RowIndex].Value))==1)
            {
                lbl.Text = "Record Deleted Successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
            else
            {
                lbl.Text = "fail to delete ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
            BindGridStd();

        }
        protected void GvStd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GvStd.EditIndex = e.NewEditIndex;
            DataRow drRole = ExamMst.GetDataByKey(int.Parse((GvStd.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text));
            GvStd.SelectedIndex = e.NewEditIndex;
            ExamID = int.Parse((GvStd.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text);
            txtstdname.Text = drRole["Name"].ToString();
            e.Cancel = true;

            txt_TotalMarks.Text = drRole["Total"].ToString();
            btnaddstd.Text = "Update";
            BindGridStd();

        }

        protected void GvStd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvStd.EditIndex = -1;
            BindGridStd();
        }
        protected void GvStd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tname = GvStd.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
        DataRow dr= ExamMst.GetDataByKey(Convert.ToInt32(GvStd.DataKeys[e.RowIndex].Value));
            if (dr != null)
            {
                ExamMst std = new ExamMst();
                std.ExamID = Convert.ToInt32(GvStd.DataKeys[e.RowIndex].Value);
                std.Name = txtstdname.Text;
                std.Total = txt_TotalMarks.Text != "" ? decimal.Parse(txt_TotalMarks.Text) : 100;
              
                std.UpdateDataByKey();
                lbl.Text = "Record Updated Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
          
            GvStd.EditIndex = -1;
            BindGridStd();

        }
    }
}
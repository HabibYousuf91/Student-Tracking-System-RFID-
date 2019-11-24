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
    public partial class AddStd : System.Web.UI.Page
    {
        private int? StdID
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
            StdMst Standard = new StdMst();
            Standard.StdName = txtstdname.Text;
            Standard.EDate = DateTime.Now;
            if (txtstdname.Text == "")
            {
                lbl.Text = "standard name is required.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                return;
            }
            if (StdID != null)
            {
                Standard.SID = int.Parse(StdID.ToString());
                Standard.UpdateDataByKey();
                lbl.Text = "Standered update Successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                BindGridStd();
                txtstdname.Text = "";
                StdID = 0;
            }

            else
            {
               
                Standard.SID = StdMst.GetNewSID();
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
            StdID = 0;
        }

            private void BindGridStd()
        {
          DataTable  StdDT = StdMst.GetAllActiveData();
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
            DataRow drRole = StdMst.GetDataByKey(int.Parse((GvStd.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text));
            GvStd.SelectedIndex = e.NewEditIndex;
            StdID = int.Parse((GvStd.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text);
            txtstdname.Text = drRole["StdName"].ToString();
            e.Cancel = true;

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
        DataRow dr= StdMst.GetDataByKey(Convert.ToInt32(GvStd.DataKeys[e.RowIndex].Value));
            if (dr != null)
            {
                StdMst std = new StdMst();
                std.SID = Convert.ToInt32(GvStd.DataKeys[e.RowIndex].Value);
                std.StdName = txtstdname.Text;
                std.EDate = DateTime.Parse(dr["EDate"].ToString());
                std.UpdateDataByKey();
                lbl.Text = "Record Updated Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
          
            GvStd.EditIndex = -1;
            BindGridStd();

        }
    }
}
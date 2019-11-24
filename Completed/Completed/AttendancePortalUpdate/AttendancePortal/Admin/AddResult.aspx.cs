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
    public partial class AddResult : System.Web.UI.Page
    {
        private int? DivId
        {
            get { return this.hidStdId.Value != "" ? (int?)Convert.ToInt32(this.hidStdId.Value) : (int?)null; }
            set { this.hidStdId.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";
            if (!Page.IsPostBack)
            {
                BindStandard();
                BindGridiv();
            }
        }

        private void BindStandard()
        {
            DataTable StdDT = StdMst.GetAllActiveData();
            drpstd.DataSource = StdDT;
            drpstd.DataTextField = "STDName";
            drpstd.DataValueField = "SID";
            drpstd.DataBind();
            
            drpstd.Items.Insert(0, new ListItem("Select", "-1"));
        }
        private void BindGridiv()
        {

            DataTable DivDT = DivMst.GetAllActiveData();
            GvDiv.DataSource = DivDT;
            GvDiv.DataBind();
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdname.Text != "" && txtseat.Text != "")
                {
                    DivMst objDiv = new DivMst();
                    objDiv.DivName = txtdname.Text;
                    if (drpstd.SelectedIndex > 0)
                    {
                        objDiv.StdName = drpstd.SelectedItem.Text;
                        objDiv.StdId = int.Parse(drpstd.SelectedValue);
                    }
                    objDiv.Seat = txtseat.Text != null ? int.Parse(txtseat.Text) : 0;
                    if (DivId == null)
                    {
                        objDiv.DID = DivMst.GetNewDID();

                        objDiv.Insert();
                        // objDiv.Insert(txtdname.Text, drpstd.SelectedItem.Text, Convert.ToInt32(txtseat.Text));
                        lbl.Text = "Record Added Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);

                    }
                    else
                    {
                        objDiv.DID = int.Parse(DivId.ToString());
                        objDiv.UpdateDataByKey();
                        lbl.Text = "Record Updated Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut", true);

                    }
                    drpstd.SelectedIndex = 0;
                    BindGridiv();
                    txtdname.Text = "";
                    txtseat.Text = "";
                    drpstd.SelectedIndex = 0;
                    btnadd.Text = "ADD";
                }
                else
                {
                    lbl.Text = "division Name and no of seats can't null";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut", true);
                }
            }
            catch (Exception ex)
            {

                lbl.Text = "Fail to add a record";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
           
        }
        protected void GvDiv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DivMst.Delete(Convert.ToInt32(GvDiv.DataKeys[e.RowIndex].Value));
            lbl.Text = "Record Deleted Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut", true);
            BindGridiv();
        }
        protected void GvDiv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GvDiv.EditIndex = e.NewEditIndex;
            DataRow drdiv = DivMst.GetDataByKey(int.Parse((GvDiv.Rows[e.NewEditIndex].FindControl("lbl_DID") as Label).Text));
            GvDiv.SelectedIndex = e.NewEditIndex;
            DivId = int.Parse((GvDiv.Rows[e.NewEditIndex].FindControl("lbl_DID") as Label).Text);
            txtdname.Text = drdiv["DivName"].ToString();
            txtseat.Text = drdiv["Seat"].ToString();
            if(!string.IsNullOrEmpty(drdiv["StdId"].ToString()))
            drpstd.SelectedValue = drdiv["StdId"].ToString();
            e.Cancel = true;


            btnadd.Text = "Update";
            BindGridiv();

        }

        protected void btnreset_Click(object sender, EventArgs e)
        {

            txtdname.Text = "";
            txtseat.Text = "";
            drpstd.SelectedIndex = 0;
            btnadd.Text = "ADD";
            DivId = null;
        }

        protected void GvDiv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox tname = GvDiv.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
                TextBox tsname = GvDiv.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
                TextBox tseat = GvDiv.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox;

                DivMst objDiv = new DivMst();
                objDiv.DID = Convert.ToInt32(GvDiv.DataKeys[e.RowIndex].Value);
                objDiv.DivName = txtdname.Text;
                objDiv.StdName = txtseat.Text;
                objDiv.Seat = int.Parse(txtseat.Text);

                objDiv.UpdateDataByKey();
                
                lbl.Text = "Record Updated Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
            catch (Exception ex)
            {
                lbl.Text = "Fail to update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
            GvDiv.EditIndex = -1;
            BindGridiv();
        }
        protected void GvDiv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvDiv.EditIndex = -1;
            BindGridiv();
        }
    }
}
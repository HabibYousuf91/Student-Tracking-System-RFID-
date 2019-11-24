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
    public partial class AddStaff : System.Web.UI.Page
    {
        private int? StaffId
        {
            get { return this.hidStaffId.Value != "" ? (int?)Convert.ToInt32(this.hidStaffId.Value) : (int?)null; }
            set { this.hidStaffId.Value = value.ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";
            if (Page.IsPostBack == false)
            {
                bindStd();

                if (!string.IsNullOrEmpty(Request.QueryString["SId"]))
                {
                    string id = Request.QueryString["SId"];
                    loaddata(int.Parse(id));
                }
                
            }
        }

        protected void bindStd()
        {
            DataTable StdDT = StdMst.GetAllActiveData();
            drpstd.DataSource = StdDT;
            drpstd.DataTextField = "STDName";
            drpstd.DataValueField = "SID";
            drpstd.DataBind();
            drpstd.Items.Insert(0, new ListItem("Select", "-1"));
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {

            resret();
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {


                DataTable StaffDT = StaffMst.Select_UNAME(txtuname.Text);
                if (string.IsNullOrEmpty( txtname.Text) || string.IsNullOrEmpty(txtemail.Text))
                {
                    lbl.Text = "Name and email can't be null !!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtuname.Text) || string.IsNullOrEmpty(txtpass.Text))
                {
                    lbl.Text = "user name and and password can't be null !!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                else
                {
                    StaffMst staff = new StaffMst();
                    staff.Name = txtname.Text;
                    if (drpstd.SelectedIndex > 0)
                    {
                        staff.StdName = drpstd.SelectedItem.Text;
                        staff.StdId = int.Parse(drpstd.SelectedValue);
                    }
                    staff.Email = txtemail.Text;
                    staff.Mobile = txtmobile.Text;
                    if (!string.IsNullOrEmpty(FileUpload1.FileName))
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/StaffImg/" + FileUpload1.FileName));
                        staff.Image = "~/StaffImg/" + FileUpload1.FileName;
                    }
                    staff.Qualification = txtqual.Text;
                    staff.Pass = txtpass.Text;
                    staff.Pincode = txtpin.Text;
                    staff.Uname = txtuname.Text;
                    staff.Add = txtadd.Text;
                    staff.City = txtcity.Text;
                    staff.Gender = DropDownList1.SelectedItem.Text;
                    if (StaffId != null)
                    {
                        staff.SID = int.Parse(StaffId.ToString());
                        if (staff.UpdateDataByKey())
                        {
                            lbl.Text = "Record updated Successfully";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                            resret();
                            Response.Redirect("StaffReport.aspx",false);
                        }
                    }
                    else
                    {
                        if (StaffDT != null && StaffDT.Rows.Count == 1)
                        {
                            lbl.Text = "UserName alredy exists !!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                        }

                        staff.SID = StaffMst.GetNewSID();
                        staff.Insert();
                        lbl.Text = "Record Added Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                        resret();
                    }

                }
            }
            catch(Exception ex)
            {

                lbl.Text = "Fail to Add record";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }
        }

        protected void resret()
        {
            txtadd.Text = "";
            txtcity.Text = "";
            txtemail.Text = "";
            txtmobile.Text = "";
            txtname.Text = "";
            txtpin.Text = "";
            txtqual.Text = "";
            txtuname.Text = "";
            drpstd.SelectedIndex = 0;
        }

        private void loaddata(int id)
        {
            try
            {
                DataRow dr = StaffMst.GetDataByKey(id);
                if (dr != null)
                {
                    txtadd.Text = dr["Add"].ToString();
                    txtcity.Text = dr["Name"].ToString();
                    txtemail.Text = dr["Email"].ToString();
                    txtmobile.Text = dr["Mobile"].ToString();
                    txtname.Text = dr["Name"].ToString();
                    txtpin.Text = dr["Pincode"].ToString();
                    txtqual.Text = dr["Qualification"].ToString();
                    txtuname.Text = dr["Uname"].ToString();
                    if(!string.IsNullOrEmpty(dr["StdId"].ToString()))
                    {
                        drpstd.SelectedValue = dr["StdId"].ToString();
                    }
                    DropDownList1.SelectedItem.Text = dr["Gender"].ToString();
                    StaffId = id;
                    btnadd.Text = "Update";
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
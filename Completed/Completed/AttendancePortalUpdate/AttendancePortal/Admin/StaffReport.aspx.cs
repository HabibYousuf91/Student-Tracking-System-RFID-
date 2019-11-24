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
    public partial class StaffRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable StaffDT = StaffMst.GetAllActiveData();
                GvStaff.DataSource = StaffDT;
                GvStaff.DataBind();
            }
        }
        protected void GvStaff_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            StaffMst.Delete(Convert.ToInt32(GvStaff.DataKeys[e.RowIndex].Value));
            DataTable StaffDT = StaffMst.GetAllActiveData();
            GvStaff.DataSource = StaffDT;
            GvStaff.DataBind();
        }
        protected void GvStaff_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GvStaff.EditIndex = e.NewEditIndex;

                int StaffId = int.Parse((GvStaff.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text);
                Response.Redirect("AddStaff.aspx?SId=" + StaffId,false);
            }
            catch (Exception ex)
            {
            }
            
        }
        }
}
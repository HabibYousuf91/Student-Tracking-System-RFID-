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
    public partial class StudentAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = CHECKINOUT.GetAllDataRecord();
                GvAttendance.DataSource = dt;
                GvAttendance.DataBind();
            }
        }
        //protected void GvStaff_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //   CHECKINOUT CHECKINOUT.Delete(Convert.ToInt32(GvStaff.DataKeys[e.RowIndex].Value));
        //    DataTable StaffDT = CHECKINOUT.GetAllActiveData();
        //    GvStaff.DataSource = StaffDT;
        //    GvStaff.DataBind();
        //}
        //protected void GvStaff_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    try
        //    {
        //        GvStaff.EditIndex = e.NewEditIndex;

        //        int StaffId = int.Parse((GvStaff.Rows[e.NewEditIndex].FindControl("lbl_SID") as Label).Text);
        //        Response.Redirect("AddStaff.aspx?SId=" + StaffId,false);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
            
        //}

        protected void btnsearch_Click(object sender, EventArgs e)
        {
          
            DataTable checkoutdt = null;
            try
            {
                DataTable dt = CHECKINOUT.GetAllDataRecord();
                if (dt != null)
                {
                    checkoutdt = dt.Select("Name LIKE '%" + txtName.Text.Trim() + "%' AND RollNo like '%" + txtRollNo.Text.Trim()+ "%'").CopyToDataTable();
                    GvAttendance.DataSource = checkoutdt;
                    GvAttendance.DataBind();
                }
              
            }
            catch
            {
                GvAttendance.DataSource = checkoutdt;
                GvAttendance.DataBind(); 
            }
           
          

        }
        }
}
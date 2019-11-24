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
    public partial class ViewResult : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {

                if (Session["SID"] != null)
                {
                     DataRow StaffDT = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));


                     if (StaffDT != null && StaffDT["StdId"].ToString() != "")
                     {
                         DataTable Student_dt = StudentResult.GetAllResultDataByStId(int.Parse(StaffDT["StdId"].ToString()));
                         GvwResult.DataSource = Student_dt;
                         GvwResult.DataBind();
                     }
                }
                BindExam();
            }
        }
        private void BindExam()
        {
            DataTable Mstdt = ExamMst.GetAllActiveData();

            drpExam.DataSource = Mstdt;
            drpExam.DataTextField = "Name";
            drpExam.DataValueField = "ExamId";
            drpExam.DataBind();
            drpExam.Items.Insert(0, "SELECT");
        }
        protected void btnsarch_Click(object sender, EventArgs e)
        {
            try
            {
                 DataRow StaffDT = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));

                 DataTable Student_dt = null;
                 if (StaffDT != null && StaffDT["StdId"].ToString() != "")
                 {
                     DataTable StuDT = StudentResult.GetAllResultDataByStId(int.Parse(StaffDT["StdId"].ToString()));
                     if (StuDT != null)
                     {
                         try
                         {
                              Student_dt = StuDT.Select("ExamId= " + drpExam.SelectedValue).CopyToDataTable(); 

                             if (Student_dt != null && Student_dt.Rows.Count > 0)
                             {


                                 GvwResult.DataSource = Student_dt;
                                 GvwResult.DataBind();
                             }
                         }
                         catch
                         {
                             GvwResult.DataSource = Student_dt;
                             GvwResult.DataBind();
                         }
                     }
                 }
            }
            catch
            {
            }
           
            //else
            //{
            //    //MultiView1.ActiveViewIndex = -1;
            //}

        }

        protected void GvStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // StudentMst.Delete(Convert.ToInt32(GvwResult.DataKeys[e.RowIndex].Value));
            if (Session["SID"] != null)
            {
                DataRow StaffDT = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));


                if (StaffDT != null && StaffDT["StdId"].ToString() != "")
                {
                    DataTable Student_dt = StudentResult.GetAllResultDataByStId(int.Parse(StaffDT["StdId"].ToString()));
                    GvwResult.DataSource = Student_dt;
                    GvwResult.DataBind();
                }
            }
        }

        protected void GvStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GvwResult.EditIndex = e.NewEditIndex;

                int ResultId = int.Parse((GvwResult.Rows[e.NewEditIndex].FindControl("lbl_ResultId") as Label).Text);
                Response.Redirect("AddResult.aspx?ResultId=" + ResultId, false);
            }
            catch (Exception ex)
            {
            }

        }

        protected void BindDiv(int StdId)
        {
                DataTable DivDT = DivMst.GetDataByStandardId(StdId);
                drpdiv.DataSource = DivDT;
                drpdiv.DataTextField = "DivName";
                drpdiv.DataValueField = "DID";
                drpdiv.DataBind();
                drpdiv.Items.Insert(0, "SELECT");
        } 
    }
}
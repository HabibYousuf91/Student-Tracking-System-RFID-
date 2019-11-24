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
    public partial class ResultReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {

                DataTable StuDT = StudentResult.GetAllResultDataByStId();

                GvwResult.DataSource = StuDT;
                GvwResult.DataBind();

                }
                catch (Exception ex)
                {
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
                DataTable Student_dt = null;
                DataTable StuDT = StudentResult.GetAllResultDataByStId();
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
            catch
            {
            }

            //else
            //{
            //    //MultiView1.ActiveViewIndex = -1;
            //}

        }
    }
}
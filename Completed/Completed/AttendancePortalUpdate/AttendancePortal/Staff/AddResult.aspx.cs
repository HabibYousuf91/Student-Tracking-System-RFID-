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
    public partial class AddResult : System.Web.UI.Page
    {
        private int? ResultID
        {
            get { return this.hidStdId.Value != "" ? (int?)Convert.ToInt32(this.hidStdId.Value) : (int?)null; }
            set { this.hidStdId.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text = "";
            if (!Page.IsPostBack)
            {
                if (Session["SID"] != null)
                {
                    DataRow StaffDT = StaffMst.GetDataByKey(int.Parse(Session["SID"].ToString()));


                    if (StaffDT != null && StaffDT["StdId"].ToString() != "")
                    {
                        lbl_standard.Text = StaffDT["StdName"].ToString();
                        BindDiv(int.Parse(StaffDT["StdId"].ToString()));
                        BindExam();
                        // BindGridiv();
                    }

                    if (!string.IsNullOrEmpty(Request.QueryString["ResultId"]))
                    {
                        string id = Request.QueryString["ResultId"];
                        loaddata(int.Parse(id));
                    }
                }
            }
        }

        private void BindDiv(int StdId)
        {
            DataTable DivDT = DivMst.GetDataByStandardId(StdId);
            drpdiv.DataSource = DivDT;
            drpdiv.DataTextField = "DivName";
            drpdiv.DataValueField = "DID";
            drpdiv.DataBind();
            drpdiv.Items.Insert(0, "SELECT");
            drpstudent.Items.Insert(0, "SELECT");
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


        protected void drpdiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindStudent();
        }

        protected void bindStudent()
        {
            DataTable dt = StudentMst.GetAllActiveData();
            if (dt != null && dt.Rows.Count > 0)
            {
                drpstudent.DataSource = dt;
                drpstudent.DataTextField = "rollno";
                drpstudent.DataValueField = "sid";
                drpstudent.DataBind();
                drpstudent.Items.Insert(0, "SELECT");
            }
        }
        protected void drpExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpExam.SelectedIndex > 0)
            {
                DataRow dr = ExamMst.GetDataByKey(int.Parse(drpExam.SelectedValue));
                if (dr != null)
                {
                    txttotalMarks.Text = dr["Total"].ToString();
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpstudent.SelectedIndex == 0)
                {
                    lbl.Text = "Select Student.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                if (drpExam.SelectedIndex == 0)
                {
                    lbl.Text = "Select Exam.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                if (txttotalMarks.Text != "" && txtObtainMarks.Text != "")
                {
                    StudentResult objDiv = new StudentResult();
                    if (drpstudent.SelectedIndex > 0)
                    {
                        //  objDiv.StdName = drpstd.SelectedItem.Text;
                        objDiv.StudentId = int.Parse(drpstudent.SelectedValue);
                    }
                    if (drpstudent.SelectedIndex > 0)
                    {
                        //  objDiv.StdName = drpstd.SelectedItem.Text;
                        objDiv.ExamID = int.Parse(drpExam.SelectedValue);
                    }
                    objDiv.TotalMarks = txttotalMarks.Text != null ? decimal.Parse(txttotalMarks.Text) : 0;
                    objDiv.ObtainMarks = txtObtainMarks.Text != null ? decimal.Parse(txtObtainMarks.Text) : 0;
                    if (ResultID == null)
                    {
                        objDiv.ResultID = StudentResult.GetNewResultID();

                        objDiv.Insert();
                        // objDiv.Insert(txtdname.Text, drpstd.SelectedItem.Text, Convert.ToInt32(txtseat.Text));
                        lbl.Text = "Record Added Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);

                    }
                    else
                    {
                        objDiv.ResultID = int.Parse(ResultID.ToString());
                        objDiv.UpdateDataByKey();
                        lbl.Text = "Record Updated Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut", true);

                    }
                    drpstudent.SelectedIndex = 0;
                    drpExam.SelectedIndex = 0;
                    drpdiv.SelectedIndex = 0;


                    txtObtainMarks.Text = "";

                    txttotalMarks.Text = "";

                    btnadd.Text = "ADD";
                }
                else
                {
                    lbl.Text = "Total Marks  and  Obtain Marks can't null";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut", true);
                }
            }
            catch (Exception ex)
            {

                lbl.Text = "Fail to add a record";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }

        }


        protected void btnreset_Click(object sender, EventArgs e)
        {
            drpstudent.SelectedIndex = 0;
            drpExam.SelectedIndex = 0;
            drpdiv.SelectedIndex = 0;


            txtObtainMarks.Text = "";

            txttotalMarks.Text = "";

            btnadd.Text = "ADD";
            ResultID = null;
        }

        private void loaddata(int id)
        {
            try
            {
                DataRow dr = StudentResult.GetDataByKey(id);
                if (dr != null)
                {
                    txttotalMarks.Text = dr["TotalMarks"].ToString();
                    txtObtainMarks.Text = dr["ObtainMarks"].ToString();

                    if (!string.IsNullOrEmpty(dr["ExamId"].ToString()))
                    {
                        drpExam.SelectedValue = dr["ExamId"].ToString();
                        //  drpstd.SelectedValue = dr["StdId"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["StudentId"].ToString()))
                    {
                        DataRow drStd = StudentMst.GetDataByKey(int.Parse(dr["StudentId"].ToString()));
                        if (drStd != null)
                        {
                            drpdiv.SelectedValue = drStd["DivId"].ToString();
                            bindStudent();
                            drpstudent.SelectedValue = drStd["SID"].ToString();
                        }
                        ResultID = id;
                        //  drpstd.SelectedValue = dr["StdId"].ToString();
                    }
                    btnadd.Text = "Update";
                }
            }
            catch
            {
            }

        }
    }
}
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
    public partial class AddStudent : System.Web.UI.Page
    {
        private int? StudentId
        {
            get { return this.hidStudentId.Value != "" ? (int?)Convert.ToInt32(this.hidStudentId.Value) : (int?)null; }
            set { this.hidStudentId.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {

                //lblstd.Text = StaffDT.Select("Uname='" + Session["uname"].ToString()+ "'")[0]["StdName"].ToString();
                bindStd();
                if (!string.IsNullOrEmpty(Request.QueryString["SId"]))
                {
                    string id = Request.QueryString["SId"];
                    loaddata(int.Parse(id));
                }
                else
                {
                    getRollNo();
                }

            }
        }
        protected void drpstd_SelectedIndexChanged(object sender, EventArgs e)
        {
            binddiv(int.Parse(drpstd.SelectedValue));
        }
        protected void binddiv(int stdId)
        {
            DataTable DivDT = DivMst.GetDataByStandardId(stdId);
            drpdiv.DataSource = DivDT;
            drpdiv.DataTextField = "DivName";
            drpdiv.DataValueField = "DID";
            drpdiv.DataBind();
            drpdiv.Items.Insert(0, new ListItem("Select", "-1"));
        }
        private void bindStd()
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
        protected void btnstuadd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable studentDT = StudentMst.GetAllActiveData();

                if (string.IsNullOrEmpty(txtroll.Text))
                {
                    lblmsg.Text = "Roll can't be null !!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtemail.Text))
                {
                    lblmsg.Text = "Name and email can't be null !!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtuname.Text) || string.IsNullOrEmpty(txtpass.Text))
                {
                    lblmsg.Text = "user name and and password can't be null !!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                    return;
                }

                else
                {
                    
                    DateTime bdate = Convert.ToDateTime(drpdd.SelectedItem.Text + " " + drpmm.SelectedItem.Text + " " + drpyyyy.SelectedItem.Text);
                    StudentMst stdobj = new StudentMst();
                    stdobj.Name = txtname.Text;
                    // stdobj.StdName = lblstd.Text;
                    // stdobj.DivName = drpdiv.SelectedItem.Text;
                    stdobj.DOB = bdate.GetDateTimeFormats()[8].ToString();
                    stdobj.EDate = DateTime.Now;
                    stdobj.Email = txtemail.Text;
                    stdobj.Mobile = txtmobi.Text;
                    stdobj.RollNo = txtroll.Text;
                    stdobj.Pincode = txtpin.Text;
                    stdobj.Uname = txtuname.Text;
                    stdobj.Pass = txtpass.Text;
                    stdobj.City = txtcity.Text;
                    stdobj.MachineCode = txt_MachineId.Text != "" ? (int?)Convert.ToInt32(txt_MachineId.Text): null;
                    if (drpdiv.SelectedIndex > 0)
                    {
                        stdobj.DivName =drpdiv.SelectedItem.Text;
                        stdobj.DivId = int.Parse(drpstd.SelectedValue);
                    }

                    if ( drpstd.SelectedIndex > 0)
                    {
                        stdobj.StdName = drpdiv.SelectedItem.Text;
                        stdobj.StdId = int.Parse(drpstd.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(FileUpload1.FileName))
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/Studentimg/" + FileUpload1.FileName));
                        stdobj.Image = "~/Studentimg/" + FileUpload1.FileName;
                    }
     
                    if (StudentId != null)
                    {
                        stdobj.SID = int.Parse(StudentId.ToString());
                        if (stdobj.UpdateDataByKey())
                        {
                            lblmsg.Text = "Record updated Successfully";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                            resret();
                            Response.Redirect("StudentReport.aspx", false);
                        }
                    }
                    else
                    {
                        if (studentDT != null && studentDT.Rows.Count == 1)
                        {
                          lblmsg.Text = "UserName alredy exists !!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                        }

                        stdobj.SID = StudentMst.GetNewSID();
                        stdobj.Insert();
                        lblmsg.Text = "Record Added Successfully";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
                        resret();
                       
                    }

                }
            }
            catch (Exception ex)
            {

              lblmsg.Text = "Fail to Add record";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "fadeLabelOut()", true);
            }  
        }


        protected void resret()
        {
            txtadd.Text = "";
            txtcity.Text = "";
            txtemail.Text = "";
           // txtmobile.Text = "";
            txtname.Text = "";
            txtpin.Text = "";
           // txtqual.Text = "";
            txtuname.Text = "";
            txt_MachineId.Text = "";
            txtmobi.Text = "";
            drpstd.SelectedIndex = 0;
            drpdiv.SelectedIndex = 0;
            txtuname.Text = "";
            getRollNo();
            // drpstd.SelectedIndex = 0;
        }

        private void loaddata(int id)
        {
            try
            {
                DataRow dr = StudentMst.GetDataByKey(id);
                if (dr != null)
                {
                    txtadd.Text = dr["Add"].ToString();
                    txtcity.Text = dr["Name"].ToString();
                    txtemail.Text = dr["Email"].ToString();
                    txtmobi.Text = dr["Mobile"].ToString();
                    txtroll.Text = dr["RollNo"].ToString();
                    // txtmobile.Text = dr["Mobile"].ToString();
                    txtname.Text = dr["Name"].ToString();
                    txtpin.Text = dr["Pincode"].ToString();
                   // txtqual.Text = dr["Qualification"].ToString();
                    txtuname.Text = dr["Uname"].ToString();
                    txt_MachineId.Text = dr["MachineCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["DOB"].ToString()))
                    {
                        DateTime date = Convert.ToDateTime(dr["DOB"].ToString());
                        drpdd.SelectedItem.Text = date.ToString("dd");
                         drpmm.SelectedItem.Text = date.ToString("MM");
                        drpyyyy.SelectedItem.Text = date.ToString("yyyy");
                    }

                    if (!string.IsNullOrEmpty(dr["StdId"].ToString()))
                    {
                     drpstd.SelectedValue = dr["StdId"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["DivId"].ToString()))
                    {
                        binddiv(int.Parse(dr["StdId"].ToString()));
                       drpdiv.SelectedValue = dr["DivId"].ToString();
                    }
                    //  DropDownList1.SelectedItem.Text = dr["Gender"].ToString();
                     StudentId = id;

                    btnstuadd.Text = "Update";
                }
            }
            catch
            {
            }

        }
        private void getRollNo()
        {
            int rno = 0;
            DataRow studentdt = StudentMst.GetMaxRow();
            //int rstu = Convert.ToInt32(DivDT.Rows[0]["Seat"].ToString()) - Convert.ToInt32(StuDT.Rows.Count.ToString());
            //lblstudent.Text = "Total Admitted =" + StuDT.Rows.Count.ToString() + " , Remaining Student = " + rstu;

            //string stddd = lblstd.Text.Substring(0, 1);
            if (studentdt != null)
            {
                rno = Convert.ToInt32(studentdt["RollNo"].ToString()) + 1;
            }
            if (rno.ToString().Length < 2)
            {
                string newrno =DateTime.Now.ToString("YY") + "000" + rno.ToString();
                txtroll.Text =  newrno;
            }
            else
            {
                txtroll.Text = rno.ToString();
            }
        }
    }
}
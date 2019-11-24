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
    public partial class Complain : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            if (Page.IsPostBack == false)
            {
                if (Session["SID"] != null)
                {


                    bindgrid();
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                
                }
                
           
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            DataRow compdr = Complainmst.GetDataByKey(Convert.ToInt32(e.CommandArgument.ToString()));
            if (compdr != null)
            {
                lblrno.Text = compdr["Rollno"].ToString();

                lblname.Text = compdr["name"].ToString();
                lblcomp.Text = compdr["message"].ToString();
                lblsub.Text = compdr["subject"].ToString();
                ViewState["cid"] = compdr["cid"].ToString();
            }
        }
        protected void btnreplyy_Click(object sender, EventArgs e)
        {
            Complainmst complainobj = new Complainmst(Convert.ToInt32(ViewState["cid"].ToString()));
            //complainobj.CID = Convert.ToInt32(ViewState["cid"].ToString());
            complainobj.Reply = txtans.Text;
            complainobj.UpdateDataByKey();
            txtans.Text = "";

            string rno = Session["std"].ToString().Substring(0, 1);

            bindgrid();
            MultiView1.ActiveViewIndex = 0;
            lblcomplain.Text = GridView1.Rows.Count.ToString();

        }

        protected void bindgrid()
        {
            try
            {

           
            if (Session["SID"] != null)
            {
                int staffId = int.Parse(Session["SID"].ToString());

                DataRow drstaff = StaffMst.GetDataByKey(staffId);
                if (drstaff != null)
                {
                    DataTable compdt = Complainmst.GetAllDataRecordByStId("", int.Parse(drstaff["StdId"].ToString()));
                    GridView1.DataSource = compdt;
                    GridView1.DataBind();
                    lblcomplain.Text = GridView1.Rows.Count.ToString();
                }
            }
            }
            catch
            {
            }
         
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
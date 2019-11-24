using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AttendancePortal.Student
{
    public partial class Student : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                int studentId = int.Parse(Session["SID"].ToString());
                DataRow studentdr = StudentMst.GetDataByKey(studentId);
                if (studentdr != null)
                    {

                    Image4.ImageUrl = studentdr["image"].ToString();
                    Label1.Text = studentdr["name"].ToString();
                }
            }
        }
    }
}
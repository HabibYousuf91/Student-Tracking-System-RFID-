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
    public partial class Password : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnchangepass_Click(object sender, EventArgs e)
        {
            try
            {

            int studentId = 1;
            DataRow dr = StudentMst.GetDataByKey(studentId);
            if (dr != null && dr["Pass"].ToString() == txtcurrent.Text)
            {
                StudentMst studentObj = new StudentMst();
                studentObj.Name = dr["Name"].ToString();
                studentObj.StdName = dr["StdName"].ToString();
                studentObj.DivName = dr["DivName"].ToString();
                studentObj.DOB = dr["DOB"].ToString();
                studentObj.EDate = dr["DOB"].ToString() !="" ? (DateTime?)Convert.ToDateTime(dr["DOB"].ToString()) : null;
                studentObj.Email = dr["Email"].ToString();
                studentObj.Mobile = dr["Mobile"].ToString();
                studentObj.RollNo = dr["RollNo"].ToString();
                studentObj.Pincode = dr["Pincode"].ToString();
                studentObj.Uname = dr["Uname"].ToString();
                studentObj.Pass = txtnewpass.Text;
                studentObj.City = dr["City"].ToString();
                studentObj.Add = dr["Add"].ToString();
                studentObj.Image = dr["Image"].ToString();
                studentObj.UpdateDataByKey();
                lbl.Text = "Password Changed";

            }
            else
            {

                lbl.Text = "Invalid Current Password";
            }


            }
            catch (Exception ex)
            {
                lbl.Text = "update password fail";
            }
        }
    }
}
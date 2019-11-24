<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Staff/Staff.Master" CodeBehind="AddResult.aspx.cs" Inherits="AttendancePortal.Staff.AddResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .style3
    {
        height: 19px;
    }
    .style2
    {
        width: 475px;
    }
    </style>

    <script type="text/javascript">
        function fadeLabelOut() {
            debugger;
            $('#<%=lbl.ClientID%>').fadeOut(4000, function () {
                $(this).html(''); //reset the label after fadeout
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:HiddenField ID="hidStdId" runat="server" />
    <table class="tbl">
    <tr>
        <td class="tblhead">
            ADD RESULT</td>
    </tr>
    <tr>
        <td class="style3">
        </td>
    </tr>
    <tr>
        <td>
            <table align="center" class="style2">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                   <td class="lbl">
                        Standard :</td>
                    <td>
                     <asp:Label ID="lbl_standard" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>

                      <td class="lbl">
                        Select Division :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdiv" runat="server" CssClass="txt" AutoPostBack="True" 
                            onselectedindexchanged="drpdiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                      <td class="lbl">
                        Student :</td>
                    <td>
                        <asp:DropDownList ID="drpstudent" runat="server" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="lbl">
                        Exam :</td>
                    <td>
                        <asp:DropDownList ID="drpExam" AutoPostBack="true"  onselectedindexchanged="drpExam_SelectedIndexChanged" runat="server" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                        <td class="lbl">Total Marks :</td>
                        <td>
                            <asp:TextBox ID="txttotalMarks" runat="server" Enabled="false" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                 <tr>
                        <td class="lbl">Obtain Marks Marks :</td>
                        <td>
                            <asp:TextBox ID="txtObtainMarks" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnadd" runat="server" CssClass="btn" Text="ADD" 
                            onclick="btnadd_Click" />
                          <asp:Button ID="btnreset" runat="server" CssClass="btn" Text="Reset"
                                OnClick="btnreset_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lbl" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
               </table>
                  
                <br/>
                <hr />
            </td>
    </tr>
</table>
</asp:Content>

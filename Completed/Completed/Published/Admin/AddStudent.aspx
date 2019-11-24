<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" MasterPageFile="~/Admin/Admin.master" Inherits="AttendancePortal.Admin.AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            width: 449px;
        }

        .style5 {
            width: 387px;
        }

        .style7 {
            text-align: right;
            color: Red;
            width: 242px;
        }

        .style6 {
            width: 140px;
        }

        .style8 {
            width: 242px;
        }
    </style>

    <script type="text/javascript">            
        function fadeLabelOut() {
            debugger;
            $('#<%=lblmsg.ClientID%>').fadeOut(4000, function () {
                $(this).html(''); //reset the label after fadeout
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidStudentId" runat="server" />
    <table class="tbl">
        <tr>
            <td class="tblhead">ADD NEW STUDENT DATA.</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table align="center" class="style2">
                    <tr>
                        <td class="lbl">RollNo : </td>
                        <td>
                            <asp:TextBox ID="txtroll" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Student Name :</td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Mobile :
                        </td>
                        <td>
                            <asp:TextBox ID="txtmobi" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Email :
                        </td>
                        <td>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="lbl">MachineId :
                        </td>
                        <td>
                            <asp:TextBox ID="txt_MachineId" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">DOB :
                        </td>
                        <td>
                            <asp:DropDownList ID="drpdd" runat="server">
                                <asp:ListItem>DD</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpmm" runat="server">
                                <asp:ListItem>MM</asp:ListItem>
                                <asp:ListItem>Jan</asp:ListItem>
                                <asp:ListItem>Feb</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpyyyy" runat="server">
                                <asp:ListItem>YYYY</asp:ListItem>
                                <asp:ListItem>1980</asp:ListItem>
                                <asp:ListItem>1981</asp:ListItem>
                                <asp:ListItem>1982</asp:ListItem>
                                <asp:ListItem>1983</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Address :
                        </td>
                        <td>
                            <asp:TextBox ID="txtadd" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">City :
                        </td>
                        <td>
                            <asp:TextBox ID="txtcity" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Pincode :
                        </td>
                        <td>
                            <asp:TextBox ID="txtpin" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="lbl"> Standard</td>
                        <td>
                              <asp:DropDownList ID="drpstd" runat="server" CssClass="txt" AutoPostBack="True" 
                            onselectedindexchanged="drpstd_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                    <td class="lbl">
                        Division :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdiv" runat="server" CssClass="txt">
                        </asp:DropDownList>
                        </td>
                    <td class="style5">
                        &nbsp;
                    </td>
                        </tr>
                    <tr>
                        <td class="lbl">Image :
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">UserName :</td>
                        <td>
                            <asp:TextBox ID="txtuname" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Password :
                        </td>
                        <td>
                            <asp:TextBox ID="txtpass" TextMode="Password" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Confirm-Pass :
                        </td>
                        <td>
                            <asp:TextBox ID="txtcpass"  TextMode="Password" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                     <tr>
                        <td class="lbl">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnstuadd" runat="server" CssClass="btn" Text="ADD Student"
                                Width="120px" OnClick="btnstuadd_Click" />
                            <asp:Button ID="btnreset" runat="server" CssClass="btn" Text="Reset"
                                OnClick="btnreset_Click" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="lbl">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

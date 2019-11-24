<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.master" CodeBehind="StudentReport.aspx.cs" Inherits="AttendancePortal.Admin.StudentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">




        .style4
    {
        width: 644px;
    }
    .style6
    {
            width: 52px;
        }
    .style5
    {
            width: 56px;
        }
        .style8
    {
        width: 527px;
    }
    .style9
    {
        width: 493px;
    }
    .style10
    {
        width: 133px;
        text-align: center;
    }
    .style11
    {
        width: 332px;
    }
    .style14
    {
        text-align: right;
        color: Red;
        width: 89px;
    }
        .style13
    {
        width: 89px;
    }
    .style15
    {
        color: #004AE6;
        text-align: right;
        width: 142px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tbl">
    <tr>
        <td class="tblhead">
            Student List</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <table align="left" class="style4">
                <tr>
                    <td class="style15">
                        Select Standard :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="drpstd" runat="server" CssClass="txt" AutoPostBack="True" 
                            onselectedindexchanged="drpstd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblstd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style15">
                        Select Division :
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="drpdiv" runat="server" CssClass="txt" AutoPostBack="True" 
                            onselectedindexchanged="drpdiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style15">
                        Select Student : 
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="drpstudent" runat="server" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td class="style5" valign="bottom">
                        <asp:Button ID="btnsarch" runat="server" CssClass="btn" 
                            Text="Select" onclick="btnsarch_Click" />
                    </td>
                    <td valign="bottom">
                        <asp:Label ID="lblcnt" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td>
                        <asp:GridView ID="GvStudent" runat="server" AutoGenerateColumns="False" DataKeyNames="SID"
                    OnRowEditing="GvStudent_RowEditing"
                    OnRowDeleting="GvStudent_RowDeleting" AllowPaging="false" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="std Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SID" runat="server" Text='<%# Eval("SID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roll No" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_RollNo" runat="server" Text='<%# Eval("RollNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgg" ImageUrl='<%#Eval("Image") %>' Width="45px" Height="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Email" runat="server" Text='<%# Eval("Email") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile#" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Mobile" runat="server" Text='<%# Eval("Mobile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Uname" runat="server" Text='<%# Eval("Uname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M.Code" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_MachineCode" runat="server" Text='<%# Eval("MachineCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Division" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_DivName" runat="server" Text='<%# Eval("DivName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgbtn_edit" runat="server" CommandArgument="Edit" CommandName="edit"
                                    Text="Edit" ToolTip="Select" />

                                <asp:LinkButton ID="imgbtn_Delete" runat="server" CommandArgument="Delete" CommandName="delete"
                                    Text="Delete" ToolTip="delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" CssClass="gv" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    <PagerTemplate>
                        <asp:Panel ID="pnl_GridPagerRow" runat="server">
                        </asp:Panel>
                    </PagerTemplate>
                </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>


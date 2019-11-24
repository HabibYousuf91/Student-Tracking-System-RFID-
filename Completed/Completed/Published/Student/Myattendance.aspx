<%@ Page Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="Myattendance.aspx.cs" Inherits="AttendancePortal.Student.Myattendance" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3 {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tbl">
        <tr>
            <td class="tblhead">Track Location</td>
        </tr>
        <tr>
            <td class="style3"></td>
        </tr>
     <%--   <tr>
            <td class="style3"></td>
        </tr>--%>
        <tr>
            <td>
              
                <asp:GridView ID="GvAttendance" runat="server" AutoGenerateColumns="False" DataKeyNames="CHECKTIME"
                    AllowPaging="false" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="RollNo">
                            <ItemTemplate>
                                <asp:Label ID="lbl_RollNo" runat="server" Text='<%# Eval("RollNo") %>' ItemStyle-Width="10%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Email Id" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Email" runat="server" Text='<%# Eval("Email") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Date & Time" ItemStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Mobile" runat="server" Font-Size="Small" Text='<%# Eval("CHECKTIME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Spoted At" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Uname" runat="server" Text='<%# Eval("MachineAlias") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <%-- <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgbtn_edit" runat="server" CommandArgument="Edit" CommandName="edit"
                                    Text="Edit" ToolTip="Select" />

                                <asp:LinkButton ID="imgbtn_Delete" runat="server" CommandArgument="Delete" CommandName="delete"
                                    Text="Delete" ToolTip="delete" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" CssClass="gv" HorizontalAlign="Center" />
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
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


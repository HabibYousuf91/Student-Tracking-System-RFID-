<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.master" CodeBehind="Message.aspx.cs" Inherits="AttendancePortal.Admin.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3 {
            height: 19px;
        }

        .style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tbl">
        <tr>
            <td class="tblhead">Leave Report</td>
        </tr>
        <tr>
            <td class="style3"></td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>Select Standard :
                        <asp:DropDownList ID="drpstd" runat="server" CssClass="txt">
                        </asp:DropDownList>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnnewleave" runat="server" CssClass="btn"
                                OnClick="btnnewleave_Click" Text="New Leave Report" Width="160px" />
                            &nbsp;
                <asp:Button ID="btnapprove" runat="server" CssClass="btn"
                    OnClick="btnapprove_Click" Text="Approve Leave" Width="160px" />
                            &nbsp;
                <asp:Button ID="btnreject" runat="server" CssClass="btn"
                    OnClick="btnreject_Click" Text="Reject Leave" Width="160px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table class="style1">
                                        <tr>
                                            <td>Total New Leave =
                                    <asp:Label ID="lblnew" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LID"
                    AllowPaging="false" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="std Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_LID" runat="server" Text='<%# Eval("LID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Roll #" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_rollno" runat="server" Text='<%# Eval("rollno") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_message" runat="server" Text='<%# Eval("message") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_nodays" runat="server" Text='<%# Eval("nodays") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Reply") %>' />
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
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table class="style1">
                                        <tr>
                                            <td>Total Apporved Leave =
                                    <asp:Label ID="lblapp" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="LID"
                                                    AllowPaging="false" PageSize="10"
                                                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                                                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="std Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_LID" runat="server" Text='<%# Eval("LID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Roll #" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_rollno" runat="server" Text='<%# Eval("rollno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_message" runat="server" Text='<%# Eval("message") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Days" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_nodays" runat="server" Text='<%# Eval("nodays") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Reply") %>' />
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
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <table class="style1">
                                        <tr>
                                            <td>Total Rejected Leave =
                                    <asp:Label ID="lblrej" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="LID"
                    AllowPaging="false" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="std Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_LID" runat="server" Text='<%# Eval("LID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Roll #" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_rollno" runat="server" Text='<%# Eval("rollno") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_message" runat="server" Text='<%# Eval("message") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_nodays" runat="server" Text='<%# Eval("nodays") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Reply") %>' />
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
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


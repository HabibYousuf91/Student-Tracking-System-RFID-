<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="AttendancePortal.Admin.AddExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2 {
            width: 394px;
        }

        .style3 {
            height: 19px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidStdId" runat="server" />
    <table class="tbl">
        <tr>
            <td class="tblhead">ADD EXAM</td>
        </tr>
        <tr>
            <td class="style3"></td>
        </tr>
        <tr>
            <td>
                <table align="center" class="style2">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="lbl">Exam Name :</td>
                        <td>
                            <asp:TextBox ID="txtstdname" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                     <tr>
                        <td class="lbl">Total Marks :</td>
                        <td>
                            <asp:TextBox ID="txt_TotalMarks" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnaddstd" runat="server" CssClass="btn" Text="ADD"
                                OnClick="btnaddstd_Click" />
                            <asp:Button ID="btnreset" runat="server" CssClass="btn" Text="Reset"
                                OnClick="btnreset_Click" />

                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lbl" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <hr />
                <asp:GridView ID="GvStd" runat="server" AutoGenerateColumns="False" DataKeyNames="ExamID"
                    OnRowEditing="GvStd_RowEditing"
                    OnRowDeleting="GvStd_RowDeleting" AllowPaging="false"
                    OnRowUpdating="GvStd_RowUpdating" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    Width="100%" BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="std Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SID" runat="server" Text='<%# Eval("ExamID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Examination Name" ItemStyle-Width="60%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_StdName" runat="server" Text='<%# Eval("Name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Total Marks" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TotalMarks" runat="server" Text='<%# Eval("Total") %>' />
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
                  <%--  <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />--%>
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
</asp:Content>

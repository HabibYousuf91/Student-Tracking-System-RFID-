<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="AddResult.aspx.cs" Inherits="AttendancePortal.Admin.AddResult" %>

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
            ADD Division</td>
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
                        Division Name :</td>
                    <td>
                        <asp:TextBox ID="txtdname" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="lbl">
                        Seat : </td>
                    <td>
                        <asp:TextBox ID="txtseat" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="lbl">
                        Standard :</td>
                    <td>
                        <asp:DropDownList ID="drpstd" runat="server" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
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
                <asp:GridView ID="GvDiv" runat="server" AutoGenerateColumns="False" DataKeyNames="DID"
                    OnRowEditing="GvDiv_RowEditing"
                    OnRowDeleting="GvDiv_RowDeleting" AllowPaging="false"
                    OnRowUpdating="GvDiv_RowUpdating" PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !"
                    BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                     <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="std Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_DID" runat="server" Text='<%# Eval("DID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Division Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_divname" runat="server" Text='<%# Eval("divname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Standard Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_StdName" runat="server" Text='<%# Eval("StdName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="No of Seats" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Seat" runat="server" Text='<%# Eval("Seat") %>' />
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
</asp:Content>

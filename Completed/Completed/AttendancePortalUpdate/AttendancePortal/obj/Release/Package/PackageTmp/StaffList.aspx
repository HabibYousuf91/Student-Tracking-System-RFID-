<%@ Page Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeBehind="StaffList.aspx.cs" Inherits="AttendancePortal.StaffList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .style1
    {
        width: 100%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tbl">
    <tr>
        <td class="tblhead">
            Staff Reports -
            <asp:Label ID="lblstaff" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>

            <br/>
                <hr />
                <asp:GridView ID="GvStaff" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false"
                     BorderColor="#339966" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    PageSize="10"
                    AllowSorting="True" EmptyDataText="No Record Found . . . !" 
                    ForeColor="#333333" GridLines="None" Width="100%" CssClass="table table-striped table-lightfont">
                     <AlternatingRowStyle BackColor="White" />
                    <Columns>
                       <asp:TemplateField HeaderText="Photo"> <ItemTemplate>
                    <asp:Image runat="server" ID="imgg" ImageUrl='<%#Eval("Image") %>' Width="45px" Height="50px" />
                    </ItemTemplate>                 
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Email" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_email" runat="server" Text='<%# Eval("email") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mobile" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_mobile" runat="server" Text='<%# Eval("mobile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="qualification" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_qualification" runat="server" Text='<%# Eval("qualification") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="city" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("city") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <asp:Panel ID="pnl_GridPagerRow" runat="server">
                        </asp:Panel>
                    </PagerTemplate>
                </asp:GridView>
           
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

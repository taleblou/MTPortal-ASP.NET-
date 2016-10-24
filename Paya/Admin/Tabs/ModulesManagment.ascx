<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModulesManagment.ascx.cs" Inherits="Paya.Admin.Tabs.ModulesManagment" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%--<cc1:InlineScript ID="InlineScript1" runat="server">
<script type="text/javascript">
    function showContextMenu(sender, e) {
        var menu = $find("<%= _rdctmnuModuleDef.ClientID %>");
        var rawEvent = e.get_domEvent().rawEvent;
        menu.show(rawEvent);
        e.get_item().select();
        $telerik.cancelRawEvent(rawEvent);
        var listBox = $find("<%= _lstModuleDef.ClientID %>");
        var listItem = listBox.get_selectedItem();
        var value = listItem.get_value();
        var showAddHeader;
        if (value == 34 || value == 3 || value == 31) {
            showAddHeader = true;
        }
        else {
            showAddHeader = false;
        }
        menuItems = menu.get_items();
        for (var i = 0; i < menuItems.get_count(); i++) {
            var menuItem = menuItems.getItem(i);
            switch (menuItem.get_value()) {
                case "AddHeader":
                    menuItem.set_enabled(showAddHeader);
                    break;
            }
        }
    }
    </script>

</cc1:InlineScript>--%>
<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<table width="100%">
    <tr>
        <td style="width: 20%; padding-top: 20px;" valign="top">
            <telerik:RadListBox ID="_lstModuleDef" OnClientContextMenu="showContextMenu" runat="server"
                OnItemDataBound="_lstModuleDef_ItemDataBound" Width="100%" Height="300px">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 48px">
                                <asp:Image ID="_imgModuleDef" runat="server" Width="48px" Height="48px" />
                            </td>
                            <td>
                                <asp:Label ID="_lblModuleDef" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:RadListBox>
            <telerik:RadContextMenu ID="_rdctmnuModuleDef" OnItemClick="_rdctmnuModuleDef_ItemClick"
                runat="server">
                <Items>
                    <telerik:RadMenuItem Text="افزودن به سر صفحه" Value="AddHeader" ImageUrl="~/Images/add.png" />
                    <telerik:RadMenuItem Text="افزودن به صفحه" Value="AddPage" ImageUrl="~/Images/add.png" />
                </Items>
            </telerik:RadContextMenu>
        </td>
        <td style="width: 80%" valign="top">
            <asp:PlaceHolder ID="_phlModules" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>

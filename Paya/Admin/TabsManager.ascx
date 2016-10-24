<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabsManager.ascx.cs" Inherits="Paya.Admin.TabsManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

    function onTabSelecting(sender, args) {
        if (args.get_tab().get_pageViewID()) {
            args.get_tab().set_postBack(false);
        }
    }
    function onClientContextMenuItemClicking(sender, args) {
        var menuItem = args.get_menuItem();
        var treeNode = args.get_node();
        menuItem.get_menu().hide();

        switch (menuItem.get_value()) {
            case "Copy":
                break;
            case "Rename":
                treeNode.startEdit();
                break;
            case "New":
                break;
            case "Edit":
                break;
            case "Up":
                break;
            case "Down":
                break;
            case "NewPage":
                break;
            case "AllPage":
                break;
            case "Delete":
                var result = confirm("آیا شما از حذف صفحه \"" + treeNode.get_text() + "\" مطمئن هستید؟");
                args.set_cancel(!result);
                break;
        }
    }

    function onClientContextMenuShowing(sender, args) {
        var treeNode = args.get_node();
        treeNode.set_selected(true);
        //enable/disable menu items
        setMenuItemsState(args.get_menu().get_items(), treeNode);
    }

    //this method disables the appropriate context menu items
    function setMenuItemsState(menuItems, treeNode) {
        for (var i = 0; i < menuItems.get_count(); i++) {
            var menuItem = menuItems.getItem(i);
            switch (menuItem.get_value()) {
                case "Copy":
                    formatMenuItem(menuItem, treeNode, 'کپی ->{0}');
                    break;
                case "Rename":
                    formatMenuItem(menuItem, treeNode, 'تغییر نام ->{0}');
                    break;
                case "Delete":
                    formatMenuItem(menuItem, treeNode, 'حذف ->{0}');
                    if (treeNode.get_attributes().getAttribute("Deleted") != null && treeNode.get_attributes().getAttribute("Deleted") == "1")
                        menuItem.set_enabled(false);
                    else
                        menuItem.set_enabled(true);
                    break;
                case "Edit":
                    formatMenuItem(menuItem, treeNode, 'ویرایش ->{0}');
                    break;
                case "Up":
                    if (treeNode.get_previousNode() == null || treeNode.get_value() == "0")
                        menuItem.set_enabled(false);
                    else
                        menuItem.set_enabled(true);
                    break;
                case "Down":
                    if (treeNode.get_nextNode() == null)
                        menuItem.set_enabled(false);
                    else
                        menuItem.set_enabled(true);
                    break;
            }
        }
    }

    function formatMenuItem(menuItem, treeNode, formatString) {
        var newText = String.format(formatString, treeNode.get_text());
        menuItem.set_text(newText);
    }

    function nodeClicking(sender, args) {
        var comboBox = $find("<%=_rcbxTabs.ClientID %>");

        var node = args.get_node()

        comboBox.set_text(node.get_text());

        comboBox.trackChanges();
        comboBox.get_items().getItem(0).set_value(node.get_text());
        comboBox.commitChanges();

        comboBox.hideDropDown();
    }

    function StopPropagation(e) {
        if (!e) {
            e = window.event;
        }

        e.cancelBubble = true;
    }

    function OnClientDropDownOpenedHandler(sender, eventArgs) {
        var tree = sender.get_items().getItem(0).findControl("radTreeTabs");
        var selectedNode = tree.get_selectedNode();
        if (selectedNode) {
            selectedNode.scrollIntoView();
        }
    }


    function enableDockDrag(enable, dockId, textboxId) {
        var dock = $find(dockId);
        if (enable) {
            dock._initializeDrag();
            var textbox = $find(textboxId);
            if (textbox) {
                $addHandler(textbox, "mousedown", function (e) {
                    e.stopPropagation();
                });
            }
        }
        else dock._disposeDrag();
    }
    function DeleteDock(dock, args) {
        if (!confirm("آیا شما مطمئن هستید؟")) {
            args.set_cancel(true);
        }
    }

    function SetDockPosition(dock, eventArgs) {
        var obj = dock;
        var cmd = eventArgs.command;
        if (obj) {
            var parentZone = obj.get_parent();
            if (parentZone) {
                var pos = obj.get_index();
                if ("Up" == cmd.get_name()) {
                    parentZone.dock(obj, pos - 1);
                }
                else if ("Down" == cmd.get_name()) {
                    parentZone.dock(obj, pos + 2);
                }
            }
        }
        return (false);
    }
</script>


</telerik:RadCodeBlock>
<%--<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="_pnlTreeView">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="_pnlTreeView"  LoadingPanelID="_rdlpPortalsManager" />
                <telerik:AjaxUpdatedControl ControlID="_pnlTabView" LoadingPanelID="_rdlpPortalsManager" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="_pnlTabView">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="_pnlTabView" LoadingPanelID="_rdlpPortalsManager" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>--%>
<telerik:RadAjaxPanel ID="RadAjaxLoadingPanel1"  runat="server" Skin="Default" LoadingPanelID="_rdlpPortalsManager">

<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<asp:Panel ID="_pnlTreeView" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: auto">
        <tr>
            <td>
                جستجو صفحه :
            </td>
            <td>
                <asp:TextBox ID="_txtTabSearch" runat="server" 
                    ontextchanged="_txtTabSearch_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="_btnTabSearch" runat="server" Text="جستجو صفحه" 
                    onclick="_btnTabSearch_Click" />
            </td>
            <td rowspan="2">
                <telerik:RadGrid ID="_rgTabs" runat="server">
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <telerik:RadTreeView ID="_rtrvTabs" runat="server" OnClientContextMenuShowing="onClientContextMenuShowing"
                    OnContextMenuItemClick="_rtrvTabs_ContextMenuItemClick" OnClientContextMenuItemClicking="onClientContextMenuItemClicking"
                    AllowNodeEditing="true" OnNodeEdit="_rtrvTabs_NodeEdit">
                    <%--<WebServiceSettings Path="~/Admin/TabsTreeView.aspx" Method="GetChildTabs" />--%>
                    <ContextMenus>
                        <telerik:RadTreeViewContextMenu ID="ContextMenuTabs" runat="server">
                            <Items>
                                <telerik:RadMenuItem Value="New" Text="صفحه جدید" ImageUrl="~/Images/add_page.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Edit" Text="ویرایش ..." ImageUrl="~/Images/page_process.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Copy" Text="کپی ..." ImageUrl="~/Images/copy_paste.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Rename" PostBack="false" Text="تغییر نام ..." ImageUrl="~/Images/rename.gif">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Delete" Text="حذف ..." ImageUrl="~/Images/delete_page.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem IsSeparator="true">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Up" Text="بالا" ImageUrl="~/Images/page_up.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Down" Text="پایین" ImageUrl="~/Images/page_down.png">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadTreeViewContextMenu>
                        <telerik:RadTreeViewContextMenu ID="RootContextMenuTabs" runat="server">
                            <Items>
                                <telerik:RadMenuItem Value="New" Text="صفحه جدید" ImageUrl="~/Images/add_page.png">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="AllPage" Text="مشاهده تمام صفحات" ImageUrl="~/Images/pages.png">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadTreeViewContextMenu>
                    </ContextMenus>
                </telerik:RadTreeView>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="_pnlTabView" runat="server">
    <table style="width: 100%">
        <tr>
            <td align="center">
                <asp:Button ID="_btnBack" runat="server" Text="بازگشت" OnClick="_btnBack_Click" Width="150px" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadTabStrip ID="_rtsTabs" runat="server" MultiPageID="_rmpTabs" Skin="Office2007"
                    OnTabClick="_rtsTabs_TabClick" OnClientTabSelecting="onTabSelecting">
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="_rmpTabs" runat="server" OnPageViewCreated="_rmpTabs_PageViewCreated">
                    <telerik:RadPageView ID="Properties" runat="server" CssClass="pageView">
                        <table cellpadding="3" cellspacing="3" style="width: 100%">
                            <tr>
                                <td style="width: 30%">
                                    نام صفحه :
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtTabName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    عنوان صفحه : (اگر خالی باشد از نام صفحه استفاده میشود.)
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtTabTitle" runat="server" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    صفحه پدر:
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="_rcbxTabs" runat="server" Height="140px" Width="215px" ShowToggleImage="True"
                                        Style="vertical-align: middle;" OnClientDropDownOpened="OnClientDropDownOpenedHandler"
                                        EmptyMessage="یک صفحه را انتخاب کنید" ExpandAnimation-Type="None" CollapseAnimation-Type="None">
                                        <ItemTemplate>

                                            <script type="text/javascript">
                                                var div1 = document.getElementById("div1");
                                                if (div1 != null)
                                                    div1.onclick = StopPropagation;
                                            </script>

                                            <div id="div1">
                                                <telerik:RadTreeView runat="server" ID="radTreeTabs" OnClientNodeClicking="nodeClicking">
                                                </telerik:RadTreeView>
                                            </div>
                                        </ItemTemplate>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    آدرس صفحه:
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtLink" runat="server" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    نوع لینک:
                                </td>
                                <td>
                                    <asp:DropDownList ID="_ddlTypeLink" runat="server">
                                        <asp:ListItem Text="همین صفحه" Value="Self"></asp:ListItem>
                                        <asp:ListItem Text="صفحه جدید" Value="Blank"></asp:ListItem>
                                        <asp:ListItem Text="بدون لینک" Value="Empty"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    کلمات کلیدی:
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtKeywords" runat="server" TextMode="MultiLine" Width="90%" Rows="4"
                                        Columns="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    توضیحات:
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtDescription" runat="server" TextMode="MultiLine" Width="90%"
                                        Rows="4" Columns="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="_cbxMainMenu" runat="server" Text="نمايش در منوي اصلی" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="_cbxHorzMenu" runat="server" Text="نمايش در منوي افقي" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="_cbxFooter" runat="server" Text="نمايش در فوتر" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="_cbxMenu" runat="server" Text="نمایش منو" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="_cbxHeader" runat="server" Text="نمایش سرصفحه" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="_btnSubmit" runat="server" Text="" OnClick="_btnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Panel>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="_rdlpPortalsManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>

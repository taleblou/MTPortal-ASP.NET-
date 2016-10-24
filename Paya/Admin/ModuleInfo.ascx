<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleInfo.ascx.cs" Inherits="Paya.Admin.ModuleInfo" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<style type="text/css">
    .style1
    {
        height: 38px;
    }
</style>
<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<table style="width: 100%" cellpadding="7px">
    <tr>
        <td style="width: 30%">
            عنوان برنامه:
        </td>
        <td>
            <asp:TextBox ID="_txtModuleTitle" Width="50%" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_txtModuleTitle"
                Display="Dynamic" ErrorMessage="لطفا عنوان برنامه وارد نمائید." ValidationGroup="ModuleInfo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            مدت زمان ذخیره سازی در حافظه(ثانیه):
        </td>
        <td>
            <telerik:RadNumericTextBox ID="_rdtxtCachTime" runat="server" EmptyMessage="لطفا یک عدد وارد نمائید."
                MinValue="0" ValidationGroup="ModuleInfo"  MaxValue="86400"
                Width="250px">
                <NumberFormat DecimalDigits="0" />
            </telerik:RadNumericTextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_rdtxtCachTime"
                Display="Dynamic" ErrorMessage="لطفا یک عدد وارد نمائید." ValidationGroup="ModuleInfo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            بازه به روز رسانی(روز):
        </td>
        <td>
            <telerik:RadNumericTextBox ID="_rdtxtRefreshCachTime" runat="server" EmptyMessage="لطفا یک عدد وارد نمائید."
                MinValue="0" ValidationGroup="ModuleInfo"  MaxValue="30"
                Width="250px">
                <NumberFormat DecimalDigits="0" />
            </telerik:RadNumericTextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_rdtxtRefreshCachTime"
                Display="Dynamic" ErrorMessage="لطفا یک عدد وارد نمائید." ValidationGroup="ModuleInfo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            در صورت انتخاب این گزینه این برنامه به صورت خودکار به صفحه اول پرتال منتقل می شود.
            <br />
            <asp:CheckBox ID="_cbxShowInAllTabs" Text="در همه صفحات نشان داده شود." runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            صفحه برنامه:
        </td>
        <td colspan="2">
            <telerik:RadComboBox ID="_rdcboModuleTabs" Width="250px" Height="100px" runat="server"
                OnClientDropDownOpened="OnClientDropDownOpenedHandler">
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
        <td class="style1">
            قالب برنامه:
        </td>
        <td colspan="2" class="style1">
            <telerik:RadComboBox ID="_rdcboModuleLayout" Width="250px" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr id="trEditor" style="display: none">
        <td colspan="3">
            برای قرار گرقتن عنوان برنامه از کلمه [moduletitle] و برای بارگزاری خود برتامه در
            مکان مورد نظر از کلمه [modulebody] استفاده نمائید.
            <telerik:RadEditor Width="100%" ID="_rdedModuleLayout" runat="server">
                <CssFiles>
                    <telerik:EditorCssFile Value="~/UI/ShareCSS/EditorContentArea.css" />
                </CssFiles>
            </telerik:RadEditor>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="_btnSubmit" runat="server" Text="اعمال تغییرات" ValidationGroup="ModuleInfo"
                OnClick="_btnSubmit_Click" />
        </td>
    </tr>
</table>

<%--<script type="text/javascript">
    function pageLoad() {
        var comboBox = $find("<%= _rdcboModuleLayout.ClientID %>");
        if (comboBox != null) {
            var item = comboBox.get_selectedItem();
            var tr = document.getElementById('trEditor');
            if (item.get_value() == "DesignedLayout.ascx")
                tr.style.display = '';
            else
                tr.style.display = 'none';
        }
    }
</script>--%>

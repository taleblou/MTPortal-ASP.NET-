﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortalsManager.ascx.cs" Inherits="Paya.Admin.PortalsManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript">
        function showContextMenu(sender, e) {
            var menu = $find("<%= _rdctmnuportals.ClientID %>");
            var rawEvent = e.get_domEvent().rawEvent;
            menu.show(rawEvent);
            e.get_item().select();
            $telerik.cancelRawEvent(rawEvent);
            var listBox = $find("<%= _rdlstPortals.ClientID %>");
            var listItem = listBox.get_selectedItem();
            var value = listItem.get_value();
            var showAddHeader;
            if (value == 0) {
                showAddHeader = false;
            }
            else {
                showAddHeader = true;
            }
            menuItems = menu.get_items();
            for (var i = 0; i < menuItems.get_count(); i++) {
                var menuItem = menuItems.getItem(i);
                switch (menuItem.get_value()) {
                    case "DeletePortal":
                        menuItem.set_enabled(showAddHeader);
                        break;
                }
            }
        }
        function txtPortalPath_Validate(source, arguments) {
            var txt = document.getElementById('<%=_txtPortalPath.ClientID%>');
            if (txt != null) {
                if (txt.lenght != 0) {
                    var str = txt.value;
                    var ok = true;
                    for (var i = 0; i < str.length; i++) {
                        if (str[i].charCodeAt(0) > 128) {
                            ok = false;
                            break;
                        }
                    }

                    arguments.IsValid = ok;
                }
            }
        }  
    </script>

</telerik:RadCodeBlock>
<telerik:RadAjaxPanel ID="_rdpPortalsManager" runat="server" LoadingPanelID="_rdlpPortalsManager" >
<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<table style="width: 100%">
    <tr>
        <td style="width: 30%" valign="top">
            <telerik:RadListBox ID="_rdlstPortals" OnClientContextMenu="showContextMenu" runat="server"
                Width="100%" Height="300px">
            </telerik:RadListBox>
            <telerik:RadContextMenu ID="_rdctmnuportals" OnItemClick="_rdctmnuportals_ItemClick"
                runat="server">
                <Items>
                    <telerik:RadMenuItem Text="پرتال جدید" Value="AddPortal" ImageUrl="~/Images/add.png" />
                    <telerik:RadMenuItem Text="ویرایش پرتال" Value="EditPortal" ImageUrl="~/Images/edit.png" />
                    <telerik:RadMenuItem Text="حذف پرتال" Value="DeletePortal" ImageUrl="~/Images/delete.png" />
                    <telerik:RadMenuItem Text="نمایش پورتال " Value="BrowsPortal" ImageUrl="~/Images/Brows.png" />
                </Items>
            </telerik:RadContextMenu>
        </td>
        <td>
            <asp:Panel ID="_pnlPortalInfo" runat="server">
                <table style="width: 100%" cellpadding="5px">
                    <tr>
                        <td style="width: 25%">
                            <span style="color: Red">*</span>نام پورتال :
                        </td>
                        <td style="width: 50%">
                            <asp:TextBox ID="_txtPortalName" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td style="width: 25%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_txtPortalName"
                                ValidationGroup="PortalAdd" Display="Dynamic" ErrorMessage="لطفا نام پرتال را وارد نمائید."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">*</span> مسیر پورتال :
                        </td>
                        <td>
                            <asp:TextBox ID="_txtPortalPath" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_txtPortalPath"
                                ValidationGroup="PortalAdd" Display="Dynamic" ErrorMessage="لطفا مسیر پرتال را وارد نمائید."></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="مسیر پرتال باید به صورت لاتین وارد شود."
                                ValidationGroup="PortalAdd" ControlToValidate="_txtPortalPath" Display="Dynamic"
                                ClientValidationFunction="txtPortalPath_Validate"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            زبان پیش فرض :
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="_rdcboLanguage" runat="server">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            پوسته گرافیکی پیش فرض :
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="_rdcboStyle" runat="server">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">*</span> پست الکترونیکی :
                        </td>
                        <td>
                            <asp:TextBox ID="_txtEmail" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="PortalAdd"
                                ControlToValidate="_txtEmail" runat="server" ErrorMessage="پست الکترونیکی خود را وارد کنید."></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="PortalAdd"
                                ControlToValidate="_txtEmail" runat="server" ErrorMessage="پست اکترونیکی خود را صحیح وارد کنید."
                                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">*</span> تعداد دفعات ورود اشتباه کاربر:(صفر=نامحدود)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="_rdtxtCount" Width="100%" runat="server" EmptyMessage="لطفا یک عدد وارد نمائید."
                                MinValue="0" ValidationGroup="ModuleInfo" DataType="System.Int32" MaxValue="50">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_rdtxtCount"
                                Display="Dynamic" ErrorMessage="لطفا یک عدد وارد نمائید." ValidationGroup="PortalAdd"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            اقدام لازم در ورود اشتباه کاربر
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="_rdcboAction" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="نمایش کاپچا" Value="1" />
                                    <telerik:RadComboBoxItem Text="قفل کاربر" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:CheckBox ID="_cbxCaptcha" runat="server" Text="نمایش کاپچا در هنگام ثبت نام کاربر" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:CheckBox ID="_cbxMonitoring" runat="server" Text="ثبت فعالیت ها" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="_pnlUser" GroupingText="مشخصات مدیر پورتال" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            نام کاربری :
                                        </td>
                                        <td>
                                            <span style="color: Red">*</span><asp:TextBox ID="_txtUserName" runat="server" ValidationGroup="PortalAdd"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="_btnChecKUserName" runat="server" Text="چک کردن نام کاربری" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_txtUserName"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: Red">*</span>رمز عبور:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="_txtPass" runat="server" TextMode="Password" ValidationGroup="PortalAdd"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="_txtPass"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: Red">*</span>تکرار رمز عبور :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="_txtPassRep" runat="server" TextMode="Password" ValidationGroup="PortalAdd"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="_txtPassRep"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmpvPass" runat="server" ControlToCompare="_txtPass" ControlToValidate="_txtPassRep"
                                                Display="Dynamic" ValidationGroup="PortalAdd" Text="تکرار رمز عبور صحیح نمی باشد."></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: Red">*</span> نام :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="_txtFirstName" runat="server" ValidationGroup="PortalAdd" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="_txtFirstName"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: Red">*</span>نام خانوادگی :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="_txtLastName" runat="server" ValidationGroup="PortalAdd" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="_txtLastName"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: Red">*</span>پست الکترونيکي مدیر :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="_txtUserEmail" runat="server" ValidationGroup="PortalAdd" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="_txtUserEmail"
                                                Display="Dynamic" ValidationGroup="PortalAdd">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="_txtUserEmail"
                                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="PortalAdd" Text="پست الکترونيکي مدیر را صحیح وارد نمائید."></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            فیلد های * دار الزامی می باشد.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="_btnSave" runat="server" Text="ثبت اطلاعات" ValidationGroup="PortalAdd"
                                OnClick="_btnSave_Click" />
                            <asp:Button ID="_btnBack" runat="server" Text="بازگشت" OnClick="_btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="_pnlModuleDefPortal" runat="server" GroupingText="لیست برنامه ها">
                <table width="100%" border="0">
                    <tr>
                        <td align="center">
                            <span style="text-align: center;">لیست برنامه های انتخابی </span>
                        </td>
                        <td align="center">
                            <span style="text-align: center;">لیست تمامی برنامه ها </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxModuleDefPortal" ButtonSettings-ShowTransferAll="false"
                                ButtonSettings-ShowTransfer="false" runat="server" EnableDragAndDrop="True" AllowTransfer="true"
                                AutoPostBackOnTransfer="true" SelectionMode="Multiple" Width="100%" Height="100px">
                                <EmptyMessageTemplate>
                                    هیچ مجوزی داده نشده است.
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                        </td>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxAllModuleDef" runat="server" AllowTransfer="True"
                                AutoPostBackOnTransfer="true" EnableDragAndDrop="True" SelectionMode="Multiple"
                                TransferToID="_rdlstboxModuleDefPortal" Width="100%" OnTransferred="RadListBox_Transferred"
                                Height="100px">
                                <EmptyMessageTemplate>
                                    هیچ مجوزی داده نشده است.
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="_rdlpPortalsManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>

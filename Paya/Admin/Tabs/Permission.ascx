<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Permission.ascx.cs" Inherits="Paya.Admin.Tabs.Permission" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<table cellpadding="1" cellspacing="1" width="100%" align="center" border="0" dir="rtl">
    <tr>
        <td align="center">
            <asp:Label ID="_lblPageTitle" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <telerik:RadAjaxPanel ID="_rdpObjRole" runat="server" Width="100%">
                <table width="100%" border="0">
                    <tr>
                        <td align="center">
                            <span style="text-align: center;">نقشهای دارای مجوز </span>
                        </td>
                        <td align="center">
                            <span style="text-align: center;">نقش های فاقد مجوز </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxHaveRole" ButtonSettings-ShowTransferAll="false"
                                ButtonSettings-ShowTransfer="false" runat="server" EnableDragAndDrop="True" AllowTransfer="true"
                                AutoPostBackOnTransfer="true" SelectionMode="Multiple" Width="100%" Height="100px">
                                <EmptyMessageTemplate>
                                    هیچ مجوزی داده نشده است.
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                        </td>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxLackingRole" runat="server" AllowTransfer="True"
                                AutoPostBackOnTransfer="true" EnableDragAndDrop="True" SelectionMode="Multiple"
                                TransferToID="_rdlstboxHaveRole" Width="100%" OnTransferred="RadListBox_Transferred"
                                Height="100px">
                                <EmptyMessageTemplate>
                                    هیچ مجوزی یافت نشد.
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                        </td>
                    </tr>
                </table>
            </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="_rdlpObjRole" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
        </td>
    </tr>
</table>

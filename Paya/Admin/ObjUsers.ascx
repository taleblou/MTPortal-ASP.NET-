<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjUsers.ascx.cs" Inherits="Paya.Admin.ObjUsers" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<table cellpadding="1" cellspacing="1" width="100%" align="center" border="0" dir="rtl">
    <tr>
        <td colspan="3" align="center">
            <asp:Label ID="_lblAuthTypeTitle" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            نام کاربری :
            <asp:TextBox ID="_txtUserName" runat="server"></asp:TextBox>
            <asp:DropDownList ID="_ddlRole" runat="server">
            </asp:DropDownList>
            <asp:Button ID="_btnSearch" runat="server" Text="جستجو" OnClick="_btnSearch_Click" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <telerik:RadAjaxPanel ID="_rdpObjUser" runat="server" Width="100%">
                <tr>
                    <td align="center">
                        <span style="text-align: center;">کاربران داری مجوز </span>
                    </td>
                    <td align="center">
                        <span style="text-align: center;">کاربران فاقد مجوز </span>
                    </td>
                </tr>
                <table width="100%" border="0">
                    <tr>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxHaveUser" ButtonSettings-ShowTransferAll="false"
                                ButtonSettings-ShowTransfer="false" runat="server" EnableDragAndDrop="True" AllowTransfer="true"
                                AutoPostBackOnTransfer="true" SelectionMode="Multiple" Width="100%" Height="150px">
                                <EmptyMessageTemplate>
                                    هیچ کاربری مشخص نشده است.
                                </EmptyMessageTemplate>
                                <ItemTemplate>
                                    <span>
                                        <%#Eval( "UserName")%>
                                    </span>
                                    <br />
                                    <span>
                                        <%# Eval("FirstName")%>&nbsp;
                                        <%# Eval( "LastName")%>
                                    </span>
                                    <hr />
                                </ItemTemplate>
                            </telerik:RadListBox>
                        </td>
                        <td style="width: 50%">
                            <telerik:RadListBox ID="_rdlstboxLackingUser" runat="server" AllowTransfer="True"
                                AutoPostBackOnTransfer="true" EnableDragAndDrop="True" SelectionMode="Multiple"
                                TransferToID="_rdlstboxHaveUser" Width="100%" OnTransferred="RadListBox_Transferred"
                                Height="150px">
                                <EmptyMessageTemplate>
                                    هیچ کاربری موجود نیست.
                                </EmptyMessageTemplate>
                                <ItemTemplate>
                                    <span>
                                        <%#Eval( "UserName")%>
                                    </span>
                                    <br />
                                    <span>
                                        <%# Eval("FirstName")%>&nbsp;
                                        <%# Eval( "LastName")%>
                                    </span>
                                    <hr />
                                </ItemTemplate>
                            </telerik:RadListBox>
                        </td>
                    </tr>
                </table>
            </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="_rdlpObjUser" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
        </td>
    </tr>
</table>

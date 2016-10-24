<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentsManager.ascx.cs" Inherits="Paya.Admin.ContentsManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<telerik:RadAjaxPanel ID="_rdpContentsManager" runat="server" LoadingPanelID="_rdlpContentsManager" >
<asp:Panel ID="Panel1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td nowrap="nowrap">
                جستجو صفحه :
            </td>
            <td>
                <asp:TextBox ID="_txtTabSearch" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="_btnTabSearch" runat="server" Text="جستجو صفحه" />
            </td>
            <td rowspan="2" style="width: 70%;">
                <asp:PlaceHolder ID="_phlModules" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <telerik:RadTreeView ID="_rdtrvTabs" runat="server" OnNodeClick="_rdtrvTabs_NodeClick">
                </telerik:RadTreeView>
            </td>
        </tr>
    </table>
</asp:Panel>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="_rdlpContentsManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>

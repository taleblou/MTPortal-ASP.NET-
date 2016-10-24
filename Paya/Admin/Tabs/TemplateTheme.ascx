<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateTheme.ascx.cs" Inherits="Paya.Admin.Tab.TemplateTheme" %>
<table style="width: 100%">
    <tr>
        <td>
            <asp:Panel ID="_pnlTemplate" runat="server">
                <asp:RadioButtonList ID="_rblstTemplate" runat="server" RepeatColumns="4">
                </asp:RadioButtonList>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="_pnlTheme" runat="server">
                <asp:RadioButtonList ID="_rblstTheme" runat="server" RepeatColumns="4">
                </asp:RadioButtonList>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="_btnSetStyle" runat="server" Text="ثبت اطلاعات" OnClick="_btnSetStyle_Click" />
        </td>
    </tr>
</table>

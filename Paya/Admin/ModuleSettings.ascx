<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleSettings.ascx.cs" Inherits="Paya.Admin.ModuleSettings" %>
<asp:Panel ID="_pnlPortalSettings" runat="server">
    <div>
        <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
        <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:PlaceHolder ID="_plhSettings" EnableViewState="false" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="_btnSaveSetting" runat="server" Text="اعمال تغییرات" 
                    onclick="_btnSaveSetting_Click" />
                <asp:Button ID="_btnResetSetting" runat="server" Text="حالت پیش فرض" 
                    onclick="_btnResetSetting_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="_pnlModuleSettings" runat="server">
</asp:Panel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterMenu.ascx.cs" Inherits="Paya.Menu.FooterMenu" %>
<asp:Repeater ID="_rpFooterMenu" runat="server">
    <HeaderTemplate>
        <ul id="FooterMenuUL" class="FooterMenuUL">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink runat="server" ID="_hyplnkTab" Target='<%# DataBinder.Eval(Container.DataItem, "Target") %>'
                NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Url") %>'>
<span ><%# DataBinder.Eval(Container.DataItem, "Name") %></span>
            </asp:HyperLink>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

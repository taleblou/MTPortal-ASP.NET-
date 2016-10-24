<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesManager.ascx.cs"
    Inherits="Paya.Admin.RolesManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<telerik:RadScriptBlock runat="server" ID="scriptBlock">
    <script type="text/javascript">
                <!--
        function onRowDropping(sender, args) {
            if (sender.get_id() == "<%=_rgAllUser.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=_rgUsersInRole.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }
            else if (sender.get_id() == "<%= _rgUsersInRole.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=_rgAllUser.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }

        }

        function isChildOf(parentId, element) {
            while (element) {
                if (element.id && element.id.indexOf(parentId) > -1) {
                    return true;
                }
                element = element.parentNode;
            }
            return false;
        }
                    -->
    </script>
</telerik:RadScriptBlock>
<telerik:RadAjaxPanel ID="_rdpRolesManager" runat="server" LoadingPanelID="_rdlpRolesManager">
    <div>
        <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green"></asp:Label>
        <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <asp:Panel ID="_pnlRoles" runat="server">
        <telerik:RadGrid ID="_rgRoles" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="_rgRoles_DeleteCommand"
            OnInsertCommand="_rgRoles_InsertCommand" OnItemDataBound="_rgRoles_ItemDataBound"
            OnItemCommand="_rgRoles_ItemCommand" OnNeedDataSource="_rgRoles_NeedDataSource"
            OnUpdateCommand="_rgRoles_UpdateCommand">
            <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="RoleId">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="نام نقش" UniqueName="RoleKey" DataField="RoleKey"
                        DataType="System.String" SortExpression="RoleKey">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridButtonColumn CommandName="Users" Text="کاربران" UniqueName="Users" ButtonType="LinkButton">
                </telerik:GridButtonColumn>
                <telerik:GridButtonColumn CommandName="Edit" Text="ویرایش" ImageUrl="~/Images/edit.png"
                    UniqueName="Select" ButtonType="ImageButton">
                </telerik:GridButtonColumn>
                <telerik:GridButtonColumn CommandName="Delete" Text="حذف"  UniqueName="Delete" ImageUrl="~/Images/delete.png"
                    ButtonType="ImageButton">
                </telerik:GridButtonColumn>--%>
                    <telerik:GridTemplateColumn UniqueName="User" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="_lnkbtnUsers" runat="server" CommandName="User">کاربران</asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Select" AllowFiltering="false">
                        <ItemTemplate>
                            <div style="cursor: pointer;">
                                <asp:ImageButton ID="_imgbtnEdit" CommandName="Edit" runat="server" AlternateText="ویرایش"
                                    ImageUrl="~/Images/edit.png"></asp:ImageButton>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Delete" AllowFiltering="false">
                        <ItemTemplate>
                            <div style="cursor: pointer;">
                                <asp:ImageButton ID="_imgbtnDelete" CommandName="Delete" runat="server" AlternateText="حذف"
                                    ImageUrl="~/Images/delete.png"></asp:ImageButton>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings EditFormType="Template">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                    <FormTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 20%">
                                    نام نقش :
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtRoleKey" runat="server" ValidationGroup="AddRole"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="AddRole" ControlToValidate="_txtRoleKey" runat="server">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    عنوان نقش در زبان های مختلف پرتال :
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Repeater ID="_rpLangRole" runat="server" OnItemDataBound="_rpLangRole_ItemDataBound">
                                        <HeaderTemplate>
                                            <table width="100%">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="_lblLanguageId" runat="server" Text='<%#Eval("LanguageId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="_lblLangName" runat="server" Text='<%#Eval("LanguageName") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtRoleName" runat="server" ValidationGroup="AddRole"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddRole" ControlToValidate="_txtRoleName" runat="server" >*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddRole" />
                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "درج اطلاعات"  : "ویرایش"%>'
                                        ValidationGroup="AddRole" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="بازگشت" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>
    <asp:Panel ID="_pnlUsers" runat="server">
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="_btnBackRole" runat="server" Text="بازگشت" OnClick="_btnBackRole_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    تمام کاربران
                </td>
                <td align="center">
                    <asp:Label ID="_lblUserInRole" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    <telerik:RadGrid ID="_rgAllUser" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                        AllowMultiRowSelection="true" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="None" OnNeedDataSource="_rgAllUser_NeedDataSource" OnRowDrop="RadGridUser_RowDrop">
                        <MasterTableView DataKeyNames="UserId" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="نام کاربری" UniqueName="UserName" DataField="UserName"
                                    DataType="System.String" SortExpression="UserName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام" UniqueName="FirstName" DataField="FirstName"
                                    DataType="System.String" SortExpression="FirstName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام خانوادگی" UniqueName="LastName" DataField="LastName"
                                    DataType="System.String" SortExpression="LastName">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowRowsDragDrop="True">
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
                            <ClientEvents OnRowDropping="onRowDropping" />
                            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
                <td>
                    <telerik:RadGrid ID="_rgUsersInRole" runat="server" AllowFilteringByColumn="True"
                        AllowMultiRowSelection="true" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="None" OnNeedDataSource="_rgUsersInRole_NeedDataSource" OnRowDrop="RadGridUser_RowDrop">
                        <MasterTableView DataKeyNames="UserId" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="نام کاربری" UniqueName="UserName" DataField="UserName"
                                    DataType="System.String" SortExpression="UserName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام" UniqueName="FirstName" DataField="FirstName"
                                    DataType="System.String" SortExpression="FirstName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام خانوادگی" UniqueName="LastName" DataField="LastName"
                                    DataType="System.String" SortExpression="LastName">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowRowsDragDrop="True">
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
                            <ClientEvents OnRowDropping="onRowDropping" />
                            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="_rdlpRolesManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>

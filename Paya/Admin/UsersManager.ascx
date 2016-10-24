<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersManager.ascx.cs" Inherits="Paya.Admin.UsersManager" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="pcalc" Namespace="PersianDatePickup" Assembly="PersianDatePickup" %>

<telerik:RadScriptBlock runat="server" ID="scriptBlock">

    <script type="text/javascript">
                <!--
        function onRowDropping(sender, args) {
            if (sender.get_id() == "<%=_rgAllRoles.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=_rgUsersRole.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }
            else if (sender.get_id() == "<%= _rgUsersRole.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=_rgAllRoles.ClientID %>', node)) {
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

<telerik:RadAjaxPanel ID="_rdpUsersManager" runat="server" LoadingPanelID="_rdlpUsersManager" >

<telerik:RadTabStrip ID="_rtsUsers" runat="server" MultiPageID="_rmpUser" Skin="Office2007"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab Text="کاربران پرتال" PageViewID="_rpPortalUser">
        </telerik:RadTab>
        <telerik:RadTab Text="کاربران متقاضی ثبت نام" PageViewID="_rpRequestRegUser">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="_rmpUser" runat="server" SelectedIndex="0">
    <telerik:RadPageView ID="_rpPortalUser" runat="server">
        <div>
            <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        </div>
        <asp:Panel ID="_pnlUsers" runat="server">
            <telerik:RadGrid ID="_rgPortalUser" runat="server" AllowFilteringByColumn="True"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                OnDeleteCommand="_rgPortalUser_DeleteCommand" OnInsertCommand="_rgPortalUser_InsertCommand"
                OnItemCommand="_rgPortalUser_ItemCommand" OnItemDataBound="_rgPortalUser_ItemDataBound"
                OnNeedDataSource="_rgPortalUser_NeedDataSource" OnUpdateCommand="_rgPortalUser_UpdateCommand">
                <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="UserId" EditMode="EditForms">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="UserName" SortExpression="UserName" DataField="UserName"
                            HeaderText="نام کاربری" DataType="System.String">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Email" SortExpression="Email" DataField="Email"
                            HeaderText="پست الکترونیکی" DataType="System.String">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="FirstName" SortExpression="FirstName" DataField="FirstName"
                            HeaderText="نام" DataType="System.String">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="LastName" SortExpression="LastName" DataField="LastName"
                            HeaderText="نام خانوادگی" DataType="System.String">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Profile" Text="پروفایل" UniqueName="Profile"
                            ButtonType="LinkButton">
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn CommandName="UserRole" Text="نقش های کاربر" UniqueName="UserRole"
                            ButtonType="LinkButton">
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="_imgbtnUserLock" CommandName="LockUser" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="Edit" Text="ویرایش" ImageUrl="~/Images/edit.png"
                            UniqueName="Select" ButtonType="ImageButton">
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="Delete" ImageUrl="~/Images/delete.png"
                            ConfirmText="آیا شما مطمئن هستید؟" ButtonType="ImageButton">
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                        <FormTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table id="tblUserName" style="width: 100%">
                                            <tr>
                                                <td>
                                                    نام کاربری :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtUserName" runat="server" Text='<%#Bind("UserName") %>'></asp:TextBox>
                                                </td>
                                                <td><%--
                                                    <input id="_btnUserName" type="button" value="ویرایش نام کاربری" onclick="javascript:SetTextBox()"
                                                        runat="server" visible='<%#!(Container is GridEditFormInsertItem) %>' />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Visible='<%#!(Container is GridEditFormInsertItem) %>'
                                                        runat="server" ErrorMessage="نام کاربری را وارد کنید." ValidationGroup="EditUserName"
                                                        Display="Dynamic" ControlToValidate="_txtUserName"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="نام کاربری را وارد کنید."
                                                        Text="*" ValidationGroup="AddUser" Display="Dynamic" Visible='<%#(Container is GridEditFormInsertItem) %>'
                                                        ControlToValidate="_txtUserName"></asp:RequiredFieldValidator>
                                                    <asp:Button ID="_btnUpdateUser" runat="server" Text="ثبت اطلاعات" CommandName="Update"
                                                        ValidationGroup="EditUserName" CommandArgument="UpdateUserName" Style="display: none" />
                                                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                                        <script type="text/javascript">
                                                            function SetTextBox() {
                                                                alert("begin");
                                                                var txt = document.getElementById('<%# Container.FindControl("_txtUserName").ClientID %>');
                                                                var btn = document.getElementById('<%# Container.FindControl("_btnUpdateUser").ClientID %>');

                                                                if (txt != null && btn != null) {
                                                                    txt.readOnly = false;
                                                                    btn.style.display = '';
                                                                }
                                                                var btn2 = document.getElementById('<%# Container.FindControl("_btnUserName").ClientID %>');
                                                                if (btn2 != null)
                                                                    btn2.style.display = 'none';
                                                                var tbl = document.getElementById("tblinfo");
                                                                if (tbl != null)
                                                                    tbl.style.display = 'none';
                                                                var tbl = document.getElementById("tblpass");
                                                                if (tbl != null)
                                                                    tbl.style.display = 'none';
                                                            }
                                                        </script>

                                                    </telerik:RadCodeBlock>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblpass" style="width: 100%">
                                            <tr>
                                                <td>
                                                    رمز عبور :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtPass" runat="server" TextMode="Password" Text="رمز عبور قدیمی"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Visible='<%#(Container is GridEditFormInsertItem) %>'
                                                        ErrorMessage="رمز عبور را وارد نمائید." ControlToValidate="_txtPass" ValidationGroup="AddUser">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Visible='<%#!(Container is GridEditFormInsertItem) %>'
                                                        runat="server" ErrorMessage="رمز عبور را وارد نمائید." ControlToValidate="_txtPass"
                                                        ValidationGroup="ChangePass"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    تکرار رمز عیور :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtPassConfirm" runat="server" TextMode="Password" Text="رمز عبور قدیمی"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="رمز عبور را تکرار نمائید."
                                                        Visible='<%#!(Container is GridEditFormInsertItem) %>' ControlToValidate="_txtPassConfirm"
                                                        ValidationGroup="ChangePass"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="رمز عبور درست تکرار نشده است."
                                                        Visible='<%#!(Container is GridEditFormInsertItem) %>' ControlToCompare="_txtPass"
                                                        ControlToValidate="_txtPassConfirm" ValidationGroup="ChangePass"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="رمز عبور را تکرار نمائید."
                                                        Visible='<%#(Container is GridEditFormInsertItem) %>' ControlToValidate="_txtPassConfirm"
                                                        ValidationGroup="AddUser">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="رمز عبور درست تکرار نشده است."
                                                        Visible='<%#(Container is GridEditFormInsertItem) %>' ControlToCompare="_txtPass"
                                                        ControlToValidate="_txtPassConfirm" ValidationGroup="AddUser">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td colspan="3">
                                                    <input id="_btnPass" type="button" value="تغییر رمز عبور" onclick="javascript:SetforChangePass()"
                                                        runat="server" visible='<%#!(Container is GridEditFormInsertItem) %>' />
                                                    <asp:Button ID="_btnChangePass" runat="server" Text="تغییر رمز عبور" CommandName="Update"
                                                        CommandArgument="UpdatePass" ValidationGroup="ChangePass" Style="display: none" />
                                                    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                                        <script type="text/javascript">
                                                            function SetforChangePass() {
                                                                var btn = document.getElementById('<%# Container.FindControl("_btnChangePass").ClientID %>');
                                                                var txt = document.getElementById('<%# Container.FindControl("_txtPass").ClientID %>');
                                                                var txt2 = document.getElementById('<%# Container.FindControl("_txtPassConfirm").ClientID %>');
                                                                if (btn != null && txt != null && txt2 != null) {
                                                                    txt.readOnly = false;
                                                                    txt2.readOnly = false;
                                                                    btn.style.display = '';
                                                                }
                                                                var btn2 = document.getElementById('<%# Container.FindControl("_btnPass").ClientID %>');
                                                                if (btn2 != null)
                                                                    btn2.style.display = 'none';
                                                                var tbl = document.getElementById("tblinfo");
                                                                if (tbl != null)
                                                                    tbl.style.display = 'none';
                                                                var tbl = document.getElementById("tblUserName");
                                                                if (tbl != null)
                                                                    tbl.style.display = 'none';
                                                            }
                                                        </script>

                                                    </telerik:RadCodeBlock>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblinfo" style="width: 100%">
                                            <tr>
                                                <td>
                                                    نام :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtFirstName" runat="server" Text='<%#Bind("FirstName") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="نام را وارد کنید."
                                                        Text="*" ValidationGroup="AddUser" Display="Dynamic" ControlToValidate="_txtFirstName"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    نام خانوادگی :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtLastName" runat="server" Text='<%#Bind("LastName") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="نام خانوادگی را وارد کنید."
                                                        Text="*" ValidationGroup="AddUser" Display="Dynamic" ControlToValidate="_txtLastName"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    پست الکترونیکی :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="_txtEmail" runat="server" Text='<%#Bind("Email") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="پست الکترونیکی را وارد کنید."
                                                        Text="*" ValidationGroup="AddUser" Display="Dynamic" ControlToValidate="_txtEmail"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="پست الکترونیکی را صحیح وارد کنید."
                                                        Text="*" ValidationGroup="AddUser" Display="Dynamic" ControlToValidate="_txtEmail"
                                                        ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    پوسته گرافیکی :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="_ddlUserStyle" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="_rowSuperUser">
                                                <td colspan="3">
                                                    <asp:CheckBox ID="_cbxSuperUser" runat="server" Text="کابر ادمین" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddUser" />
                                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "درج اطلاعات"  : "ویرایش"%>'
                                                        ValidationGroup="AddUser" runat="server" CommandArgument="UpdateInfo" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                    </asp:Button>&nbsp;
                                                    <asp:Button ID="btnCancel" Text="بازگشت" runat="server" CausesValidation="False"
                                                        CommandName="Cancel"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
           <%-- <telerik:RadContextMenu ID="RadMenu1" runat="server" OnItemClick="RadMenu1_ItemClick"
            EnableRoundedCorners="true" EnableShadows="true">
            <Items>
                <telerik:RadMenuItem Text="کاربر جدید" />
                <telerik:RadMenuItem Text="ویرایش" />
                <telerik:RadMenuItem Text="حذف" />
            </Items>
        </telerik:RadContextMenu>--%>
        </asp:Panel>
        <asp:Panel ID="_pnlProfile" runat="server">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="_btnBackUser" runat="server" Text="بازگشت" OnClick="BtnBackUser_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                   
                        <asp:Panel ID="_pnlControl" runat="server">
                        <pcalc:PersianDatePickup runat="server" ID="dd"/>
                         <telerik:RadBinaryImage ID="asdfsadd" runat="server" />
                        </asp:Panel>
                        <telerik:RadAsyncUpload ID="RadAsyncUpload12" runat="server">
                        </telerik:RadAsyncUpload>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="_pnlUserRoles" runat="server">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="بازگشت" OnClick="BtnBackUser_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        نقش های موجود
                    </td>
                    <td align="center">
                        نقش های کاربر
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <telerik:RadGrid ID="_rgAllRoles" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                            AllowMultiRowSelection="true" AllowSorting="True" AutoGenerateColumns="False"
                            GridLines="None" OnNeedDataSource="_rgAllRoles_NeedDataSource" OnRowDrop="RadGridRole_RowDrop">
                            <MasterTableView DataKeyNames="RoleId" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="نام نقش" UniqueName="RoleKey" DataField="RoleKey"
                                        DataType="System.String" SortExpression="RoleKey">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="عنوان نقش" UniqueName="RoleName" DataField="RoleName"
                                        DataType="System.String" SortExpression="RoleName">
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
                        <telerik:RadGrid ID="_rgUsersRole" runat="server" AllowFilteringByColumn="True" AllowMultiRowSelection="true"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="_rgUsersRole_NeedDataSource" OnRowDrop="RadGridRole_RowDrop">
                            <MasterTableView DataKeyNames="RoleId" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="نام نقش" UniqueName="RoleKey" DataField="RoleKey"
                                        DataType="System.String" SortExpression="RoleKey">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="عنوان نقش" UniqueName="RoleName" DataField="RoleName"
                                        DataType="System.String" SortExpression="RoleName">
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
    </telerik:RadPageView>
    <telerik:RadPageView ID="_rpRequestRegUser" runat="server">
        <telerik:RadGrid ID="_rgRegisterReq" runat="server" AllowFilteringByColumn="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
            OnDeleteCommand="_rgRegisterReq_DeleteCommand" OnItemCommand="_rgRegisterReq_ItemCommand"
            OnNeedDataSource="_rgRegisterReq_NeedDataSource">
            <MasterTableView DataKeyNames="ReqId" EditMode="EditForms">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="UserName" SortExpression="UserName" DataField="UserName"
                        HeaderText="نام کاربری" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Email" SortExpression="Email" DataField="Email"
                        HeaderText="پست الکترونیکی" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FirstName" SortExpression="FirstName" DataField="FirstName"
                        HeaderText="نام" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="LastName" SortExpression="LastName" DataField="LastName"
                        HeaderText="نام خانوادگی" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PhoneNo" SortExpression="PhoneNo" DataField="PhoneNo"
                        HeaderText="تلفن اضطراری" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn CommandName="AcceptUser" Text="ثبت تقاضا" UniqueName="AcceptUser"
                        ButtonType="LinkButton">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="Delete" ImageUrl="~/Images/delete.png"
                        ConfirmText="آیا شما مطمئن هستید؟" ButtonType="ImageButton">
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
 </telerik:RadPageView>
</telerik:RadMultiPage>

</telerik:RadAjaxPanel>

<telerik:RadAjaxLoadingPanel ID="_rdlpUsersManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>

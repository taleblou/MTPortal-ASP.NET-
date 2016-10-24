<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlackList.ascx.cs" Inherits="Paya.Admin.BlackList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<telerik:RadAjaxPanel ID="_rdpBlackListManager" runat="server" LoadingPanelID="_rdlpBlackListManager" >
<telerik:RadTabStrip ID="_rdtsBlackList" runat="server" MultiPageID="_rdmpBlackList"
    Skin="Office2007" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab Text="جلوگیری از دسترسی یک کامپیوتر" PageViewID="_rdpBannedIpAddress">
        </telerik:RadTab>
        <telerik:RadTab Text="جلوگیری از دسترسی یک شبکه" PageViewID="_rdpBannedIpNetwork">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="_rdmpBlackList" runat="server" SelectedIndex="0">
    <telerik:RadPageView ID="_rdpBannedIpAddress" runat="server">
        <telerik:RadGrid ID="_rdgrBannedIpAddress" runat="server" AllowFilteringByColumn="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
            OnDeleteCommand="_rdgrBannedIpAddress_DeleteCommand" OnInsertCommand="_rdgrBannedIpAddress_InsertCommand"
            OnItemDataBound="_rdgrBannedIpAddress_ItemDataBound" OnNeedDataSource="_rdgrBannedIpAddress_NeedDataSource"
            OnUpdateCommand="_rdgrBannedIpAddress_UpdateCommand">
            <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="Id" EditMode="EditForms">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="IpAddress" SortExpression="IpAddress" DataField="IpAddress"
                        HeaderText="IP Address" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="تاریخ افزودن به سیستم">
                        <ItemTemplate>
                            <asp:Label ID="_lblAddedDate" runat="server" Text=""></asp:Label>
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
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%">
                                    IP آدرس :
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtIPAddress" Text='<%#Bind("IPAddress") %>' runat="server"></asp:TextBox>(Exp : 192.168.1.50)
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddBannedIpAddress"
                                        ControlToValidate="_txtIPAddress" runat="server" ErrorMessage="IP آدرس را وارد نمائید."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    توضیحات :
                                </td>
                                <td colspan="2">
                                    <asp:TextBox Rows="5" Width="90%"  Text='<%#Bind("Comment") %>'  ID="_txtComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "درج اطلاعات"  : "ویرایش"%>'
                                        ValidationGroup="AddBannedIpAddress" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
    </telerik:RadPageView>
    <telerik:RadPageView ID="_rdpBannedIpNetwork" runat="server">
        <telerik:RadGrid ID="_rdgrBannedIpNetwork" runat="server" AllowFilteringByColumn="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
            OnDeleteCommand="_rdgrBannedIpNetwork_DeleteCommand" OnInsertCommand="_rdgrBannedIpNetwork_InsertCommand"
            OnItemDataBound="_rdgrBannedIpNetwork_ItemDataBound" OnNeedDataSource="_rdgrBannedIpNetwork_NeedDataSource"
            OnUpdateCommand="_rdgrBannedIpNetwork_UpdateCommand">
            <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="Id" EditMode="EditForms">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="StartIpAddress" SortExpression="StartIpAddress"
                        DataField="StartIpAddress" HeaderText="Start IP Address" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="EndIpAddress" SortExpression="EndIpAddress"
                        DataField="EndIpAddress" HeaderText="End IP Address" DataType="System.String">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="تاریخ افزودن به سیستم">
                        <ItemTemplate>
                            <asp:Label ID="_lblAddedDate" runat="server" Text=""></asp:Label>
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
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%">
                                    IP آدرس شروع شبکه :
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtStartIPAddress"  Text='<%#Bind("StartIPAddress") %>' runat="server"></asp:TextBox>(Exp : 192.168.1.50)
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddBannedIpNetwork"
                                        ControlToValidate="_txtStartIPAddress" runat="server" ErrorMessage="IP آدرس شروع شبکه را وارد نمائید."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IP آدرس انتها شبکه :
                                </td>
                                <td>
                                    <asp:TextBox ID="_txtEndIpAddress"  Text='<%#Bind("EndIpAddress") %>' runat="server"></asp:TextBox>(Exp : 192.168.1.140)
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="AddBannedIpNetwork"
                                        ControlToValidate="_txtEndIpAddress" runat="server" ErrorMessage="IP آدرس شروع انتها را وارد نمائید."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IP آدرس های مستثنی :
                                </td>
                                <td colspan="2">
                                    <asp:TextBox Width="90%" ID="_txtIpException"  Text='<%#Bind("IpException") %>' runat="server"></asp:TextBox><br/>(Exp
                                    : 192.168.1.71;192.168.1.120)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    توضیحات :
                                </td>
                                <td colspan="2">
                                    <asp:TextBox Rows="5" Width="90%" ID="_txtComment"  Text='<%#Bind("Comment") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "درج اطلاعات"  : "ویرایش"%>'
                                        ValidationGroup="AddBannedIpNetwork" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
    </telerik:RadPageView>
</telerik:RadMultiPage>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="_rdlpBlackListManager" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>
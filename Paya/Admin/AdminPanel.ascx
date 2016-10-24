<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.ascx.cs" Inherits="Paya.Admin.AdminPanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <style type="text/css">
        .restrictionZone
        {
            width: 781px;
            height: 463px;
        }
        .taskbar
        {
            display: block;
            float: left;
        }
        .monitor 
        {
            width: 781px;
            height: 463px;
            padding: 30px 33px 85px 34px;
            background-image: url('Images/monitor.jpg');
            background-repeat: no-repeat;
            margin: 15px 0 0 15px;
        }
        
        div.RadTabStrip .rtsLevel {
            width: auto;
        }
    </style>
</head>
<body class="BODY">
    <form id="form1">
    
    <div class="monitor">
        <div id="RestrictionZone" class="restrictionZone">
            <!-- / -->
        </div>
        <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Web20" ClickToOpen="true" OnClientItemClicking="openNewWindow">
            <Items>
                <telerik:RadMenuItem Text="Start">
                    <GroupSettings ExpandDirection="Up" Flow="Vertical" />
                    <Items>
                        <telerik:RadMenuItem Text="UI Components">
                            <GroupSettings Flow="Vertical" />
                            <Items>
                                <telerik:RadMenuItem Text="RadControls for ASP.NET AJAX" ImageUrl="images/ajax.png"
                                    Value="http://www.telerik.com/products/aspnet-ajax.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="RadControls for WinForms" ImageUrl="images/win.png" Value="http://www.telerik.com/products/winforms.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="RadControls for WPF" ImageUrl="images/wpf.png" Value="http://www.telerik.com/products/wpf.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="RadControls for Silverlight" ImageUrl="images/sl.png"
                                    Value="http://www.telerik.com/products/silverlight.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="Telerik Extensions for ASP.NET MVC" ImageUrl="images/mvc.png"
                                    Value="http://www.telerik.com/products/aspnet-mvc.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="TFS Tools">
                            <GroupSettings Flow="Vertical" />
                            <Items>
                                <telerik:RadMenuItem Text="TFS Work Item Manager" ImageUrl="images/tfs.png" Value="http://www.telerik.com/products/tfsmanager-and-tfsdashboard/tfs-work-item-manager-features.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="TFS Project Dashboard" ImageUrl="images/tfs.png" Value="http://www.telerik.com/products/tfsmanager-and-tfsdashboard.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Data">
                            <GroupSettings Flow="Vertical" />
                            <Items>
                                <telerik:RadMenuItem Text="OpenAccess ORM" ImageUrl="images/orm.png" Value="http://www.telerik.com/products/orm/features.aspx">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Text="Reporting" ImageUrl="images/rep.png" Value="http://www.telerik.com/products/reporting/reporting-features.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Content Management">
                            <Items>
                                <telerik:RadMenuItem Text="Sitefinity CMS" ImageUrl="images/sitefinity.png" Value="http://www.telerik.com/products/sitefinity.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Automated Testing">
                            <Items>
                                <telerik:RadMenuItem Text="WebUI Test Studio" ImageUrl="images/test.png" Value="http://www.telerik.com/products/web-testing-tools/webui-test-studio-features.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Productivity">
                            <Items>
                                <telerik:RadMenuItem Text="JustCode" ImageUrl="images/justcode.png" Value="http://www.telerik.com/products/justcode/features.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenu>
            <telerik:RadTabStrip OnClientTabSelected="OnClientTabSelected"  ID="RadTabStrip1" 
        ScrollChildren="True" ScrollButtonsPosition="Middle" Width="730px"
            CssClass="taskbar" Orientation="HorizontalBottom" runat="server" 
            Skin="Web20" runat="server" SelectedIndex="0">               
            <Tabs>
                <telerik:RadTab style="display: none;" Selected="True" />                
            </Tabs>
        </telerik:RadTabStrip>
    </div>
    <telerik:RadWindowManager ShowOnTopWhenMaximized="false" Skin="Web20" Width="590" 
        Height="360" OnClientActivate="OnClientActivate" OnClientClose="OnClientClose" Behaviors="Close,Maximize,Minimize,Move,Reload,Resize"
        OnClientCommand="OnClientCommand" ID="RadWindowManager" RestrictionZoneID="RestrictionZone"
        runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            // <![CDATA[

            var manager = null;
            var tabStrip = null;

            function pageLoad() {
                //get a reference to the needed controls - 
                manager = $find("<%=RadWindowManager.ClientID %>");
                tabStrip = $find("<%=RadTabStrip1.ClientID %>");
            }

            //opening the window
            function openNewWindow(sender, args) {
                var item = args.get_item();
                var itemUrl = item.get_value();
                var itemText = item.get_text();
                if (itemUrl) {
                    var windowURL = itemUrl;
                    var oWnd = radopen(itemUrl, null);
                    oWnd.set_title(itemText);
                    oWnd.center();
                    tabStrip.trackChanges();
                    //create a new tab
                    var tab = new Telerik.Web.UI.RadTab();
                    //set the text of the tab
                    tab.set_text(itemText);
                    oWnd.correspondingTab = tab;
                    //add the tab to the tabstrip
                    tabStrip.get_tabs().add(tab);
                    tabStrip.repaint();
                    tab.correspondingWnd = oWnd;
                    tab.set_imageUrl(item.get_imageUrl());
                    tabStrip.commitChanges();

                    //Select this tab
                    tab.select();
                }
            }

            function OnClientCommand(sender, args) {
                //because we don't want to show the minimized RadWindow, we hide it after minimizing
                //and raise the _Maximized flag (used in OnClientTabSelected)
                if (args.get_commandName() == "Minimize") {
                    if (sender.isMaximized()) {
                        sender._Maximized = true;
                    }

                    sender.hide();
                    //raise the _toMinimize flag (used in OnClientActivate)
                    sender._toMinimize = true;
                    var tab = sender.correspondingTab;
                    if (tab) {
                        tab.set_selected(false);
                    }
                }
            }

            function OnClientTabSelected(sender, args) {
                //get a reference to the corresponding window
                var win = args.get_tab().correspondingWnd;

                if (!win) return;

                if (!win.isVisible()) {
                    win.show();
                    win.restore();
                    //if the window was maximized before client minimizes it, we need to restore
                    //its maximized state
                    if (win._Maximized) {
                        win.maximize();
                        win._Maximized = null;
                    }

                }

                //ensure that the currently active RadWindow will have the highest z-Index.
                var popupElem = win.get_popupElement();
                var oldZindex = parseInt(popupElem.style.zIndex);
                var styleZIndex = win.get_stylezindex();
                var newZIndex = (styleZIndex) ? styleZIndex : Telerik.Web.UI.RadWindowUtils.get_newZindex(oldZindex);
                popupElem.style.zIndex = "" + newZIndex;
                win.setActive(true);



            }

            function OnClientActivate(sender, args) {
                var tab = sender.correspondingTab;
                if (tab && !sender._toMinimize) {
                    tab.set_selected(true);
                }
                sender._toMinimize = false;
            }
            function OnClientClose(oWnd) {

                //remove the corresponding tab from the tabstrip
                var tab = oWnd.correspondingTab;
                if (tab) {
                    tabStrip.trackChanges();
                    tabStrip.get_tabs().remove(tab);
                    tabStrip.commitChanges();
                }
            }
            // ]]>

        </script>

    </telerik:RadCodeBlock>
    </form>
</body>
</html>
<asp:Literal ID="ltrMenu" runat="server" EnableViewState="false"></asp:Literal>
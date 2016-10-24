<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Modules.ascx.cs" Inherits="Paya.Admin.Tabs.Modules" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript">
        function enableDockDrag(enable, dockId, textboxId) {
            var dock = $find(dockId);
            if (enable) {
                dock._initializeDrag();
                var textbox = $find(textboxId);
                if (textbox) {
                    $addHandler(textbox, "mousedown", function (e) {
                        e.stopPropagation();
                    });
                }
            }
            else dock._disposeDrag();
        } 
    </script>

    <script type="text/javascript">
        function DeleteDock(dock, args) {
            if (!confirm("آیا شما مطمئن هستید؟")) {
                args.set_cancel(true);
            }
        }

        function SetDockPosition(dock, eventArgs) {
            var obj = dock;
            var cmd = eventArgs.command;
            if (obj) {
                var parentZone = obj.get_parent();
                if (parentZone) {
                    var pos = obj.get_index();
                    if ("Up" == cmd.get_name()) {
                        parentZone.dock(obj, pos - 1);
                    }
                    else if ("Down" == cmd.get_name()) {
                        parentZone.dock(obj, pos + 2);
                    }
                }
            }
            return (false);
        }
        //    function KeepLastDock(dock, e) {
        //        var id = dock.get_dockZoneID();
        //        if (id != "") {
        //            var zone = $find(id);
        //            if (zone)
        //                zone.dock(dock);
        ////            var docks = zone.get_docks();
        ////            alert(docks[0].get_uniqueID());
        ////            if (docks.length == 0)
        ////                e.set_cancel(true);
        //        }
        //    }
    </script>

</telerik:RadCodeBlock>

<div>
    <asp:Label ID="_lblSucc" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
    <asp:Label ID="_lblFail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
</div>
<asp:Panel ID="_pnlTemplate" runat="server" HorizontalAlign="Center">
    <asp:Button ID="_btnSave" runat="server" Text="ثبت اطلاعات" OnClick="BtnSave_Click" style="margin-left: 0px;" Width="174px" />
    <%--<telerik:RadDockLayout ID="_rdlTemplateEdit" runat="server" OnSaveDockLayout="_rdlTemplateEdit_SaveDockLayout"
        OnLoadDockLayout="_rdlTemplateEdit_LoadDockLayout">--%>
    <asp:PlaceHolder ID="_plhTemplateEdit" runat="server"></asp:PlaceHolder>
    <%--</telerik:RadDockLayout>--%>
    <%-- <asp:UpdatePanel runat="server" ID="_uppnlEditTemplate">
    </asp:UpdatePanel>--%>
</asp:Panel>
<asp:Panel ID="_pnlRoles" runat="server">
    <div style="text-align: center">
        <asp:Button ID="btnRoleReturn" runat="server" Text="بازگشت" OnClick="BackToTemplate_Click" />
    </div>

</asp:Panel>
<asp:Panel ID="_pnlSettings" runat="server">
    
    <div style="text-align: center">
        <asp:Button ID="btnSettReturn" runat="server" Text="بازگشت" OnClick="BackToTemplate_Click" />
    </div>
</asp:Panel>
<asp:Panel ID="_pnlInfo" runat="server">
    <div style="text-align: center">
        <asp:Button ID="btnInfoReturn" runat="server" Text="بازگشت" OnClick="BackToTemplate_Click" />
    </div>
</asp:Panel>
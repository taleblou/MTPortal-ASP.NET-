<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageManagment.aspx.cs" Inherits="Paya.PageManagment" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="_PageScriptManager" runat="server">
    </asp:ScriptManager>
    <telerik:RadAjaxManager runat="server">
    </telerik:RadAjaxManager>
    <asp:Label ID="_lblLoadTime" runat="server" Text="" Visible="false"></asp:Label>
    <asp:PlaceHolder ID="_plhControl" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

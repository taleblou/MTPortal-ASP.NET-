<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Paya.Default" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.2.713.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="description" name="keywords" runat="server" id="_keywordsMeta" enableviewstate="false" />
    <meta content="description" name="description" runat="server" id="_descMeta" enableviewstate="false" />
    <link rel="stylesheet"  type="text/css" href="Styles/Site.css"/>
</head>

<body>
    <form id="form2" runat="server">
    <table width="100%">
        <tr>
            <td>
            </td>
            <td class="mainTbl">
                <div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    </telerik:RadAjaxManager>
                    <asp:PlaceHolder ID="_plhMain" runat="server"></asp:PlaceHolder>
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

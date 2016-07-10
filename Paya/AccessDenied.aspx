<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Paya.AccessDenied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Access Denied</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Image ID="Image1" ImageUrl="~/Images/lockPage.png" runat="server" />
        <p style="color:Red;font-family:Tahoma;">
        <b>شما مجوز ورود به این صفحه را ندارید</b>
        </p>
    </div>
    </form>
</body>
</html>

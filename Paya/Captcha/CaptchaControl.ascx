<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="Paya.Captcha.CaptchaControl" %>
<img src="<%=Page.ResolveUrl("~/Captcha/CaptchaHandler.aspx?Id="+ ID + "&W="+WidthCaptcha+"&H="+HeightCaptcha)%>"><br />
<em>
    <asp:Literal ID="Captcha" runat="server" Text="کد فوق را اینجا وارد کنید" />
</em>
<br />
<asp:TextBox ID="_txtCode" runat="server"></asp:TextBox>
<br />
<asp:Label ID="_txtMessage" runat="server" EnableViewState="false"></asp:Label>
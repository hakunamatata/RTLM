<%@ Page Title="" Language="C#" MasterPageFile="~/mainframe.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-horizontal" method="post" action="Login.aspx?action=login">
    <div class="control-group">
        <label class="control-label" for="inputEmail">
            帐号
        </label>
        <div class="controls">
            <input type="text" id="txtboxAccount" name="txtboxAccount" placeholder=" Email / 手机号 ">
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPassword">
            密码</label>
        <div class="controls">
            <input type="password" id="txtboxPassword" name="txtboxPassword" placeholder="密码">
        </div>
    </div>
    <div class="control-group">
        <label class="control-label">
        </label>
        <div class="controls" id="divLoginState">
        </div>
    </div>
    <div class="control-group">
        <div class="controls">
            <label class="checkbox">
                <input type="checkbox">
                记住我
            </label>
            <button type="button" class="btn vtn" id="buttonLogin">
                登陆</button>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content runat="server" ID="ContentClientScripts" ContentPlaceHolderID="ContentPlaceHolderClientScripts">
    <script>
        navBar.active("nav_login");

        var login = new RTLM.page.$login;
    </script>
</asp:Content>

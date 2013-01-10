<%@ Page Title="" Language="C#" MasterPageFile="~/mainframe.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-horizontal" method="post" action="Register.aspx?action=register"
    id="formRegister">
    <div class="control-group">
        <label class="control-label" for="inputEmail">
            邮箱</label>
        <div class="controls">
            <input type="text" id="txtboxEmail" name="txtboxEmail" placeholder="" />
            <span class="help-inline" id="email_msg">填写正确的邮箱地址, 忘记密码时可以轻松找回</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputMobile">
            手机号码</label>
        <div class="controls">
            <input type="text" id="txtboxMobile" name="txtboxMobile" placeholder="" />
            <span class="help-inline" id="mobile_msg">方便我们发送时跟您联系</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPassword">
            密码</label>
        <div class="controls">
            <input type="password" id="txtboxPassword" name="txtboxPassword" placeholder="" />
            <span class="help-inline">6-20位字符数字组合</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPasswordConfirm">
            再次输入密码</label>
        <div class="controls">
            <input type="password" id="txtboxRepeat" name="txtboxRepeat" placeholder="" />
            <span class="help-inline">确保密码输入正确</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputVerifyCode">
            验证码</label>
        <div class="controls">
            <input type="text" id="inputVerifyCode" class="span1" name="inputVerifyCode" placeholder="" />
            <span class="help-inline"></span>
        </div>
    </div>
    <div class="control-group">
        <div class="controls">
            <label class="checkbox">
                <input type="checkbox" id="checkboxRead" name="checkboxRead">
                已经阅读 <a href="#">使用协议</a>
            </label>
            <button type="submit" class="btn" id="btnRegister">
                注册</button>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content runat="server" ID="ContentClientScript" ContentPlaceHolderID="ContentPlaceHolderClientScripts">
    <script>
        navBar.active("nav_register");
    </script>
</asp:Content>

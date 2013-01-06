﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mainframe.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        PAGE_ACTION = "nav_register";
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-horizontal">
    <div class="control-group">
        <label class="control-label" for="inputUserName">
            用户名</label>
        <div class="controls">
            <input type="text" id="inputUserName" placeholder="" onblur="isUserNameAvailable(this)">
            <span class="help-inline">（5-20位字母、数字或下划线组合，首字符必须为字母。）</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPassword">
            密码</label>
        <div class="controls">
            <input type="password" id="inputPassword" placeholder="" onblur="isPasswordAvailable(this)">
            <span class="help-inline">(6-20位字符数字组合)</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPasswordConfirm">
            再次输入密码</label>
        <div class="controls">
            <input type="password" id="inputPasswordConfirm" placeholder=""  onblur="isPasswordAvailable(this)">
            <span class="help-inline">(确保密码输入正确)</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputEmail">
            邮箱</label>
        <div class="controls">
            <input type="text" id="inputEmail" placeholder=""  onblur="isEmailAvailable(this)">
            <span class="help-inline">(填写正确的邮箱地址, 忘记密码时可以轻松找回)</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputMobile">
            手机号码</label>
        <div class="controls">
            <input type="text" id="inputMobile" placeholder="" onblur="isMobileAvailable(this)">
            <span class="help-inline">(方便我们发送时跟您联系)</span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputVerifyCode">
            验证码</label>
        <div class="controls">
            <input type="text" id="inputVerifyCode" class="span1" placeholder="">
            <span class="help-inline"></span>
        </div>
    </div>
    <div class="control-group">
        <div class="controls">
            <label class="checkbox">
                <input type="checkbox">
                已经阅读 <a href="#">使用协议</a>
            </label>
            <button type="submit" class="btn" id="btnRegister">
                注册</button>
        </div>
    </div>
    </form>

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">
    <script>
        registerValidationCheck();
    </script>
</asp:Content>

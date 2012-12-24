<%@ Page Title="" Language="C#" MasterPageFile="~/mainframe.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="DTcms.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-horizontal">
    <div class="control-group">
        <label class="control-label" for="inputEmail">
            帐号
        </label>
        <div class="controls">
            <input type="text" id="inputEmail" placeholder="用户名 / Email / 手机号 ">
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="inputPassword">
            密码</label>
        <div class="controls">
            <input type="password" id="inputPassword" placeholder="密码">
        </div>
    </div>
    <div class="control-group">
        <div class="controls">
            <label class="checkbox">
                <input type="checkbox">
                记住我
            </label>
            <button type="submit" class="btn">
                登陆</button>
        </div>
    </div>
    </form>
</asp:Content>

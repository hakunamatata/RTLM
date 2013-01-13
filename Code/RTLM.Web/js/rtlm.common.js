
/* ===========================================================================
    包体说明：   用来模拟实现面向对象编程机制， 并且提供一种简单的MVC框架

    索引器：     _ROOT
   =========================================================================*/

(function (scope) {


    var Class = function (parent) {
        var klass = function () {
            this.init.apply(this, arguments);
        };
        if (parent) {
            var subclass = function () { };
            subclass.prototype = parent.prototype;
            klass.prototype = new subclass;
        };

        klass.proxy = function (func) {
            var that = this;
            return function () {
                return func.apply(that, arguments);
            }
        }

        klass.extend = function (obj) {
            var extended = obj.exnteded;
            for (var i in obj)
                klass.fn[i] = obj[i];
            if (extended) extended(klass);
        };

        klass.prototype.init = function () { };
        klass.fn = klass.prototype;
        klass.fn.parent = klass;
        klass.fn.proxy = klass.proxy;
        return klass;
    };

    scope.Class = Class;


})(window);


(function (scope, $) {

    var $view = new Class;

    $view.extend({

        init: function () {

        },

        bind: function (pairs) {

            for (var i in pairs) {

                var d = i.split(' ')[0],
                    e = i.split(' ')[1],
                    h = pairs[i];

                $(d).bind(e, this.proxy(h));

            }

        }

    });

    scope.View = $view;

})(window, jQuery);





/* ===========================================================================
    包体说明： 顶级命名空间RTLM，包含系统中所有前端对象，控制与操作

    索引器：   RTLM
   =========================================================================*/

(function (scope) {

    var $RTLM = new Class;

    // RTLM预定义
    $RTLM.extend({
        init: function () {

        }
    });

    scope.RTLM = new $RTLM;

})(window);


/* ===========================================================================
    包体说明： 系统前端一些通用处理对象，包含视图的定义，实用操作
               方法等，为搭建简单的MVC创建了可能

    索引器：   RTLM.common
   =========================================================================*/
(function (scope) {

    var $common = new Class;


    //common预定义
    $common.extend({
        init: function () {

        }
    });

    scope.common = new $common;

})(RTLM);



/* ===========================================================================
    包体说明： 这部分是为系统提供了前端页面交互的对象，实现各个页面
               互动的对象都将继承此类

    索引器：   RTLM.page
=========================================================================*/
(function (scope) {

    $page = new Class;

    //validate预定义
    $page.extend({
        init: function () { }
    });

    scope.page = new $page;

})(RTLM);




/* ===========================================================================
    包体说明： 这部分是系统导航类，有关导航栏的操作都将在此处定义

    索引器：   RTLM.common.navigator
=========================================================================*/
(function (scope, $) {

    var $navigator = new Class;
    $navigator.extend({

        //         enumPageAction: {
        //            home: "nav_home",            //导航之首页
        //            register: "nav_register",    //导航之注册
        //            login: "nav_login",           //导航之登陆
        //            orders: "nav_orders",         //导航之我的订单
        //            buy: "nav_buy"               //导航之我要买
        //        },

        init: function () {

        },

        active: function (active) {
            $('#' + active).addClass("active");
        }

    });

    scope.navigator = new $navigator;

})(RTLM.common, jQuery);



/* ===========================================================================
    包体说明： 这部分是系统页面信息输出类，主要负责系统信息的输出
               和简单错误信息，和提醒的输出

    索引器：   RTLM.common.$message
=========================================================================*/
(function (scope, $) {

    var $message = new Class;
    $message.extend({

        //        消息格式                
        //        message:{
        //            
        //            type: [                
        //                    0,        //success,
        //                    1,        //failed,
        //                    2,        //error,
        //                    3,        //info
        //                  ],

        //            message:"暂无信息"

        //        },

        init: function () {

            if (this.message) {
                switch (this.message.type) {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:

                        $.dialog(this.message.message);

                        break;
                }

            }

        }

    });

    scope.$message = $message;

})(RTLM.common, jQuery);



/* ===========================================================================
    包体说明： 这部分是为了防止页面重复提交而设置的插件，在页面
               元素被点击时，2s内无法再次点击

    索引器：   RTLM.common.防止重复提交
=========================================================================*/
(function (scope, $) {

    var buttons = $(".vtn");

    buttons.click(function () {

        var that = $(this),
            timeout = that.attr("data-timeout");

        if(!timeout) timeout = 2000;

        setTimeout(function () {

            that.attr("disabled", "disabled");

            var text = that.val();

            that.val(text + "...");

            t = setTimeout(function () {

                that.removeAttr("disabled");

                that.val(text);

            }, timeout);

        }, 20);

    });

})(RTLM.common, jQuery);


/* ===========================================================================
    包体说明： 这部分主要为注册页面提供控制引擎，继承自视图，
               涉及到的控件的命名必须严格按照包内定义的名称命名

    索引器：   RTLM.page.$register
=========================================================================*/
(function (scope, $) {


    /*
    *   方法：控件状态却换
    *   参数：element  控件
    *         state    当前所处状态
    *   返回：    
    */


    var $register = new Class(View);

    $register.extend({
        /*
        定义实例方法
        */
        email : $('#txtboxEmail'),
        mobile : $('#txtboxMobile'),
        password : $('#txtboxPassword'),
        repeat: $('#txtboxRepeat'),
        veryfy: $('#txtboxVerify'),
        agree: $('#checkboxRead'),

        init: function () {
        
            // Default Settings
            this.email.defualtMessage = "填写正确的邮箱地址, 忘记密码时可以轻松找回";
            this.email.existMessage = "该邮箱已经被注册";
            this.mobile.defualtMessage = "方便我们发送时跟您联系";
            this.mobile.existMessage = "该手机号已经被注册";

            this.bind({

                '#txtboxEmail blur': this.isEmailAvailable,
    
                '#txtboxMobile blur': this.isMobileAvailable,

                '#txtboxPassword blur': this.isPasswordAvailable,

                '#txtboxRepeat blur': this.isPasswordRepeat,

                '#checkboxRead click': this.isContractRead

            });

            this.validationCheck();
        },

        /*
        *   方法：导航切换
        *   参数：@page_action 表示为哪个功能页面
        *   返回：null
        */
        navigationChange: function (page_action) {
            var tar = $('#' + page_action);
            tar.addClass("active");
        },


        isContractRead: function(){
            this.agree.validate = this.agree[0].checked;
        },

        /*
        *   方法：用户名称是否存在
        *   参数：email    用户邮箱
        *   返回：bool     是否存在
        */
        isEmailAvailable: function () {
            var patrn = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/;
            if (patrn.exec(this.email.val())) $.getJSON('api/handlers/users.ashx?action=valid_email&p1=' + this.email.val(), this.proxy(this.ajaxEmail));
            else this.validateFailed(this.email, "不可用");
        },

        /*
        *   方法：用户密码是否存在
        *   参数：password 用户邮箱
        *   返回：bool     是否存在
        */
        isPasswordAvailable: function (password) {
            var patrn = /^(\w){6,20}$/;
            if (patrn.exec(this.password.val())) this.validateSuccess(this.password, "可用");
            else this.validateFailed(this.password, "不可用");
        },

        /*
        *   方法：用户手机号是否存在
        *   参数：mobile   用户手机号码
        *   返回：bool     是否存在
        */
        isMobileAvailable: function () {
            var patrn = /^(\w){6,20}$/;
            if (patrn.exec(this.mobile.val())) $.getJSON('api/handlers/users.ashx?action=valid_mobile&p1=' + this.mobile.val(),  this.proxy(this.ajaxMobile));
            else this.validateFailed(this.mobile, "不可用");

        },

        isPasswordRepeat: function (password, repeat) {
            if (this.password.val() == this.repeat.val()) this.validateSuccess(this.repeat, "可用");
            else this.validateFailed(this.repeat, "不可用");

        },

        ajaxEmail: function(data){
            var msg = this.email.next();
            msg.html();
            if(data.success) this.validateSuccess(this.email, "可用")
            else this.validateFailed(this.email, this.email.existMessage)
        },

        ajaxMobile: function(data){
            var msg = this.mobile.next();
            msg.html();
            if(data.success) this.validateSuccess(this.mobile, "可用")
            else this.validateFailed(this.mobile, this.mobile.existMessage)
        },

        validateFailed:function(which, message)
        {
            var line = which.parent().parent();
                line.removeClass("success");
                line.addClass("error"),
            msg = which.next();
            msg.html(message);
            which.validate = false;
        },

        validateSuccess: function(which, message){
            var line = which.parent().parent();
                line.removeClass("error");
                line.addClass("success"),
            msg = which.next();
            msg.html(message);
            which.validate = true;

        },

        ajaxWaiting: function(which, reset){
        
            var help_inline = which.next(),
                validating = help_inline.attr("data-validating");
                waitingImg = $("<img>").attr("src","img/loading_tiny.gif");

                if (reset){
                    help_inline.removeAttr("data-validating");
                    help_inline.html(this.which.defaultMessage);    
                    return;
                }
                
                if (!validating){
                    help_inline.attr("data-validating",true);
                    help_inline.html(waitingImg);
                }
         },

         validationCheck:function ()
         {
            var i = false,
                that = this,
                btn = $('#btnRegister'),
                timer = setInterval(function(){
                
                    i = that.email.validate && that.mobile.validate && that.agree.validate && that.password.validate && that.repeat.validate ;

                    if (i){
                        btn.removeClass('disabled');
                        btn.removeAttr('disabled')
                    }else{
                        btn.addClass('disabled');
                        btn.attr('disabled','disabled')
                    }

                },200);
         }
    });

    scope.$register = $register;

})(RTLM.page, jQuery);


/* ===========================================================================
    包体说明： 部分主要提供注册页面相关操作对象

    索引器：   RTLM.page.$login
=========================================================================*/
(function (scope, $) { 

    var $login = new Class(View);

    $login.extend({
    
        init: function(){
        
            this.account = $("#txtboxAccount");

            this.password = $("#txtboxPassword");

            this.loginState = $("#divLoginState");

            this.bind({
            
                "#buttonLogin click": this.submit
            
            });

        },

        submit:function(){
            
           $.getJSON('api/handlers/users.ashx',{action:"login", p1:"consumer",p2:this.account.val(), p3:this.password.val() } , this.proxy(this.ajaxAccount));

        },

        ajaxAccount: function(data)
        {
            if(data.success)
                location.href="Default.aspx";
            else
                {
                    this.loginState.text("登录失败");
                    this.loginState.css("color","red");
                }
        }
    
    });

    scope.$login = $login;

})(RTLM.page, jQuery);
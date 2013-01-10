
/*
类构造器
*/

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

(function (scope) {

    var $RTLM = new Class;

    // RTLM预定义
    $RTLM.extend({
        init: function () {

        }
    });

    scope.RTLM = new $RTLM;

})(window);


(function (scope) {

    var $common = new Class;


    //common预定义
    $common.extend({
        init: function () {

        }
    });

    scope.common = new $common;

})(RTLM);

(function (scope) {

    $validate = new Class;

    //validate预定义
    $validate.extend({
        init: function () { }
    });

    scope.validate = new $validate;

})(RTLM.common);


/*
包体说明：  
本包主要为控制页面导航控制其样式
*/
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


/*
包体说明：
        
本包主要为注册页面提供控制引擎，涉及到的控件的命名必须严格按照
包内定义的名称命名，涉及到的控件以及命名规范如下：
邮箱 id: 'txtboxEmail'
            


*/

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
        //构造实例并且初始化pageAction为首页
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

                '#txtboxRepeat blur': this.isPasswordRepeat

            });
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

               
         }
    });

    scope.register = new $register;

})(RTLM.common.validate, jQuery);

//    var PAGE_ACTION = 'nav_home';
//    var valid_username = false,
//    valid_email = false,
//    valid_mobile = false,
//    valid_verifycode = true,
//    valid_password = false;


//    navigationChange = function() {
//        $('#' + PAGE_ACTION).addClass("active");
//    };




//    function registerValidationCheck() {
//        var i = false,
//        btn = $("#btnRegister"),
//        timer = setInterval(function () {
//            i = valid_email && valid_mobile && valid_username && valid_verifycode && valid_password;

//            if (i) {
//                btn.removeClass("disabled");
//                btn.removeAttr("disabled");
//            } else {
//                btn.addClass("disabled");
//                btn.attr("disabled", "disabled");
//            }
//        }, 200);


//    }

//    function do_register() {

//    }



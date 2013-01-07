
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
        klass.prototype.init = function () { };
        klass.fn = klass.prototype;
        klass.fn.parent = klass;
        klass.extend = function (obj) {
            var extended = obj.exnteded;
            for (var i in obj)
                klass.fn[i] = obj[i];
            if (extended) extended(klass);
        };
        return klass;
    }

    scope.Class = Class;

})(window);


(function(scope){

    var $RTLM = new Class;

    // RTLM预定义
    $RTLM.extend({
        init: function () {
            
        }
    });

    scope.RTLM = new $RTLM;

})(window);


(function(scope){ 

    var $common = new Class;
    

        //common预定义
    $common.extend({
        init: function () {

        }
    });

    scope.common = new $common;

})(RTLM);

(function (scope){

    $validate = new Class;

    //validate预定义
    $validate.extend({
        init: function () { }
    });

    scope.validate = new $validate;

})(RTLM.common);
    

/*
    包体说明：
        
        本包主要为注册页面提供控制引擎，涉及到的控件的命名必须严格按照
        包内定义的名称命名，涉及到的控件以及命名规范如下：
            邮箱 id: 'txtboxEmail'
            


*/


(function (scope, $) {

    var $register = new Class,
        enumAction = {
            home: "nav_home",            //导航之首页
            register: "nav_register",    //导航之注册
            login: "nav_login",           //导航之登陆
            orders: "nav_orders",         //导航之我的订单
            buy: "nav_buy"               //导航之我要买
        },

        /*
        *   方法：控件状态却换
        *   参数：element  控件
        *         state    当前所处状态
        *   返回：    
        */
        validateState = function (element, state) {
            var t = $(element.parentNode.parentNode);
            switch (state) {
                case "success":
                    t.removeClass("error");
                    t.addClass("success");
                    break;
                case "error":
                    t.removeClass("success");
                    t.addClass("error");
                    break;
            }
        };

    /*
        定义实例方法
    */
    $register.extend({

        //构造实例并且初始化pageAction为首页
        init: function () {
            
            var email = $('#txtboxEmail'),
                mobile = $('#txtboxMobile'),
                password = $('#txtboxPassword'),
                repeat = $('#txtboxRepeat'),
                that = this;

            this.pageAction = enumAction.home;
            
            email.blur(function(){ that.isEmailAvailable(email[0]); });
            mobile.blur(function(){ that.isMobileAvailable(mobile[0]); });
            password.blur(function(){ that.isPasswordAvailable(password[0]); });
            repeat.blur(function(){ that.isPasswordRepeat(password[0], repeat[0]); });
            //this.isMobileAvailable(mobile);
            //this.isPasswordAvailable(password);

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
        isEmailAvailable: function (email) {
            var patrn = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/;
            if (patrn.exec(email.value)) 
                validateState(email, "success");
            else 
                validateState(email, "error");
        },

        /*
        *   方法：用户密码是否存在
        *   参数：password 用户邮箱
        *   返回：bool     是否存在
        */
        isPasswordAvailable: function (password) {
            var patrn = /^(\w){6,20}$/;
            if (patrn.exec(password.value)) 
                validateState(password, "success");
            else
                validateState(password, "error");
        },

        /*
        *   方法：用户手机号是否存在
        *   参数：mobile   用户手机号码
        *   返回：bool     是否存在
        */
        isMobileAvailable: function (mobile) {
            var patrn = /^(\w){6,20}$/;
            if (patrn.exec(mobile.value)) 
                validateState(mobile, "success");
            else 
                validateState(mobile, "error");
            
        },

        isPasswordRepeat: function(password, repeat){
            if(password.value == repeat.value)
                validateState(repeat, "success");
            else
                validateState(repeat, "error");

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



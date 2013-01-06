/*
*   PAGE_ACTION: 'nav_home' 导航之首页
*                'nav_wanna_buy' 导航之我要买
*                'nav_orders' 导航之我的订单
*                'nav_login' 导航之登陆
*                'nav_register' 导航之注册
*/


var PAGE_ACTION = 'nav_home';




var valid_username = false,
    valid_email = false,
    valid_mobile = false,
    valid_verifycode = true,
    valid_password = false;


function navigationChange() {
    $('#' + PAGE_ACTION).addClass("active");
}

function isUserNameAvailable(user_name) {
    var patrn = /^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}$/;
    if (patrn.exec(user_name.value)) {
        valid_username = true;
        validateState(user_name, "success");
    }
    else {
        valid_username = false;
        validateState(user_name, "error");
    }

}

function isEmailAvailable(email) {
    var patrn = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/;
    if (patrn.exec(email.value)) {
        valid_email = true;
        validateState(email, "success");
    }
    else {
        valid_email = false;
        validateState(email, "error");
    }


}

function isPasswordAvailable(password) {
    var patrn = /^(\w){6,20}$/;
    if (patrn.exec(password.value)) {
        valid_password = true;
        validateState(password, "success");
    }
    else {
        valid_password = false;
        validateState(password, "error");
    }

}

function isMobileAvailable(mobile) {
    var patrn = /^(\w){6,20}$/;
    console.log(patrn.exec(mobile));
    if (patrn.exec(mobile.value)) {
        valid_mobile = true;
        validateState(mobile, "success");
    }
    else {
        valid_mobile = false;
        validateState(mobile, "error");
    }
}

function getVerifyCode() {
    return true;
}

function validateState(element, state, message) {
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
}

function registerValidationCheck() {
    var i = false,
        btn = $("#btnRegister"),
        timer = setInterval(function () {
            i = valid_email && valid_mobile && valid_username && valid_verifycode && valid_password;

            if (i) {
                btn.removeClass("disabled");
                btn.removeAttr("disabled");
            } else {
                btn.addClass("disabled");
                btn.attr("disabled", "disabled");
            }
        }, 200);


}

function do_register() {

}

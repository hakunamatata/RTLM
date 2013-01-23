
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

        init: function(view){



        },

        bind: function (pairs) {

            for (var i in pairs) {

                var d = i.split(' ')[0],
                    e = i.split(' ')[1],
                    h = pairs[i];

                $(d).bind(e, this.proxy(h));

            }

        },

        render:function(){

        }

    });

    scope.$View = $view;

})(window, jQuery);





/* ===========================================================================
 包体说明：   地图类，实为BaiduMap的实例，在基础上添加了自己业务中所需要的内容，包括
            锁定地图，开启影藏和显示其余控制元素等等。

            百度地图常用操作有：
                方法:
                    centerAndZoom(BMap.Point, level);   以某点为中心
                    getZoom();                          获取当前缩放级别
                    zoomTo(level);                      设置地图缩放级别
                    addOverlay(BMap.Marker);             添加遮罩
                属性:
                    enableScrollWheelZoom;              设置是否开启滚轮的缩放


            实例地图常用操作有:
                方法：
                    setToBackground;                    将地图设置为背景，不可操作
                    appear;                             将地图设为可操作


 索引器：     $Map, 地图类
 =========================================================================*/
(function (scope, $) {

    var $map = new Class($View);

    $map.extend({

            init:function () {

                this.view = $("#allmap");

                this.mask = $("#mapmask");

            this.hide();

        },

        // 设置为背景，关闭所有地图操作，至于底层，添加遮罩
        // 但是可以及时标出配送员和商家位置，也可以显示自己
        // 位置，一切数据的发送和接受正常运作
        hide:function(){

            if(!this.locked){

                this.mask.css("z-index","1");
                this.mask.fadeIn(500);
                this.view.css("z-index","0");

                this.locked = true;
            }
        },

        // 呈现地图，将地图设为可操作，隐藏遮罩
        show:function(){

            if(this.locked){
                this.mask.css("z-index","1");
                this.mask.fadeOut(500);
                this.view.css("z-index","0");

                this.locked = false;
            }
        }




    });

    scope.$Map = $map;

})(window, jQuery);



/* ===========================================================================
 包体说明：   用户用户前段呈现的控件基类，所有用户前段控件都继承此类

 索引器：     $UI
 =========================================================================*/

(function (scope, $){

    var $UI = new Class($View);

    $UI.extend({

        init:function(view){

        }
    });

    scope.UI = $UI;

})(window, jQuery);


/* ===========================================================================
 包体说明：   用户导航条

 索引器：     $Navbar
 =========================================================================*/
(function (scope, $){


    // 导航条，页面顶部导航
    var $Navbar = new Class(scope.UI);

    $Navbar.extend({

        navs:[],

        typeE:{

            brand:"brand",

            nav:"nav"

        },

        init: function(view){

            this.view = $(view);

            this.derender();

        },

        derender:function(){

            for(var i in this.typeE){

                var Nav = new $Nav(this.view.find("." + this.typeE[i]), this.typeE[i], this);

                this.navs.push(Nav);

            }

        },

        hide:function(func){

            this.view.fadeOut(500, func);

        },

        show:function(func){

            this.view.fadeIn(500, func);

        }

    });

    var $Nav = new Class;

    $Nav.extend({

        navs:[],

        active: false,

        init:function(view, type, parent){

            this.parent = parent;

            this.view = $(view);

            this.type = type;

            this.derender();
        },

        derender:function(){


            if (this.type == this.parent.typeE.nav){

                var that = this;

                this.view.find("li").each(function(){

                    var nav = new $nav($(this));

                    nav.parent = $(this);

                    that.navs.push(nav);

                });

            }

        }

    });

    var $nav = new Class;

    $nav.extend({

        init:function(view){

            this.view = $(view);

        }

    })

    scope.UI.Navbar = $Navbar;

})(window, jQuery);

/* ===========================================================================
 包体说明：   窗口展示

 索引器：     $Hero
 =========================================================================*/

(function(scope, $){

    var $Hero = new Class($View);

    $Hero.extend({

        typeE:{

            about: 'about',

            costomer:'customer',

            consumer:'consumer'

        },

        units:[],

        init:function(view, func){

            this.view = $(view);

            this.callbackFun = func;

            this.derender();

        }   ,

        derender:function(){

            for(var i in this.typeE){

                var unit = new $unit(this.view.find("." + this.typeE[i]), this.typeE[i], this);

                this.units.push(unit);

            }

        },

        hide:function(func){

            this.view.fadeOut(500, func);

        },

        show:function(func){

            this.view.fadeIn(500, func);

        }



    })


    var $unit = new Class($View);

    $unit.extend({

        init:function(view, type, parent){

            this.parent = parent;

            this.view = $(view);

            this.type = type;

            this.derender();

        },

        derender:function(){

            var that = this;

            this.myPosition = $(".position-my", this.view.selector);

            this.tarPosition = $(".position-tar", this.view.selector);

            this.phone = $(".cellphone", this.view.selector);

            if( this.type == this.parent.typeE.consumer){

                this.tarPosition.bind("click", this.parent.callbackFun);

            }

        }
    })

    scope.Hero = $Hero;

})(UI, jQuery);




















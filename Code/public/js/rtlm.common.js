
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

        klass.include = function (obj) {
            var included = obj.included;
            for (var i in obj)
                klass.fn[i] = obj[i];
            if (included) included(klass);
        };
        klass.extend = function (obj){
            var extended = obj.extended;
            for (var i in obj)
                klass[i] = obj[i];
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

    $view.include({

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

    $map.include({

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

    $UI.include({

        init:function(view){

        },

        show:function(){

            this.view.fadeIn(500);

        },

        hide:function(){

            this.view.fadeOut(500);

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

    $Navbar.include({

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

        }

    });

    var $Nav = new Class;

    $Nav.include({

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

    $nav.include({

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

    var $Hero = new Class(UI);

    $Hero.include({

        typeE:{

            about: 'about',

            costomer:'customer',

            consumer:'consumer'

        },

        units:{},

        init:function(view){

            this.view = $(view);

            this.derender();

        },

        derender:function(){

            for(var i in this.typeE){

                var unit = new $unit(this.view.find("." + this.typeE[i]), this.typeE[i], this);

                this.units[this.typeE[i]] = unit;

            }

        }

    })

    // 首页展示的每个子单元类
    var $unit = new Class($View);

    $unit.include({

        init:function(view, type, parent){

            this.parent = parent;

            this.view = $(view);

            this.type = type;

            this.derender();

        },

        derender:function(){

            var that = this;

            this.tarPosition = $(".btn-primary", this.view.selector);

            if( this.type == this.parent.typeE.consumer){

                // 定义遮罩层
                this.modalView = $("#Modal" + this.parent.typeE.consumer);

                this.tarPosition.bind('click', that.proxy(that.consInit));

            }

        },

        // 消费者单元目标关闭回调
        consInit: function(){

            var that = this;

            this.modalView.modal({

                backdrop: 'static',

                keyboard: true

            });


        }
    })

    scope.Hero = $Hero;

})(UI, jQuery);


/* ===========================================================================
 包体说明：   配送员类

 索引器：     $Deliver
 =========================================================================*/
(function(scope, $){

    var $Deliver = new Class;

    $Deliver.include({

//        option:{
//              speed   :Number
//
//        }
        init: function(marker, dest, opt){

            this.marker = marker;
            this.speed = opt.speed || 0.0001;
            this.dest = dest;
            this.map = this.marker.marker.getMap();
            this.icon = this.marker.marker.getIcon();
            this.delivered = false;

        },

        fetch:function(){

            var that = this,
                markerPos = this.marker.getPostion(),
                destPos = this.dest.getPostion(),
                alpha = Math.atan2(destPos.lat - markerPos.lat, destPos.lng - markerPos.lng);

            this.marker.remove();

            var t = setInterval(function(){

                that.proxy(that.moveTo(markerPos, alpha,that.speed));

            }, 1000)

        },

        delivery: function(){




        },

        moveTo:function(oldPos, alpha, step){

            var point = new BMap.Point(

                oldPos.lng += step * Math.cos(alpha),

                oldPos.lat += step * Math.sin(alpha)

            );

            marker = new MapMarker(point, null, {icon:this.icon}, this.map);

            this.map.addOverlay(marker);

            this.marker = marker;

        }


    })

    scope.Deliver = $Deliver;

})(window, jQuery)













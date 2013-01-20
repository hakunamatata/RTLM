
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

        },

        render:function(model){

        }

    });

    scope.View = $view;

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
(function (scope, baiduMap, $) {

    var $map = new Class;

    $map.extend({

        init:function (name) {

            this.map = new baiduMap(name);

        },

        // 设置为背景，关闭所有地图操作，至于底层，添加遮罩
        // 但是可以及时标出配送员和商家位置，也可以显示自己
        // 位置，一切数据的发送和接受正常运作
        setToBackground:function(){

            if(!this.locked){
                this.locked = true;
            }
        },

        // 呈现地图，将地图设为可操作，隐藏遮罩
        appear:function(){

            if(this.locked){
                this.locked = false;
            }
        }

    });

    scope.$Map = $map;

})(window, BMap.Map, jQuery);



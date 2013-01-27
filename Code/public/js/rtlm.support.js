
// 异步加载地图
function loadScript(src){
    var script = document.createElement("script");
    script.src = src;
    document.body.appendChild(script);
}

// 执行地图的加载
loadScript("http://api.map.baidu.com/api?v=1.4&callback=inittializeMap");

// 地图对象， 包括遮罩层和一些控制方法
var map = new $Map,
    // 百度地图实例
    Map = null,
    // 加载地图的Timer
    t_mapLoading;

// 标识符， 地图是否加载完毕
function isMapLoaded(){
    if (Map == null)  {
        return false
    }
    else {
        clearInterval(t_mapLoading);
        return true;;
    }
}
t_mapLoading = setInterval(isMapLoaded, 10);

// 初始化地图
function inittializeMap(){
    loadScript("/js/rtlm.mapextend.js"); //加载地图扩展
    Map = new BMap.Map('allmap');
    var point = new BMap.Point(118.796116, 32.056313);    // 创建点坐标
    Map.centerAndZoom(point,16);                     // 初始化地图,设置中心点坐标和地图级别。
}

// 定义页面UI
var nav = new UI.Navbar(".navbar");

// 首页展示加载完毕之后
var hero = new UI.Hero(".Heroframe");

window.onload = function(){

        // 点击消费者遮罩层书法的事件

        var csrm = hero.units["consumer"],
            __cons_confirm = false;
        csrm.modalView.on("show", function(){
            hero.hide();
        });
        csrm.modalView.on("hide", function(){
            if(!__cons_confirm)
                hero.show()
        })

        $(".btn-primary", "#Modalconsumer").click(function(){

            __cons_confirm = true; // 标记此为用户确认操作， Modal消失后不在显示 consumer

            if(isMapLoaded()){


                hero.units["consumer"].modalView.modal('hide');
                hero.hide();
                map.show();

                var point = new BMap.Point(118.796116, 32.056313);
                var point2 = new BMap.Point(118.807116, 32.066413);
                var marker = new MapMarker(point, {icon:'/img/marker.png'}, Map);
                var marker2 = new MapMarker(point2, {icon:'/img/marker.png'}, Map);
                marker.lineTo(marker2);

            }

            else

                console.log("please waiting, while tha map is loading...");

        });


//    if(isMapLoaded()){
//        var point = new BMap.Point(118.796116, 32.056313);
//        var point2 = new BMap.Point(118.807116, 32.066413);
//        var marker = new MapMarker(point, {icon:'/img/marker.png'}, Map);
//        var marker2 = new MapMarker(point2, {icon:'/img/marker.png'}, Map);
//        marker.lineTo(marker2);
//    }
    // $.post("/add", {cellphone: '13776571079', location:{lt:118.790722, dm:32.044015}}, function(d){  if(d.success){  console.log("saved");  } })

};
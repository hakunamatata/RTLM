// 地图对象， 包括遮罩层和一些控制方法
var map = new $Map,
    // 百度地图实例
    Map = null;
// 标识符， 地图是否加载完毕
var mapLoading_t;

function isMapLoaded(){
    if (Map == null)  {
        return false
    }
    else {
        clearInterval(mapLoading_t);
        return true;;
    }
}
mapLoading_t = setInterval(isMapLoaded, 10);

// 初始化地图
function inittializeMap(){

    loadScript("/js/rtlm.mapextend.js"); //加载地图扩展
    Map = new BMap.Map('allmap');
    var point = new BMap.Point(118.796116, 32.056313);    // 创建点坐标
    Map.centerAndZoom(point,16);                     // 初始化地图,设置中心点坐标和地图级别。
}

// 异步加载地图
function loadScript(src){
    var script = document.createElement("script");
        script.src = src;
    document.body.appendChild(script);
}

window.onload = function(){loadScript("http://api.map.baidu.com/api?v=1.4&callback=inittializeMap");}



var nav = new UI.Navbar(".navbar");
var hero = new UI.Hero(".Heroframe", function(){

    if(isMapLoaded()){

        map.show();
        hero.hide();

        var point = new BMap.Point(118.796116, 32.056313);
        var point2 = new BMap.Point(118.807116, 32.066413);
        var marker = new MapMarker(point, {icon:'/img/marker.png'}, Map);
        var marker2 = new MapMarker(point2, {icon:'/img/marker.png'}, Map);

        marker.lineTo(marker2);
    }
    // $.post("/add", {cellphone: '13776571079', location:{lt:118.790722, dm:32.044015}}, function(d){  if(d.success){  console.log("saved");  } })
    $('.btn btn-primary').tooltip({title:'123', animation:true, placement:'top', trigger:'hover'});
});
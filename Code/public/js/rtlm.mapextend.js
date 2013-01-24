/* ===========================================================================
 包体说明：   地图标记


 索引器：     $MapMarker
 =========================================================================*/
(function(scope, jQuery){

    // 地图标记类，继承自BMap.Marker
    var $Marker = new Class(UI);
    $Marker.include({
        init:function(point, opt, map){

            // 创建图标对象
            var icon = new BMap.Icon(opt.icon, new BMap.Size(29, 43), {
                // 指定定位位置。
                // 当标注显示在地图上时，其所指向的地理位置距离图标左上
                // 角各偏移10像素和25像素。您可以看到在本例中该位置即是
                // 图标中央下端的尖角位置。
                offset: new BMap.Size(10, 25),
                // 设置图片偏移。
                // 当您需要从一幅较大的图片中截取某部分作为标注图标时，您
                // 需要指定大图的偏移位置，此做法与css sprites技术类似。
                imageOffset: new BMap.Size(0, 0 - opt.index * 25)   // 设置图片偏移

            });

            this.marker = new BMap.Marker(point, {icon: icon});

            this.map = map;
            // 允许拖动
            this.marker.enableDragging();
            this.marker.addEventListener("dragend", function(e){
                var info = new $InfoWindow("当前位置：" + e.point.lng + ", " + e.point.lat, {}, this.map);
                info.addToMap(e.point);
            })
        },

        addToMap:function(){
            this.map.addOverlay(this.marker);
        },

        remove:function(){
            this.map.removeOverlay(this.marker);
            this.marker.dispose();
            delete this;
        }
    });

    // 信息窗口类
    var $InfoWindow = new Class(UI);

    $InfoWindow.include({

        init:function(txt, opt, map){

            this.infoWindow = new BMap.InfoWindow(txt, opt);

            this.map = map;

        },

        addToMap:function(point){

            this.map.openInfoWindow(this.infoWindow, point ? point : this.map.getCenter());

        }
    });

    scope.Marker = $Marker;
    scope.InfoWindow = $InfoWindow;

})(window, jQuery);


/* ===========================================================================
 包体说明：   地图标记


 索引器：     $MapMarker
 =========================================================================*/
(function(scope, jQuery){

    // 地图标记类，继承自BMap.Marker
    var $Marker = new Class($View);
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

            // 定义标记
            this.marker = new BMap.Marker(point, {icon: icon});

            // 定义marker所属的map
            this.map = map;

            // 允许拖动
            this.marker.enableDragging();

            // 定义标签
            this.label = new BMap.Label(this.getLabelTitle());
            this.label.setOffset(new BMap.Size(30,10));
            this.marker.setLabel(this.label);

            // 事件绑定
            var that = this,
                _label = this.label;
            this.marker.addEventListener("dragend", function(e){
                // 这里 this 指的是这个Marker
                //var pos = this.map.pointToOverlayPixel(this.getPosition());
                _label.setContent(that.getLabelTitle());

            });
            this.marker.addEventListener("dragging", function(e){
                _label.setContent(that.getLabelTitle());
            })

        },

        // 添加至地图
        addToMap:function(){
            this.map.addOverlay(this.marker);
        },

        // 获取标注标题
        getLabelTitle:function(){
            return "当前位置：" + this.marker.getPosition().lng + ", " + this.marker.getPosition().lat;
        },

        // 删除这个标记
        remove:function(){
            this.map.removeOverlay(this.marker);
            this.marker.dispose();
            delete this;
        }
    });

    // 信息窗口类
    var $Info = new Class($View);

    $Info.include({

        init:function(txt, opt, map){

            this.infoWindow = new BMap.InfoWindow(txt, opt);

            this.map = map;

        },

        addToMap:function(point){

            this.map.openInfoWindow(this.infoWindow, point ? point : this.map.getCenter());

        }
    });

    scope.MapMarker = $Marker;
    scope.MapInfo = $Info;
})(window, jQuery);


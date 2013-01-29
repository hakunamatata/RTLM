/* ===========================================================================
 包体说明：   地图标记，
            属性：
                map             ,所属地图
                marker          ,BMap标记
                label           ,BMap标签
            方法
                lineTo          ,连接地图上另外一个标记
                    marker      ,另外一个标记
                    opts        ,连接线属性
                isEnableDrag    ,是否允许拖拽


 索引器：     $Marker
 =========================================================================*/
(function(scope, jQuery){

    // 地图标记类，继承自BMap.Marker
    var $Marker = new Class($View);
    $Marker.include({

         lines:[],

        init:function(point, infoWindow, opt, map){


            var
                // 对象this 的备份
                that = this,
                // 创建图标对象
                icon = new BMap.Icon(opt.icon, new BMap.Size(29, 43), {
                // 指定定位位置。
                // 当标注显示在地图上时，其所指向的地理位置距离图标左上
                // 角各偏移10像素和25像素。您可以看到在本例中该位置即是
                // 图标中央下端的尖角位置。
                offset: new BMap.Size(10, 25),
                // 设置图片偏移。
                // 当您需要从一幅较大的图片中截取某部分作为标注图标时，您
                // 需要指定大图的偏移位置，此做法与css sprites技术类似。
                imageOffset: new BMap.Size(0, 0 - opt.index * 25)   // 设置图片偏移

                }),

                // 定义标记
                marker = new BMap.Marker(point, {icon: icon}),

                // 定义marker所属的map
                map = map,

                // 定义标签
                label = new BMap.Label();

            label.setOffset(new BMap.Size(30,10));

            //此处判断是否需要添加Label
            //marker.setLabel(label);

            // 事件绑定
            marker.addEventListener("dragend", function(e){
                // 这里 this 指的是这个Marker
                //var pos = this.map.pointToOverlayPixel(this.getPosition());
                this.setTitle(that.getLabelTitle());

            });
            marker.addEventListener("dragging", function(e){
                this.setTitle(that.getLabelTitle());
            })

            if (infoWindow){
                marker.addEventListener("click", function(e){
                    this.openInfoWindow(infoWindow);
                });
                this.infoWindow = infoWindow;
            }




            this.map = map;
            this.marker = marker;
            this.label = label;


            this.addToMap();

        },

        // 添加至地图
        addToMap:function(){
            this.map.addOverlay(this.marker);
        },

        // 获取标注标题
        getLabelTitle:function(){
            return "当前位置：" + this.marker.getPosition().lng + ", " + this.marker.getPosition().lat;
        },

        // 连接另外一个标注
        lineTo:function(marker, otps){
            var
                defaultOpts = otps ? opts:{
                    strokeColor:'blur',
                    strokeWeight:6,
                    strokeOpacity:0.5
                },

                // 连接线
                polyline = new BMap.Polyline([
                    this.marker.getPosition(),
                    marker.marker.getPosition()
                ]),

                // 本标记位置
                thisPosition = this.marker.getPosition(),

                // 目标标记位置
                thatPosition = marker.marker.getPosition(),

                // 本标记像素位置
                thisPixel = this.map.pointToOverlayPixel(thisPosition),

                // 目标标记像素位置
                thatPixel = this.map.pointToOverlayPixel(thatPosition),

                markersOffset = new BMap.Size(
                    //100,200
                    (thatPixel.x - thisPixel.x) / 2 ,
                    (thatPixel.y - thisPixel.y) / 2
                ),

                // 距离标签
                distlabel = new BMap.Label(Math.round(this.map.getDistance(thatPosition, thisPosition)) + '米' ,{offset:markersOffset});


            this.marker.setLabel(distlabel);

            this.map.addOverlay(distlabel);

            this.map.addOverlay(polyline, defaultOpts);

            this.lines.push(polyline);

            marker.lines.push(polyline);

            return this.marker;
        },

        enableDrag:function(){

            this.marker.enableDragging();

            return this.marker;

        },

        disableDrag:function(){

            this.marker.disableDragging();

            return this.marker;

        },

        // 删除这个标记
        remove:function(){
            this.map.removeOverlay(this.marker);
            this.marker.dispose();
            for(var i in this.lines)
                delete this.lines[i];
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

// 异步加载地图
function loadScript(src) {
    var script = document.createElement("script");
    script.src = src;
    document.body.appendChild(script);
}

// 标识符， 地图是否加载完毕
function isMapLoaded() {
    if (map == null) {
        return false
    }
    else {
        clearInterval(t_mapLoading);
        return true;
        ;
    }
}


// 初始化地图
function inittializeMap() {

    map = new BMap.Map('allmap');
    var point = new BMap.Point(118.796116, 32.056313);    // 创建点坐标
    map.centerAndZoom(point, 17);                     // 初始化地图,设置中心点坐标和地图级别。
}


function createMarker(type, point) {

    var typeImg = 'img/' + (type == 'customer' ? 'marker' : type === 'consumer' ? 'marker_orange_c' : 'small_red_loc') + '.png'

        , icon = new BMap.Icon(typeImg, new BMap.Size(29, 43), {
            // 指定定位位置。
            // 当标注显示在地图上时，其所指向的地理位置距离图标左上
            // 角各偏移10像素和25像素。您可以看到在本例中该位置即是
            // 图标中央下端的尖角位置。
            offset:new BMap.Size(10, 25),
            // 设置图片偏移。
            // 当您需要从一幅较大的图片中截取某部分作为标注图标时，您
            // 需要指定大图的偏移位置，此做法与css sprites技术类似。
            imageOffset:new BMap.Size(0, 0)   // 设置图片偏移

        }),


    // 定义标记
        marker = new BMap.Marker(point, {icon:icon, animation:type === 'consumer' });

    return marker

}

function createCustomer() {

    var
        point = new BMap.Point(118.796116, 32.056313),

        marker = createMarker('customer', point);

    map.addOverlay(marker);

    return marker;
}


function createConsumer(customer) {

    var

        customerPosition = customer.getPosition(),

        newPoint = new BMap.Point(

            customerPosition.lng + (1 - Math.random() * 2) / 80,

            customerPosition.lat + (1 - Math.random() * 2) / 80

        ),

        marker = createMarker('consumer', newPoint);

    map.addOverlay(marker);

    return marker;


}


function randomCustomers(consumer) {

    var
        html = [];
    html.push('<span style="font-size:15px; font-weight: bold">属性信息: </span><br/>');
    html.push('<table border="0" cellpadding="1" cellspacing="1" >');
    html.push('  <tr>');
    html.push('      <td align="left" class="common">名 称：</td>');
    html.push('      <td colspan="2">');
    html.push('商家【' + Math.round(Math.random() * 100) + '】');
    html.push(' </td>');
    html.push('  </tr>');
    html.push('  <tr>');
    html.push('      <td  align="left" class="common">电 话：</td>');
    html.push('      <td colspan="2">');
    html.push('137' + Math.round(Math.random() * 100000000));
    html.push(' </td>');
    html.push('  </tr>');
    html.push('  <tr>');
    html.push('      <td  align="left" class="common">商品</td>');
    html.push('      <td colspan="2">');
    html.push('各种商品,blabla...');
    html.push(' </td>');
    html.push('  </tr>');
    html.push('  <tr>');
    html.push('	     <td  align="center" colspan="3">');
    html.push('          <input type="button" id="buttonSelect" value="选择">&nbsp;&nbsp;');
    html.push('	     </td>');
    html.push('  </tr>');
    html.push('</table>');


    var
        maxNum = 50,
        customerPosition = consumer.getPosition(),
        customers = [];


    for (var i = 0; i < maxNum; i++) {

        var
            newPoint = new BMap.Point(

                customerPosition.lng + (1 - Math.random() * 2) / 80,

                customerPosition.lat + (1 - Math.random() * 2) / 80

            ),

            customer = createMarker('customer', newPoint);

        customer.addEventListener('click', function (e) {

            var infoWin = new BMap.InfoWindow(html.join(""), {offset:new BMap.Size(0, -10)});

            if (!_onDelivering) {
                infoWin.addEventListener('open', function (ie) {

                    $("#buttonSelect", ie.content).click(function () {

                        DeliverThisForMe(e.currentTarget);

                    })


                })
            }


            e.currentTarget.openInfoWindow(infoWin);


        })


        customers.push(customer);

        map.addOverlay(customer);

    }

    return customers;


}


function randomDelivers(customer) {

    var
        maxNum = 10,
        customerPosition = customer.getPosition(),
        delivers = [];


    for (var i = 0; i < maxNum; i++) {

        var
            newPoint = new BMap.Point(

                customerPosition.lng + (1 - Math.random() * 2) / 200,

                customerPosition.lat + (1 - Math.random() * 2) / 200

            ),

            deliver = createMarker('deliver', newPoint);

        delivers.push(deliver);

        map.addOverlay(deliver);

    }

    return delivers;

}


function searchForNearestDeliver(tar, delivers) {

    var
        nearest,

        minDistance = 99999999;

    for (var i = 0; i < delivers.length; i++) {

        var distance = map.getDistance(tar.getPosition(), delivers[i].getPosition());

        if (distance < minDistance) {

            minDistance = distance;

            nearest = delivers[i];

        }

    }

    return nearest;

}

function delivery(deliverMarker, tarMarker, callback) {


    var

        from_p = deliverMarker.getPosition(),

        to_p = tarMarker.getPosition(),

        step = 0.0001,

        alpha = Math.atan2(to_p.lat - from_p.lat, to_p.lng - from_p.lng),


        distanceLine,

        t;


    // 删除其余配送员
    setTimeout(function () {

        for (var i = 0; i < deliversMarker.length; i++) {

            if (deliversMarker[i] === deliverMarker) {

                continue;

            }
            else
                map.removeOverlay(deliversMarker[i]);


        }

    }, 2000);


    t = setInterval(function () {

            var

                deliver_p = deliverMarker.getPosition(),

                tar_p = tarMarker.getPosition(),

                nextpoint = new BMap.Point(

                    deliver_p.lng += step * Math.cos(alpha),

                    deliver_p.lat += step * Math.sin(alpha)

                ),

                distance = Math.sqrt(Math.pow(deliver_p.lng - tar_p.lng, 2) + Math.pow(deliver_p.lat - tar_p.lat, 2)),

                distanceLable = new BMap.Label(Math.round(map.getDistance(tar_p, deliver_p)) + '米', {offset:markerOffset }),

                from_pixel = map.pointToOverlayPixel(deliver_p),

                to_pixel = map.pointToOverlayPixel(tar_p),

                markerOffset = new BMap.Size(

                    (to_pixel.x - from_pixel.x) / 2,

                    (to_pixel.y - from_pixel.y) / 2

                ),

                originLabel = deliverMarker.getLabel();


            if (distance > step) {


                if (!distanceLine)

                    distanceLine = new BMap.Polyline([

                        deliver_p,

                        tar_p

                    ]);

                else

                    distanceLine.setPath([


                        deliver_p,

                        tar_p

                    ]);


                deliverMarker.setPosition(nextpoint);

                if (originLabel) map.removeOverlay(originLabel);

                deliverMarker.setLabel(distanceLable);

                map.addOverlay(distanceLine);

            } else {

                clearInterval(t);

                map.removeOverlay(deliverMarker.getLabel())

                map.removeOverlay(distanceLine);

                if (callback) callback.call();


            }


        }
        ,
        1000
    )
    ;

}


function DeliverThisForMe(customer) {

    map.closeInfoWindow();

    consumerMarker.disableDragging();

    _onDelivering = true;

    deliversMarker = randomDelivers(customer);

    nearestDeliver = searchForNearestDeliver(customer, deliversMarker);

    delivery(nearestDeliver, customer, function () {

        delivery(nearestDeliver, consumerMarker, function () {

            map.removeOverlay(nearestDeliver);

            _onDelivering = false;

            consumerMarker.enableDragging();

        })


    })


}


// 执行地图的加载


// 地图对象， 包括遮罩层和一些控制方法
var
    Map = new $Map,
// 百度地图实例
    map = null,
// 加载地图的Timer
    t_mapLoading,
// 定义页面UI
    nav = new UI.Navbar(".navbar"),
// 首页展示加载完毕之后
    hero = new UI.Hero(".Heroframe"),

    consumerMarker,

    customrsMarker,

    deliversMarker,

    _onDelivering,

    nearestDeliver;


t_mapLoading = setInterval(isMapLoaded, 10);

loadScript("http://api.map.baidu.com/api?v=1.4&callback=inittializeMap");

window.onload = function () {

    // 点击消费者遮罩层书法的事件
    var csrm = hero.units["consumer"],
        __cons_confirm = false;
    csrm.modalView.on("show", function () {
        hero.hide();
    });
    csrm.modalView.on("hide", function () {
        if (!__cons_confirm)
            hero.show()
    });

    var cstm = hero.units["customer"],
        __cstm_confirm = false;
    cstm.modalView.on("show", function () {
        hero.hide();
    });
    cstm.modalView.on("hide", function () {
        if (!__cstm_confirm)
            hero.show();
    })

    // 消费者订购货物
    $(".btn-primary", "#Modalconsumer").click(function () {

        __cons_confirm = true; // 标记此为用户确认操作， Modal消失后不在显示 consumer

        if (isMapLoaded()) {

            hero.units["consumer"].modalView.modal('hide');
            hero.hide();
            Map.show();


            var point = new BMap.Point(118.796116, 32.056313);


            consumerMarker = createMarker('consumer', point);
            consumerMarker.enableDragging();

            customrsMarker = randomCustomers(consumerMarker);

            _onDelivering = false;

            map.addOverlay(consumerMarker);


        }

        else

            console.log("please waiting, while tha map is loading...");

    });


    // 商家要求配送
    $(".btn-primary", "#Modalcustomer").click(function () {

        __cstm_confirm = true;

        if (isMapLoaded()) {

            hero.units["customer"].modalView.modal('hide');
            hero.hide();
            Map.show();

            customerMarker = createCustomer(),

                consumerMarker = createConsumer(customerMarker),

                deliversMarker = randomDelivers(customerMarker),

                nearestDeliverMarker = searchForNearestDeliver(customerMarker, deliversMarker);

            delivery(nearestDeliverMarker, customerMarker, function () {

                delivery(nearestDeliverMarker, consumerMarker, function () {

                    map.removeOverlay(nearestDeliverMarker);

                });

            });


        }


    });


};


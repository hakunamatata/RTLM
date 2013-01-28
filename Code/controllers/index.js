var models = require('../models'),
    Consumer = models.Consumer,
    Customer = models.Customer;

exports.add_consumer = function (req, res, next){
    var q = req.body;
    Consumer.findOne({cellphone: q.cellphone},function(err , doc){
        if(err){
            return next(err);
        };

        if (doc == null || doc.length == 0) {
            var consumer = new Consumer();
            consumer.cellphone = q.cellphone;
            consumer.location = q.location;
            consumer.save(function(err){
                if (err) {
                    return next(err);
                }
                return res.json({success:true});
            })
        }
        else  return res.json({success:false});

    })
};

// 随即创建商家
exports.enumCustomer = function(req, res, next){

    var maxnum = 1000;

    for(i=0;  i < maxnum; i++){

        var customer = new Customer();
        customer.name = '随机商家【' + i + '】';
        customer.cellphone = Math.round(Math.random() * 1000000) + 10000000 + '';
        customer.location = {
            lng: ( 118763418 + Math.round( Math.random()*51340) ) / 1000000,
            lat: ( 32011844 + Math.round( Math.random()*51951 ) ) / 1000000
        };
        customer.save(function(err, i){

            if(err){return next(err);}

            console.log("》》  %s  《《 customer(s) saved. ", i)

        })

    }

};

exports.findCustomersAround = function(req, res, next){
    var q = req.body;
    Customer.find({"$where": function(){
            var that = this["location"];
            var range = Math.sqrt( Math.pow(that.lng - 118.796116, 2) + Math.pow(that.lat - 32.056313, 2));
            if(range <= 0.005) return true;
            else
            return false;
        }
        }, function(err, doc){
            if(err)
                return next(err);
            res.json(doc);

        })

}

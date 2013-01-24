var models = require('../models'),
    Consumer = models.Consumer;

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
}
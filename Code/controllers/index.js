var models = require('../models'),
    Consumer = models.Consumer;

exports.add_consumer = function (req, res, next){
    console.log(req.cellphone);
    Consumer.findOne({cellphone: req.cellphone},function(err,doc){
        if(err){
            return next(err);
        };

        if (doc.length > 0) return res.json({success:false})
        else{
            Consumer.cellphone = req.cellphone;
            Consumer.location = req.location;
            Consumer.save(function(err){
                if (err) {
                    return next(err);
                }
                return res.json({success:true});
            })
        }

    })
}
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var ConsumerSchema = new Schema({
    cellphone: { type: String },
    location: { lng: Number, lat:Number, monment: Date }
});

mongoose.model('Consumer', ConsumerSchema);
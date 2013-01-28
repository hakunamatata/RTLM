var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var CustomerSchema = new Schema({
    name:{ type : String },
    cellphone: { type: String },
    location: { lng: Number, lat: Number }
});

mongoose.model('Customer', CustomerSchema);
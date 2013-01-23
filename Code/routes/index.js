
/*
 * GET home page.
 */
var cons_ctrl = require("../controllers");


var brand = 'Bootstrap for Jade';

exports.index = function(req, res){
  res.redirect('/home');
}

exports.home = function(req, res){
  res.render('home', { title: 'Home', id: 'home', brand: brand })
};

exports.about = function(req, res){
  res.render('about', { title: 'About', id: 'about', brand: brand })
};

exports.addConsumer = function(req, res, next){
    return cons_ctrl.add_consumer(req,res,next);
}

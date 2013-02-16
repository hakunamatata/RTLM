
/*
 * GET home page.
 */


var brand = '快速物流';

exports.index = function(req, res){
  res.redirect('/home');
}

exports.home = function(req, res){
  res.render('home', {active: ''})
};

exports.about = function(req, res){
  res.render('about', {active: 'about'})
};

exports.store = function(req, res){
    res.render('store', {active: 'store'})
}

exports.buy = function(req, res){
    res.render('buy',{active: 'buy'})
}
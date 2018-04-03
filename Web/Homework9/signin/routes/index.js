var express = require('express');
var router = express.Router();

/* GET home page. */
module.exports = function(app){
	app.get('/', function(req, res, next) {
		res.redirect('/signin');
	});
	app.get('/signin', function(req, res, next) {
		res.render('signin', { title: 'signin'});
	});
	app.post('/signin', function(req, res, next) {

	});
	
	app.get('/signup', function(req, res, next) {
		res.render('signup', { title: 'signup'});
	});

	app.post('/signup', function(req, res, next) {
	});

	app.get('/detail', function(req, res, next) {
		res.render('detail', { title : 'detail', user: user});
	});

	app.get('/logout', function(req, res, next) {
		res.redirect('/signin');
	});
}

module.exports = router;

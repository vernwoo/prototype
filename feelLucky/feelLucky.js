//a lightweight web service that returns random fortune cookie style 'i feel lucky' phrases
//
var http = require('http');
var goodies = [ 'i feel lucky today', 'have a nice day', 'money falling from sky'];
http.createServer(function(req, res) {
	index = Math.floor(Math.random()*3);
	res.write(goodies[index]);
	res.end();
	console.log('index is ' + index + ':' + goodies[index]);
}).listen(8080);


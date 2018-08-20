//a lightweight web service that returns random fortune cookie style 'i feel lucky' phrases
//
var http = require('http');
var fs = require('fs');
var readline = require('readline');
var os = require('os');
var stream = require('stream');

var port = 80;
var filePath = process.cwd() + "/lucky.txt";
console.log("Reading from file " + filePath);
var instream = fs.createReadStream(filePath);
var outstream = new stream;
var rl = readline.createInterface(instream, outstream);
var goodies = [];

console.log('feelLucky site staring ......');

rl.on('line', function(line) { 
			console.log("line:", line);
			goodies.push(line);
		});
rl.on('error', function(err) {
			console.log('Error:', err);
		});
rl.on('close', function() {
			console.log('Finish reading.');
			var size = goodies.length;
			http.createServer(function(req, res) {
				index = Math.floor(Math.random()*size);
				res.write(goodies[index]);
				res.end();
				console.log('index is ' + index + ':' + goodies[index]);
			}).listen(port);
			console.log('feelLucky site started. Goto http://host:' + port + ' to try your luck');
		});

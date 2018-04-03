var http = require('http');
var url = require('url');
var path = require('path');
var fs = require('fs');
var port = 3000;

var server = http.createServer(function (req, res) {
    var path = url.parse(req.url).pathname;
    var mimeType = getMimeType(path);
    if (!!mimeType) {
        fs.readFile(__dirname + path, function (err, data) {
            if (err) {
                res.writeHead(500);
                res.end();
            } else {
                res.writeHead(200,{ 'Content-Type': mimeType});
                res.end(data);
            }
        });
    } else {
        returnNum(req, res);
    }
});
server.listen(port);
console.log('Server is running at', port);

function getMimeType(pathname) {
    var type = {
        ".html": "text/html",
        ".js": "application/javascript",
        ".css": "text/css",
        ".jpg": "image/jpeg",
        ".png": "image/png"
    };
    var ext = path.extname(pathname);
    return type[ext];
}


function returnNum(req, res) {
    var randomTime = 1000 + getRandomNum(2000);
    var randomNum = 1 + getRandomNum(9);
    setTimeout(function () {
        res.writeHead(200, { 'Content-Type': 'text/plain' });
        res.end("" + randomNum);
    }, randomTime);
}

function getRandomNum(limit) {
    return Math.round(Math.random() * limit);
}
var fs    = require('fs'),
    path  = require('path'),
    query = require('querystring');
    data  = require('./dataHandle.js');

var publicPath = '../public/';

function registerPage(response) {
    fs.readFile('../public/index/index.html', 'utf-8', function(err, data) {
        if (err) {
            response.writeHead(404, { "Content-Type": "text/html" });
            response.end(err.toString());
        } else {
            response.writeHead(200, { "Content-Type": "text/html" });
            response.end(data);
        }
    });
}

function register(response, newUserInfo) {
    var postJSONData = query.parse(newUserInfo);
    var db = data.insert(postJSONData);
    if(db === true) {
        console.log("Register succeed");
        response.writeHead(302, {'Location': ('http://127.0.0.1:8000/?username=' + postJSONData.username)});
        response.end();
    } else {
        var scriptsToWrite = "<script>" +
                "$('#warnings').html(\"" + db + "\")" +
                "</script>";
        var revealHTML = fs.readFileSync('../public/index/index.html', 'utf-8');
        response.writeHead(200, {"Content-Type": "text/html"});
        response.write(revealHTML);
        response.write(scriptsToWrite);
        response.end();
    }
}

function mimeDate(response, pathName) {
    var ext = path.extname(pathName);
    console.log("MIME : GET " + pathName);
    var MIMEobj = {
        '.html': 'text/html',
        '.css': 'text/css',
        '.js': 'application/javascript',
        '.jpg': 'image/jpg',
    };
    if (ext === '.ico') {
        response.writeHead(404);
        response.end();
    } else if (ext !== '.html' && ext !== '') {
        console.log("path : " + publicPath + pathName.slice(1).toString());
        fs.readFile(publicPath + pathName.slice(1).toString(), function(err, data) {
            if (err) {
                response.writeHead(404, { 'Content-Type': MIMEobj[ext] });
                response.end(err.toString());
            } else {
                response.writeHead(200, { 'Content-Type': MIMEobj[ext] });
                response.write(data);
            }
            response.end();
        });
    } else {
        registerPage(response);
    }
}

function getUserInfo(response, username) {
    var userInfo = data.UserInfo(username);
    if(!userInfo) {
        fs.readFile('../public/index/index.html', 'utf-8', function(err, data) {
            if (err) {
                response.writeHead(404, { "Content-Type": "text/html" });
                response.end(err.toString());
            } else {
                response.writeHead(200, { "Content-Type": "text/html" });
                response.end(data);
            }
        });
    } else {
        var userInfoFile = fs.readFileSync('../public/index/userInfo.html', 'utf-8');
        var scriptsToWrite = "<script>$(function() {" +
            "$('#userName').html(\"" + userInfo.username + "\");" +
            "$('#studentId').html(\"" + userInfo.studentId + "\");" +
            "$('#phone').html(\"" + userInfo.phone + "\");" +
            "$('#email').html(\"" + userInfo.email + "\");" +
            "});</script>";
        response.writeHead(200, {"Content-Type": "text/html"});
        response.write(userInfoFile);
        response.write(scriptsToWrite);
        response.end();
    }
}

exports.registerPage = registerPage;
exports.mimeDate     = mimeDate;
exports.getUserInfo  = getUserInfo;
exports.register     = register;
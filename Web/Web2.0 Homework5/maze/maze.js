var isStart = 0;
var isCheat = 0;
var isEnd = 0;
var PathPass = [0, 0, 0, 0, 0];

window.onload = function(){
	check();
}
function check(){
	var s = document.getElementById("start");
	s.addEventListener("mouseover",startFunc);
	var w = document.getElementsByClassName("walls");	
	for (var i = 0; i < 5; i++) {
		w[i].addEventListener("mouseover",wallFunc);
		w[i].addEventListener("mouseout",reset);
	}
	var p = document.getElementsByClassName("paths");
	for (var i = 0; i < 5; i++) {
		p[i].addEventListener("mouseover",pathFunc);
	}
	var e = document.getElementById("end");
	e.addEventListener("mouseover",endFunc);
}
function startFunc(){
	isCheat = 0;
	isEnd = 0;
	for (var i = 0; i < 5; i++)
		PathPass[i] = 0;
	isStart = 1;
	document.getElementById("gameResult").innerHTML = "Begin!";
}
function wallFunc(event){
	if (isStart == 1 && isEnd == 0) {
		document.getElementById("gameResult").innerHTML = "You Lose!";
		event.target.className = "hitWall";
		isStart = 0;
		isEnd = 0;
		isCheat = 0;
		for (var i = 0; i < 5; i++) 
			PathPass[i] = 0;
	}
}
function reset(event){
	event.target.className = "walls";
}
function pathFunc(event){
	var pathnum = event.target.id;
	if (pathnum == "pathBlock_1") 
		PathPass[0] = 1;
	if (pathnum == "pathBlock_2")
		PathPass[1] = 1;
	if (pathnum == "pathBlock_3")
		PathPass[2] = 1;
    if (pathnum == "pathBlock_4")
		PathPass[3] = 1;
	if (pathnum == "pathBlock_5")
		PathPass[4] = 1;	
}
function endFunc(){
	if (isStart == 1) {
		for (var i = 0; i < 5; i++) {
		    if (PathPass[i] != 1) {
		    	isCheat = 1;
		    	break;
		    }
	    }
	    if (isCheat == 1) {
	    	document.getElementById("gameResult").innerHTML = "Don't Cheat, you should start from the 'S' and move to the 'E' inside the maze!";
	    }else{
	    	document.getElementById("gameResult").innerHTML = "You Win!";
	    }
	}
	isEnd = 1;
	isStart = 0;
}

var isStart = 0;
var count = 0;
var c = 30;
var t;
var check;
var b = new Array();

window.onload = function(){
	createButton();	
	var ss = document.getElementById('startStop');
	ss.addEventListener('click',control);
}
function createButton(){
	var gameBlock = document.getElementById("mainBlock");
	for (var i = 0; i < 60; i++) {
		var newButton = document.createElement("input");
		newButton.setAttribute("type","radio");
		newButton.setAttribute("class","button");
		newButton.addEventListener('click',clk);
		newButton.chosen = false;
		gameBlock.appendChild(newButton);
		b[i] = newButton;
	}
}
function timer() {
    c = c - 1;
    document.getElementById("time").value = c;
    t = setTimeout(timer, 1000);
    if (c == 0) {
    	isStart = 0;
    	clearInterval(t);
        alert("Game Over.\n Your score is: " + document.getElementById("score").value);
        document.getElementById("state").value = "Game over";
        for (var i = 0; i < 60; i++) {
        	b[i].checked = false;
        	b[i].chosen = false;
        }
    }
}
function control(){
	if (isStart == 0) {
		isStart = 1;
		count = 0;
		c = 31;
		document.getElementById("score").value = count;
		document.getElementById("state").value = "Playing";
        for (var i = 0; i < 60; i++) 
        	b[i].chosen = false;
		random();
		timer();

	}else{
		alert("Game Over.\n Your score is: " + document.getElementById("score").value);
		isStart = 0;
		c = 0;
		clearInterval(t);
		document.getElementById("time").value = c;
		document.getElementById("score").value = count;
		document.getElementById("state").value = "Game over";
		for (var i = 0; i < 60; i++) {
        	b[i].checked = false;
        	b[i].chosen = false;
        }
	}
}
function random(){	
    if (c != 0) { 
        check = Math.floor(Math.random()*60);
        b[check].checked = true;
        b[check].chosen = true;
    }
}
function clk(event){
	if (c == 0) {
		this.checked = false;
	}else{
		if (this.checked == true && this.chosen == true) {
            count++;
			document.getElementById("score").value = count;
			this.checked = false;
            for (var i = 0; i < 60; i++) 
        		b[i].chosen = false;			
			random();
	    }else {
            count--;
            document.getElementById("score").value = count;
            b[check].checked = true;
            this.checked = false;
	    }
	}
}

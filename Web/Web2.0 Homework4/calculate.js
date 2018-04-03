function back()
{
	var ret = document.getElementById('inputscreen');
	if (ret.value.length > 1) 
	{
		ret.value = ret.value.substring(0, ret.value.length - 1);
	}else{
		ret.value = '0';
	}
}
var num;
function show(num)
{
	var str = document.getElementById("inputscreen").value;
    if (str.length > 25) 
    {
    	alert("The length is too long!");
    	clearall();
    }else{
    	var y = document.getElementById(num).value;
	    if (str.length == 1 && str[0] == '0') 
	    {
	    	if (y == ".") 
	    	{
	    		document.getElementById("inputscreen").value += document.getElementById(num).value;
	    	}else if (y != "/" && y != "*" && y != ")") 
		    {
			    document.getElementById("inputscreen").value = document.getElementById(num).value;
		    }
	    }else{	    	
	            if (str.length > 1 && (y >= "0" && y <= "9") && str[str.length - 1] == '0' && (((str[str.length - 2] < '0' || str[str.length - 2] > '9') && str[str.length - 2] != '.'))) {back();}	    	    
		            document.getElementById("inputscreen").value += document.getElementById(num).value;
	    }
    }
}

function clearall()
{
	document.getElementById("inputscreen").value = '0';
}
function equal()
{
	var x = document.getElementById("inputscreen").value;
    var arr = new Array();
    var t = 0;
	var out = 1;
	var operator = 0;
	if ((x[x.length - 1] > '9' || x[x.length - 1] < '0') && x[x.length - 1] != ')') 
	{
		out = 0;
	}else{
		for (var i = 0; i < x.length; i++) 
	    {
		    if (x[i] == '(' || x[i] == ')') 
	    	{
	    		arr[t] = x[i];
	    		t++;
	    	}
		    if (x[i] > '9' || x[i] < '0') 
		    {
		    	if (x[i] == '.')
		    	{
		    		if (x[i + 1] > '9' || x[i + 1] < '0')
		    		{
		    		    out = 0;
		    		    break;		    			
		    		}
		    		var k = 1;
		    		while(x[i + k] >= '0' && x[i + k] <= '9')
		    		{
		    			if (x[i + k + 1] == '.') {out = 0;}
		    			k++;
		    		}
		    	}else if(x[i] == '('){
		    		if ((x[i + 1] > '9' || x[i + 1] < '0') && x[i + 1] != '+' && x[i + 1] != '-')
		    		{
		    		    out = 0;
		    		    break;		    			
		    		}
		    	}else if(x[i] == ')'){
		    		if (x[i + 1] == '(' || x[i + 1] == '.')
		    		{
		    		    out = 0;
		    		    break;		    			
		    		}		    		
		    	}else{
		    		operator++;
		    		if ((x[i + 1] > '9' || x[i + 1] < '0') && x[i + 1] != '(' ) 
		    		{
		    			out = 0;
		    			break;
		    		}
		    	}
		    }
	    }
	}
	if (operator == 0) {out = 0;}

	if (t % 2 == 1) 
	{
		out = 0;
	}else{
		for (var i = 0; i < t; i++) {
			if (i < t/2 && arr[i] != '(') {out = 0; break;}
			if (i >= t/2 && arr[i] != ')') {out = 0; break;}
		}
	}
	if (out == 1){
		document.getElementById("inputscreen").value = parseFloat(eval(x).toFixed(12));
	}     
	else
	{
		alert("The expression is not valid");
		clearall();
	}
}
function onload()
{
	document.getElementById("inputscreen").value = '0';
}
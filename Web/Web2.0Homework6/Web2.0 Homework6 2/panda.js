var posX=new Array(16);
var posY=new Array(16);
var check = 0;
var win = 0;
var s = 0;
var step = 0;
window.onload = function () 
{
        var l,t;
        for(var i = 0; i < 16; i++)
        {
                posX[i] = Math.floor(i / 4) + 1;
                posY[i] = i % 4 + 1;
                var pic = '<div class="piece" id="p_' + i + '"></div>';
                l = (i % 4) * 88 + "px";
                t = Math.floor(i / 4) * 88 + "px";
                $("#play").append(pic);
                $("#p_" + i).css("background-position", "-" + l + " -" + t);
                $("#p_" + i).css("left", l);
                $("#p_" + i).css("top", t);

                $("#p_" + i).click(move);
                $("#p_" + i).attr("num", i);
        } 
        $("#p_15").css("background", "0");
        $("#p_15").css("border", "0");
        $("#steps").val(0);
        $("button").click(start);
}
function start()
{
        var t1,t2;
        win = 0;
        s = 1;
        step = 0;
        $("#steps").val(0);
        for (var i = 0; i < 20; i++) 
        {
                var ran = Math.floor(Math.random()*10+1);
                var direction = ran % 4;
                if (direction == 0 && posX[15] == 1) {continue;}
                else if(direction == 1 && posY[15] == 4){continue;}
                else if(direction == 2 && posX[15] == 4){continue;}
                else if(direction == 3 && posY[15] == 1){continue;}
                else
                {
                        if(direction == 0)
                        {
                                for (var i = 0; i < 16; i++) 
                                {
                                        if (posY[i] == posY[15] && posX[15] - posX[i] == 1) 
                                                break;         
                                }
                                posX[15]--;
                                posX[i]++;
                                t1 = (posX[15] - 1) * 88 + "px";
                                t2 = (posX[i] - 1) * 88 + "px";
                                $("#p_" + i).css("top", t2);
                                $("#p_15").css("top", t1);
                        }
                        else if(direction == 1)
                        {
                                for (var i = 0; i < 16; i++) 
                                {
                                        if (posX[i] == posX[15] && posY[i] - posY[15] == 1) 
                                                break;         
                                }
                                posY[15]++;
                                posY[i]--;
                                t1 = (posY[15] - 1) * 88 + "px";
                                t2 = (posY[i] - 1) * 88 + "px";
                                $("#p_15").css("left", t1);
                                $("#p_" + i).css("left", t2);
                        }
                        else if(direction == 2)
                        {
                                for (var i = 0; i < 16; i++) 
                                {
                                        if (posY[i] == posY[15] && posX[i] - posX[15] == 1) 
                                                break;         
                                }
                                posX[15]++;
                                posX[i]--;
                                t1 = (posX[15] - 1) * 88 + "px";
                                t2 = (posX[i] - 1) * 88 + "px";
                                $("#p_" + i).css("top", t2);
                                $("#p_15").css("top", t1);
                        }
                        else if(direction == 3)
                        {
                                for (var i = 0; i < 16; i++) 
                                {
                                        if (posX[i] == posX[15] && posY[15] - posY[i] == 1) 
                                                break;         
                                }
                                posY[15]--;
                                posY[i]++;
                                t1 = (posY[15] - 1) * 88 + "px";
                                t2 = (posY[i] - 1) * 88 + "px";
                                $("#p_15").css("left", t1);
                                $("#p_" + i).css("left", t2);                               
                        }
                }            
        }
}
function move()
{
        if(s == 1)
        {
                var t = $(this).attr("num");
                if (this.id != "#p_15" && ((Math.abs(posX[t] - posX[15]) == 1 && posY[t] == posY[15])||(Math.abs(posY[t] - posY[15]) == 1 && posX[15] == posX[t]))) 
                {
                        var t1 = (posX[15] - 1) * 88 + "px";
                        var t2 = (posY[15] - 1) * 88 + "px";
                        var t3 = (posX[t] - 1) * 88 + "px";
                        var t4 = (posY[t] - 1) * 88 + "px";
                        $("#p_15").css("left", t4);
                        $("#p_" + t).css("left", t2);                    
                        $("#p_15").css("top", t3);
                        $("#p_" + t).css("top", t1);    
                        var t5 = posX[15];
                        var t6 = posY[15];
                        var t7 = posX[t];
                        var t8 = posY[t];
                        posX[t] = t5;
                        posY[t] = t6;
                        posX[15] = t7;
                        posY[15] = t8;
                        win = 1;
                        step++;
                        $("#steps").val(step);
                }
                for (var i = 0; i < 16; i++) 
                {               
                        if (posX[i] - Math.floor(i / 4) == 1 && posY[i] - i % 4 == 1) 
                                check++;  
                        else
                                break;                      
                }
                if (check == 16 && win) 
                {
                        alert("You win!"); 
                        s = 0;
                        step = 0;
                }
                check = 0;                
        }
        

}
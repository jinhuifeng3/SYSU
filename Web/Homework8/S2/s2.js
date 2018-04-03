var clickedBtns = 0;
var total = 0;

$(function(){
	init();
	$('#button').mouseleave(init);	
});

function init(){
	clickedBtns = 0;
	total = 0;
	$("#info-bar").text("");
	$("#info-bar").addClass("disable");
	$(".btns").removeClass("disable");
	$("#info-bar").css("background","grey");
	$(".btns").removeClass("clicked");
	$(".unread").hide();
	$(".icon").bind('click',robot);
	$('.btns').bind('click', getNum);  
	$('#info-bar').bind('click',sum);  	
	$(".icon").click(robot);
}

function robot(event, callback){
	$('.icon').unbind('click', robot); 
	for (var i = 0; i < 5; i++) {
		$($(".btns")[i]).trigger('click');
	}
}
function getNum(event,callback){
	var current = $(this);
	if (!current.hasClass("disable") && !current.hasClass("clicked")) {
		clickedBtns++;
		current.addClass("clicked");
		current.siblings().addClass("disable");
		current.children(".unread").text("...").show();
        $.post('/', function (data) {
        	current.children(".unread").text(data);
        	total += parseInt(data);
        	current.addClass("disable");
        	if (clickedBtns < 5) {
        		$(current).siblings().removeClass("disable");
        		callback(robot);
        	}else{
        		$("#info-bar").trigger('click');
        	}
        });		
	}
}
function sum(){
	if (!$('#info-bar').hasClass("disable")) {
        $("#info-bar").text(total);
        $("#info-bar").addClass("disabled");
        $("#info-bar").css("background","grey");
        
	}
}
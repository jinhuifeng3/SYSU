var clickedBtns = 0;
var total = 0;

$(function(){
	$(".icon").on("mouseover",function(){
		clickedBtns = 0;
		total = 0;
		$("#info-bar").text("");
		$("#info-bar").addClass("disable");
		$(".btns").removeClass("disable");
		$("#info-bar").css("background","grey");
		$(".btns").removeClass("clicked");
		$(".unread").hide();

	});
	$(".btns").click(getNum);
	$("#info-bar").click(sum);
});


function getNum(){
	if (!$(this).hasClass("disable") && !$(this).hasClass("clicked")) {
		clickedBtns++;
		$(this).addClass("clicked");
		$(this).siblings().addClass("disable");
		$(this).children(".unread").text("...").show();
		var button = this;
        $.post('/', function (data) {
        	$(button).children(".unread").text(data);
        	total += parseInt(data);
        	$(button).addClass("disable");
        	if (clickedBtns < 5) {
        		$(button).siblings().removeClass("disable");
        		$(button).siblings().bind('click', getNum);
        	}else{
        		$("#info-bar").css("background","rgba(48, 63, 159, 1)");
        		$("#info-bar").removeClass("disable");
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
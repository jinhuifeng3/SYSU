var total = 0;
$(function(){
    $("#button").on("mouseenter", function () {
        $("#info").text("");
        $("#info-bar").addClass("disabled");
        $(".button").removeClass("disabled");
        $(".unread").hide();
        total = 0;
    });
    $(".button").click(getNum);
    $("#info-bar").click(sum)
});


function getNum(event, callback) {
    if (!$(this).hasClass("disabled") && $(this).children(".unread:hidden").length) {
        callback = arguments[1] ? arguments[1] : function(){
            if (!$(".button:not(.disabled),.button:has(.unread:hidden)").length) {
                $("#info-bar").removeClass("disabled");
            }
        };
        $(this).children(".unread").text("...").show();
        $(this).siblings().addClass("disabled");
        var current = this;
        $.get('/', function (data) {
            if (!$(current).children(".unread:hidden").length && !$(current).hasClass("disabled")) {
                total += parseInt(data);
                $(current).children(".unread").text(data);
                $(current).addClass("disabled");
                $(current).siblings(":has(.unread:hidden)").removeClass("disabled");
                callback.call(this);
            }
        });
    }
}

function sum(event, callback) {
    var bigBubble = $(this);
    if (!bigBubble.hasClass("disabled")) {
        $("#info").text(total);
        bigBubble.addClass("disabled");
    }
}


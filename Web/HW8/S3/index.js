var total = 0;

$(function(){
    $("#button").on("mouseenter", function () {
        $("#info").text("");
        $("#info-bar").addClass("disabled");
        $(".button").removeClass("disabled");
        $(".unread").hide();
    });
    $(".button").click(getNum);
    $("#info-bar").click(sum);
    $(".apb").click(function () {
        $("#button").trigger("mouseenter");
        var buttons = $(".button")
        for (var i = 0; i < 5; ++i) {
            $(buttons[i]).triggerHandler('click', function () {
                if (!$(".button:not(.disabled),.button:has(.unread:hidden)").length) {
                    $("#info-bar").removeClass("disabled");
                }
                $("#info-bar").trigger("click");
            });
        }
        $("#info-bar").trigger('click');
    })
});

function getNum(event, callback) {
    if (!$(this).hasClass("disabled") && $(this).children(".unread:hidden").length) {
        callback = arguments[1] ? arguments[1] : function(){
            if (!$(".button:not(.disabled),.button:has(.unread:hidden)").length) {
                $("#info-bar").removeClass("disabled");
            }
        };
        $(this).children(".unread").text("...").show();
        var current = this;
        $.get('/' + this.id, function (data) {
            if (!$(current).children(".unread:hidden").length && !$(current).hasClass("disabled")) {
                $(current).children(".unread").text(data);
                $(current).addClass("disabled");
                $(current).siblings(":has(.unread:hidden)").removeClass("disabled");
                callback.call(this);
            }
        });
    }
}

function sum(event, callback) {
    if (!$(this).hasClass("disabled")) {
        total = 0;
        $(".unread").each(function () {
            total += parseInt($(this).text());
        });
        $("#info").text(total);
        $(this).addClass("disabled");
    }
}



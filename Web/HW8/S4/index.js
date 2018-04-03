var total;

$(function(){
    $("#button").on("mouseenter", function () {
        $("#info").text("");
        $("#info-bar").addClass("disabled");        
        $(".button").removeClass("disabled");
        $("#order").text("");
        $(".unread").hide();
    });
    $(".button").click(getNum);
    $("#info-bar").click(sum);
    $(".apb").click(function () {
        $("#button").trigger("mouseenter");
        var buttons = getRandom($(".button"));
        var callback = [];
        var order = '';
        for (var i = 0; i < 5; i++) {
            order += buttons[i].id;
        }
        $("#order").text(order);
        for (var i = 0; i < 5; ++i) {
            (function (i) {
                callback[i] = function () {
                    $(buttons[i]).triggerHandler('click', function () {
                        if (!$(".button:not(.disabled),.button:has(.unread:hidden)").length) {
                            $("#info-bar").removeClass("disabled");
                        }
                        callback[i + 1]();
                    });
                };
            })(i);
        }
        callback[5] = function () {
            $("#info-bar").trigger('click');
        };
        callback[0]();
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
        $(this).siblings().addClass("disabled");
        var current = this;
        $.get('/', function (data) {
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

function getRandom(btns) {
    var order = [];
    while (1) {
        var ranNum = Math.floor(Math.random() * btns.length);
        order.push(btns[ranNum]);
        btns.splice(ranNum, 1);
        if (btns.length == 0)
            break; 
    }
    return order;
}

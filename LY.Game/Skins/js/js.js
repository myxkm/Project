$(function () {
    $(".header ul li i").mouseover(function () {
        $(this).prev(".en").css("display", "none")
        $(this).next(".ch").css("display", "inline")
    }).stop(true, true);
    $(".header ul li i").mouseout(function () {
        $(this).next(".ch").css("display", "none")
        $(this).prev(".en").css("display", "inline")
    }).stop(true, true);

    $(".change").mouseover(function () {
        $(this).children(".en").hide().stop(true, true);
        $(this).children(".ch").show().stop(true, true);
    })
    $(".change").mouseout(function () {
        $(this).children(".ch").hide().stop(true, true);
        $(this).children(".en").show().stop(true, true);
    })
    $(".change2").mouseover(function () {
        $(this).children(".en").hide().stop(true, true);
        $(this).children(".ch").show().stop(true, true);
    })
    $(".change2").mouseout(function () {
        $(this).children(".ch").hide().stop(true, true);
        $(this).children(".en").show().stop(true, true);
    })

    $(".space div.l").mouseover(function () {
        $(this).children(".en").css("display", "none").stop(true, true);
        $(this).children(".ch").css("display", "block").stop(true, true);
    })
    $(".space div.l").mouseout(function () {
        $(this).children(".ch").css("display", "none").stop(true, true);
        $(this).children(".en").css("display", "block").stop(true, true);
    })
    $(".style div.r").mouseover(function () {
        $(this).children(".en").css("display", "none").stop(true, true);
        $(this).children(".ch").css("display", "block").stop(true, true);
    })
    $(".style div.r").mouseout(function () {
        $(this).children(".ch").css("display", "none").stop(true, true);
        $(this).children(".en").css("display", "block").stop(true, true);
    })

    $(".trendout .play").click(function () {
        $(".videobox").fadeIn(1500);
    })
    $(".videobox .close").click(function () {
        $(".videobox").fadeOut(1500);
    })

    $(".send input").click(function () {
        $(".email").fadeIn(1000);
    });
    $(".email .close").click(function () {
        $(".email").fadeOut(1000);
    })

    //	$(function () {
    //  var ie6 = document.all;
    //  var dv = $('.headerbox2'), st;//获取要锁定的div的id
    //  dv.attr('otop', dv.offset().top); //存储原来的距离顶部的距离
    //  $(window).scroll(function () {
    //      st = Math.max(document.body.scrollTop || document.documentElement.scrollTop);
    //      if (st > parseInt(dv.attr('otop'))) {
    //          if (ie6) {//IE6不支持fixed属性，所以只能靠设置position为absolute和top实现此效果
    //              dv.css({ position: 'absolute', top: st ,'z-index':999 });
    //          }
    //          else if (dv.css('position') != 'fixed') dv.css({ 'position': 'fixed', top: 0, 'z-index':999 });
    //      } else if (dv.css('position') != 'static') dv.css({ 'position': 'static' });
    //  });
    //});
    //	jQuery(".slideBox.banner").slide({
    //		mainCell: ".bd ul",
    //		autoPlay: true,
    //		interTime:5000,
    //		delayTime:700
    //	});


    $(function () {
        $(".ys").click(function (e) {

        });

        $(".open").click(function () {
            var url = $(this).attr("href");
            layer.open({
                type: 2,
                title: false,
                closeBtn: 0,
                shadeClose: false,
                area: ['660px', '540px'],
                content: url
            });
            return false;
        })
        $(".wf_zzcrl span.close").click(function () {
            $(".wf_zhezhao").css("display", "none")
            $(".wf_zzcrt").css("z-index", "-1")
        })
        $(".none2").click(function () {
            $(".wf_zhezhao").css("display", "none")
            $(".wf_zzcrt").css("z-index", "-1")
        })
        $(".mil_4 input.buy").click(function () {
            $(".wf_zhezhao2").css("display", "block")
        })
        $("span.close3").click(function () {
            $(".wf_zhezhao2").css("display", "none")
        })
        $(".none3").click(function () {
            $(".wf_zhezhao2").css("display", "none")
        })
        $(".wf_zzcrr2 .add2 input.l").click(function () {
            $(".wf_zhezhao2").css("display", "none")
        })
        $(".wf_zzcrt .add3 input.l").click(function () {
            $(".wf_zhezhao").css("display", "none")
        })
        $(".mil_1 input").click(function () {
            $(".wf_zhezhao").css("display", "block");
            $(".wf_zzcrt").css("z-index", "2")
        })
        $("span.close4").click(function () {
            $(".wf_zhezhao").css("display", "none")
            $(".wf_zzcrt").css("z-index", "-1")
        })
    })
    $(".content .clearfix img").click(function () {
        $(this).parent(".clearfix").css("display", "none");
        $(this).parent().next(".content textarea").css("display", "block");
        $(this).parent().next(".content textarea").val($(this).prev("p").text());
    });
    $(".none").click(function () {
        $(this).parent('.wf_zzcl').find(".content .clearfix").css("display", "block");
        $(this).parent('.wf_zzcl').find(".content textarea").css("display", "none");
        $(this).parent('.wf_zzcl').find(".content p").text($(this).parent('.wf_zzcl').find(".content textarea").val())
    })
    $(".wf_zzcrl .add").click(function () {
        $(this).parent().next(".wf_zzcrr").animate({
            left: "0",
            opacity: "1"
        }, "500", "linear");
    })
    $(".wf_zzcrr .close2").click(function () {
        $(this).parent().animate({
            left: "375px",
            opacity: "0"
        }, "500", "linear");
    })
    $(".wf_zzcrr .add input.l").click(function () {
        $(this).parent().parent().animate({
            left: "375px",
            opacity: "0"
        }, "500", "linear");
    })
    $(".wf_zzcrl span.mils").click(function () {

        $(".mis").css("display", "block")

        setTimeout(function () {
            $(".mis").css("display", "none")
        }, 1000)

    });
    $(".wf_zzcrr .add input.r").click(function () {

        $(".mis").css("display", "block")

        setTimeout(function () {
            $(".mis").css("display", "none")
        }, 1000)
        $(this).parent().parent().delay(1000).animate({
            left: "375px",
            opacity: "0"
        }, "500", "linear")
    });

    //购买

    $(".rr2_4 span").click(function () {

        $(".rr2_5 p").children("em").removeClass("on");

        if ($(this).is(".on")) {
            $(this).removeClass("on");
        } else {
            $(".rr2_4 span").removeClass('on');
            $(this).addClass('on');
        }
    });

    $(".rr2_5 p").click(function () {
        if ($(".rr2_5 p").children("em").is(".on")) {
            $(".rr2_5 p").children("em").removeClass("on")

        } else {
            $(".rr2_5 p").children("em").addClass("on")
            $(".rr2_4 span").removeClass('on');
        }
    });

    $(".wf_zzcrr2 .add2 input.r").click(function () {
        if ($(".rr2_5 p").children("em").is(".on") || $(".rr2_4 span").is('.on')) {
            $(".mis4").css("display", "block");

            setTimeout(function () {
                $(".mis4").css("display", "none")
                $(".wf_zhezhao2").css("display", "none")
            }, 1000)
        }

    })
    var txtop = $(".txnavbox").css("top", "130px");
    window.onscroll = function () {
        var top = document.body.scrollTop;
        if (top >= 500) {
            $("span.backtop").css("display", "block");
        } else {
            $("span.backtop").css("display", "none");
        }
        if (top >= 44) {
            $(".headerbox").addClass("headerbox-fix");
        } else {
            $(".headerbox").removeClass("headerbox-fix");
        }
        if (top >= 44) {
            $(".headerbox2 .header").hide();
        } else {
            $(".headerbox2 .header").show();
        }
        //		if (top >= 200) {
        //			$(".colorselect").addClass("colorselect2")
        //		} else {
        //			$(".colorselect").removeClass("colorselect2")
        //		}
        if (top >= 50) {
            $(".shaixuantopbox").addClass("shaixuantopbox2");
        } else {
            $(".shaixuantopbox").removeClass("shaixuantopbox2");
        }
        $(function () {
            var h4 = $(".headerbox").height();

            if (top <= 280) {
                $(".txnavbox").removeClass("txnavbox-fix").css("bottom", "0");
                $(".txnavbox").css("top", '130px');
            } else {
                $(".txnavbox").addClass("txnavbox-fix").css("top", h4);
            }
            if (top <= 280) {
                $(".txnavbox2").removeClass("txnavbox-fix").css("bottom", "0");
                $(".txnavbox2").css("top", '130px');
            } else {
                $(".txnavbox").addClass("txnavbox-fix").css("top", h4);
                //$(".txnavbox2").addClass("txnavbox-fix").css("top", 0);
            }

        })

    }

    $("span.backtop").click(function () {
        $("body").animate({
            scrollTop: "0"
        })
    })

    $(".txl").click(function () {
        var tx = $(this).index()
        var tsrc = $('#touxiang').attr("src");
        var lsrc = $(this).attr("src");
        $('#touxiang').attr("src", lsrc);
    })
    $(".tx").click(function () {
        $(".xgtx").css("display", "table")
    })
    $("span.close5").click(function () {
        $(".xgtx").css("display", "none")
    })
    $(".txbg").click(function () {
        $(".xgtx").css("display", "none")
    })
    $(".xgtxbtn .l span.r").click(function () {
        $(".xgtx").css("display", "none")
    })
    //	$(".wf_zzcrr2 .add2 input.r").click(function() {
    //
    //			$(".mis3").css("display","block")
    //
    //		setTimeout(function(){$(".mis3").css("display","none")},1000)
    //	})

})

$(function () {
    var mousestate = false, imgSrc = '', elems = null, dataImage = '';
    $(".mb_box .mb-img").click(function ($event) {
        if (mousestate) {
            if (($(this).css('backgroundImage') == imgSrc)) {
                mousestate = true;
            } else {
                elems.css('backgroundImage', $(this).css('backgroundImage'));
                elems.attr("data-img", $(this).attr('data-img'))
                $(this).css('backgroundImage', imgSrc);
                $(this).attr('data-img', dataImage);
                mousestate = false;
            }
        } else {
            elems = $($event.target);
            sessionStorage.setItem('isSelf', $(this));
            imgSrc = $(this).css("backgroundImage");
            dataImage = $(this).attr("data-img");
            mousestate = !mousestate;
        }
    });
});

$(function () {
    var color = "";
    //	$(".fills").click(function (){
    //		var color = $(this).css("backgroundColor");
    //		console.log(color);
    //		$(".xsq2 div").css("backgroundColor",color)
    //	})
    $(".fills").click(function () {
        //		sessionStorage.setItem('isSelf', $(this));
        color = $(this).css("backgroundColor");

    })
    $(".xsq2 div").click(function () {
        if (color != "") {
            $(this).css('backgroundColor', color);
            color = "";

        }

    })

})

$(function () {


    $(".bainijnn").click(function () {
        var h1 = $(".daochuc .rpt p").css("height");
        var h2 = $(".daochuc .rpt textarea").css("height");
        $(".bianji.clearfix").prev(".rpt").children("p").css("display", "none");
        $("#form1").children("textarea").css("display", "block");
        $("#form1").children("textarea").css("height", h1)
        //$("#form1").children("textarea").val($(".bianji.clearfix").prev(".rpt").children("p").text());

    });


    $(".daochuc .r span.l").click(function () {
        var h1 = $(".daochuc .rpt p").css("height");
        var h2 = $(".daochuc .rpt textarea").css("height");
        $(this).parent(".clearfix").prev(".rpt").children("p").css("display", "none");


        $("#form1").children("textarea").css("display", "block");
        $("#form1").children("textarea").css("height", h1)
        //$("#form1").children("textarea").val($(this).parent(".clearfix").prev(".rpt").children("p").text());

    });

    $(".daochuc .r span.r").click(function () {
        $(this).parent(".clearfix").prev(".rpt").children("textarea").css("display", "none");
        $(this).parent(".clearfix").prev(".rpt").children("p").css("display", "block");
        $(this).parent(".clearfix").prev(".rpt").children("p").text($(this).parent(".clearfix").prev(".rpt").children("textarea").val())
        var h3 = $(".daochuc .rpt p").css("height")
        $(this).parent(".clearfix").prev(".rpt").children("textarea").css("height", h3)
    })

    var h4 = $(".daochuc .rpt p").css("height")
    if (h4 >= 530) {
        $(".daochuc .rpt p").css("overflow-y", "scroll");
    } else {
        $(".daochuc .rpt p").css("overflow-y", "auto");
    }

})

$(function () {

    $(".shaixuanmenu").click(function () {
        $(this).next(".sxlist").animate({
            left: "0"
        }, 500);
    })
    $(".sxlist .close").click(function () {
        $(this).parents(".sxlist").animate({
            left: "-314px"
        }, 500);
        $(".sxlist-cont").slideUp();
    })
    $(".sxlist-title").click(function () {
        if ($(this).is(".on")) {

            $(this).removeClass("on");

            $(this).next(".sxlist-cont").slideUp();
            $(".sx-color-nav.clearfix li").children(".sx-selector-color-nav").stop(true, true).removeClass("on");
            $(".sx-small-color-nav-list li .sx-small-color-nav").stop(true, true).slideUp()
        } else {
            $(this).parent().parent().children().children(".sxlist-title").removeClass("on");

            $(this).addClass("on");
            $(".sxlist-cont").slideUp();
            $(this).next(".sxlist-cont").slideDown();
        }
    })
    //	$(".sxlist-title").click(function() {
    //		if ($(this).is(".on")) {
    //			$(this).removeClass("on");
    //			$(this).next(".sxlist-cont").slideUp();
    //		} else {
    //			$(this).addClass("on");
    //			$(".sxlist-cont").slideUp();
    //			$(this).next(".sxlist-cont").slideDown();
    //		}
    //	})
    $(".sx-color-nav.clearfix li").click(function () {
        var i = $(this).index()
        if ($(this).children(".sx-selector-color-nav").is(".on")) {
            $(this).children(".sx-selector-color-nav").stop(true, true).removeClass("on");
            $(".sx-small-color-nav-list li .sx-small-color-nav").stop(true, true).eq(i).slideUp()
        } else {
            $(".sx-selector-color-nav").removeClass("on");
            $(this).children(".sx-selector-color-nav").stop(true, true).addClass("on");
            $(".sx-small-color-nav").stop(true, true).slideUp();
            $(".sx-small-color-nav-list li .sx-small-color-nav").eq(i).stop(true, true).slideDown()
        }
    })
    $(".sx-nav-box p").click(function () {
        if ($(this).is(".on")) {
            $(this).removeClass("on");
            $(this).next(".sx-nav2").slideUp();
        } else {
            $(this).addClass("on");
            $(".sx-nav2").slideUp();
            $(this).next(".sx-nav2").slideDown();
        }
    })
    $(".sx-nav li").click(function () {
        if ($(this).is(".on")) {
            $(this).removeClass("on");
        } else {
            //			$(this).parent(".sx-nav").children(" li").removeClass("on")
            $(this).addClass("on");
        }
    })
    $(".sx-nav2 li").click(function () {
        if ($(this).is(".on")) {
            $(this).removeClass("on");
        } else {
            //			$(this).parent(".sx-nav2").children(" li").removeClass("on")
            $(this).addClass("on");
        }
    })

    //window.onload = function(){
    //	var mcImgH = $(".mc img.w5").height()
    //	$(".mc div.w5.table").height(mcImgH)
    //}

    window.onload = function () {
        var mcTh = $(".mc img.w5").css("height");
        console.log(mcTh)
        $(".mc .clearfix .table").css("height", mcTh);
    }

})

//window.onscroll = function() {
//	var top = document.body.scrollTop;
//	console.log(top)
//	if (top >= 200) {
//		$(".colorselect").addClass("colorselect2")
//	} else {
//		$(".colorselect").removeClass("colorselect2")
//	}
//	if (top >= 500) {
//		$(".backtop").css("display", "block");
//	} else {
//		$(".backtop").css("display", "none");
//	};
//}
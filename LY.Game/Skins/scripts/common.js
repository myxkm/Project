/* 
*作者：一些事情
*时间：2015-4-17
*需要结合jquery和Validform和artdialog一起使用
----------------------------------------------------------*/

//写Cookie
function addCookie(objName, objValue, objHours) {
    var str = objName + "=" + escape(objValue);
    if (objHours > 0) {//为0时不设定过期时间，浏览器关闭时cookie自动消失
        var date = new Date();
        var ms = objHours * 3600 * 1000;
        date.setTime(date.getTime() + ms);
        str += "; expires=" + date.toGMTString();
    }
    document.cookie = str;
}

//读Cookie
function getCookie(objName) {//获取指定名称的cookie的值
    var arrStr = document.cookie.split("; ");
    for (var i = 0; i < arrStr.length; i++) {
        var temp = arrStr[i].split("=");
        if (temp[0] == objName) return unescape(temp[1]);
    }
    return "";
}
//四舍五入函数
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}
//只允许输入数字
function checkNumber(e) {
    var keynum = window.event ? e.keyCode : e.which;
    if ((48 <= keynum && keynum <= 57) ||(96 <= keynum && keynum <= 105)|| keynum == 8) {
        return true;
    } else {
        return false;
    }
}
//只允许输入小数
function checkForFloat(obj, e) {
    var isOK = false;
    var key = window.event ? e.keyCode : e.which;
    if ((key > 95 && key < 106) || //小键盘上的0到9  
        (key > 47 && key < 60) ||  //大键盘上的0到9  
        (key == 110 && obj.value.indexOf(".") < 0) || //小键盘上的.而且以前没有输入.  
        (key == 190 && obj.value.indexOf(".") < 0) || //大键盘上的.而且以前没有输入.  
         key == 8 || key == 9 || key == 46 || key == 37 || key == 39) {
        isOK = true;
    } else {
        if (window.event) { //IE
            e.returnValue = false;   //event.returnValue=false 效果相同.    
        } else { //Firefox 
            e.preventDefault();
        }
    }
    return isOK;
}
//复制文本
function copyText(txt) {
    window.clipboardData.setData("Text", txt);
    var d = dialog({ content: '复制成功，可以通过粘贴来发送！' }).show();
    setTimeout(function () {
        d.close().remove();
    }, 2000);
}
//全选取消按钮函数，调用样式如：
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).text("取消");
        $(".checkall").prop("checked", true);
    } else {
        $(chkobj).text("全选");
        $(".checkall").prop("checked", false);
    }
}
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).text("取消");
        $(".checkall").prop("checked", true);
    } else {
        $(chkobj).text("全选");
        $(".checkall").prop("checked", false);
    }
}
//Tab控制选项卡
function tabs(tabObj, event) {
    //绑定事件
    var tabItem = $(tabObj).find(".tab-head ul li a");
    tabItem.bind(event, function () {
        //设置点击后的切换样式
        tabItem.removeClass("selected");
        $(this).addClass("selected");
        //设置点击后的切换内容
        var tabNum = tabItem.parent().index($(this).parent());
        $(tabObj).find(".tab-content").hide();
        $(tabObj).find(".tab-content").eq(tabNum).show();
    });
}

//显示浮动窗口
function showWindow(obj) {
    var tit = $(obj).attr("title");
    var box = $(obj).html();
    dialog({
        width: 500,
        title: tit,
        content: box,
        okValue: '确定',
        ok: function () { }
    }).showModal();
}
function showDilogTime(msg,time) {
    var d = dialog({ content: msg }).show();
    setTimeout(function () {
        d.close().remove();
    }, time);
}
/*页面级通用方法
------------------------------------------------*/
//智能浮动层函数
$.fn.smartFloat = function () {
    var position = function (element) {
        var top = element.position().top, pos = element.css("position");
        var w = element.innerWidth();
        $(window).scroll(function () {
            var scrolls = $(this).scrollTop();
            if (scrolls > top) {
                if (window.XMLHttpRequest) {
                    element.css({
                        width: w,
                        position: "fixed",
                        top: 55
                    });
                } else {
                    element.css({
                        top: scrolls
                    });
                }
            } else {
                element.css({
                    position: pos,
                    top: top
                });
            }
        });
    };
    return $(this).each(function () {
        position($(this));
    });
};

//执行删除操作
function ExecDelete(sendUrl, checkValue, urlObj) {
    //检查传输的值
    if (!checkValue) {
        dialog({ title: '提示', content: '对不起，没有选择项！', okValue: '确定', ok: function () { } }).showModal();
        return false;
    }
    dialog({
        title: '提示',
        content: '删除记录后不可恢复，您确定吗？',
        okValue: '确定',
        ok: function () {
            $.ajax({
                type: "POST",
                url: sendUrl,
                dataType: "json",
                data: {
                    "Id": checkValue
                },
                timeout: 20000,
                success: function (data, textStatus) {
                    if (data.Success) {
                            if (urlObj!="") {
                                location.href = urlObj;
                            } else {
                                window.location.href = window.location.href;
                            }
                    } else {
                        dialog({ title: '提示', content: '操作出现错误', okValue: '确定', ok: function () { } }).showModal();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    dialog({ title: '提示', content: '操作出现错误', okValue: '确定', ok: function () { } }).showModal();
                }
            });
        },
        cancelValue: '取消',
        cancel: function () { }
    }).showModal();
}
//执行删除操作
function ExecCopys(sendUrl, data, urlObj) {
    //检查传输的值
    //if (!data) {
    //    dialog({ title: '提示', content: '对不起，没有选择项！', okValue: '确定', ok: function () { } }).showModal();
    //    return false;
    //}
    dialog({
        title: '提示',
        content: '您确定执行此操作？',
        okValue: '确定',
        ok: function () {
            $.ajax({
                type: "POST",
                url: sendUrl,
                dataType: "json",
                data: data,
                timeout: 20000,
                success: function (data, textStatus) {
                    if (data.Success) {
                        if (urlObj != "") {
                            location.href = urlObj;
                        } else {
                            window.location.href = window.location.href;
                        }
                    } else {
                        dialog({ title: '提示', content: '操作出现错误', okValue: '确定', ok: function () { } }).showModal();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    dialog({ title: '提示', content: '操作出现错误', okValue: '确定', ok: function () { } }).showModal();
                }
            });
        },
        cancelValue: '取消',
        cancel: function () { }
    }).showModal();
}
//单击执行AJAX请求操作
function ExecPost(sendUrl,data,callback) {
    $.ajax({
        type: "POST",
        url: sendUrl,
        data:data,
        dataType: "json",
        timeout: 20000,
        success: function (data, textStatus) {
            if (data.Success) {
                callback();
            } else {
                dialog({ title: '提示', content: "操作出现错误", okValue: '确定', ok: function () { } }).showModal();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialog({ title: '提示', content: "操作出现错误", okValue: '确定', ok: function () { } }).showModal();
        }
    });
}

/*表单AJAX提交封装(包含验证)
------------------------------------------------*/
function InitForm(formObj, callback) {
    $(formObj).Validform({
        tiptype: 3,
        //ignoreHidden: true,
        showAllError: true,
        callback: callback
    });
}
function post(data, formObj, btnObj, isDialog, urlObj, callback, dateType) {
    var argNum = arguments.length; //参数个数
    $.ajax({
        type: "POST",
        url: $(formObj).attr("url"),
        data: data,
        dataType: dateType,
        timeout: 20000,
        beforeSend: function () {
            $(btnObj).prop("disabled", true);
            $(btnObj).val("提交中...");
        },
        success: function (data, textStatus) {
            if (data.Success) {
                $(btnObj).val("提交成功");
                //是否提示，默认不提示
                if (isDialog == 1) {
                    var d = dialog({ content: data.Message }).show();
                    setTimeout(function () {
                        d.close().remove();
                        if (argNum == 6) {
                            callback();
                        } else if (typeof (urlObj) != "undefined" && urlObj != "") {
                            location.href = urlObj;
                        } else {
                            window.location.href = location.href;
                        }
                    }, 2000);
                } else {
                    if (argNum == 6) {
                        callback();
                    } else if (typeof (urlObj) != "undefined" && urlObj != "") {
                        location.href = urlObj;
                    } else {
                        window.location.href = location.href;
                    }
                }
            } else {
                dialog({ title: '提示', content: "数据错误，提交失败", okValue: '确定', ok: function () { } }).showModal();
                $(btnObj).prop("disabled", false);
                $(btnObj).val("提交");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialog({ title: '提示', content: "数据错误，提交失败", okValue: '确定', ok: function () { } }).showModal();
            $(btnObj).prop("disabled", false);
            $(btnObj).val("提交");
        }
    });
}
function AjaxInitForm(formObj, btnObj, isDialog, urlObj, callback) {
    var argNum = arguments.length; //参数个数
    $(formObj).Validform({
        tiptype: 3,
        showAllError: true,
        callback: function (form) {
            //AJAX提交表单
            $(form).ajaxSubmit({
                beforeSubmit: formRequest,
                success: formResponse,
                error: formError,
                url: $(formObj).attr("url"),
                type: "post",
                dataType: "json",
                timeout: 60000
            });
            return false;
        }
    });

    //表单提交前
    function formRequest(formData, jqForm, options) {
        $(btnObj).prop("disabled", true);
        $(btnObj).val("提交中...");
    }

    //表单提交后
    function formResponse(data, textStatus) {
        if (data.status == 1) {
            $(btnObj).val("提交成功");
            //是否提示，默认不提示
            if (isDialog == 1) {
                var d = dialog({ content: data.msg }).show();
                setTimeout(function () {
                    d.close().remove();
                    if (argNum == 5) {
                        callback();
                    } else if (data.url) {
                        location.href = data.url;
                    } else if ($(urlObj).length > 0 && $(urlObj).val() != "") {
                        location.href = $(urlObj).val();
                    } else {
                        location.reload();
                    }
                }, 2000);
            } else {
                if (argNum == 5) {
                    callback();
                } else if (data.url) {
                    location.href = data.url;
                } else if ($(urlObj)) {
                    location.href = $(urlObj).val();
                } else {
                    location.reload();
                }
            }
        } else {
            dialog({ title: '提示', content: data.msg, okValue: '确定', ok: function () { } }).showModal();
            $(btnObj).prop("disabled", false);
            $(btnObj).val("再次提交");
        }
    }
    //表单提交出错
    function formError(XMLHttpRequest, textStatus, errorThrown) {
        dialog({ title: '提示', content: '状态：' + textStatus + '；出错提示：' + errorThrown, okValue: '确定', ok: function () { } }).showModal();
        $(btnObj).prop("disabled", false);
        $(btnObj).val("再次提交");
    }
}


//初始化视频播放器需配合ckplayer.js使用
function initCKPlayer(boxId, videoSrc, playerSrc) {
    var flashvars = {
        f: videoSrc,
        c: 0,
        loaded: 'loadedHandler'
    };
    var video = [videoSrc];
    CKobject.embed(playerSrc, boxId, 'video_v1', '100%', '100%', false, flashvars, video);
}
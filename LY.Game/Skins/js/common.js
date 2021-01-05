//layer = parent.layer || layer;
Date.prototype.format = function (format) {
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss";
    }
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()
        // millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

function checkNumber(e) {
    if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {  //FF 
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || (e.which == 8) || (e.which == 46)))
            return false;
    } else {
        if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 8) || (event.keyCode == 46)))
            event.returnValue = false;
    }
}
function changePageSize(e) {

    var p = $("#pagesize"), size = p.val(), pageUrl = window.location.href;
    if (/pageSize=(\d+)/.test(pageUrl)) {
        pageUrl = pageUrl.replace(/pageSize=(\d+)/g, "pageSize=" + size);
    } else {
        pageUrl = (pageUrl + (pageUrl.indexOf("?") > 0 ? "&" : "?") + "pageSize=" + size)
    }
    if (e) {
        alert(event.keyCode);
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {  //FF 
            if (!((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || (e.which == 8) || (e.which == 46)))
                return false;
        } else {
            if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 8) || (event.keyCode == 46)))
                event.returnValue = false;
        }
    } else {
        window.location = pageUrl;
    }

}
function post(url, data, callback, type) {

    var loadingIndex, layer = parent.layer || layer;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: type,
        beforeSend: function () {
            loadingIndex = layer.load(0, { shade: 0.1 });
        },
        success: function (data, textStatus) {
            layer.close(loadingIndex);
            if (callback) {
                callback(data, textStatus);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.close(loadingIndex);
            alert.error(XMLHttpRequest)
            //var api = frameElement.api;
            //var W = getDialogOwner();
            //W.dialog({ title: "程序错误", content: XMLHttpRequest.responseText, lock: true, parent: api || null });
        }
    });
}
function load(url, data, callback, type) {

    var loadingIndex, layer = parent.layer || layer;
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        dataType: type,
        beforeSend: function () {
            loadingIndex = layer.load(0, { shade: 0.1 });
        },
        success: function (data, textStatus) {
            layer.close(loadingIndex);
            if (callback) {
                callback(data, textStatus);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.close(loadingIndex);
            alert.error(XMLHttpRequest)
            //var api = frameElement.api;
            //var W = getDialogOwner();
            //W.dialog({ title: "程序错误", content: XMLHttpRequest.responseText, lock: true, parent: api || null });
        }
    });
}

window.layerAlert = function (msg, title) {
    var layer = parent.layer || layer;
    var index = layer.open({
        content: msg,
        shift: -1
    });
}
window.alert.success = function (msg, url_callback) {
    var layer = parent.layer || layer, index = layer.alert(msg, { shift: -1, icon: 1 }, function () {
 
        layer.close(index);
 
        if (url_callback) {
            switch (typeof url_callback) {
                //如果是String类型、直接跳转到该链接
                case "string":
                    window.location = url_callback;
                    break;
                    //回调函数
                case "function":
                    url_callback();
                    break;
                default: 
                    break;
            }
        }
    });
}
window.alert.failed = function (msg, callback) {
    var layer = parent.layer || layer, index = layer.alert(msg, { shift: -1, icon: 2 }, function () {
        layer.close(index);
        if (callback) callback();
    });
}
window.alert.error = function (response) {
    var layer = parent.layer || layer, index = layer.alert(response.responseText, { shift: -1, title: "程序错误 - " + response.status + "(" + response.statusText + ")", icon: 5 });
}
window.confirm2 = function (msg, callback, cancel) {
    var layer = parent.layer || layer;
    layer.confirm(msg || "确定操作？", { shift: -1, icon: 3 }, function (index) {
        layer.close(index);
        callback();
    }, cancel);
}

window.open2 = function (url, title, size) {
    var layer = parent.layer || layer, index = layer.open({
        type: 2,
        title: title,
        shadeClose: true,
        shift: -1,
        shade: 0.8,
        area: size || ['80%', '80%'],
        content: url //iframe的url
    });
}
window.openFrame = function (url, title, size, yesFunction) {
    url = url.indexOf("?") > -1 ? url + "&type=1" : url + "?type=1";
    var layer = parent.layer || layer, index = layer.open({
        type: 2,
        title: title,
        shadeClose: true,
        shift: -1,
        area: size || ['80%', '80%'],
        shade: 0.8,
        maxmin: true,
        content: url //iframe的url
        , btn: ['确定', '取消']
         , yes: function (index, layero) {
             var iframeWin = parent.window[layero.find('iframe')[0]['name']],
             btn = $(iframeWin.document).find("button[type='submit']")[0];
             if (btn) $(iframeWin.document).find("button[type='submit']")[0].click();
             else layer.close(index);
             
         }, cancel: function (index) {
         }
    });
}

$(function () {
    $(".dataTable thead th[class*=sort]").click(function () {
        var t = $(this), thisClass = t.attr("class"), desc = thisClass.indexOf("sorting_desc") > -1;
        if (desc) t.removeClass("sorting_desc");
        else { t.removeClass("sorting_asc"); }
        t.addClass(desc ? "sorting_asc" : "sorting_desc");
        t.siblings("th[class*=sort]").removeClass("sorting_asc").removeClass("sorting_desc").addClass("sorting");

        $("#sort").val(t.attr("sort") + (desc ? "_asc" : "_desc"));
        $("#search-form").submit();
    });
});
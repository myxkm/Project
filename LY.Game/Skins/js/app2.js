window.addEventListener('DOMContentLoaded', function (e) {

    var waterfall = new Waterfall({
        minBoxWidth: 250
    });

    // button click handle
    var btn = document.getElementById('add-btn');
    var boxHandle = newNode();
    window.onscroll = function () {
        var i = waterfall.getHighestIndex();
        if (i > -1) {
            // get last box of the column
            var lastBox = Array.prototype.slice.call(waterfall.columns[i].children, -1)[0];
            if (checkSlide(lastBox)) {
                $.get("/pictures/waterfall", { categoryid: 1, pageIndex: 1, pageSize: 5 }, function (response) {
                    for (var item in response.data) {
                        waterfall.addBox(createFromJson(item));
                    }
                });
                ////ajax
                //var count = 5;
                //while (count--) waterfall.addBox(boxHandle());
            }
        }
        var top = document.body.scrollTop;
        //	{
        //			$(".shaixuantopbox h2").animate({top:"5px"})
        //		} 
        //		if(top < 25){
        //			$(".shaixuantopbox h2").animate({top:"-44px"})		
        //		};
        //		if (top >= 25) {
        //			$(".shaixuantopbox2 h2").animate({
        //				top: "5px"
        //			},500).stop(false);
        //		} else {
        //			$(".shaixuantopbox2 h2").animate({
        //				top: "-44px"
        //			},500).stop(false);
        //		};
        if (top >= 500) {
            $(".backtop").css("display", "block")

        } else {
            $(".backtop").css("display", "none")

        };
        if (top >= 50) {
            $(".shaixuantopbox").addClass("shaixuantopbox2");
            $(".wf_zhezhao").css("margin-top", "-6px");
            $(".wf-container").css("margin-top", "30px")

        } else {
            $(".shaixuantopbox").removeClass("shaixuantopbox2");
            $(".wf_zhezhao").css("margin-top", "0px");
            $(".wf-container").css("margin-top", "0");

        }

    };

    function checkSlide(elem) {
        if (elem) {
            var screenHeight = (document.documentElement.scrollTop || document.body.scrollTop) +
				(document.documentElement.clientHeight || document.body.clientHeight);
            var elemHeight = elem.offsetTop + elem.offsetHeight / 2;

            return elemHeight < screenHeight;
        }
    }

    function newNode() {

        var color = ['1.png', '2.png', '3.png', '4.png', '5.png', '6.png', '7.png', '8.png', '9.png', '10.png', '11.png', '12.png'];
        var i = 0;

        return function () {

            var box = document.createElement('div');
            box.className = 'wf-box';

            var wfbox = document.createElement('div');
            wfbox.className = 'img-box'
            box.appendChild(wfbox);

            var image = document.createElement('img');
            image.src = 'img/' + color[i];
            wfbox.appendChild(image);

            var zhezhao = document.createElement('div');
            zhezhao.className = 'zhezhao'
            wfbox.appendChild(zhezhao);

            var zspan = document.createElement('span');
            zspan.className = 'open'
            $("div.zhezhao span.open").click(function () {
                $(".wf_zhezhao").css("display", "block")
                $("body").css("overflow", "hidden")
            })
            zhezhao.appendChild(zspan);

            var za = document.createElement('a');
            za.setAttribute('href', "#")
            zhezhao.appendChild(za);

            var zp = document.createElement('p');
            za.appendChild(zp);

            var zem = document.createElement('em');
            zp.appendChild(zem);
            zp.appendChild(document.createTextNode('来自品牌图片集'));

            var content = document.createElement('div');
            content.className = 'content';

            var title = document.createElement('h3');
            title.appendChild(document.createTextNode('12'));
            content.appendChild(title);

            var p = document.createElement('p');
            p.appendChild(document.createTextNode('50 Flawless Examples of Indus...'));

            content.appendChild(p);

            box.appendChild(content);

            if (++i === color.length) i = 0;

            return box;

        };
    }

    function createFromJson(data) {

        var box = document.createElement('div');
        box.className = 'wf-box';

        var wfbox = document.createElement('div');
        wfbox.className = 'img-box'
        box.appendChild(wfbox);

        var image = document.createElement('img');
        image.src = data.Preview;
        wfbox.appendChild(image);

        var zhezhao = document.createElement('div');
        zhezhao.className = 'zhezhao'
        wfbox.appendChild(zhezhao);

        var zspan = document.createElement('span');
        zspan.className = 'open'
        $("div.zhezhao span.open").click(function () {
            $(".wf_zhezhao").css("display", "block")
            $("body").css("overflow", "hidden")
        })
        zhezhao.appendChild(zspan);

        var za = document.createElement('a');
        za.setAttribute('href', "#")
        zhezhao.appendChild(za);

        var zp = document.createElement('p');
        za.appendChild(zp);

        var zem = document.createElement('em');
        zp.appendChild(zem);

        if (data.Album.IsPublic)
            zp.appendChild(document.createTextNode('来自品牌图片集'));

        var content = document.createElement('div');
        content.className = 'content';

        var title = document.createElement('h3');
        title.appendChild(document.createTextNode(data.Price));
        content.appendChild(title);

        var p = document.createElement('p');
        p.appendChild(document.createTextNode('50 Flawless Examples of Indus...'));

        content.appendChild(p);

        box.appendChild(content);


        return box;
    }
});
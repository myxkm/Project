//window.onload = function () { 
    waterfall('main','pin');
 
 
    var dataInt={'data':[{'src':'1.png'},{'src':'2.png'},{'src':'3.png'},{'src':'4.png'}]};
    
    main.onscroll=function(){
        if(checkscrollside()){
            var oParent = document.getElementById('main');// 父级对象
            for(var i=0;i<dataInt.data.length;i++){
                var oPin=document.createElement('div'); //添加 元素节点
                oPin.className='pin';                   //添加 类名 name属性
                oParent.appendChild(oPin);              //添加 子节点
                var oImg=document.createElement('img');
                oImg.src='./img/'+dataInt.data[i].src;
                oPin.appendChild(oImg);
            }
            waterfall('main','pin');
        };
    }
}
 
//    var dataInt = { 'data': [{ 'src': '1.png' }, { 'src': '2.png' }, { 'src': '3.png' }, { 'src': '4.png' }] };
 

 
/*
    parend 父级id
    pin 元素id
*/
function waterfall(parent,pin){
    var oParent=document.getElementById(parent);// 父级对象
    var aPin=getClassObj(oParent,pin);// 获取存储块框pin的数组aPin
    var iPinW=aPin[0].offsetWidth;// 一个块框pin的宽
    var mianW = 336;
    var num2=Math.floor(mianW/iPinW);//每行中能容纳的pin个数【窗口宽度除以一个块框宽度】
 
//    main.onscroll = function () {
//        if (checkscrollside()) {

//            var oParent = document.getElementById('main');// 父级对象
//            for (var i = 0; i < dataInt.data.length; i++) {
//                var oPin = document.createElement('div'); //添加 元素节点
//                oPin.className = 'pin';                   //添加 类名 name属性
//                oParent.appendChild(oPin);              //添加 子节点
//                var oImg = document.createElement('img');
//                oImg.src = './img/' + dataInt.data[i].src;
//                oPin.appendChild(oImg);
//                conlose.log(dataInt.data.length)
//            }
//            waterfall('main', 'pin');

//        };
//    }
//}

/*
    parend 父级id
    pin 元素id
*/
    function waterfall(parent,pin){
        var oParent=document.getElementById(parent);// 父级对象
        var aPin=getClassObj(oParent,pin);// 获取存储块框pin的数组aPin
        var iPinW=aPin[0].offsetWidth;// 一个块框pin的宽
        var mianW = 336;
        var num2=Math.floor(mianW/iPinW);//每行中能容纳的pin个数【窗口宽度除以一个块框宽度】
    }



 
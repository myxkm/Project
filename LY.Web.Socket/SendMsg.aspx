<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMsg.aspx.cs" Inherits="LY.Web.Socket.SendMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <input id="user" type="text" value="u_" />
    <input id="conn" type="button" value="连接" />
    <input id="close" type="button" value="关闭" /><br />
    <span id="tips"></span>
    <input id="content" type="text" value="SendMsg 有人闯入了" />
    <input id="send" type="button" value="发送" /><br />
    <input id="to" type="text" />目的用户
    <div id="msg">
    </div>
    <script>
        var ws;
        $().ready(function () {
            ws = new WebSocket('ws://' + window.location.hostname + ':' + window.location.port + '/WebSocketDemos.ashx?user=' + $("#user").val() + Math.random());
            $('#msg').append('<p>正在连接</p>');
            console.log(ws)
            ws.onopen = function () {
                $('#msg').append('<p>已经连接</p>');

                if (ws.readyState == WebSocket.OPEN) {
                    ws.send("default|" + $('#content').val());
                }
                else {
                    $('#tips').text('连接已经关闭');
                }
            }
            ws.onmessage = function (evt) {
                $('#msg').append('<p>onmessage ' + evt.data + '</p>');
            }
            ws.onerror = function (evt) {
                $('#msg').append('<p>onerror ' + JSON.stringify(evt) + '</p>');
            }
            ws.onclose = function () {
                $('#msg').append('<p>已经关闭</p>');
            }






            $('#close').click(function () {
                ws.close();
            });



        });
    </script>
</asp:Content>

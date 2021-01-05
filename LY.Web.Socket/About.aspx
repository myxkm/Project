<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="LY.Web.Socket.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>


        <input id="user" type="text" />
        <input id="conn" type="button" value="连接" />
        <input id="close" type="button"  value="关闭"/><br />

        <span id="tips"></span>
        <input id="content" type="text" />
        <input id="send" type="button"  value="发送"/><br />
        <input id="to" type="text" />目的用户
        <div id="msg">
        </div>
    

    <script>
        var ws;
        $().ready(function () {
            $('#conn').click(function () {
                ws = new WebSocket('ws://' + window.location.hostname + ':' + window.location.port + '/WebSocketDemos.ashx?user=' + $("#user").val());
                $('#msg').append('<p>正在连接</p>');
                console.log(ws)
                ws.onopen = function () {
                    $('#msg').append('<p>已经连接</p>');
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
            });
            $('#close').click(function () {
                ws.close();
            });
            $('#send').click(function () {
                if (ws.readyState == WebSocket.OPEN) {
                    ws.send($("#to").val() + "|" + $('#content').val());
                }
                else {
                    $('#tips').text('连接已经关闭');
                }
            });

        });
    </script>
</asp:Content>

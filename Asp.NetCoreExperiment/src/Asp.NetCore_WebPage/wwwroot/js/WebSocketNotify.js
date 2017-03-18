
var socket;
var uri = "ws://localhost:5000/ws";
//初始化连接
function doConnect(open, accept, close, error) {
    console.log("doConnect")
    socket = new WebSocket(uri);
    socket.onopen = function (e) { open();};
    socket.onclose = function (e) { close(); };
    socket.onmessage = function (e) {
        accept(e.data);
    };
    socket.onerror = function (e) { error(e.data); };
}
//发送信息
function doSend(message) {
    console.log("doSend")
    socket.send(message);
}
//关闭socket
function doClose() {
    console.log("doClose")
    socket.close();
}


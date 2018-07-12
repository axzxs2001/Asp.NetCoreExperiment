var READYTOPROCESS = false;
window.onbeforeunload = function closeWindow(e) {
    if (!READYTOPROCESS) {
        setCookie("username", "");
        return false;
   
    }
};
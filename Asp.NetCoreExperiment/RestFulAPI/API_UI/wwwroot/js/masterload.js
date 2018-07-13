
function GetHtml(url, callback) {
    var req = false;
    // Safari, Firefox, 及其他非微软浏览器
    if (window.XMLHttpRequest) {
        try {
            req = new XMLHttpRequest();
        } catch (e) {
            req = false;
        }
    } else if (window.ActiveXObject) {
        try {
            req = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                req = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {
                req = false;
            }
        }
    }
    if (req) {
        req.onreadystatechange = function () {
            if (req.status == 200) {
                callback(req.responseText);        
            } else {
                console.log(req.status);
            }
        };
        req.open('GET', url, true);
        req.send(null);
    } else {
        console.log("req空");
    }
}
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}


var scripthtml = document.getElementById("loadscript").outerHTML;
var innerHTML = document.body.innerHTML.replace(scripthtml, "");
GetHtml("/main/index", function (backcontent) {
    document.body.innerHTML = backcontent.replace("#body", innerHTML);
    if (getCookie('username') == null) {
        location.href = "/login";
    } 
    
  });

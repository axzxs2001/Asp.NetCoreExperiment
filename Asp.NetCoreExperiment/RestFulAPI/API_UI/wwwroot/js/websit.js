$(function () {   
    if ($.cookie('username') == null) {    
        location.href = "login.html";
    } 
})

function showtitle(title) {
    document.getElementById("title").innerText = title
}



//window.getHelloMessage = (name) => {

//    DotNet.invokeMethodAsync("WinFormsBlazor01", "GetHelloMessageAsync", name)
//        .then(data => {
//            alert(data);

//        });
//};

class GreetingHelpers {
    static dotNetHelper;

    static setDotNetHelper(value) {
        GreetingHelpers.dotNetHelper = value;
    }

    static async sayHello(name) {     
        const msg =await GreetingHelpers.dotNetHelper.invokeMethodAsync('SayHello',name);
        alert(`Message from .NET: "${msg}"`);
    }
}
window.GreetingHelpers = GreetingHelpers;
//window.sayHello = (dotNetHelper, name) => {
//    alert(dotNetHelper)
 
//    return dotNetHelper.invokeMethodAsync("SayHello", name);
//    //.then(data => {
//    //    alert(data)
//    //});
//};
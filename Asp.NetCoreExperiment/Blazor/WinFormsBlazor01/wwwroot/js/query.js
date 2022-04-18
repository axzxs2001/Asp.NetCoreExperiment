
function showtitle(title) {
    document.getElementById("title").innerText = title
}



window.getHelloMessage = (name) => {

    DotNet.invokeMethodAsync("WinFormsBlazor01", "GetHelloMessageAsync", name)
        .then(data => {
            alert(data);

        });
};

class GreetingHelpers {
    static dotNetHelper;

    static setDotNetHelper(value) {
        GreetingHelpers.dotNetHelper = value;
    }

    static async sayHello(name) {     
        const msg =await GreetingHelpers.dotNetHelper.invokeMethodAsync('SayHello',name);
        alert(`Message from .NET: "${msg}"`);
    }

    static async callForm2(name) {
     
        const msg = await GreetingHelpers.dotNetHelper.invokeMethodAsync('CallForm2', name);
       // alert(`.NET: "${msg}"`);
    }
    
}
window.GreetingHelpers = GreetingHelpers;

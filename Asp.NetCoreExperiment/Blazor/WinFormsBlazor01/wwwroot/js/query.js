
function showtitle(title) {
    document.getElementById("title").innerText = "JS接到WinForm数据：" + title
    return title
}

class CallHelpers {
    static dotNetHelper;

    static DotNetHelper(value) {
        CallHelpers.dotNetHelper = value;
    }

   
    static async callWinForm(name) {
        //CallWinFormMethod方法是默认调用CSharp方法的名称
        const msg = await CallHelpers.dotNetHelper.invokeMethodAsync('CallWinFormMethod', 'clientclick', name);
        alert(`JS接到WinForm的返回值: "${msg}"`);
    }
}
window.CallHelpers = CallHelpers;

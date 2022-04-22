
function showtitle(title) {
    document.getElementById("title").innerText = "JS接到WinForm数据：" + title
    return title
}

class CallHelpers {
    static dotNetHelper;

    static DotNetHelper(value) {
        CallHelpers.dotNetHelper = value;
    }

    static async callForm2(name) {

        const msg = await CallHelpers.dotNetHelper.invokeMethodAsync('CallForm2', name);
        alert(`JS接到WinForm的返回值: "${msg}"`);
    }
}
window.CallHelpers = CallHelpers;

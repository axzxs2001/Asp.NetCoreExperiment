using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01.Razors
{

    public abstract class EventComponent : ComponentBase, IDisposable
    {
        protected DotNetObjectReference<EventComponent>? dotNetHelper;
        protected override async Task OnInitializedAsync()
        {
            IEventHub? eventHub = null;
            IJSRuntime? js = null;
            foreach (var pro in this.GetType().GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
            {
                if (typeof(IEventHub).IsInstanceOfType(pro.GetValue(this, new object[0])))
                {
                    eventHub = pro.GetValue(this, new object[0]) as IEventHub;
                }
                if (typeof(IJSRuntime).IsInstanceOfType(pro.GetValue(this, new object[0])))
                {
                    js = pro.GetValue(this, new object[0]) as IJSRuntime;
                }
            }
            if (eventHub != null && js != null)
            {
                eventHub.OnCallJSAsync += CallAsync;
                async Task<object> CallAsync(object sender, object?[]? eventArgs)
                {
                    var eventhub = sender as EventHub;
                    return await js.InvokeAsync<object>(eventhub?.EventName!, eventArgs);
                }
            }

            if (js != null)
            {
                dotNetHelper = DotNetObjectReference.Create(this);
                await js.InvokeVoidAsync("CallHelpers.DotNetHelper", dotNetHelper);
            }
            base.OnInitialized();
        }

        public void Dispose()
        {
            dotNetHelper?.Dispose();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01
{
    public class EventHub : IEventHub
    {
        public string? EventName { get; set; }

        public event EventHubHandlerAsync<object?[]>? OnCallJSAsync;

        public async Task<object> CallJSAsync(string eventName, params object?[]? eventArgs)
        {
            if (OnCallJSAsync != null)
            {
                EventName = eventName;
                return await OnCallJSAsync(this, eventArgs);               
            }
            return await Task.FromResult("");
        }

        public event EventHubHandlerAsync<object?[]>? OnCallCSharpAsync;

        public async Task<object> CallCSharpAsync(string eventName, params object?[]? eventArgs)
        {
            if (OnCallCSharpAsync != null)
            {
                EventName = eventName;
                return await OnCallCSharpAsync(this, eventArgs);          
            }
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01
{
    public delegate Task<object> EventHubHandlerAsync<TEventArgs>(object sender, TEventArgs? eventArgs);
 
    public interface IEventHub
    {
        string? EventName { get; set; }
        
        event EventHubHandlerAsync<object?[]>? OnCallJSAsync;
        Task<object> CallJSAsync(string eventName, params object?[]? eventArgs);

        event EventHubHandlerAsync<object?[]>? OnCallCSharpAsync;
        Task<object> CallCSharpAsync(string eventName, params object?[]? eventArgs);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01
{
    public interface IEventHub
    {
        event EventHubHandler<object?[]>? OnCallJS;

        void CallJS(string eventName, params object?[]? eventArgs);

        event EventHubHandler<object?[]>? OnCallCSharp;

        void CallCSharp(string eventName, params object?[]? eventArgs);

        string? EventName { get; set; }
    }

    public delegate void EventHubHandler<TEventArgs>(object sender, TEventArgs? eventArgs);
}

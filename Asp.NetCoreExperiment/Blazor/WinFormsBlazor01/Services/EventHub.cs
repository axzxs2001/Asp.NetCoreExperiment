using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01
{
    public class EventHub : IEventHub
    {
        public event EventHubHandler<object?[]>? OnCallJS;

        public string? EventName { get; set; }

        public void CallJS(string eventName, params object?[]? eventArgs)
        {
            if (OnCallJS != null)
            {
                EventName = eventName;
                OnCallJS(this, eventArgs);
            }
        }
        public event EventHubHandler<object?[]>? OnCallCSharp;

        public void CallCSharp(string eventName, params object?[]? eventArgs)
        {
            if (OnCallCSharp != null)
            {
                EventName = eventName;
                OnCallCSharp(this, eventArgs);
            }
        }
    }
}

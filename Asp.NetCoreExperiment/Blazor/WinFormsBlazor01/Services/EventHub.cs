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

        public void CallJS(string eventName, params object?[]? eventArgs)
        {
            if (OnCallJS != null)
            {
                OnCallJS(this, eventName, eventArgs);
            }
        }
        public event EventHubHandler<object?[]>? OnCallDotNet;

        public void CallDotNet(string eventName, params object?[]? eventArgs)
        {
            if (OnCallDotNet != null)
            {
                OnCallDotNet(this, eventName, eventArgs);
            }
        }
    }
}

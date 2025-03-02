using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IRabbitMQEventBus
    {
        void Publish(string queue, string message);
        void Subscribe(string queue, Action<string> onMessageReceived);
    }
}

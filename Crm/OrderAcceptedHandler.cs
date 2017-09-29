using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Events;

namespace Crm
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(GetType()).Info("Order accepted");

            return Task.CompletedTask;
        }
    }
}
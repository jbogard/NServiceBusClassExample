using NServiceBus;
using NServiceBus.Logging;
using Sales.Events;
using System.Threading.Tasks;

namespace Billing
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(typeof(OrderAcceptedHandler)).Info("Received Order Accepted: " + message.OrderId);

            return Task.CompletedTask;
        }
    }
}
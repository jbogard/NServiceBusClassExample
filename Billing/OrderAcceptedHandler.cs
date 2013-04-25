using NServiceBus;
using NServiceBus.Logging;
using Sales.Events;

namespace Billing
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public void Handle(OrderAccepted message)
        {
            LogManager.GetLogger(typeof(OrderAcceptedHandler)).Info("Received Order Accepted: " + message.OrderId);
        }
    }
}
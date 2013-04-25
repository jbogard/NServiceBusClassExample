using NServiceBus;
using NServiceBus.Logging;
using Sales.Events;

namespace Pricing
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public void Handle(OrderAccepted message)
        {
            LogManager.GetLogger(GetType()).Info("Order accepted");
        }
    }
}
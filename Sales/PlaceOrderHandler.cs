using NServiceBus;
using NServiceBus.Logging;
using Sales.Commands;
using Sales.Events;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }
        private static int OrderId;

        public void Handle(PlaceOrder message)
        {
            LogManager.GetLogger(GetType()).Info("Received Place Order");
            Bus.Publish<OrderAccepted>(m => m.OrderId = OrderId++);
        }
    }
}
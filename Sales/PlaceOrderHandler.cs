using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Commands;
using Sales.Events;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private static int OrderId;

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(GetType()).Info("Received Place Order");
            return context.Publish<OrderAccepted>(m => m.OrderId = OrderId++);
        }
    }
}
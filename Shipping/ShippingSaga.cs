using Billing.Events;
using NServiceBus.Logging;
using Sales.Events;
using Shipping.Commands;
using Shipping.Events;
using System.Threading.Tasks;
using NServiceBus.Persistence.Sql;

namespace Shipping
{
    using NServiceBus;

    public class ShippingSaga
        : SqlSaga<ShippingSaga.SagaData>,
        IAmStartedByMessages<OrderAccepted>,
        IAmStartedByMessages<OrderBilled>,
        IAmStartedByMessages<OrderCancelled>,
        IHandleMessages<ReturnProduct>
    {
        protected override void ConfigureMapping(IMessagePropertyMapper mapper)
        {
            mapper.ConfigureMapping<OrderAccepted>(m => m.OrderId);
            mapper.ConfigureMapping<OrderBilled>(m => m.OrderId);
            mapper.ConfigureMapping<OrderCancelled>(m => m.OrderId);
            mapper.ConfigureMapping<ReturnProduct>(m => m.OrderId);
        }

        protected override string CorrelationPropertyName => nameof(SagaData.OrderId);

        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Accepted: " + message.OrderId);

            Data.OrderAccepted = true;
            return CheckIfComplete(context);
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Billed: " + message.OrderId);

            Data.OrderBilled = true;
            return CheckIfComplete(context);
        }

        public Task Handle(OrderCancelled message, IMessageHandlerContext context)
        {
            Data.OrderCancelled = true;

            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Shipping cancelled. " + message.OrderId);
            return CheckIfComplete(context);
        }

        public Task Handle(ReturnProduct message, IMessageHandlerContext context)
        {
            Data.ProductReturned = true;
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Product Returned: " + message.OrderId);
            return CheckIfComplete(context);
        }

        private async Task CheckIfComplete(IMessageHandlerContext context)
        {
            if (Data.OrderCancelled)
            {
                if (Data.OrderShipped && Data.ProductReturned)
                {
                    await context.Publish<ProductReturned>(msg => msg.OrderId = Data.OrderId);
                }
                else if (!Data.OrderShipped)
                {
                    await context.Publish<ShippingCancelled>(msg => msg.OrderId = Data.OrderId);
                }
                return;
            }

            if (Data.OrderBilled && Data.OrderAccepted)
            {
                LogManager.GetLogger(typeof(ShippingSaga))
                    .Info("Shipping Order: " + Data.OrderId);
                Data.OrderShipped = true;
            }
        }

        public class SagaData : ContainSagaData
        {
            public int OrderId { get; set; }
            public bool OrderAccepted { get; set; }
            public bool OrderBilled { get; set; }
            public bool OrderShipped { get; set; }
            public bool ProductReturned { get; set; }
            public bool OrderCancelled { get; set; }
        }

    }
}

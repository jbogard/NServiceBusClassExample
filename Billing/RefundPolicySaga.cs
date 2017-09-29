using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Sagas;
using NServiceBus.Persistence.Sql;
using Sales.Events;
using Shipping.Events;
using Billing.Events;

namespace Billing
{
    public class RefundPolicySaga
        : SqlSaga<RefundPolicySaga.SagaData>,
        IAmStartedByMessages<OrderCancelled>,
        IAmStartedByMessages<ShippingCancelled>,
        IAmStartedByMessages<ProductReturned>
    {
        protected override void ConfigureMapping(IMessagePropertyMapper mapper)
        {
            mapper.ConfigureMapping<OrderCancelled>(m => m.OrderId);
            mapper.ConfigureMapping<ShippingCancelled>(m => m.OrderId);
            mapper.ConfigureMapping<ProductReturned>(m => m.OrderId);
        }

        protected override string CorrelationPropertyName => nameof(SagaData.OrderId);

        public Task Handle(OrderCancelled message, IMessageHandlerContext context)
        {
            Data.OrderCancelled = true;
            return CheckRefund();
        }

        public Task Handle(ShippingCancelled message, IMessageHandlerContext context)
        {
            Data.ShippingCancelled = true;
            return CheckRefund();
        }

        public Task Handle(ProductReturned message, IMessageHandlerContext context)
        {
            Data.ProductReturned = true;
            return CheckRefund();
        }

        private Task CheckRefund()
        {
            // Can also implement partial refunds here
            if (Data.OrderCancelled && (Data.ShippingCancelled || Data.ProductReturned))
            {
                LogManager.GetLogger(typeof(RefundPolicySaga))
                    .Info("Refund issued for order " + Data.OrderId);
                MarkAsComplete();
            }
            return Task.CompletedTask;
        }

        public class SagaData : ContainSagaData
        {
            public int OrderId { get; set; }

            public bool OrderCancelled { get; set; }

            public bool ShippingCancelled { get; set; }

            public bool ProductReturned { get; set; }
        }

    }
}
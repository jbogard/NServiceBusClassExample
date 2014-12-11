using System;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Sales.Events;
using Shipping.Events;

namespace Billing
{
    public class RefundPolicySaga
        : Saga<RefundPolicyData>,
        IAmStartedByMessages<OrderCancelled>,
        IAmStartedByMessages<ShippingCancelled>,
        IAmStartedByMessages<ProductReturned>
    {

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RefundPolicyData> mapper)
        {
            mapper.ConfigureMapping<OrderCancelled>(s => s.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<ShippingCancelled>(s => s.OrderId).ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<ProductReturned>(s => s.OrderId).ToSaga(m => m.OrderId);
        }

        public void Handle(OrderCancelled message)
        {
            Data.OrderId = message.OrderId;
            Data.OrderCancelled = true;
            CheckRefund();
        }

        public void Handle(ShippingCancelled message)
        {
            Data.OrderId = message.OrderId;
            Data.ShippingCancelled = true;
            CheckRefund();
        }

        public void Handle(ProductReturned message)
        {
            Data.OrderId = message.OrderId;
            Data.ProductReturned = true;
            CheckRefund();
        }

        private void CheckRefund()
        {
            // Can also implement partial refunds here
            if (Data.OrderCancelled && (Data.ShippingCancelled || Data.ProductReturned))
            {
                LogManager.GetLogger(typeof(RefundPolicySaga))
                    .Info("Refund issued for order " + Data.OrderId);
                MarkAsComplete();
            }
        }
    }
}
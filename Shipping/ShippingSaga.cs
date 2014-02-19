using System;
using Billing.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Sales.Events;
using Shipping.Commands;
using Shipping.Events;

namespace Shipping
{
    using NServiceBus;

    public class ShippingSaga
        : Saga<ShippingSagaData>,
        IAmStartedByMessages<OrderAccepted>,
        IAmStartedByMessages<OrderBilled>,
        IAmStartedByMessages<OrderCancelled>,
        IHandleMessages<ReturnProduct>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<OrderAccepted>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
            ConfigureMapping<OrderBilled>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
            ConfigureMapping<OrderCancelled>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
            ConfigureMapping<ReturnProduct>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
        }

        public void Handle (OrderAccepted message)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Accepted: " + message.OrderId);

            Data.OrderId = message.OrderId;
            Data.OrderAccepted = true;
            CheckIfComplete();
        }

        public void Handle(OrderBilled message)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Billed: " + message.OrderId);

            Data.OrderId = message.OrderId;
            Data.OrderBilled = true;
            CheckIfComplete();
        }

        public void Handle(OrderCancelled message)
        {
            Data.OrderId = message.OrderId;
            Data.OrderCancelled = true;

            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Shipping cancelled. " + message.OrderId);
            CheckIfComplete();
        }


        public void Handle(ReturnProduct message)
        {
            Data.ProductReturned = true;
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Product Returned: " + message.OrderId);
            CheckIfComplete();
        }


        private void CheckIfComplete()
        {
            if (Data.OrderCancelled)
            {
                if (Data.OrderShipped && Data.ProductReturned)
                {
                    Bus.Publish<ProductReturned>(msg => msg.OrderId = Data.OrderId);
                }
                else if (!Data.OrderShipped)
                {
                    Bus.Publish<ShippingCancelled>(msg => msg.OrderId = Data.OrderId);
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
    }
}

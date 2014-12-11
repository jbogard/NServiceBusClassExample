using Billing.Events;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Sales.Events;

namespace Shipping
{
    public class ShippingSaga
        : Saga<ShippingSagaData>,
        IAmStartedByMessages<OrderAccepted>,
        IAmStartedByMessages<OrderBilled>
    {
        protected override void ConfigureHowToFindSaga(
            SagaPropertyMapper<ShippingSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderAccepted>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
            mapper.ConfigureMapping<OrderBilled>(s => s.OrderId)
                .ToSaga(m => m.OrderId);
        }

        public void Handle(OrderAccepted message)
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

        private void CheckIfComplete()
        {
            if (Data.OrderBilled && Data.OrderAccepted)
            {
                LogManager.GetLogger(typeof(ShippingSaga))
                    .Info("Shipping order. " + Data.OrderId);
                MarkAsComplete();
            }
        }
    }
}
using Billing.Events;
using NServiceBus.Logging;
using Sales.Events;
using System.Threading.Tasks;
using NServiceBus.Persistence.Sql;

namespace Shipping
{
    using NServiceBus;

    public class ShippingSaga
        : SqlSaga<ShippingSaga.SagaData>,
        IAmStartedByMessages<OrderAccepted>,
        IAmStartedByMessages<OrderBilled>
    {
        protected override void ConfigureMapping(IMessagePropertyMapper mapper)
        {
            mapper.ConfigureMapping<OrderAccepted>(m => m.OrderId);
            mapper.ConfigureMapping<OrderBilled>(m => m.OrderId);
        }

        protected override string CorrelationPropertyName => nameof(SagaData.OrderId);

        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Accepted: " + message.OrderId);

            Data.OrderAccepted = true;
            CheckIfComplete();

            return Task.CompletedTask;
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(typeof(ShippingSaga))
                .Info("Received Order Billed: " + message.OrderId);

            Data.OrderBilled = true;
            CheckIfComplete();
            return Task.CompletedTask;
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

        public class SagaData : ContainSagaData
        {
            public int OrderId { get; set; }
            public bool OrderAccepted { get; set; }
            public bool OrderBilled { get; set; }
        }

    }
}
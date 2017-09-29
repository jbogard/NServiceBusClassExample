using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Sagas;
using NServiceBus.Persistence.Sql;
using Sales.Commands;
using Sales.Events;

namespace Sales
{
    public class OrderPlacementSaga
        : SqlSaga<OrderPlacementSaga.SagaData>,
        IAmStartedByMessages<PlaceOrder>,
        IHandleMessages<CancelOrder>,
        IHandleTimeouts<OrderPlacementSaga.OrderReadyToBePlaced>
    {
        protected override void ConfigureMapping(IMessagePropertyMapper mapper)
        {
            mapper.ConfigureMapping<PlaceOrder>(m => m.OrderId);
            mapper.ConfigureMapping<CancelOrder>(m => m.OrderId);
        }

        protected override string CorrelationPropertyName => nameof(SagaData.OrderId);

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            await RequestTimeout<OrderReadyToBePlaced>(context, TimeSpan.FromSeconds(30));

            LogManager.GetLogger(typeof (OrderPlacementSaga))
                      .Info("Place Order request received " + Data.OrderId);
        }

        public async Task Handle(CancelOrder message, IMessageHandlerContext context)
        {
            if (Data.OrderCancelled)
                return;

            Data.OrderCancelled = true;
            if (Data.OrderAccepted)
            {
                await context.Publish<OrderCancelled>(msg => msg.OrderId = message.OrderId);
            }
            LogManager.GetLogger(typeof(OrderPlacementSaga))
                      .Info("Order Cancelled " + Data.OrderId);
        }

        public async Task Timeout(OrderReadyToBePlaced state, IMessageHandlerContext context)
        {
            if (!Data.OrderCancelled)
            {
                await context.Publish<OrderAccepted>(msg => msg.OrderId = Data.OrderId);
                Data.OrderAccepted = true;
                LogManager.GetLogger(typeof(OrderPlacementSaga))
                          .Info("Order Accepted " + Data.OrderId);
            }
        }

        public class SagaData : ContainSagaData
        {
            public int OrderId { get; set; }

            public bool OrderCancelled { get; set; }

            public bool OrderAccepted { get; set; }
        }

        public class OrderReadyToBePlaced
        {
        }
    }

}
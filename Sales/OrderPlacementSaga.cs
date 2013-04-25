using System;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Sales.Commands;
using Sales.Events;

namespace Sales
{
    public class OrderPlacementSaga
        : Saga<OrderPlacementData>,
        IAmStartedByMessages<PlaceOrder>,
        IHandleMessages<CancelOrder>,
        IHandleTimeouts<OrderReadyToBePlaced>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<CancelOrder>(s => s.OrderId).ToSaga(m => m.OrderId);
        }

        public void Handle(PlaceOrder message)
        {
            Data.OrderId = message.OrderId;
            RequestTimeout<OrderReadyToBePlaced>(TimeSpan.FromSeconds(30));

            LogManager.GetLogger(typeof (OrderPlacementSaga))
                      .Info("Place Order request received " + Data.OrderId);
        }

        public void Handle(CancelOrder message)
        {
            if (Data.OrderCancelled)
                return;

            Data.OrderCancelled = true;
            if (Data.OrderAccepted)
            {
                Bus.Publish<OrderCancelled>(msg => msg.OrderId = message.OrderId);
            }
            LogManager.GetLogger(typeof(OrderPlacementSaga))
                      .Info("Order Cancelled " + Data.OrderId);
        }

        public void Timeout(OrderReadyToBePlaced state)
        {
            if (!Data.OrderCancelled)
            {
                Bus.Publish<OrderAccepted>(msg => msg.OrderId = Data.OrderId);
                Data.OrderAccepted = true;
                LogManager.GetLogger(typeof(OrderPlacementSaga))
                          .Info("Order Accepted " + Data.OrderId);
            }
        }
    }

    public class OrderReadyToBePlaced
    {
    }
}
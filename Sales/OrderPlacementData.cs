using System;
using NServiceBus.Saga;

namespace Sales
{
    public class OrderPlacementData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        [Unique]
        public int OrderId { get; set; }

        public bool OrderCancelled { get; set; }

        public bool OrderAccepted { get; set; }
    }
}
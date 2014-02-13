using System;
using NServiceBus.Saga;

namespace Shipping
{
    public class ShippingSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        [Unique]
        public int OrderId { get; set; }
        public bool OrderAccepted { get; set; }
        public bool OrderBilled { get; set; }
    }
}
using System;
using NServiceBus.Saga;

namespace Shipping
{
    public class ShippingSagaData : ContainSagaData
    {
        [Unique]
        public int OrderId { get; set; }
        public bool OrderAccepted { get; set; }
        public bool OrderBilled { get; set; }
    }
}
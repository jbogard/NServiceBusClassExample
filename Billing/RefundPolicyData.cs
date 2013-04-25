using System;
using NServiceBus.Saga;

namespace Billing
{
    public class RefundPolicyData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        [Unique]
        public int OrderId { get; set; }

        public bool OrderCancelled { get; set; }

        public bool ShippingCancelled { get; set; }

        public bool ProductReturned { get; set; }
    }
}
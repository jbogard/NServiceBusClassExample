using NServiceBus;

namespace Sales.Events
{
    public class OrderAccepted : IEvent
    {
        public int OrderId { get; set; }
    }
}
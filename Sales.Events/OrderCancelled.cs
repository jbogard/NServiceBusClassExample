using NServiceBus;

namespace Sales.Events
{
    public class OrderCancelled : IEvent
    {
        public int OrderId { get; set; }
    }
}
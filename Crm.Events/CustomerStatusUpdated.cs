using NServiceBus;

namespace Crm.Events
{
    public class CustomerStatusUpdated : IEvent
    {
        public int CustomerId { get; set; }
    }
}
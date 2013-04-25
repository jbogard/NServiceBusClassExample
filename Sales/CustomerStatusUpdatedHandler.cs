using Crm.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class CustomerStatusUpdatedHandler : IHandleMessages<CustomerStatusUpdated>
    {
        public void Handle(CustomerStatusUpdated message)
        {
            LogManager.GetLogger(GetType()).Info("Customer status updated");
        }
    }
}
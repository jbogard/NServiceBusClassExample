using System.Threading.Tasks;
using Crm.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class CustomerStatusUpdatedHandler : IHandleMessages<CustomerStatusUpdated>
    {
        public Task Handle(CustomerStatusUpdated message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(GetType()).Info("Customer status updated");
            return Task.CompletedTask;
        }
    }
}
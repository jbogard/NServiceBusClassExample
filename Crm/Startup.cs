using System;
using System.Threading.Tasks;
using Crm.Events;
using NServiceBus;

namespace Crm
{
    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine("Enter customer ID:");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    await session.Publish<CustomerStatusUpdated>(m => m.CustomerId = customerId);
                }
            });

            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) => Task.CompletedTask;
    }
}
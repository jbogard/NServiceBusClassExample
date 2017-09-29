using System;
using System.Threading.Tasks;
using Billing.Events;
using NServiceBus;

namespace Billing
{
    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("Enter order ID:");
                    int orderId = Convert.ToInt32(Console.ReadLine());

                    await session.Publish<OrderBilled>(m =>
                    {
                        m.OrderId = orderId;
                    });

                    Console.WriteLine("Order billed. " + orderId);
                }
            });

            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) => Task.CompletedTask;
    }
}
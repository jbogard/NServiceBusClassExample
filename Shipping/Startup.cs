using System;
using System.Threading.Tasks;
using NServiceBus;
using Shipping.Commands;

namespace Shipping
{
    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("Enter shipping product returned: ");
                    int orderId = Convert.ToInt32(Console.ReadLine());

                    await session.SendLocal(new ReturnProduct { OrderId = orderId });
                }
            });

            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) => Task.CompletedTask;

    }
}
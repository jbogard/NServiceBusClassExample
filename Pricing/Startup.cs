using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NServiceBus;
using Pricing.Events;

namespace Pricing
{
    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine("Enter product ID:");
                    int productId = Convert.ToInt32(Console.ReadLine());
                    await session.Publish<ProductPricingUpdated>(m => m.ProductId = productId);
                }
            });

            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) => Task.CompletedTask;
    }
}
using System;
using NServiceBus;
using Pricing.Events;

namespace Pricing
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Enter product ID:");
                int productId = Convert.ToInt32(Console.ReadLine());
                Bus.Publish<ProductPricingUpdated>(m => m.ProductId = productId);
            }
        }

        public void Stop()
        {
        }
    }
}
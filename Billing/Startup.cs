using System;
using System.Threading;
using Billing.Events;
using NServiceBus;

namespace Billing
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            int orderId = 1;
            while (true)
            {
                Console.Write("Enter order ID:");

                Bus.Publish<OrderBilled>(m =>
                {
                    m.OrderId = orderId++;
                });
                Thread.Sleep(200);

                Console.WriteLine("Order billed. " + orderId);
            }
        }

        public void Stop()
        {
        }
    }
}
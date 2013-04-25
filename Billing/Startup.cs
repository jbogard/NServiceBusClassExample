using System;
using Billing.Events;
using NServiceBus;

namespace Billing
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            while (true)
            {
                Console.Write("Enter order ID:");
                int orderId = Convert.ToInt32(Console.ReadLine());

                Bus.Publish<OrderBilled>(m =>
                {
                    m.OrderId = orderId;
                });

                Console.WriteLine("Order billed. " + orderId);
            }
        }

        public void Stop()
        {
        }
    }
}
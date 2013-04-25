using System;
using NServiceBus;
using Shipping.Commands;

namespace Shipping
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            while (true)
            {
                Console.Write("Enter shipping product returned: ");
                int orderId = Convert.ToInt32(Console.ReadLine());

                Bus.SendLocal(new ReturnProduct { OrderId = orderId });
            }
        }

        public void Stop()
        {
        }
    }
}
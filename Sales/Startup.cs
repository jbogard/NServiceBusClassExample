using System;
using NServiceBus;
using Sales.Commands;

namespace Sales
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Enter customer ID:");
                int customerId = Convert.ToInt32(Console.ReadLine());
                Bus.SendLocal<PlaceOrder>(m => m.CustomerId = customerId);
            }
        }

        public void Stop()
        {
        }
    }
}
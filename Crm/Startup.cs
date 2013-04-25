using System;
using Crm.Events;
using NServiceBus;

namespace Crm
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
                Bus.Publish<CustomerStatusUpdated>(m => m.CustomerId = customerId);
            }
        }

        public void Stop()
        {
        }
    }
}
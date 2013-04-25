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
            int i = 0;
            while (true)
            {
                Console.WriteLine("Enter P to place and C to cancel an order...");
                var line = Console.ReadLine();
                switch (line)
                {
                    case "P":
                        i++;
                        Console.WriteLine("Your order number is: " + i);
                        Bus.SendLocal(new PlaceOrder { OrderId = i });
                        break;
                    case "C":
                        Console.Write("Enter order to cancel: ");
                        var order = Console.ReadLine();
                        Bus.SendLocal(new CancelOrder { OrderId = Convert.ToInt32(order) });
                        break;
                }
            }
        }

        public void Stop()
        {
        }
    }
}
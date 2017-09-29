using System;
using System.Threading.Tasks;
using NServiceBus;
using Sales.Commands;

namespace Sales
{
    public class Startup : IWantToRunWhenEndpointStartsAndStops
    {
        public Task Start(IMessageSession session)
        {
            Task.Run(async () =>
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
                            await session.SendLocal(new PlaceOrder { OrderId = i });
                            break;
                        case "C":
                            Console.Write("Enter order to cancel: ");
                            var order = Console.ReadLine();
                            await session.SendLocal(new CancelOrder { OrderId = Convert.ToInt32(order) });
                            break;
                    }
                }
            });

            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) => Task.CompletedTask;
    }
}
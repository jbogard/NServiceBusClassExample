using NServiceBus;

namespace Sales.Commands
{
    public class PlaceOrder : ICommand
    {
        public int CustomerId { get; set; }
    }
}
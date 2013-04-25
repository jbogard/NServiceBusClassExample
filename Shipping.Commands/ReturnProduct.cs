using NServiceBus;

namespace Shipping.Commands
{
    public class ReturnProduct : ICommand
    {
        public int OrderId { get; set; }
    }
}
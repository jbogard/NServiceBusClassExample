using NServiceBus;

namespace Sales.Commands
{
    public class CancelOrder : ICommand
    {
        public int OrderId { get; set; }
    }
}
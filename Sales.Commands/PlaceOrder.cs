﻿using NServiceBus;

namespace Sales.Commands
{
    public class PlaceOrder : ICommand
    {
        public int OrderId { get; set; }
    }
}
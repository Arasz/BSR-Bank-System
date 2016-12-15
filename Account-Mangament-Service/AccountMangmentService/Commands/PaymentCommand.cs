﻿using CQRS.Commands;

namespace AccountMangmentService.Commands
{
    /// <summary>
    /// Minimal common part of all payment commands 
    /// </summary>
    public abstract class PaymentCommand : ICommand
    {
        public decimal Amount { get; set; }
    }
}
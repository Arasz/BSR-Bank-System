﻿using CQRS.Events;

namespace Service.Bank.Events
{
    /// <summary>
    /// Triggered when any operation is executed 
    /// </summary>
    /// <typeparam name="TOperation"> Operation type </typeparam>
    public class OperationExecuted<TOperation> : IEvent
    {
        public TOperation Operation { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InterbankTransactionService.Exceptions
{
    public class TransferValidationException : Exception
    {
        public override string Message => ValidationMessageParametersMap
            .Select(pair => $"Validation error: {pair.Key}.\nParameter value: {pair.Value}\n")
            .Aggregate("", (accu, message) => accu += message);

        public IReadOnlyDictionary<string, object> ValidationMessageParametersMap { get; }

        public TransferValidationException(IDictionary<string, object> validationMessageParametersMap)
        {
            ValidationMessageParametersMap = new ReadOnlyDictionary<string, object>(validationMessageParametersMap);
        }
    }
}
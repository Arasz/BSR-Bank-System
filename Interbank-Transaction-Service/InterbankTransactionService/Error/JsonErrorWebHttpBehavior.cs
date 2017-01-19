﻿using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Service.InterbankTransfer.Error
{
    public class JsonErrorWebHttpBehavior : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();

            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new JsonErrorHandler());
        }
    }
}
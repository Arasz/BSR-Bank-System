using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using Service.Dto;

namespace Service.InterbankTransfer.Error
{
    public class JsonErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error) => true;

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var statusDescription = HttpStatusCode.BadRequest.ToString();

            var innerException = DetailProperty(error)?.GetValue(error);
            if (innerException != null && innerException is Exception)
                error = (Exception)innerException;

            fault = GetJsonFaultMessage(version, error);

            ApplyJsonSettings(fault);
            ApplyHttpResponseSettings(fault, statusCode, statusDescription);
        }

        protected virtual void ApplyHttpResponseSettings(Message fault, HttpStatusCode statusCode, string statusDescription)
        {
            var httpResponse = new HttpResponseMessageProperty
            {
                StatusCode = statusCode,
                StatusDescription = statusDescription
            };

            httpResponse.Headers[HttpRequestHeader.ContentType] = "application/json";

            fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);
        }

        protected virtual void ApplyJsonSettings(Message fault)
        {
            var jsonFormatting = new WebBodyFormatMessageProperty(WebContentFormat.Json);

            fault.Properties.Add(WebBodyFormatMessageProperty.Name, jsonFormatting);
        }

        protected virtual Message GetJsonFaultMessage(MessageVersion version, Exception error)
        {
            var interbankTransferError = new InterbankTransferError(error.Message);

            return CreateFaultMessage(version, interbankTransferError);
        }

        private static Message CreateFaultMessage(MessageVersion version, InterbankTransferError interbankTransferError) =>
            Message.CreateMessage(version, "", interbankTransferError, new DataContractJsonSerializer(typeof(InterbankTransferError)));

        private PropertyInfo DetailProperty(Exception error)
        {
            return error.GetType()
                .GetProperties()
                .FirstOrDefault(info => info.Name == nameof(WebFaultException<object>.Detail));
        }
    }
}
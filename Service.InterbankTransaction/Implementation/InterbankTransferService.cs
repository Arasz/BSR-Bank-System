using Autofac.Features.AttributeFilters;
using Service.Contracts;
using Service.Dto;
using System.Net;
using System.ServiceModel.Web;

namespace Service.InterbankTransfer.Implementation
{
    public class InterbankTransferService : IInterbankTransferService
    {
        private readonly IBankService _bankService;

        public InterbankTransferService([KeyFilter("BankService")]IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            _bankService.ExternalTransfer((TransferDescription)transferDescription);

            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
        }
    }
}
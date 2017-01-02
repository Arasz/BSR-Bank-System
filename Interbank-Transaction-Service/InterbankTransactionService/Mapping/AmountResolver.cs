using AutoMapper;
using InterbankTransactionService.Dto;
using Shared.Transfer;

namespace InterbankTransactionService.Mapping
{
    public class AmountResolver : IValueResolver<InterbankTransferDescription, TransferDescription, decimal>
    {
        public decimal Resolve(InterbankTransferDescription source, TransferDescription destination, decimal destMember, ResolutionContext context)
            => source.Amount / 100M;
    }
}
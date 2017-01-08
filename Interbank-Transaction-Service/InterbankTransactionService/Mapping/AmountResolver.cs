using AutoMapper;
using Service.Dto;
using Shared.Transfer;

namespace Service.InterbankTransaction.Mapping
{
    public class AmountResolver : IValueResolver<InterbankTransferDescription, TransferDescription, decimal>
    {
        public decimal Resolve(InterbankTransferDescription source, TransferDescription destination, decimal destMember,
                ResolutionContext context)
            => source.Amount / 100M;
    }
}
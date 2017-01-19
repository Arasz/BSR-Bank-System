using AutoMapper;
using Service.Dto;

namespace Service.InterbankTransfer.Mapping
{
    public class AmountResolver : IValueResolver<InterbankTransferDescription, TransferDescription, decimal>
    {
        public decimal Resolve(InterbankTransferDescription source, TransferDescription destination, decimal destMember,
                ResolutionContext context)
            => source.Amount / 100M;
    }
}
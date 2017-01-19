using System.Resources;
using AutoMapper;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.Mappings
{
    public class WithdrawCommandConverter : OperationConverterBase<WithdrawCommand>
    {
        public WithdrawCommandConverter(ResourceManager resourceManager) : base(resourceManager)
        {
        }

        protected override void CommandSpecificConversion(Operation destination, ResolutionContext context)
        {
            destination.Credit = 0;
            destination.Debit = _transferDescription.Amount;
        }
    }
}
using AutoMapper;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.Mappings
{
    public class DepositCommandConverter : ITypeConverter<DepositCommand, Operation>
    {
        public Operation Convert(DepositCommand source, Operation destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
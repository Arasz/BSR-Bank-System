using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    ///     Charge for external service payment
    /// </summary>
    public class ExternalTransferChargeCommand : TransferCommand
    {
        public ExternalTransferChargeCommand(TransferDescription description, decimal chargePercent = 0.05M)
            : base(description)
        {
            ChargePercent = chargePercent;
        }

        public decimal ChargePercent { get; set; }
    }
}
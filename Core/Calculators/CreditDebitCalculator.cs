namespace Core.Common.Calculators
{
    public class CreditDebitCalculator : ICreditDebitCalculator
    {
        private readonly decimal _balance;
        public decimal Credit { get; private set; }

        public decimal Debit { get; private set; }

        public CreditDebitCalculator(decimal balance)
        {
            _balance = balance;
        }

        public void CalculateCreditAndDebit(decimal newBalance)
        {
            var balanceDiffrence = newBalance - _balance;
            Credit = balanceDiffrence > 0 ? balanceDiffrence : 0M;
            Debit = balanceDiffrence < 0 ? -balanceDiffrence : 0M;
        }
    }
}
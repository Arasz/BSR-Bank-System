namespace Core.Common.Calculators
{
    public interface ICreditDebitCalculator
    {
        decimal Credit { get; }

        decimal Debit { get; }

        void CalculateCreditAndDebit(decimal newBalance);
    }
}
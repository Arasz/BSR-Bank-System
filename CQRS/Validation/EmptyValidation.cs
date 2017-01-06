namespace CQRS.Validation
{
    public class EmptyValidation : IValidation
    {
        public static EmptyValidation Empty = new EmptyValidation();
    }
}
namespace CQRS.Validation
{
    public interface IValidation
    {
    }

    public interface IValidation<in TValidated> : IValidation
    {
        void Validate(TValidated validated);
    }
}
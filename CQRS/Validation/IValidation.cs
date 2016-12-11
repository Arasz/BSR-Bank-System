namespace CQRS.Validation
{
    public interface IValidation
    {
    }

    public interface IValidation<in TValidated> : IValidation
    {
        /// <summary>
        /// Validates given object 
        /// </summary>
        /// <param name="validated"> Validated object </param>
        void Validate(TValidated validated);
    }
}
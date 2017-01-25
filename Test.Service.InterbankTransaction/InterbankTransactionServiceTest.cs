using FluentValidation;
using FluentValidation.Results;
using Moq;
using Service.Contracts;
using Service.Dto;
using Service.InterbankTransfer.Implementation;
using System.Linq;
using Xunit;

namespace InterbankTransactionServiceTest
{
    public class InterbankTransactionServiceTest
    {
        [Fact]
        public void TestTransfer_CorrectInputData_ShouldPassToBankService()
        {
            var bankServiceMock = new Mock<IBankService>();
            bankServiceMock
                .Setup(service => service.ExternalTransfer(It.IsAny<TransferDescription>()));

            var validatorMock = new Mock<IValidator<InterbankTransferDescription>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<InterbankTransferDescription>()))
                .Returns(() => new ValidationResult(Enumerable.Empty<ValidationFailure>()));

            var interbankTransferDescription = Mock.Of<InterbankTransferDescription>();

            var interbankTransactionService = new InterbankTransferService(bankServiceMock.Object);

            interbankTransactionService.Transfer(interbankTransferDescription);

            bankServiceMock.Verify(service => service.ExternalTransfer(It.IsAny<TransferDescription>()));
        }
    }
}
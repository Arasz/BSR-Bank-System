using AccountMangementService.Service;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InterbankTransactionService.Dto;
using InterbankTransactionService.Mapping;
using InterbankTransactionService.Service;
using Moq;
using Shared.Transfer;
using System.Linq;
using Xunit;

namespace InterbankTransactionServiceTest
{
    public class InterbankTransactionServiceTest
    {
        [Fact]
        public void TestTransfer_CorrectInputData_ShouldPassToBankService()
        {
            var managementServiceMock = new Mock<IAccountManagementService>();
            managementServiceMock
                .Setup(service => service.ExternalTransfer(It.IsAny<TransferDescription>()));

            var validatorMock = new Mock<IValidator<InterbankTransferDescription>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<InterbankTransferDescription>()))
                .Returns(() => new ValidationResult(Enumerable.Empty<ValidationFailure>()));

            var interbankTransferDescription = Mock.Of<InterbankTransferDescription>();
            var transferDescriptionMock = Mock.Of<TransferDescription>();

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<InterbankTransferDescription, TransferDescription>(It.IsAny<InterbankTransferDescription>()))
                .Returns(transferDescriptionMock);

            var mappingMock = new Mock<IMapping>();
            mappingMock
                .Setup(mapping => mapping.Mapper)
                .Returns(mapperMock.Object);

            var interbankTransactionService = new InterbankTransactionServiceImpl(managementServiceMock.Object, validatorMock.Object,
                mappingMock.Object);

            interbankTransactionService.Transfer(interbankTransferDescription);

            managementServiceMock.Verify(service => service.ExternalTransfer(It.IsAny<TransferDescription>()));
        }
    }
}
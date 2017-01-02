using FluentAssertions;
using InterbankTransactionService.Dto;
using InterbankTransactionService.Mapping;
using Shared.Transfer;
using Xunit;

namespace InterbankTransactionServiceTest
{
    public class MapperTest
    {
        [Fact]
        public void Mapping_TransferDescriptionMapping_ShouldBeCorrectlyMapped()
        {
            var mapping = new Mapping();

            var interbankTransferDescription = new InterbankTransferDescription
            {
                SenderAccount = "A",
                ReceiverAccount = "B",
                Amount = 1999,
                Title = "C"
            };

            var expectedTransferDescription = new TransferDescription
            {
                SenderAccount = "A",
                ReceiverAccount = "B",
                Amount = 19.99M,
                Title = "C"
            };

            var mappingResult = mapping.Mapper.Map<InterbankTransferDescription, TransferDescription>(interbankTransferDescription);

            mappingResult.Amount.Should().Be(expectedTransferDescription.Amount);
            mappingResult.SenderAccount.Should().Be(expectedTransferDescription.SenderAccount);
            mappingResult.ReceiverAccount.Should().Be(expectedTransferDescription.ReceiverAccount);
            mappingResult.Title.Should().Be(expectedTransferDescription.Title);
        }

        [Fact]
        public void MappingConstruction_Configuration_ShouldBeValid()
        {
            var mapping = new Mapping();
            mapping.Configuration.AssertConfigurationIsValid();
        }
    }
}
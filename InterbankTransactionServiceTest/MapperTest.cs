using FluentAssertions;
using Service.Dto;
using Service.InterbankTransfer.Mapping;
using Xunit;

namespace InterbankTransactionServiceTest
{
    public class MapperTest
    {
        [Fact]
        public void Mapping_TransferDescriptionMapping_ShouldBeCorrectlyMapped()
        {
            var mapping = new ConfiguredMapperProvider();

            var interbankTransferDescription = new InterbankTransferDescription
            {
                SenderAccount = "A",
                ReceiverAccount = "B",
                Amount = 1999,
                Title = "C"
            };

            var expectedTransferDescription = new TransferDescription
            {
                From = "A",
                To = "B",
                Amount = 19.99M,
                Title = "C"
            };

            var mappingResult = mapping.Mapper.Map<InterbankTransferDescription, TransferDescription>(interbankTransferDescription);

            mappingResult.Amount.Should().Be(expectedTransferDescription.Amount);
            mappingResult.From.Should().Be(expectedTransferDescription.From);
            mappingResult.To.Should().Be(expectedTransferDescription.To);
            mappingResult.Title.Should().Be(expectedTransferDescription.Title);
        }

        [Fact]
        public void MappingConstruction_Configuration_ShouldBeValid()
        {
            var mapping = new ConfiguredMapperProvider();
            mapping.Configuration.AssertConfigurationIsValid();
        }
    }
}
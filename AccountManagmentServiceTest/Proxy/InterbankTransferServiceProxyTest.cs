using System.Collections.Generic;
using Core.Common.AccountNumber;
using Core.Common.AccountNumber.Parser;
using Moq;
using Service.Bank.Proxy;
using Service.Bank.Proxy.Configuration;
using Service.Bank.Proxy.ServiceHttpClient;
using Service.Contracts;
using Service.Dto;
using Xunit;

namespace Test.Service.Bank.Proxy
{
    public class InterbankTransferServiceProxyTest
    {
        private readonly List<Mock> _mocks = new List<Mock>();
        private string _bankId = "00129725";
        private string _receiverAccountNumber = "47001297250000000000000001";
        private string _serviceAddress = "https://192.168.0.1:8080";

        [Theory]
        [InlineData(100)]
        public void TestTransfer_CorrectTransferDescription_TransferShouldBeMade(int transferAmount)
        {
            var proxy = CreateProxy();

            proxy.Transfer(CreateTransferDescription(transferAmount));

            VerifyMocks();
        }

        private IAccountNumberParser AccountNumberParserMock()
        {
            var parser = new Mock<IAccountNumberParser>();

            _mocks.Add(parser);

            parser
                .Setup(numberParser => numberParser.Parse(_receiverAccountNumber))
                .Returns(new BankAccountNumber("xx", _bankId, "xx"))
                .Verifiable();

            return parser.Object;
        }

        private IInterbankTransferService CreateProxy()
        {
            var httpClient = HttpClientMock();

            var register = ServiceRegisterMock();

            var parser = AccountNumberParserMock();

            return new InterbankTransferServiceProxy(httpClient, register, parser);
        }

        private InterbankTransferDescription CreateTransferDescription(int amount) => new InterbankTransferDescription
        {
            Amount = amount,
            ReceiverAccount = _receiverAccountNumber,
            SenderAccount = "xx"
        };

        private ITransferServiceHttpClient HttpClientMock()
        {
            var httpClient = new Mock<ITransferServiceHttpClient>();
            _mocks.Add(httpClient);

            httpClient
                .Setup(client => client.SendTransfer(It.IsAny<InterbankTransferDescription>(), _serviceAddress))
                .Verifiable();

            return httpClient.Object;
        }

        private ITransferServicesRegister ServiceRegisterMock()
        {
            var register = new Mock<ITransferServicesRegister>();

            _mocks.Add(register);

            register
                .Setup(servicesRegister => servicesRegister.GetTransferServiceAddress(_bankId))
                .Returns(_serviceAddress)
                .Verifiable();

            return register.Object;
        }

        private void VerifyMocks()
        {
            foreach (var mock in _mocks)
                mock.Verify();
        }
    }
}
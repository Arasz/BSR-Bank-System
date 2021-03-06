﻿using System;
using System.IO;
using FluentAssertions;
using Service.Bank.Exceptions;
using Service.Bank.Proxy.ServicesRegister;
using Xunit;

namespace Test.Service.Bank.Proxy
{
    public class TransferServicesRegisterTest
    {
        private readonly string _login = "login";
        private readonly string _password = "pswd";
        private readonly string _testBankId = "00112241";
        private readonly string _testServiceAddress = "address";

        private ITransferServicesRegister CreateRegister(
            string testConfigPath = @"..\..\TestInterbankTransferConfiguration.json")
            => new TransferServicesRegister(testConfigPath);

        [Fact]
        public void LazyLoadConfigurationFile_IncorrectFilePath_ShouldThrowFileNotFoundException()
        {
            var register = CreateRegister("incorrect");

            Action getServiceAddressAction = () => register.GetTransferServiceAddress(_testBankId);

            getServiceAddressAction.ShouldThrow<FileNotFoundException>();
        }

        [Fact]
        public void LoadAllDataFromConfiguration_ServicesLoginDataAndRegister_ShouldLoadCorrectConfiguration()
        {
            var register = CreateRegister();

            register.Login.Should().Be(_login);

            register.Password.Should().Be(_password);

            var serviceAddress = register.GetTransferServiceAddress(_testBankId);
            serviceAddress.Should().Be(_testServiceAddress);
        }

        [Fact]
        public void TryGetServiceAddress_GetAddressForWrongBankId_ShouldThrowServiceNotFoundException()
        {
            var register = CreateRegister();

            var wrongBankID = "0000";

            Action getServiceAddressAction = () => register.GetTransferServiceAddress(wrongBankID);

            getServiceAddressAction.ShouldThrow<TransferServiceNotFoundException>();
        }
    }
}
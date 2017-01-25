using Data.Core.Entities;
using System.ServiceModel;

namespace Service.Contracts
{
    [ServiceContract]
    public interface IUserAccountService
    {
        [OperationContract]
        Account CreateBankAccountForUser(string userName);

        [OperationContract]
        User CreateUserAccount(string userName, string password);

        [OperationContract]
        void DeleteUserAccount(string userName);
    }
}
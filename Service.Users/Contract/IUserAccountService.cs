using System.ServiceModel;
using Data.Core;

namespace Service.UserAccount.Contract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name
    //       "IService1" in both code and config file together.
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
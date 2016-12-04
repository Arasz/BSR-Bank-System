using System.ServiceModel;

namespace AccountMangmentService
{
    /// <summary>
    /// Responsible for user bank account management 
    /// </summary>
    [ServiceContract]
    public interface IAccountManagmentService
    {
        [OperationContract]
        string GetData(int value);
    }
}
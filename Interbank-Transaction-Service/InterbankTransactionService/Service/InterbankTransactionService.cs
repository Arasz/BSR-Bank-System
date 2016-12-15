using AccountMangmentService.Service;
using InterbankTransactionService.DataStructures;

namespace InterbankTransactionService.Service
{
    public class InterbankTransactionService : IInterbankTransactionService
    {
        public InterbankTransactionService(IAccountManagmentService accountManagmentService)
        {
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            throw new System.NotImplementedException();
        }
    }
}
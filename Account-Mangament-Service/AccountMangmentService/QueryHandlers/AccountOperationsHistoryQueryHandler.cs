using System.Collections.Generic;
using System.Linq;
using Core.Common.Exceptions;
using CQRS.Queries;
using Data.Core;
using Service.Bank.Exceptions;
using Service.Bank.Queries;

namespace Service.Bank.QueryHandlers
{
    public class AccountOperationsHistoryQueryHandler : IQueryHandler<IEnumerable<Operation>, AccountOperationsHistoryQuery>
    {
        private readonly BankDataContext _dataContext;

        public AccountOperationsHistoryQueryHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Operation> HandleQuery(AccountOperationsHistoryQuery query)
        {
            var queriedAccount = _dataContext.Accounts
                .SingleOrDefault(account => account.Number == query.AccountNumber);

            if (queriedAccount == null)
                throw new AccountNotFoundException($"Account with number {query.AccountNumber} not found");

            return queriedAccount.Operations.ToList();
        }
    }
}
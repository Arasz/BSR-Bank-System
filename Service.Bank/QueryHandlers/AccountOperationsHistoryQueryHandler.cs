using Core.Common.Exceptions;
using Core.CQRS.Queries;
using Data.Core;
using Data.Core.Entities;
using Service.Bank.Queries;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Bank.QueryHandlers
{
    public class AccountOperationsHistoryQueryHandler :
        IQueryHandler<IEnumerable<Operation>, AccountOperationsHistoryQuery>
    {
        private readonly BankDataContext _dataContext;

        public AccountOperationsHistoryQueryHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Operation> HandleQuery(AccountOperationsHistoryQuery query)
        {
            var queriedAccount = _dataContext.Accounts
                .Include(account => account.Operations)
                .SingleOrDefault(account => account.Number == query.AccountNumber);

            if (queriedAccount == null)
                throw new AccountNotFoundException($"Account with number {query.AccountNumber} not found");

            return queriedAccount.Operations.ToList();
        }
    }
}
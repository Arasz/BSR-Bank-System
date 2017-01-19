using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using Service.Bank.Commands;
using Service.Bank.OperationRegister;
using Test.Common;

namespace Test.Service.Bank.OperationHistory
{
    public class OperationHistoryRegisterTest<TCommand> : HandlerTestBase<OperationHistoryRegister<TCommand>, Operation>
        where TCommand : TransferCommand
    {
        protected override Expression<Func<BankDataContext, DbSet<Operation>>> SelectDataSetFromDataContextExpression
            => context => context.Operations;

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);
        }
    }
}
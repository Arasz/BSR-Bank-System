using Data.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Test.Common
{
    public abstract class DataContextAccessTest<TEntity>
        where TEntity : class
    {
        protected List<TEntity> MockDataSource { get; } = new List<TEntity>();

        protected BankDataContext MockDataContext(Expression<Func<BankDataContext, DbSet<TEntity>>> dbSetSelector)
        {
            var dataContextMock = new Mock<BankDataContext>();

            var queryable = MockDataSource.AsQueryable();

            var setMock = new Mock<DbSet<TEntity>>();

            setMock.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(queryable.Provider);

            setMock.As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(queryable.Expression);

            setMock.As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(queryable.ElementType);

            setMock.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(MockDataSource.GetEnumerator());

            setMock.Setup(dbSet => dbSet.Add(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity => MockDataSource.Add(entity));

            dataContextMock.Setup(dbSetSelector)
                .Returns(setMock.Object);

            return dataContextMock.Object;
        }
    }
}
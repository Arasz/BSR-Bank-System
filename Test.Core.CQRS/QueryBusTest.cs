using System;
using Core.CQRS.Exceptions;
using Core.CQRS.Queries;
using FluentAssertions;
using Moq;
using Xunit;

namespace CQRSTest
{
    public class QueryBusTest
    {
        [Fact]
        public void SendQuery_BehaviorWithNullHandler_ShouldThrowNullHandlerException()
        {
            var queryMock = new Mock<IQuery<int>>();
            var queryResult = 2;

            var queryHandler = new Mock<IQueryHandler<int, IQuery<int>>>();

            queryHandler
                .Setup(handler => handler.HandleQuery(queryMock.Object))
                .Returns(queryResult);

            Func<QueryHandlerKey, IQueryHandler<int, IQuery<int>>> queryFactory = queryHandlerKey => null;

            var bus = new QueryBus(queryFactory);

            Action action = () => bus.Send<int, IQuery<int>>(queryMock.Object);
            action.ShouldThrow<NullHandlerException>();
        }

        [Fact]
        public void SendQuery_QueryResult_ResultShouldBeEqualToExpected()
        {
            var queryMock = new Mock<IQuery<int>>();
            var queryResult = 2;

            var queryHandler = new Mock<IQueryHandler<int, IQuery<int>>>();

            queryHandler
                .Setup(handler => handler.HandleQuery(queryMock.Object))
                .Returns(queryResult);

            Func<QueryHandlerKey, IQueryHandler<int, IQuery<int>>> queryFactory = queryHandlerKey => queryHandler.Object;

            var bus = new QueryBus(queryFactory);

            var result = bus.Send<int, IQuery<int>>(queryMock.Object);

            result.Should().Be(queryResult);
        }
    }
}
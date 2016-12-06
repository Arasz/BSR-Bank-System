using CQRS.Events;
using CQRS.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CQRSTest
{
    public class EventBusTest
    {
        [Fact]
        public void PublishEvent_BehaviorWithNullHandler_ShouldThrowNullHandlerException()
        {
            var @event = new Mock<IEvent>();

            var eventsFactory = new Func<Type, IEnumerable<IEventHandler<IEvent>>>(type => null);

            var bus = new EventBus(eventsFactory);

            Action action = () => bus.Publish(@event.Object);
            action.ShouldThrow<NullHandlerException>();
        }

        [Fact]
        public void PublishEvent_OnlyOneSubscriber_HandleShouldBeCalled()
        {
            var @event = new Mock<IEvent>();

            var subscriberMock = new Mock<IEventHandler<IEvent>>();

            var eventBus = new EventBus(type => new List<IEventHandler> { subscriberMock.Object });

            eventBus.Publish(@event.Object);

            Action throwing = () => subscriberMock.Verify(handler => handler.HandleEvent(@event.Object), Times.AtLeastOnce);

            throwing.ShouldNotThrow<MockException>("handler was never called");
        }

        [Fact]
        public void PublishEvent_TwoSubscribers_HandleShouldBeCalledOnEach()
        {
            var @event = new Mock<IEvent>();

            var mocks = new List<Mock<IEventHandler<IEvent>>>
            {
                new Mock<IEventHandler<IEvent>>(),
                new Mock<IEventHandler<IEvent>>()
            };

            var eventBus = new EventBus(type => mocks.Select(mock => mock.Object));

            eventBus.Publish(@event.Object);

            Action throwing = () =>
            {
                foreach (var mock in mocks)
                    mock.Verify(handler => handler.HandleEvent(@event.Object), Times.AtLeastOnce);
            };

            throwing.ShouldNotThrow<MockException>("handler was never called");
        }
    }
}
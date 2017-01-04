using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CQRS.Queries.Autofac
{
    public sealed class QueryModule : Module
    {
        public Assembly QueryHandlersAssembly { get; }

        public QueryModule(Assembly queryHandlersAssembly)
        {
            QueryHandlersAssembly = queryHandlersAssembly;
        }

        public QueryModule()
        {
            QueryHandlersAssembly = ThisAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(QueryHandlersAssembly)
                .Where(type => type.IsAssignableTo<IQueryHandler>())
                .AsImplementedInterfaces();

            builder.Register<Func<QueryHandlerKey, IQueryHandler>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return (queryHandlerKey =>
                {
                    var handlerType = typeof(IQuery<>).MakeGenericType(queryHandlerKey.ResultType,
                        queryHandlerKey.QueryType);
                    return (IQueryHandler)componentContext.Resolve(handlerType);
                });
            });

            builder.RegisterType<QueryBus>()
                .Named<IQueryBus>(nameof(QueryBus));
        }
    }
}
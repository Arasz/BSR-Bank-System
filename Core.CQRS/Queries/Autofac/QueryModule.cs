﻿using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Core.CQRS.Queries.Autofac
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
                    var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryHandlerKey.ResultType,
                        queryHandlerKey.QueryType);

                    return (IQueryHandler) componentContext.Resolve(handlerType);
                });
            });

            builder.RegisterType<QueryBus>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
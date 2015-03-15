﻿using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery
{
    public interface IMediator
    {
        /// <summary>
        /// Method for api controllers.
        /// </summary>
        TResponse RequestHttp<TResponse>(IQuery<TResponse> query);

        /// <summary>
        /// Method for mvc controllers.
        /// </summary>
        TResponse RequestMvc<TResponse>(IQuery<TResponse> query);
    }

    public class Mediator : IMediator
    {
        private const string HandleMethodName = "Handle";

        public TResponse RequestHttp<TResponse>(IQuery<TResponse> query)
        {
            return CallHandle(GetHandlerType(query), GlobalConfiguration.Configuration.DependencyResolver.GetService(GetHandlerType(query)), query);
        }

        public TResponse RequestMvc<TResponse>(IQuery<TResponse> query)
        {
            return CallHandle(GetHandlerType(query), DependencyResolver.Current.GetService(GetHandlerType(query)), query);
        }

        private Type GetHandlerType<TResponse>(IQuery<TResponse> query)
        {
            return typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        }

        private TResponse CallHandle<TResponse>(Type handlerType, object handlerInstance, IQuery<TResponse> query)
        {
            return (TResponse)handlerType.InvokeMember(HandleMethodName,
                                                        BindingFlags.InvokeMethod,
                                                        null,
                                                        handlerInstance,
                                                        new object[] { query });
        }
    }
}

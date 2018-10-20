using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Grpc.Core;
using Grpc.Core.Interceptors;

using mxProject.Helpers.GrpcHelper.Clients;
using mxProject.Helpers.GrpcHelper.Servers;

namespace mxProject.Helpers.GrpcHelper.Interceptors
{

    /// <summary>
    /// 
    /// </summary>
    public class DeepInterceptor : Interceptor
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public DeepInterceptor() : base()
        {
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public bool DeepIntercepterEnabled { get; protected set; }

        #region the client side

        /// <summary>
        /// Intercepts a blocking invocation of a simple remote call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next  interceptor in the chain, or the service request handler, in case of the last interceptor and the interceptor can choose to call it zero or more times at its discretion. The interceptor has the ability to wrap or substitute the request value and the response stream when calling the continuation.</param>
        /// <returns>The response message of the invocation.</returns>
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return base.BlockingUnaryCall(request, context, continuation);
        }

        /// <summary>
        /// Intercepts an asynchronous invocation of a simple remote call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next  interceptor in the chain, or the service request handler, in case of the last interceptor and the interceptor can choose to call it zero or more times at its discretion. The interceptor has the ability to wrap or substitute the request value and the response stream when calling the continuation.</param>
        /// <returns>A return object of the invocation.</returns>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = base.AsyncUnaryCall(request, context, continuation);

            if (!DeepIntercepterEnabled) { return call; }

            Task<TResponse> getResponse = InterceptGetResponse(call.ResponseAsync, context);
            Task<Metadata> getHeader = InterceptGetResponseHeaders(call.ResponseHeadersAsync, context);
            Func<Status> getStatus = () => { return InterceptGetStatus(call.GetStatus, context); };
            Func<Metadata> getTrailer = () => { return InterceptGetTrailers(call.GetTrailers, context); };

            return new AsyncUnaryCall<TResponse>(getResponse, getHeader, getStatus, getTrailer, call.Dispose);
        }

        /// <summary>
        /// Intercepts an asynchronous invocation of a client streaming remote call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next  interceptor in the chain, or the service request handler, in case of the last interceptor and the interceptor can choose to call it zero or more times at its discretion. The interceptor has the ability to wrap or substitute the request value and the response stream when calling the continuation.</param>
        /// <returns>A return object of the invocation.</returns>
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var call = base.AsyncClientStreamingCall(context, continuation);

            if (!DeepIntercepterEnabled) { return call; }

            IClientStreamWriter<TRequest> streamWriter = CreateClientStreamWriter(call.RequestStream, context);
            Task<TResponse> getResponse = InterceptGetResponse(call.ResponseAsync, context);
            Task<Metadata> getHeader = InterceptGetResponseHeaders(call.ResponseHeadersAsync, context);
            Func<Status> getStatus = () => { return InterceptGetStatus(call.GetStatus, context); };
            Func<Metadata> getTrailer = () => { return InterceptGetTrailers(call.GetTrailers, context); };

            return new AsyncClientStreamingCall<TRequest, TResponse>(streamWriter, getResponse, getHeader, getStatus, getTrailer, call.Dispose);
        }

        /// <summary>
        /// Intercepts an asynchronous invocation of a server streaming remote call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next  interceptor in the chain, or the service request handler, in case of the last interceptor and the interceptor can choose to call it zero or more times at its discretion. The interceptor has the ability to wrap or substitute the request value and the response stream when calling the continuation.</param>
        /// <returns>A return object of the invocation.</returns>
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var call = base.AsyncServerStreamingCall(request, context, continuation);

            if (!DeepIntercepterEnabled) { return call; }

            IAsyncStreamReader<TResponse> streamReader = CreateClientStreamReader(call.ResponseStream, context);
            Task<Metadata> getHeader = InterceptGetResponseHeaders(call.ResponseHeadersAsync, context);
            Func<Status> getStatus = () => { return InterceptGetStatus(call.GetStatus, context); };
            Func<Metadata> getTrailer = () => { return InterceptGetTrailers(call.GetTrailers, context); };

            return new AsyncServerStreamingCall<TResponse>(streamReader, getHeader, getStatus, getTrailer, call.Dispose);
        }

        /// <summary>
        /// Intercepts an asynchronous invocation of a duplex streaming remote call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next  interceptor in the chain, or the service request handler, in case of the last interceptor and the interceptor can choose to call it zero or more times at its discretion. The interceptor has the ability to wrap or substitute the request value and the response stream when calling the continuation.</param>
        /// <returns>A return object of the invocation.</returns>
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var call = base.AsyncDuplexStreamingCall(context, continuation);

            if (!DeepIntercepterEnabled) { return call; }

            IClientStreamWriter<TRequest> streamWriter = CreateClientStreamWriter(call.RequestStream, context);
            IAsyncStreamReader<TResponse> streamReader = CreateClientStreamReader(call.ResponseStream, context);
            Task<Metadata> getHeader = InterceptGetResponseHeaders(call.ResponseHeadersAsync, context);
            Func<Status> getStatus = () => { return InterceptGetStatus(call.GetStatus, context); };
            Func<Metadata> getTrailer = () => { return InterceptGetTrailers(call.GetTrailers, context); };

            return new AsyncDuplexStreamingCall<TRequest, TResponse>(streamWriter, streamReader, getHeader, getStatus, getTrailer, call.Dispose);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="getReponse"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task<TResponse> InterceptGetResponse<TRequest, TResponse>(Task<TResponse> getReponse, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return getReponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="getReponseHeaders"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task<Metadata> InterceptGetResponseHeaders<TRequest, TResponse>(Task<Metadata> getReponseHeaders, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return getReponseHeaders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="getStatus"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Status InterceptGetStatus<TRequest, TResponse>(Func<Status> getStatus, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return getStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="getTrailers"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Metadata InterceptGetTrailers<TRequest, TResponse>(Func<Metadata> getTrailers, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return getTrailers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private IClientStreamWriter<TRequest> CreateClientStreamWriter<TRequest, TResponse>(IClientStreamWriter<TRequest> streamWriter, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {

            Task onComplete(IClientStreamWriter<TRequest> writer)
            {
                async Task func()
                {
                    await writer.CompleteAsync().ConfigureAwait(false);
                }

                return InterceptClientCompleteRequest(func, context);
            }

            Task onWrite(IClientStreamWriter<TRequest> writer, TRequest request)
            {
                async Task func(TRequest req)
                {
                    await writer.WriteAsync(req).ConfigureAwait(false);
                }

                return InterceptClientWriteRequest(func, request, context);
            }

            return streamWriter.Wrap(onComplete, onWrite);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="writeRequest"></param>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task InterceptClientWriteRequest<TRequest, TResponse>(Func<TRequest, Task> writeRequest, TRequest request, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return writeRequest(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="completeRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task InterceptClientCompleteRequest<TRequest, TResponse>(Func<Task> completeRequest, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return completeRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="streamReader"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private IAsyncStreamReader<TResponse> CreateClientStreamReader<TRequest, TResponse>(IAsyncStreamReader<TResponse> streamReader, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {

            async Task<bool> onMoveNext(IAsyncStreamReader<TResponse> reader, CancellationToken cancellationToken)
            {
                bool result = false;

                async Task<bool> func()
                {
                    result = await reader.MoveNext(cancellationToken).ConfigureAwait(false);
                    return result;
                }

                await InterceptClientMoveNextResponse(func, context).ConfigureAwait(false);

                return result;
            }

            void onDispose(IAsyncStreamReader<TResponse> reader)
            {
                reader.Dispose();
            }

            return streamReader.Wrap(onMoveNext, onDispose);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="moveNext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async virtual Task<bool> InterceptClientMoveNextResponse<TRequest, TResponse>(Func<Task<bool>> moveNext, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            return await moveNext().ConfigureAwait(false);
        }

        #endregion

        #region the server side

        /// <summary>
        /// Server-side handler for intercepting and incoming unary call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next interceptor in the chain, or the service request handler, in case of the last interceptor and return the response value of the RPC. The interceptor can choose to call it zero or more times at its discretion.</param>
        /// <returns>A response value of the RPC.</returns>
        public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            return await base.UnaryServerHandler(request, context, continuation).ConfigureAwait(false);
        }

        /// <summary>
        /// Server-side handler for intercepting client streaming call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="requestStream">The request stream of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next interceptor in the chain, or the service request handler, in case of the last interceptor and return the response value of the RPC. The interceptor can choose to call it zero or more times at its discretion.</param>
        /// <returns>A response value of the RPC.</returns>
        public async override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            if (DeepIntercepterEnabled)
            {
                requestStream = CreateServerStreamReader(requestStream, context);
            }

            return await base.ClientStreamingServerHandler(requestStream, context, continuation).ConfigureAwait(false);
        }

        /// <summary>
        /// Server-side handler for intercepting server streaming call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request of the invocation.</param>
        /// <param name="responseStream">The response stream of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next interceptor in the chain, or the service request handler, in case of the last interceptor and return the response value of the RPC. The interceptor can choose to call it zero or more times at its discretion.</param>
        /// <returns></returns>
        public async override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            if (DeepIntercepterEnabled)
            {
                responseStream = CreateServerStreamWriter(responseStream, context);
            }

            await base.ServerStreamingServerHandler(request, responseStream, context, continuation).ConfigureAwait(false);
        }

        /// <summary>
        /// Server-side handler for intercepting duplex streaming call.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="requestStream">The request stream of the invocation.</param>
        /// <param name="responseStream">The response stream of the invocation.</param>
        /// <param name="context">The context associated with the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling the next interceptor in the chain, or the service request handler, in case of the last interceptor and return the response value of the RPC. The interceptor can choose to call it zero or more times at its discretion.</param>
        /// <returns></returns>
        public async override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            if (DeepIntercepterEnabled)
            {
                requestStream = CreateServerStreamReader(requestStream, context);
                responseStream = CreateServerStreamWriter(responseStream, context);
            }

            await base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="streamReader"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private IAsyncStreamReader<TRequest> CreateServerStreamReader<TRequest>(IAsyncStreamReader<TRequest> streamReader, ServerCallContext context)
            where TRequest : class
        {

            Task<bool> onMoveNext(IAsyncStreamReader<TRequest> reader, CancellationToken cancellationToken)
            {
                bool result = false;

                async Task<TRequest> func()
                {
                    result = await reader.MoveNext(cancellationToken).ConfigureAwait(false);
                    return result ? reader.Current : default(TRequest);
                }

                InterceptServerMoveNextRequest(func, context);

                return Task.FromResult(result);
            }

            void onDispose(IAsyncStreamReader<TRequest> reader)
            {
                reader.Dispose();
            }

            return streamReader.Wrap(onMoveNext, onDispose);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="moveNext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task<TRequest> InterceptServerMoveNextRequest<TRequest>(Func<Task<TRequest>> moveNext, ServerCallContext context)
            where TRequest : class
        {
            return moveNext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private IServerStreamWriter<TResponse> CreateServerStreamWriter<TResponse>(IServerStreamWriter<TResponse> streamWriter, ServerCallContext context)
            where TResponse : class
        {

            Task onWrite(IServerStreamWriter<TResponse> writer, TResponse response)
            {
                async Task func(TResponse res)
                {
                    await writer.WriteAsync(res).ConfigureAwait(false);
                }

                return InterceptServerWriteResponse(func, response, context);
            }

            return streamWriter.Wrap(onWrite);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="writeResponse"></param>
        /// <param name="reponse"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task InterceptServerWriteResponse<TResponse>(Func<TResponse, Task> writeResponse, TResponse reponse, ServerCallContext context)
            where TResponse : class
        {
            return writeResponse(reponse);
        }

        #endregion

    }

}

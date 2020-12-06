using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WeirdFlex.Api.Behaviours
{
    internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        const string MediatRRequestType = nameof(MediatRRequestType);
        const string Source = nameof(Source);
        const string Request = nameof(Request);
        const string Response = nameof(Response);

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = GetTypeName<TRequest>();

            using var scope = logger.BeginScope($"Handling request of type {{{MediatRRequestType}}}", typeName);

            logger.LogInformation($"Handling request of type {{{MediatRRequestType}}}", typeName);

            var serRequest = JsonConvert.SerializeObject(request);
            if (!string.IsNullOrEmpty(serRequest?[1..^1]))
            {
                logger.LogDebug($"Handling request of type {{{MediatRRequestType}}}, [{{{Source}}}] {{{Request}}}",
                    typeName,
                    Request,
                    JsonConvert.SerializeObject(request));
            }

            var response = await next();

            var serResponse = JsonConvert.SerializeObject(response);
            if (response is IResult result &&
                !result.IsSuccess)
            {
                logger.LogError($"Handling request of type {{{MediatRRequestType}}}, [{{{Source}}}], Result: {{Result}}",
                    typeName,
                    Response,
                    JsonConvert.SerializeObject(response));
            }
            else
            {
                logger.LogDebug($"Handling request of type {{{MediatRRequestType}}}, [{{{Source}}}] {{{Response}}}",
                    typeName,
                    Response,
                    JsonConvert.SerializeObject(response));
            }

            return response;
        }

        private static string GetTypeName<T>()
        {
            var type = typeof(T);
            return type.ReflectedType?.Name ??
                type.FullName?.Replace(type.Assembly.FullName!, string.Empty) ??
                type.Name;
        }
    }
}
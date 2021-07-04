using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Livraria.Api.Test.Helper
{
    public static class LoggerHelper
    {
        public static Mock<ILogger<T>> GetLogger<T>(ILogRepositorio _logRepositorio)
        {
            var logger = new Mock<ILogger<T>>();

            logger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback(new InvocationAction(invocation =>
                {
                    var logLevel = (LogLevel)invocation.Arguments[0];
                    var state = invocation.Arguments[2];
                    var exception = (Exception)invocation.Arguments[3];
                    var formatter = invocation.Arguments[4];
                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                    var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                    var log = new Log
                    {
                        Message = logMessage,
                        Level = logLevel.ToString()
                    };
                    _logRepositorio.Create(log).RunSynchronously();
                }));

            return logger;
        }
    }
}

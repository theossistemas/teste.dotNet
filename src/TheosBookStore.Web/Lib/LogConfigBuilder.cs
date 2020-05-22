using System;
using System.Reflection;

using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace TheosBookStore.Web.Lib
{

    public class LogConfigBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly string _environment;

        public LogConfigBuilder(IConfigurationRoot configuration, string environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        public void Build()
        {
            Log.Logger = GetLoggerConfiguration();
        }

        private ILogger GetLoggerConfiguration() => new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), GetLogFileName(), LogEventLevel.Debug)
                .WriteTo.Elasticsearch(ConfigureElasticSink())
                .Enrich.WithProperty("Environment", _environment)
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

        private string GetLogFileName() => $"{GetLogIndentifier()}.log";

        private string GetLogIndentifier()
        {
            var logBaseName = Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-");
            var logByDay = $"{DateTime.UtcNow:yyyy-MM}";
            return $"{logBaseName}-{logByDay}";
        }
        private ElasticsearchSinkOptions ConfigureElasticSink()
        {
            var uri = new Uri(_configuration["ElasticConfiguration:Uri"]);
            var options = new ElasticsearchSinkOptions(uri);
            options.AutoRegisterTemplate = true;
            options.AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7;
            options.IndexFormat = GetLogIndentifier();
            options.EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback;
            Console.WriteLine(options.IndexFormat);
            return options;
        }
    }
}

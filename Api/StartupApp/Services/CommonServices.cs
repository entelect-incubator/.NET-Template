namespace Api.StartupApp.Services;

using System.Text.Json.Serialization;
using Common.Behaviours;
using Common.Models;
using Correlate.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Polly;
using Polly.Extensions.Http;
using Serilog.Enrichers.Correlate;

public static class CommonServices
{
    public const string CORRELATIONHEADER = "Pezza-Correlation-ID";

    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddNewtonsoftJson(x => x.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        ////COMPRESSION
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        if (StartupSettings.Current.IncludeHeaderVersion)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        services.AddHttpContextAccessor();

        ////Correlation Id
        if (StartupSettings.Current.IncludeCorrelationId)
        {
            services.AddCorrelate(options =>
                options.RequestHeaders = new[]
                {
                    CORRELATIONHEADER,
                }).AddCorrelationContextEnricher();
        }

        ////FEATURE FLAGS
        services.AddFeatureManagement();

        ////SWAGGER
        if (StartupSettings.Current.IncludeSwaggerDoc)
        {
            services.AddSwaggerDocument(config =>
            {
                config.GenerateEnumMappingDescription = true;
                config.PostProcess = document =>
                {
                    document.Info.Version = StartupSettings.Current.SwaggerDocVersion;
                    document.Info.Title = StartupSettings.Current.SwaggerDocTitle;
                };
            });
        }

        ////COMMON
        services.AddLazyCache();

        ////Correlation Id
        if (StartupSettings.Current.IncludeCorrelationId)
        {
            services.AddHttpClient(Naming.HttpClientName)
                .AddPolicyHandler(GetRetryPolicy())
                .CorrelateRequests(CORRELATIONHEADER);
        }
        else
        {
            services.AddHttpClient(Naming.HttpClientName)
                .AddPolicyHandler(GetRetryPolicy());
        }

        services.AddHealthChecks();

        ////DEPENDENCY INJECTION
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}
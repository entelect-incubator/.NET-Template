namespace Api.StartupApp.Services;

using System.Net;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using Correlate.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog.Enrichers.Correlate;
using Utilities;
using Utilities.Behaviours;
using Utilities.Models;

public static class CommonServices
{
    public const string CORRELATIONHEADER = "Correlation-ID";

    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddNewtonsoftJson(x => x.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        ////Rate Limiting
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("Fixed", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(3);
                opt.PermitLimit = 3;
                opt.QueueLimit = 2;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });

        ////COMPRESSION
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        if (StartupSettings.Current.IncludeHeaderVersion)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                new HeaderApiVersionReader("api-version"),
                                                new MediaTypeApiVersionReader("api-version"));
            });

            apiVersioningBuilder.AddApiExplorer(options =>
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
                options.RequestHeaders =
                [
                    CORRELATIONHEADER,
                ]).AddCorrelationContextEnricher();
        }

        ////FEATURE FLAGS
        services.AddFeatureManagement();

        ////OPEN API
        if (StartupSettings.Current.OpenApi.Enable)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = StartupSettings.Current.OpenApi.Version;
                    document.Info.Title = StartupSettings.Current.OpenApi.Title;
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

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

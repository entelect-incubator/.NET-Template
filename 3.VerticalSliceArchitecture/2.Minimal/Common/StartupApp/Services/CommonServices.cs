namespace Common.StartupApp.Services;

using System.Net;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using Common.Models;
using Correlate.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Newtonsoft.Json.Serialization;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog.Enrichers.Correlate;

public static class CommonServices
{
    public const string CORRELATIONHEADER = "Correlation-ID";

    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                // Add a trace ID for correlation (optional but useful)
                ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;

                // Optional: map common status codes to titles
                if (ctx.ProblemDetails.Status == 500)
                {
                    ctx.ProblemDetails.Title = "An unexpected error occurred";
                }
            };
        });

        services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddNewtonsoftJson(x => x.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problem = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https://yourdomain/errors/validation",
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Status = StatusCodes.Status400BadRequest
                    };

                    return new BadRequestObjectResult(problem)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

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

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Pezza"))
        .WithMetrics(metrics =>
        {
            metrics
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation();

            metrics.AddOtlpExporter(options => options.Endpoint = new Uri(Settings.Current.OpenTelemetryExportUrl));
        })
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation();

            tracing.AddOtlpExporter(options => options.Endpoint = new Uri(Settings.Current.OpenTelemetryExportUrl));
        });

        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

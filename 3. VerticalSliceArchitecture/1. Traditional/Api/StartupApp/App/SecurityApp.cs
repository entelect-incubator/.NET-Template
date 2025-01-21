namespace Api.StartupApp.App;

public static class SecurityApp
{
    public static IApplicationBuilder AddSecurity(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
            {
                context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
            }

            if (!context.Response.Headers.ContainsKey("Permissions-Policy"))
            {
                context.Response.Headers.Append("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
            }

            if (!context.Response.Headers.ContainsKey("Feature-Policy"))
            {
                context.Response.Headers.Append("Feature-Policy", "accelerometer 'none'; camera 'none'; microphone 'none';");
            }

            await next();
        });

        app.UseCsp(opts => opts
            .BlockAllMixedContent()
            .StyleSources(s => s.Self())
            .StyleSources(s => s.UnsafeInline())
            .FontSources(s => s.Self())
            .FormActions(s => s.Self())
            .FrameAncestors(s => s.Self())
            .ImageSources(imageSrc => imageSrc.Self())
            .ImageSources(imageSrc => imageSrc.CustomSources("data:")));

        app.UseXContentTypeOptions();
        app.UseXfo(options => options.Deny());
        app.UseReferrerPolicy(options => options.NoReferrer());
        app.UseXXssProtection(options => options.EnabledWithBlockMode());

        app.UseCors("CorsPolicy");
        app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());

        return app;
    }
}

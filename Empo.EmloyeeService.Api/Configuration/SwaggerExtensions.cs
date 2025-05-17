using System.Reflection;

namespace Empo.EmloyeeService.Api.Configuration;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Empo Employee API",
                Version = "v1",
                Description = "Empo Employee API",
            });
            options.OperationFilter<OperationFilter>();

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var commentsFIle = Path.Combine(baseDirectory, commentsFileName);
        });
        return services;
    }
    internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Empo Employee API");
        });
        return app;
    }
}

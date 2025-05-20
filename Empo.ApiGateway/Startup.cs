using Empo.ApiGateway.Configurations;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Empo.ApiGateway;

public class Startup
{
    private readonly IWebHostEnvironment _environment;
    public IConfiguration Configuration { get; }
    private readonly ILogger _logger;

    public Startup(IWebHostEnvironment host, IConfiguration configuration)
    {
        Configuration = configuration;
        _environment = host;

    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors( options =>
        {
            options.AddPolicy("AllowAll",
                build =>
                {
                    build
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });
        services.AddOcelot()
            .AddDelegatingHandler<HeaderDelegatingHandler>(true)
            .AddConsul();
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors(options =>
            options
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseOcelot().Wait();
        app.UseRouting();
        app.UseEndpoints(endPoints =>
        {
            endPoints.MapControllers();
        });
    }
}

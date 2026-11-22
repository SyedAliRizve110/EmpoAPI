using Empo.BuildingBlocks.Infrastructure.Configuration.Auth;
using Empo.MediaService.Infrastructure;
using Empo.MediaService.Infrastructure.Data.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace Empo.MediaService.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private static ILogger _logger;

        public Startup(IWebHostEnvironment env)
        {
            _logger = ConfigureLogger();
            _logger.Information("<logger Configured");

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            if (env.EnvironmentName.ToLower() == "development")
            {
                configurationBuilder = configurationBuilder
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json");
            }
            this._config = configurationBuilder
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Empo.MediaService.Application.AssemblyReference).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddAutoMapper(config => { /* configuration */}, typeof(Program).Assembly);
            services.AddAutoMapper(config => { /* configuration */}, typeof(MediaMapper));
            services.AddControllers();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("MyCustomPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token like this: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ── Shared JWT validation & Permissions ─────────────────────────
            // Validates tokens issued by EmployeeService using the same Jwt:Key.
            // Evaluates [HasPermission(...)] by reading claims in the token (no DB needed).
            services.AddSharedJwtAuth(_config);
            services.AddPermissionAuthorization();

            var assemblyName = typeof(MediaContext).Assembly.GetName().Name;

            services.AddDbContext<MediaContext>(options =>
            {
                options.UseNpgsql(this.GetConnectionString(),
                    sql =>
                    {
                        sql.MigrationsAssembly(assemblyName);
                    });
            });

            services.AddInfrastructureServices(_config);
        }
        // IExecutionContextAccessor executionContextAcessor = new ExecutionContextAccessor(serviceProvider.GetService<HttpContextAccessor>());

        //return ApplicationStartup.Initialize(
        //    services,
        //    this.GetConnectionString(),
        //    //  cacheStore,
        //    serviceProvider,
        //    //  emailSender,
        //    // emailsSettings,
        //    _logger,
        //     executionContextAcessor
        //);
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();
            app.InitializeDataBase();

            app.UseRouting();
            app.UseCors("MyCustomPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        private static ILogger ConfigureLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();
        }

        public string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public string GetAssemblyName()
        {
            var AssemblyName = typeof(Program).Assembly.GetName().Name;
            return AssemblyName ?? "";
        }
    }
}

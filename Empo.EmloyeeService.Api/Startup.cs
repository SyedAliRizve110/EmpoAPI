
using Empo.BuildingBlocks.Application;
using Empo.EmloyeeService.Api.Configuration;
using Empo.EmployeeService.Infrastructure;
using Empo.EmployeeService.Infrastructure.Data.Mappers;
using Kingfisher.TourService.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;
using ILogger = Serilog.ILogger;

namespace Empo.EmloyeeService.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString = "ConnectionStrings";
        private static ILogger _logger;

        public Startup(IWebHostEnvironment env)
        {
            _logger = ConfigureLogger();
            _logger.Information("<ogger Configured");

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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers(options =>
            options.Filters.Add<HttpResponseAxceptionFilter>());

            services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(typeof(EmployeeMapper));

            services.AddMemoryCache();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidAudience = _config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "https://localhost:5000"; //identityServer url
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateAudience = false
            //        };
            //    });
            services.AddAuthorization();

            // var assemblyName = this.GetAssemblyName();
            var assemblyName = typeof(EmployeeContext).Assembly.GetName().Name;

            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseNpgsql(this.GetConnectionString(),
                    sql =>
                    {
                        sql.MigrationsAssembly(assemblyName);
                    });
            });

            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAcessor = new ExecutionContextAccessor(serviceProvider.GetService<HttpContextAccessor>());

            return ApplicationStartup.Initialize(
                services,
                this.GetConnectionString(),
                //  cacheStore,
                serviceProvider,
                //  emailSender,
                // emailsSettings,
                _logger,
                 executionContextAcessor
            );

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeDataBase();
            // app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
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

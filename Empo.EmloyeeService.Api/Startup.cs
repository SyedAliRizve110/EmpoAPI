using Empo.EmployeeService.Api;
using Empo.EmployeeService.Infrastructure;
using Empo.EmployeeService.Infrastructure.Data.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            //services.AddControllers(options =>
            // options.Filters.Add<HttpResponseAxceptionFilter>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Empo.EmployeeService.Application.AssemblyReference).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddAutoMapper(config => { /* configuration */}, typeof(Program).Assembly);
            services.AddAutoMapper(config => { /* configuration */}, typeof(EmployeeMapper));
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
                    options.MapInboundClaims = false;
                });

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
            services.AddInfrastructureServices(_config);

            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

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

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();
            app.InitializeDataBase();
            // app.UseDeveloperExceptionPage();

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
                // The version string 'v1' here must match your AddSwaggerGen version exactly
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

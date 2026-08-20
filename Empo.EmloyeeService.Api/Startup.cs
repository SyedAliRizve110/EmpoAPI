using Empo.EmployeeService.Api;
using Empo.EmployeeService.Api.Jwt;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Interface;
using Empo.EmployeeService.Infrastructure;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Empo.EmployeeService.Infrastructure.Data.Mappers;
using Empo.EmployeeService.Infrastructure.Data.Repositories;
using Empo.EmployeeService.Infrastructure.Data.Repositories.AuthRepository;
using Empo.EmployeeService.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
            services.AddSwaggerGen();


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
            services.AddScoped<PasswordHasher<UserEntity>>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IEmployeeService, EmployeeRepository>();
            services.AddScoped<IAttendanceService, AttendanceRepository>();
            services.AddScoped<IDepartmentService, DepartmentRepository>();
            services.AddScoped<IDesignationService, DesignationRepository>();
            services.AddScoped<IBranchService, BranchRepository>();
            services.AddScoped<IUserService, UserRepository>();
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
            app.UseHttpsRedirection();
            app.InitializeDataBase();
            // app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors();
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

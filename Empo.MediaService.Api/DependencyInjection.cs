using Empo.BuildingBlocks.Infrastructure.Configuration.UserConfiguration;
using Empo.EmployeeService.Infrastructure.Data.Services;
using Empo.MediaService.Application.EmployeeDocument;
using Empo.MediaService.Application.Media;
using Empo.MediaService.Infrastructure.Data.Services;

namespace Empo.MediaService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            //services
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IMediaService, Infrastructure.Data.Services.MediaService>();

            //repo
            services.AddScoped<IEmployeeDocumentService, DocumentService>();
            return services;
        }
    }
}

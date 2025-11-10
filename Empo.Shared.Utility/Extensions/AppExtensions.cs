using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Empo.Shared.Utility.Extensions;

public static class AppExtensions
{
	public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration, ILogger? logger = null)
	{
		services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
		{
			var Scheme = configuration.GetValue<string>("Consul:Scheme");
			var Host = configuration.GetValue<string>("Consul:Host");
			var Port = configuration.GetValue<int>("Consul:Port");
			if (logger != null)
			{
				logger.Information(Scheme);
				logger.Information(Host);
				logger.Information(Port.ToString());
			}
			var urlbuilder = new UriBuilder(Scheme, Host, Port);
			consulConfig.Address = urlbuilder.Uri;
		}));
		return services;
	}

	public static IApplicationBuilder UseConsul(this IApplicationBuilder app, string appName)
	{
		var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
		var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
		var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

		//if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;

		//var addresses = features.Get<IServerAddressesFeature>();
		//var address = addresses.Addresses.First();
		var address = Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First();

		var uri = new Uri(address);
		var registration = new AgentServiceRegistration()
		{
			ID = $"Empo-{uri.Host}-{uri.Port}",
			Name = appName,
			Address = $"{uri.Host}",
			Port = uri.Port
		};

		logger.LogInformation("Registering with Consul");
		consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
		consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

		lifetime.ApplicationStopping.Register(() =>
		{
			logger.LogInformation("Unregistering from Consul");
			consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
		});

		return app;
	}
}

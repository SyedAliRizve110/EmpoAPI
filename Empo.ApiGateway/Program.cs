using Empo.ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration((hostContext, config) =>
        {
            if (hostContext.HostingEnvironment.EnvironmentName.ToLower() == "development")
            {
                config
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true)
                .AddJsonFile($"ocelot.{hostContext.HostingEnvironment.EnvironmentName}.json");
            }
            else
            {
                config.AddJsonFile($"ocelot.json");
            }
            config.AddEnvironmentVariables();
        });
}
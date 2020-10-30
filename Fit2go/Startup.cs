using System.IO;
using Fit2go;
using Fit2go.Clients;
using Fit2go.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Fit2go
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration config = builder.GetContext().Configuration;
            builder.Services.Configure<SportivityOptions>(config);
            builder.Services.Configure<EmailOptions>(config.GetSection(EmailOptions.Section));

            builder.Services.AddHttpClient<SportivityClient>();
            builder.Services.AddHttpClient<SendgridClient>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "config.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}

using System.Reflection;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SaveUp.MigrateDatabase.CLI.Exceptions;
using SaveUp.Web.API;

namespace SaveUp.MigrateDatabase.CLI;

internal class Program
{
    private static string connectionString = "Server=.;Database=save_up;User Id=dev;password=dev;MultipleActiveResultSets=true;Encrypt=False;";

    private static int Main(string[] args)
    {
        CheckMigrationInfrastructure(args).Build();
        Parser.Default.ParseArguments<CommandlineOptions>(args)
            .WithParsed(CheckDatabaseUpdates);

        return 0;
    }

    public static IHostBuilder CheckMigrationInfrastructure(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                (hostContext, services) =>
                {
                    // Set the active provider via configuration
                    ////var configuration = hostContext.Configuration;
                    var provider = "SqlServer";

                    _ = services.AddDbContext<SaveUpDbContext>(
                        options => _ = provider switch
                        {
                            "SqlServer" => options.UseSqlServer(
                                connectionString,
                                x => x.MigrationsAssembly("SaveUp.Web.API")),

                            _ => throw new DatabaseException($"Unsupported provider: {provider}")
                        });
                });
    }

    public static void CheckDatabaseUpdates(CommandlineOptions options)
    {
        var builder = new ConfigurationBuilder().SetBasePath(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly()
                        .Location) ?? string.Empty)
            .AddJsonFile(
                "appsettings.json",
                optional: true,
                reloadOnChange: true);
        builder.AddEnvironmentVariables();

        builder.Build();

        var services = new ServiceCollection();

        if (string.IsNullOrWhiteSpace(
                options.Connectionstring)
            == false)
        {
            connectionString = options.Connectionstring;
        }

        var provider = "SqlServer";

        services
            .AddDbContext<SaveUpDbContext>(
                o => _ = provider switch
                {
                    "SqlServer" => o
                        .UseSqlServer(
                            connectionString,
                            x => x.MigrationsAssembly(
                                "SaveUp.Web.API")),

                    _ => throw new DatabaseException(
                        $"Unsupported provider: {provider}")
                });

        services.AddLogging(
            config =>
            {
                config.AddDebug();
                config.AddConsole();
            });

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider
            .GetRequiredService<
                IServiceScopeFactory>()
            .CreateScope();

        scope.ServiceProvider
            .GetService<SaveUpDbContext>()
            ?.Database.Migrate();

        Console.WriteLine(@"Migration ausgeführt");
    }
}
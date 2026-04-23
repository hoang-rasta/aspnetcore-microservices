using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions
{
    //public static class HostExtensions
    //{
    //    public static IHost MigrateDatabase<TContext>(
    //        this IHost host,
    //        Action<TContext, IServiceProvider> seeder,
    //        int retry = 0
    //    )
    //        where TContext : DbContext
    //    {
    //        int retryForAvailability = retry;

    //        using var scope = host.Services.CreateScope();
    //        var services = scope.ServiceProvider;
    //        var logger = services.GetRequiredService<ILogger<TContext>>();
    //        var context = services.GetRequiredService<TContext>();

    //        try
    //        {
    //            logger.LogInformation(
    //                "Migrating database associated with context {DbContextName}",
    //                typeof(TContext).Name
    //            );

    //            InvokeSeeder(seeder, context, services);

    //            logger.LogInformation(
    //                "Migrated database associated with context {DbContextName}",
    //                typeof(TContext).Name
    //            );
    //        }
    //        catch (SqlException ex)
    //        {
    //            logger.LogError(
    //                ex,
    //                "An error occurred while migrating the database used on context {DbContextName}",
    //                typeof(TContext).Name
    //            );

    //            if (retryForAvailability < 50)
    //            {
    //                retryForAvailability++;
    //                Thread.Sleep(2000);
    //                return host.MigrateDatabase(seeder, retryForAvailability);
    //            }
    //        }

    //        return host;
    //    }

    //    private static void InvokeSeeder<TContext>(
    //        Action<TContext, IServiceProvider> seeder,
    //        TContext context,
    //        IServiceProvider services
    //    )
    //        where TContext : DbContext
    //    {
    //        context.Database.Migrate();
    //        seeder(context, services);
    //    }
    //}
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(
            this IHost host,
            Action<TContext, IServiceProvider> seeder,
            int retry = 0
        )
            where TContext : DbContext
        {
            int retryForAvailability = retry;

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation(
                    "Migrating database associated with context {DbContextName}",
                    typeof(TContext).Name
                );

                InvokeSeeder(seeder, context, services);

                logger.LogInformation(
                    "Migrated database associated with context {DbContextName}",
                    typeof(TContext).Name
                );
            }
            catch (Exception ex) // Catch all exceptions, not just SqlException
            {
                logger.LogError(
                    ex,
                    "An error occurred while migrating the database used on context {DbContextName}",
                    typeof(TContext).Name
                );

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    return host.MigrateDatabase(seeder, retryForAvailability);
                }
                else
                {
                    // After 50 retry attempts, log error and throw
                    logger.LogError("Max retry attempts reached. Migration failed.");
                    throw;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext, IServiceProvider> seeder,
            TContext context,
            IServiceProvider services
        )
            where TContext : DbContext
        {
            context.Database.Migrate(); // Run migrations
            seeder(context, services); // Seed data
        }
    }
}

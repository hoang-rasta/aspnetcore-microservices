using Npgsql;

namespace Discount.API.Persistence.Extensions
{
    public static class DiscountDatabaseExtensions
    {
        public static async Task<WebApplication> InitializeDiscountDatabaseAsync<TContext>(
            this WebApplication app,
            int maxRetry = 50,
            int delayMs = 2000,
            CancellationToken cancellationToken = default
        )
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            var connectionString =
                configuration.GetValue<string>("DatabaseSettings:ConnectionString")
                ?? throw new InvalidOperationException("Missing DatabaseSettings:ConnectionString");

            for (var attempt = 1; attempt <= maxRetry; attempt++)
            {
                try
                {
                    logger.LogInformation(
                        "Migrating PostgreSQL database (attempt {Attempt}/{MaxRetry})...",
                        attempt,
                        maxRetry
                    );

                    await using var connection = new NpgsqlConnection(connectionString);
                    await connection.OpenAsync(cancellationToken);

                    await using var command = connection.CreateCommand();

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    await command.ExecuteNonQueryAsync(cancellationToken);

                    command.CommandText = """
                        CREATE TABLE Coupon(
                            Id SERIAL PRIMARY KEY,
                            ProductName VARCHAR(24) NOT NULL,
                            Description TEXT,
                            Amount INT
                        )
                        """;
                    await command.ExecuteNonQueryAsync(cancellationToken);

                    command.CommandText =
                        "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    await command.ExecuteNonQueryAsync(cancellationToken);

                    command.CommandText =
                        "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    await command.ExecuteNonQueryAsync(cancellationToken);

                    logger.LogInformation("Migrated PostgreSQL database.");
                    return app;
                }
                catch (NpgsqlException ex) when (attempt < maxRetry)
                {
                    logger.LogWarning(ex, "Migration failed. Retry in {Delay}ms...", delayMs);
                    await Task.Delay(delayMs, cancellationToken);
                }
            }

            return app;
        }
    }
}

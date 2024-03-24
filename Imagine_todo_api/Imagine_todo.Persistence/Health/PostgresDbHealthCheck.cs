using Imagine_todo.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Imagine_todo_api.Health
{
    public class PostgresDbHealthCheck : IHealthCheck
    {
        private readonly ApplicationDbContext _dbContext;

        public PostgresDbHealthCheck(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Attempt to connect to the database
                var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);

                return canConnect
                    ? HealthCheckResult.Healthy("Database connection is healthy.")
                    : HealthCheckResult.Unhealthy("Unable to connect to the database.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and mark the health check as unhealthy
                return HealthCheckResult.Unhealthy("An error occurred while checking the database connection.", ex);
            }
        }
    }
}

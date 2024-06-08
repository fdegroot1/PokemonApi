using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using PokemonApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApi.HostedService
{
    public class DatabaseSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PokemonContext>();
            await DbInitializer.Initialize(context);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

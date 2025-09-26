using Store.Core.Shared;

internal sealed class PersistenceInitializationService(IEnumerable<IAppInitializer> initializers) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var initializer in initializers)
        {
            await initializer.Execute();
        }
    }
}
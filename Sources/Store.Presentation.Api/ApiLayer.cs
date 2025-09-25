using Store.Core.Business;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

namespace Store.Presentation.Api;

public static class ApiLayer
{
    public static IServiceCollection AddCurrentProject(this IServiceCollection services)
    {
        return services
            .AddBusiness()
            .AddPersistence()
            .AddTransient<ICurrentAccount, CurrentAccount>();
    }
}
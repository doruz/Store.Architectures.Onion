using Store.Core.Domain;

namespace Store.Architecture.Tests.Core;

public class CoreDomainTests
{
    private static readonly Types DomainTypes = Types.InAssembly(DomainLayer.Assembly);

    [Fact]
    public void RepositoriesInterfaces_Should_ResideInRepositoriesNamespace()
    {
        var result = DomainTypes
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .ResideInNamespace("Store.Core.Domain.Repositories").And().BeInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
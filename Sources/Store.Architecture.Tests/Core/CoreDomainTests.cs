using Store.Core.Domain;

namespace Store.Architecture.Tests.Core;

public class CoreDomainTests
{
    private static readonly Types DomainTypes = Types.InAssembly(DomainLayer.Assembly);

    [Fact]
    public void DomainLayer_Should_NotHaveDependenciesOnLayersAboveDomain()
    {
        var result = DomainTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Core.Business", "Store.Infrastructure", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllRepositoriesContracts_ShouldHaveCorrectNamespaceAndBeInterfaces()
    {
        var result = DomainTypes
            .That().HaveNameEndingWith("Repository")
            .Should().ResideInNamespace("Store.Core.Domain.Repositories")
            .And().BeInterfaces().And().BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
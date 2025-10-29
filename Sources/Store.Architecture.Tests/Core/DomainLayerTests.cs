using Store.Architecture.Tests.Constants;
using Store.Core.Domain;
using Store.Core.Domain.Entities;

namespace Store.Architecture.Tests.Core;

public class DomainLayerTests
{
    [Fact]
    public void DomainEntities_Should_ResideInCorrectNamespace()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .Inherit(typeof(BaseEntity)).Or().AreRecords()
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Core.Domain}.Entities").And().BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainValues_Should_FollowConventions()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .AreRecords()
            .Should()
            .BePublic().And().HaveInitOnlyProperties()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void RepositoriesInterfaces_Should_ResideInCorrectNamespace()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Core.Domain}.Repositories").And().BeInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
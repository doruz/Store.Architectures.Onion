using Store.Architecture.Tests.Constants;
using Store.Core.Business;
using Store.Core.Domain;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

namespace Store.Architecture.Tests;

public class SolutionArchitectureTests
{
    [Fact]
    public void SharedLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.Core.Shared
            .ShouldNot()
            .HaveDependencyOnAny
            (
                SolutionNamespaces.Core.Domain,
                SolutionNamespaces.Core.Business,
                SolutionNamespaces.Infrastructure.All,
                SolutionNamespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.Core.Domain
            .ShouldNot()
            .HaveDependencyOnAny
            (
                SolutionNamespaces.Core.Business,
                SolutionNamespaces.Infrastructure.All,
                SolutionNamespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.Core.Business
            .ShouldNot()
            .HaveDependencyOnAny
            (
                SolutionNamespaces.Infrastructure.All,
                SolutionNamespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void PersistenceLayer_Should_NotHaveDependenciesOnLayersAboveBusiness()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .ShouldNot()
            .HaveDependencyOnAny
            (
                SolutionNamespaces.Core.Business,
                SolutionNamespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
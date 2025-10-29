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
        var result = Types.InAssembly(SharedLayer.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny
            (
                Namespaces.Core.Domain,
                Namespaces.Core.Business,
                Namespaces.Infrastructure.All,
                Namespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = Types.InAssembly(DomainLayer.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny
            (
                Namespaces.Core.Business,
                Namespaces.Infrastructure.All,
                Namespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = Types.InAssembly(BusinessLayer.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny
            (
                Namespaces.Infrastructure.All,
                Namespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void PersistenceLayer_Should_NotHaveDependenciesOnLayersAboveBusiness()
    {
        var result = Types.InAssembly(PersistenceLayer.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny
            (
                Namespaces.Core.Business,
                Namespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
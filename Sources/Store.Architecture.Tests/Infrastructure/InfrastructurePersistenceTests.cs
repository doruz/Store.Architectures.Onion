using System.Reflection.Metadata;
using Store.Core.Business;
using Store.Infrastructure.Persistence;
using TypeDefinition = Mono.Cecil.TypeDefinition;

namespace Store.Architecture.Tests.Infrastructure;

public class InfrastructurePersistenceTests
{
    private static readonly Types PersistenceTypes = Types.InAssembly(InfrastructureLayer.Assembly);

    [Fact]
    public void InfrastructureLayer_Should_NotHaveDependenciesOnLayersAboveBusiness()
    {
        var result = PersistenceTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Core.Business", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllTypes_Should_NotBeExposedExternally()
    {
        var result = PersistenceTypes
            .That().DoNotHaveName(nameof(InfrastructureLayer))
            .ShouldNot().BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllTypes_Should_NotBeInterfacesOrAbstractions()
    {
        var result = PersistenceTypes
            .That().DoNotHaveName(nameof(InfrastructureLayer))
            .ShouldNot().BeInterfaces().And().NotBeAbstract()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllRepositories_Should_HaveNameEndingWithRepository()
    {
        const string repositoriesSuffix = "Repository";

        var result = PersistenceTypes
            .That().HaveNameEndingWith(repositoriesSuffix)
            .Should().NotBePublic().And().BeSealed().And().MeetCustomRule(new NameEndsWith(repositoriesSuffix))
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
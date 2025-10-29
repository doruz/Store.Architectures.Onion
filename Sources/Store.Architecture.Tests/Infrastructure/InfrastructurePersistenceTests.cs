using System.Reflection.Metadata;
using Store.Core.Business;
using Store.Infrastructure.Persistence;

namespace Store.Architecture.Tests.Infrastructure;

public class InfrastructurePersistenceTests
{
    private static readonly Types PersistenceTypes = Types.InAssembly(PersistenceLayer.Assembly);

    [Fact]
    public void AllTypes_Should_NotBeExposedExternally()
    {
        var result = PersistenceTypes
            .That()
            .DoNotHaveName(nameof(PersistenceLayer))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllTypes_Should_NotBeInterfacesOrAbstractions()
    {
        var result = PersistenceTypes
            .That()
            .DoNotHaveName(nameof(PersistenceLayer))
            .ShouldNot()
            .BeInterfaces().And().NotBeAbstract()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllRepositories_Should_HaveNameEndingWithRepository()
    {
        const string repositoriesSuffix = "Repository";

        var result = PersistenceTypes
            .That()
            .HaveNameEndingWith(repositoriesSuffix)
            .Should()
            .NotBePublic().And().BeSealed().And().HaveNameEndingWith(repositoriesSuffix)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
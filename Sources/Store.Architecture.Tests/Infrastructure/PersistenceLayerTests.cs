using System.Reflection.Metadata;
using Store.Core.Business;
using Store.Infrastructure.Persistence;

namespace Store.Architecture.Tests.Infrastructure;

public class PersistenceLayerTests
{
    [Fact]
    public void AllTypes_Should_NotBeExposedExternally()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .DoNotHaveName(nameof(PersistenceLayer))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    // TODO: to add a custom rule which finds types that are implementing an interface with certain naming pattern
    [Fact]
    public void AllRepositories_Should_HaveNameEndingWithRepository()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .BeSealed().And().ImplementInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
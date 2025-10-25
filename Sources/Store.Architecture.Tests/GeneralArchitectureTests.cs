global using FluentAssertions;
global using NetArchTest.Rules;
using Store.Core.Business;
using Store.Core.Shared;

namespace Store.Architecture.Tests;

public class GeneralArchitectureTests
{
    private static readonly Types AllTypes = Types.FromPath(Directory.GetCurrentDirectory());

    [Fact]
    public void Extensions_Should_BeStatic()
    {
        var result = AllTypes
            .That()
            .ResideInNamespaceMatching("Store.*").And().HaveNameEndingWith("Extensions")
            .Should()
            .BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Interfaces_Should_BePublicAndFollowNamingConvention()
    {
        var result = AllTypes
            .That()
            .ResideInNamespaceMatching("Store.*").And().AreInterfaces()
            .Should()
            .BePublic().And().HaveNameStartingWith("I")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AppInitializers_Should_FollowNamingConvention()
    {
        var result = AllTypes
            .That()
            .ImplementInterface(typeof(IAppInitializer))
            .Should()
            .MeetCustomRule(new NameEndsWith("Initializer"))
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AppInitializers_Should_NotBeExposed()
    {
        var result = AllTypes
            .That()
            .ImplementInterface(typeof(IAppInitializer))
            .Should()
            .NotBePublic().And().BeSealed()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
using Store.Core.Business;

namespace Store.Architecture.Tests.Core;

public class CoreBusinessTests
{
    private static readonly Types BusinessTypes = Types.InAssembly(BusinessLayer.Assembly);

    [Fact]
    public void BusinessLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = BusinessTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Infrastructure", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_FollowConventions()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .BePublic().And().BeSealed().And().BeClasses()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_NotImplementInterfaces()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .MeetCustomRule(new ImplementInterfaces())
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_FollowConventions()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Model")
            .Should()
            .BePublic().And().BeClasses().And().NotBeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_NotExposeDomainEntities()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Model")
            .ShouldNot()
            .MeetCustomRule(new HavePropertiesAndFieldsFromNamespace("Store.Core.Domain.Entities"))
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_InitializeAllPropertiesUsingInit()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Model")
            .Should()
            .MeetCustomRule(new HaveInitOnlyProperties())
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllErrorsAndMappers_Should_FollowConventions()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith(@"\b\w+(Mapper|Errors)\b")
            .Should()
            .NotBePublic().And().BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
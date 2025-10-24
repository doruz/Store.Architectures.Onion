using Store.Core.Business;

namespace Store.Architecture.Tests.Core;

public class CoreBusinessTests
{
    private static readonly Types BusinessTypes = Types.InAssembly(BusinessLayer.Assembly);

    [Fact]
    public void BusinessLayer_Should_NotHaveDependenciesOnLayersAboveBusiness()
    {
        var result = BusinessTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Infrastructure", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllServices_Should_BeExposedAndNotHaveAnAbstractionLayer()
    {
        var result = BusinessTypes
            .That().HaveNameEndingWith("Service")
            .Should().BePublic().And().BeClasses().And().BeSealed()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllModels_Should_BeExposedForExternalUse()
    {
        var result = BusinessTypes
            .That().HaveNameEndingWith("Model")
            .Should().BePublic().And().BeClasses().And().NotBeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllErrorsAndMappers_Should_BeUsedInternally()
    {
        var result = BusinessTypes
            .That().HaveNameEndingWith("\\b\\w+(Mapper|Errors)\\b")
            .Should().NotBePublic().And().BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
using NetArchTest.Rules.Policies;
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
            .ImplementInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }


    [Fact]
    public void BusinessServices_Should_UseDomainTypesOnPublicMethods()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .HaveDependencyOnPublicMethodsFrom("Domain")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    // TODO: to continue with this
    [Fact]
    public void Test()
    {
        var businessServicesPolicy = Policy
            .Define("Business Services Policies", "Enforce all")
            .For(BusinessTypes)

            .Add(types => types
                    .That()
                    .HaveNameEndingWith("Service")
                    .ShouldNot()
                    .HaveDependencyOnPublicMethodsFrom("Domain"),
                "",
                "Business services should not depend on domain types in their public methods"
            )

            .Add(types => types
                .That()
                .HaveNameEndingWith("Service")
                .ShouldNot()
                .ImplementInterfaces(),
                "",
                "Business services should implement any interfaces"
            );

        businessServicesPolicy.Evaluate().ShouldBeSuccessful();
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
    public void BusinessModels_Should_UseDomainTypesOnProperties()
    {
        var result = BusinessTypes
            .That()
            .HaveNameEndingWith("Model")
            .ShouldNot()
            .HaveDependencyOnPublicPropertiesFrom("Domain")
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
            .HaveInitOnlyProperties()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ErrorsAndMappers_Should_FollowConventions()
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
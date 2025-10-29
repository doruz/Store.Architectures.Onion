using NetArchTest.Rules.Policies;
using Store.Core.Business;

namespace Store.Architecture.Tests.Core;

public class BusinessLayerTests
{
    [Fact]
    public void BusinessServices_Should_FollowConventions()
    {
        var result = SolutionTypes.Core.Business
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
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .ImplementInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_NotUseDomainTypesOnPublicMethods()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .HaveDependencyOnPublicMethodsFrom(SolutionNamespaces.Core.Domain)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    // TODO: to continue with this
    [Fact]
    public void Test()
    {
        var businessServicesPolicy = Policy
            .Define("Business Services Policies", "Enforce all")
            .For(SolutionTypes.Core.Business)

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
        var result = SolutionTypes.Core.Business
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
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Model")
            .ShouldNot()
            .HaveDependencyOnPublicPropertiesFrom(SolutionNamespaces.Core.Domain)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_InitializeAllPropertiesUsingInit()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Model")
            .Should()
            .HaveInitOnlyProperties()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    // TODO: models should be records

    [Fact]
    public void ErrorsAndMappers_Should_FollowConventions()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith(@"\b\w+(Mapper|Errors)\b")
            .Should()
            .NotBePublic().And().BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
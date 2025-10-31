using Microsoft.AspNetCore.Mvc;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using Store.Infrastructure.Persistence;

namespace Store.Architecture.Tests;

public class ApiLayerTests
{
    [Fact]
    public void ApiController_Should_NotDependOnDomain()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(ControllerBase))
            .ShouldNot()
            .HaveDependencyOn(SolutionNamespaces.Core.Domain)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ApiControllersActions_Should_ReturnIActionResult()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .PublicMethodsReturn<IActionResult>()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AdminControllersRoutes_Should_BePrefixedWithAdmin()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(ControllerBase)).And().HaveNameStartingWith("Admin")
            .Should()
            .HaveAllRoutesPrefixedWith("admins")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CustomerControllersRoutes_Should_BePrefixedWithCustomer()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(BaseApiController)).And().HaveNameStartingWith("Customer")
            .Should()
            .HaveAllRoutesPrefixedWith("customers/current")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
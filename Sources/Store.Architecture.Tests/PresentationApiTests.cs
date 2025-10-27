using Microsoft.AspNetCore.Mvc;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using Store.Infrastructure.Persistence;

namespace Store.Architecture.Tests;

public class PresentationApiTests
{
    private static readonly Types ApiTypes = Types.InAssembly(ApiLayer.Assembly);

    [Fact]
    public void ApiControllersActions_Should_ReturnIActionResult()
    {
        var result = ApiTypes
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .AllPublicMethodsReturn<IActionResult>()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ApiControllerActions_Should_NotUseDomainTypes()
    {
        var result = ApiTypes
            .That()
            .Inherit(typeof(ControllerBase))
            .ShouldNot()
            .HaveDependencyOnPublicMethodsFrom("Domain")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AdminControllersRoutes_Should_BePrefixedWithAdmin()
    {
        var result = ApiTypes
            .That()
            .Inherit(typeof(ControllerBase)).And().HaveNameStartingWith("Admin")
            .Should()
            .HaveRoutePrefix("admins")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CustomerControllersRoutes_Should_BePrefixedWithCustomer()
    {
        var result = ApiTypes
            .That()
            .Inherit(typeof(BaseApiController)).And().HaveNameStartingWith("Customer")
            .Should()
            .HaveRoutePrefix("customers/current")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
using Store.Core.Shared;

namespace Store.Architecture.Tests.Core;

public class CoreSharedTests
{
    private static readonly Types SharedTypes = Types.InAssembly(SharedLayer.Assembly);

    [Fact]
    public void SharedLayer_Should_NotHaveDependenciesOnLayersAboveShared()
    {
        var result = SharedTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Core.Domain","Store.Core.Business", "Store.Infrastructure", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllTypes_Should_HaveFixedNamespace()
    {
        var result = SharedTypes
            .Should().ResideInNamespaceStartingWith("Store.Core.Shared")
            .And().ResideInNamespaceEndingWith("Store.Core.Shared")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
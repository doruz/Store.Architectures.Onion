using Store.Core.Shared;

namespace Store.Architecture.Tests.Core;

public class CoreSharedTests
{
    private static readonly Types SharedTypes = Types.InAssembly(SharedLayer.Assembly);

    [Fact]
    public void SharedLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SharedTypes
            .ShouldNot()
            .HaveDependencyOnAny("Store.Core.Domain","Store.Core.Business", "Store.Infrastructure", "Store.Presentation")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
namespace SimpleAsyncRepository.InMemory.UnitTests;

using Shouldly;

public class InMemoryBaseRepositoryTests
{
    [Fact]
    public async Task ShouldlyBeEmptyAfterInstantiation ()
    {
        // Arrange + Act
        InMemoryBaseRepository<TestModel> repository = new ();

        // Assert
        IList<TestModel> content = await repository.GetAll ();
        content.ShouldBeEmpty ();
    }

    [Fact]
    public async Task ShouldlyReturnCountOfZeroAfterInstantiation ()
    {
        // Arrange + Act
        InMemoryBaseRepository<TestModel> repository = new ();

        // Assert
        int count = await repository.Count ();
        count.ShouldBe ( 0 );
    }
}

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

    [Fact]
    public async Task ShouldAddMultipleEntitiesSimultaneously ()
    {
        // Arrange
        string testName = "Test";
        IEnumerable<TestModel> models = new int[100]
            .Select ( i => new TestModel { Name = testName } );
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await Task.WhenAll ( models.Select ( repository.Upsert ) );
        int count = await repository.Count ();
        IList<TestModel> content = await repository.GetAll ();
        IEnumerable<Guid> guids = content.Select ( model => model.Id );
        bool isAllNamedCorrectly = content.All ( model => model.Name == testName );

        // Assert
        count.ShouldBe ( 100 );
        content.ShouldNotBeEmpty ();
        isAllNamedCorrectly.ShouldBeTrue ();
        guids.ShouldBeUnique ();
    }

    [Fact]
    public async Task ShouldUpdateSingleItemByIdCorrectly ()
    {
        // Arrange
        string nameBefore = "TestBefore";
        string nameAfter = "TestAfter";
        Guid guid = Guid.NewGuid ();
        TestModel modelBefore = new () { Id = guid, Name = nameBefore };
        TestModel modelAfter = new () { Id = guid, Name = nameAfter };
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await repository.Upsert ( modelBefore );
        await repository.Upsert ( modelAfter );
        TestModel? actualModelAfter = await repository.GetById ( guid );

        // Assert
        actualModelAfter.ShouldNotBeNull ();
        actualModelAfter.Name.ShouldBe ( nameAfter );
    }
}

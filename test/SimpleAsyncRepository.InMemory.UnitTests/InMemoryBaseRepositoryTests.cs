namespace SimpleAsyncRepository.InMemory.UnitTests;

using Shouldly;

public class InMemoryBaseRepositoryTests
{
    #region Instantiation

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

    #endregion

    #region Upsert

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

    [Fact]
    public async Task ShouldCreateGuidsForInsertedItems ()
    {
        // Arrange
        Guid existingGuid = Guid.NewGuid ();
        TestModel model1 = new () { Id = existingGuid };
        TestModel model2 = new ();
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await repository.Upsert ( model1 );
        await repository.Upsert ( model2 );
        IList<TestModel> content = await repository.GetAll ();
        IEnumerable<Guid> guids = content.Select ( x => x.Id );

        // Assert
        content.ShouldNotBeEmpty ();
        guids.ShouldBeUnique ();
        guids.ShouldNotContain ( Guid.Empty );
    }

    #endregion

    #region Removal

    [Fact]
    public async Task ShouldRemoveExistingItem ()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid ();
        Guid guid2 = Guid.NewGuid ();
        TestModel model1 = new () { Id = guid1 };
        TestModel model2 = new () { Id = guid2 };
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await repository.Upsert ( model1 );
        await repository.Upsert ( model2 );
        await repository.RemoveById ( guid1 );
        IList<TestModel> contents = await repository.GetAll ();
        TestModel? actualModel1 = await repository.GetById ( guid1 );
        TestModel? actualModel2 = await repository.GetById ( guid2 );

        // Assert
        contents.Count.ShouldBe ( 1 );
        actualModel1.ShouldBeNull ();
        actualModel2.ShouldNotBeNull ();
        actualModel2.Id.ShouldBe ( guid2 );
    }

    [Fact]
    public async Task ShouldNotThrowOnRemoveNonexistingItem ()
    {
        // Arrange
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await repository.RemoveById ( Guid.NewGuid () );

        // Assert
        // If nothing was thrown, then this test has passed
    }

    [Fact]
    public async Task ShouldNotTouchWrongItemsOnInvalidRemove ()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid ();
        Guid guid2 = Guid.NewGuid ();
        Guid guid3 = Guid.NewGuid ();
        TestModel model1 = new () { Id = guid1 };
        TestModel model2 = new () { Id = guid2 };
        InMemoryBaseRepository<TestModel> repository = new ();

        // Act
        await repository.Upsert ( model1 );
        await repository.Upsert ( model2 );
        await repository.RemoveById ( guid3 );
        IList<TestModel> contents = await repository.GetAll ();
        TestModel? actualModel1 = await repository.GetById ( guid1 );
        TestModel? actualModel2 = await repository.GetById ( guid2 );

        // Assert
        contents.Count.ShouldBe ( 2 );
        actualModel1.ShouldNotBeNull ();
        actualModel1.Id.ShouldBe ( guid1 );
        actualModel2.ShouldNotBeNull ();
        actualModel2.Id.ShouldBe ( guid2 );
    }

    #endregion
}

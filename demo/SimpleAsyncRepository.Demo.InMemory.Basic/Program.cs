using SimpleAsyncRepository.Demo.InMemory.Basic;
using SimpleAsyncRepository.InMemory;

// Create an instance based on your type
InMemoryBaseRepository<MyModel> myRepository = new ();

await PrintHelper.PrintRepositoryState ( "Repository initialised.", myRepository );

// Create an item
// Notice the empty Id field
MyModel item1 = new ()
{
    Foo = "Some string content",
    Bar = 27,
    Baz = false,
};

Console.WriteLine ( "Item before adding to repository:" );
PrintHelper.PrintItem ( item1 );
Console.WriteLine ();

// Add the item
await myRepository.Upsert ( item1 );
await PrintHelper.PrintRepositoryState ( "One item added.", myRepository );

// Update the item
Guid item1Id = ( await myRepository.GetAll () ).First ().Id;
item1 = new ()
{
    Id = item1Id, // Keep the Id equal to update the existing item
    Foo = "Foo",
    Bar = -185,
    Baz = true,
};

await myRepository.Upsert ( item1 );
await PrintHelper.PrintRepositoryState ( "One item updated.", myRepository );

// Add another item
// Notice that how the Guid has been set beforehand this time.
Guid item2Id = Guid.NewGuid ();
MyModel item2 = new ()
{
    Id = item2Id,
    Foo = "Second item",
    Bar = 0,
    Baz = true,
};

await myRepository.Upsert ( item2 );
await PrintHelper.PrintRepositoryState ( "Second item inserted.", myRepository );

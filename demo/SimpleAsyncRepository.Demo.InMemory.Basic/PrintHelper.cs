namespace SimpleAsyncRepository.Demo.InMemory.Basic;

using SimpleAsyncRepository.InMemory;

internal static class PrintHelper
{
    public static void PrintItem ( MyModel item )
    {
        Console.WriteLine ( $"Id: {item.Id}" );
        Console.WriteLine ( $"Foo: {item.Foo}" );
        Console.WriteLine ( $"Bar: {item.Bar}" );
        Console.WriteLine ( $"Baz: {item.Baz}" );
    }

    public static async Task PrintRepositoryState ( string message, InMemoryBaseRepository<MyModel> repository )
    {
        Console.WriteLine ( $"=============== {message} ===============" );

        int count = await repository.Count ();
        Console.WriteLine ( $"Number of entities contained: {count}" );
        if ( count == 0 )
        {
            Console.WriteLine ();
            return;
        }

        IList<MyModel> allItems = await repository.GetAll ();

        for ( int i = 0; i < allItems.Count; ++i )
        {
            Console.WriteLine ( $"=============== # {i}\n" );
            MyModel item = allItems[i];
            PrintItem ( item );
            Console.WriteLine ();
        }

        Console.WriteLine ( "=============== End" );

        Console.WriteLine ();
    }
}

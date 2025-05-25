namespace SimpleAsyncRepository.Demo.InMemory.Basic;

using SimpleAsyncRepository.Abstractions;

internal class MyModel : IModel
{
    // This one is mandatory
    public Guid Id { get; set; }

    // These are your payload, put whatever properties you want here
    public string? Foo { get; set; }
    public int Bar { get; set; }
    public bool Baz { get; set; }
}

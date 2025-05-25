
using System;
using SimpleAsyncRepository.Abstractions;

namespace SimpleAsyncRepository.InMemory.UnitTests;
/// <summary>
/// Minimalistic model type for testing purposes
/// </summary>
public class TestModel : IModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

namespace SimpleAsyncRepository.InMemory.UnitTests;

using System;
using SimpleAsyncRepository.Abstractions;

/// <summary>
/// Minimalistic model type for testing purposes
/// </summary>
public class TestModel : IModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

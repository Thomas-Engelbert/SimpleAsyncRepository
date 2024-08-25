# Simple Async Repository

This is a super simplistic and straight forward implementation of the repository pattern. I found myself doing it over and over again and again for different projects and so I decided to just turn it into a NuGet package.

# Getting Started

## Installation process

Simply add the desired NuGet package to your project. At the moment there's `SimpleAsyncRepository.Abstractions` and `SimpleAsyncRepository.InMemory`.

## Usage
1. Create your model type (i.e. `MyModel`). It must implement `SimpleAsyncRepository.Abstractions.IModel`.
1. Create an interface defining your repository type (i.e. `IMyModelRepository`). It should inherit from `SimpleAsyncRepository.Abstractions.IRepository<T>`.
1. Create an implementation of your repository by inheriting from the implementation of your choice (i.e. `InMemoryBaseRepository`). Make it also inherit from your interface.
1. Register your interface and implementation with the DI system of your choice.

#### MyModel.cs
```csharp
using SimpleAsyncRepository.Abstractions;

public class MyModel : IModel
{
    public Guid Id { get; set; }
    public string? Foo { get; set; }
    public int Bar { get; set; }
    public bool Baz { get; set; }
}
```

#### IMyModelRepository.cs
```csharp
using SimpleAsyncRepository.Abstractions;

public interface IMyModelRepository : IRepository<MyModel> {}
```

#### MyModelRepository.cs
```csharp
using SimpleAsyncRepository.InMemory;

public class MyModelRepository<MyModel> : InMemoryBaseRepository<MyModel>, IMyModelRepository {}
```

## Latest releases
1.0.0 - Initial Release

# Contribute
Thank you for your consideration. At the moment I'm not looking for contributions.

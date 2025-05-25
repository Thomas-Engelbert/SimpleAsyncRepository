using System;

namespace SimpleAsyncRepository.Abstractions;

public interface IModel
{
    Guid Id { get; set; }
}

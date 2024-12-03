namespace Todo.Data.Repositories;

public interface IUnitOfWork : IDisposable
{
    ITodoItemRepository TodoItems { get; }
    Task<int> CompleteAsync();
}
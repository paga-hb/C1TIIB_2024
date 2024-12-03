namespace Todo.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public ITodoItemRepository TodoItems { get; private set; }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        TodoItems = new TodoItemRepository(_dbContext);
    }

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
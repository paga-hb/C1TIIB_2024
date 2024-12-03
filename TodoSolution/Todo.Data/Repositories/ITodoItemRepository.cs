using Todo.Data.Entities;

namespace Todo.Data.Repositories;

public interface ITodoItemRepository : IRepository<ApplicationDbContext, TodoItem>
{
}
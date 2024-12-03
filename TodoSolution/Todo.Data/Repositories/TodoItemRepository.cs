using Todo.Data.Entities;

namespace Todo.Data.Repositories;

public class TodoItemRepository : Repository<ApplicationDbContext, TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
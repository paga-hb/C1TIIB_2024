using Microsoft.EntityFrameworkCore;
using Todo.Data.Entities;

namespace Todo.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        var todoItem1 = new TodoItem
        {
            Id = 1,
            Name = "Learn app development",
            Notes = "Take Microsoft Learn Courses",
            Comments = "Maybe take Coursera Courses too?",
            Completed = true
        };

        var todoItem2 = new TodoItem
        {
            Id = 2,
            Name = "Develop apps",
            Notes = "Use Visual Studio and Visual Studio for Mac",
            Comments = "Maybe use Visual Studio Code instead?",
            Completed = false
        };

        var todoItem3 = new TodoItem
        {
            Id = 3,
            Name = "Publish apps",
            Notes = "All app stores",
            Completed = false
        };

        builder.Entity<TodoItem>().HasData(todoItem1, todoItem2, todoItem3);
    }
}
using System;

namespace Todo.Blazor.BDDTests.Models;

public class TodoItem
{
    public string TaskName { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public bool Done { get; set; }
}
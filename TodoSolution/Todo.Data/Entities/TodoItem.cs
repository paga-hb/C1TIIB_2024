using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Data.Entities;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(25)]
    public required string Name { get; set; }

    [Required]
    [StringLength(500)]
    public required string Notes { get; set; }

    [StringLength(500)]
    public string? Comments { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public required bool Completed { get; set; } = false;
}
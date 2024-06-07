using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Domain.Entities;

public class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(50)] [Unicode] public string Name { get; set; } = null!;
    [StringLength(255)] public string Image { get; set; } = null!;
    public virtual ICollection<Film> Films { get; set; } = new HashSet<Film>();
}
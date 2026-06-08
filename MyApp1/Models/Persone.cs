using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyApp1.Models;

[Table("Persone")]
public partial class Persone
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    [StringLength(50)]
    public string Nome { get; set; } = null!;

    [Column("dataNascita")]
    public DateOnly DataNascita { get; set; }
}

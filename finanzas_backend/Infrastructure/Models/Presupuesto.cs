using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Presupuesto")]
public class Presupuesto
{
    [Key]
    [Column("id_presupuesto")]
    public int IdPresupuesto { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("anio")]
    public int Anio { get; set; }

    [Column("mes")]
    public int Mes { get; set; }

    [Column("estado")]
    public bool Estado { get; set; } = true;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    public ICollection<PresupuestoItem> Items { get; set; } = new List<PresupuestoItem>();
}

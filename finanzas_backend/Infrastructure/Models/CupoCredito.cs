using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Cupo_Credito")]
public class CupoCredito
{
    [Key]
    [Column("id_cupo_credito")]
    public int IdCupoCredito { get; set; }

    [Column("id_credito")]
    public int IdCredito { get; set; }

    [Column("monto", TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    // Navegación
    [ForeignKey(nameof(IdCredito))]
    public Credito Credito { get; set; } = null!;
}

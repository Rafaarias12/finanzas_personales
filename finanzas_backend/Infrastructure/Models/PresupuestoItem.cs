using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Presupuesto_Item")]
public class PresupuestoItem
{
    [Key]
    [Column("id_presupuesto_item")]
    public int IdPresupuestoItem { get; set; }

    [Column("id_presupuesto")]
    public int IdPresupuesto { get; set; }

    [Column("id_categoria_gasto")]
    public int? IdCategoriaGasto { get; set; }

    [Column("id_tipo_gasto")]
    public int? IdTipoGasto { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("tipo_item")]
    public string TipoItem { get; set; } = string.Empty;

    [Column("monto_objetivo", TypeName = "decimal(18,2)")]
    public decimal MontoObjetivo { get; set; }

    [Column("monto_real", TypeName = "decimal(18,2)")]
    public decimal MontoReal { get; set; } = 0;

    // Navegación
    [ForeignKey(nameof(IdPresupuesto))]
    public Presupuesto Presupuesto { get; set; } = null!;

    [ForeignKey(nameof(IdCategoriaGasto))]
    public CategoriaGasto? CategoriaGasto { get; set; }

    [ForeignKey(nameof(IdTipoGasto))]
    public TipoGasto? TipoGasto { get; set; }
}

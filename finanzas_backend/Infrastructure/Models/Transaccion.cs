using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Transaccion")]
public class Transaccion
{
    [Key]
    [Column("id_transaccion")]
    public int IdTransaccion { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(500)]
    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Column("monto", TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("id_tipo_transaccion")]
    public int IdTipoTransaccion { get; set; }

    [Column("id_metodo_pago")]
    public int IdMetodoPago { get; set; }

    [Column("id_categoria_gasto")]
    public int? IdCategoriaGasto { get; set; }

    [Column("id_tipo_gasto")]
    public int? IdTipoGasto { get; set; }

    [Column("id_detalle_cuotas")]
    public int? IdDetalleCuotas { get; set; }

    [Column("id_credito")]
    public int? IdCredito { get; set; }

    [Column("id_deuda")]
    public int? IdDeuda { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    [ForeignKey(nameof(IdTipoTransaccion))]
    public TipoTransaccion TipoTransaccion { get; set; } = null!;

    [ForeignKey(nameof(IdMetodoPago))]
    public MetodoPago MetodoPago { get; set; } = null!;

    [ForeignKey(nameof(IdCategoriaGasto))]
    public CategoriaGasto? CategoriaGasto { get; set; }

    [ForeignKey(nameof(IdTipoGasto))]
    public TipoGasto? TipoGasto { get; set; }

    [ForeignKey(nameof(IdDetalleCuotas))]
    public DetalleCuotas? DetalleCuotas { get; set; }

    [ForeignKey(nameof(IdCredito))]
    public Credito? Credito { get; set; }

    [ForeignKey(nameof(IdDeuda))]
    public Deuda? Deuda { get; set; }

    public SoporteFactura? SoporteFactura { get; set; }
}

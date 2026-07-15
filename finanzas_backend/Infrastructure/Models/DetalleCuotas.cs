using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Detalle_Cuotas")]
public class DetalleCuotas
{
    [Key]
    [Column("id_detalle_cuotas")]
    public int IdDetalleCuotas { get; set; }

    [Required]
    [MaxLength(500)]
    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Column("monto_total", TypeName = "decimal(18,2)")]
    public decimal MontoTotal { get; set; }

    [Column("cantidad_cuotas")]
    public int CantidadCuotas { get; set; }

    [Column("monto_cuota", TypeName = "decimal(18,2)")]
    public decimal MontoCuota { get; set; }

    [Column("cuotas_pagadas")]
    public int CuotasPagadas { get; set; } = 0;

    [Column("pagada_completamente")]
    public bool PagadaCompletamente { get; set; } = false;

    [Column("fecha_compra")]
    public DateTime FechaCompra { get; set; }

    [Column("fecha_ultima_cuota")]
    public DateTime? FechaUltimaCuota { get; set; }

    // Navegación
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

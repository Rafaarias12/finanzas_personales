using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("SoporteFactura")]
public class SoporteFactura
{
    [Key]
    [Column("id_factura")]
    public int IdFactura { get; set; }

    [Column("id_transaccion")]
    public int? IdTransaccion { get; set; }

    [Required]
    [MaxLength(500)]
    [Column("nombre_archivo")]
    public string NombreArchivo { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("tipo_mime")]
    public string TipoMime { get; set; } = string.Empty;

    [Column("tamano_bytes")]
    public int TamanoBytes { get; set; }

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; }

    [Column("procesado_ia")]
    public bool ProcesadoIa { get; set; } = false;

    [MaxLength(1000)]
    [Column("direccion")]
    public string? Direccion { get; set; }

    // Navegación
    [ForeignKey(nameof(IdTransaccion))]
    public Transaccion? Transaccion { get; set; }
}

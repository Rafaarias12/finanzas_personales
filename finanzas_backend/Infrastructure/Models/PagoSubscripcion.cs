using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Pago_Subscripcion")]
public class PagoSubscripcion
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_subscripcion")]
    public int IdSubscripcion { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("mes")]
    public string Mes { get; set; } = string.Empty;

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("precio", TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Column("fechaInicio")]
    public DateTime FechaInicio { get; set; }

    [Column("fechaFin")]
    public DateTime FechaFin { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("estado")]
    public string Estado { get; set; } = string.Empty;

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    [ForeignKey(nameof(IdSubscripcion))]
    public Subscripcion Subscripcion { get; set; } = null!;
}

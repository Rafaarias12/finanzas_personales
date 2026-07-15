using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Deudas")]
public class Deuda
{
    [Key]
    [Column("id_deuda")]
    public int IdDeuda { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("monto_original", TypeName = "decimal(18,2)")]
    public decimal MontoOriginal { get; set; }

    [Column("saldo_actual", TypeName = "decimal(18,2)")]
    public decimal SaldoActual { get; set; }

    [Column("tasa_interes", TypeName = "decimal(5,2)")]
    public decimal? TasaInteres { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

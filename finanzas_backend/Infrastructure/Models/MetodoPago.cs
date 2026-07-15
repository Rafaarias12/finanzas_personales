using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Metodo_Pago")]
public class MetodoPago
{
    [Key]
    [Column("id_metodo")]
    public int IdMetodo { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

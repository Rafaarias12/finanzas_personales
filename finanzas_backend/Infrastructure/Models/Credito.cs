using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Credito")]
public class Credito
{
    [Key]
    [Column("id_credito")]
    public int IdCredito { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("entidad")]
    public string Entidad { get; set; } = string.Empty;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario Usuario { get; set; } = null!;

    public ICollection<CupoCredito> CuposCredito { get; set; } = new List<CupoCredito>();
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

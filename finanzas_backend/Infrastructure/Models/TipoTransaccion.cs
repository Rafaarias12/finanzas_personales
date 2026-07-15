using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Tipo_Transaccion")]
public class TipoTransaccion
{
    [Key]
    [Column("id_tipo_transaccion")]
    public int IdTipoTransaccion { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

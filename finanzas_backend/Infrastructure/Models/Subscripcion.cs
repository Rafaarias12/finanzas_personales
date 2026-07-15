using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Subscripcion")]
public class Subscripcion
{
    [Key]
    [Column("id_subscripcion")]
    public int IdSubscripcion { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("precio", TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Column("estado")]
    public bool Estado { get; set; } = true;

    // Navegación
    public ICollection<PagoSubscripcion> PagosSubscripcion { get; set; } = new List<PagoSubscripcion>();
}

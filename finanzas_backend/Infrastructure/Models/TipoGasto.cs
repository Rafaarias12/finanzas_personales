using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Tipo_Gasto")]
public class TipoGasto
{
    [Key]
    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    // Navegación
    [ForeignKey(nameof(IdUsuario))]
    public Usuario? Usuario { get; set; }

    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    public ICollection<PresupuestoItem> PresupuestosItems { get; set; } = new List<PresupuestoItem>();
}

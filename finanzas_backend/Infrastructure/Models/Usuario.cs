using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finanzas_backend.Infrastructure.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("nombre_usuario")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    [Column("correo")]
    public string Correo { get; set; } = string.Empty;

    [Column("estado")]
    public bool Estado { get; set; } = true;

    [MaxLength(500)]
    [Column("uid_auth")]
    public string? UidAuth { get; set; }

    // Navegación
    public ICollection<PagoSubscripcion> PagosSubscripcion { get; set; } = new List<PagoSubscripcion>();
    public ICollection<MetodoPago> MetodosPago { get; set; } = new List<MetodoPago>();
    public ICollection<CategoriaGasto> CategoriasGasto { get; set; } = new List<CategoriaGasto>();
    public ICollection<TipoGasto> TiposGasto { get; set; } = new List<TipoGasto>();
    public ICollection<Credito> Creditos { get; set; } = new List<Credito>();
    public ICollection<Deuda> Deudas { get; set; } = new List<Deuda>();
    public ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

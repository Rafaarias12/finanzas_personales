namespace finanzas_backend.Infrastructure.Models
{
    public class AuditableEntity
    {
        public string CreadoPor { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

        public string? ActualizadoPor { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}

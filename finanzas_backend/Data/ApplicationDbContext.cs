using EFCore.BulkExtensions;
using finanzas_backend.Common.Constanst;
using finanzas_backend.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace finanzas_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor? httpContextAccessor = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<TipoTransaccion> TipoTransacciones { get; set; }
        public DbSet<TipoGasto> TipoGastos { get; set; }
        public DbSet<Subscripcion> Subscripciones { get; set; }
        public DbSet<PresupuestoItem> PresupuestoItems { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<PagoSubscripcion> PagoSubscripciones { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Deuda> Deudas { get; set; }
        public DbSet<DetalleCuotas> DetalleCuotas { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<CategoriaGasto> CategoriaGastos { get; set; }
        public DbSet<CupoCredito> CuposCredito { get; set; }
        public DbSet<SoporteFactura> SoporteFacturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. CONFIGURACIÓN CENTRAL DEL USUARIO Y PAGOS
            // ==========================================

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(300);
                entity.Property(e => e.UidAuth).HasMaxLength(500);
            });

            modelBuilder.Entity<Subscripcion>(entity =>
            {
                entity.HasKey(e => e.IdSubscripcion);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<PagoSubscripcion>(entity =>
            {
                entity.HasKey(e => e.IdPago);
                entity.Property(e => e.Mes).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.PagosSubscripcion)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subscripcion)
                    .WithMany(s => s.PagosSubscripcion)
                    .HasForeignKey(e => e.IdSubscripcion)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // 2. NÚCLEO FINANCIERO
            // ==========================================

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodo);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.MetodosPago)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CategoriaGasto>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.CategoriasGasto)
                    .HasForeignKey(e => e.IdUsuario)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TipoGasto>(entity =>
            {
                entity.HasKey(e => e.IdTipo);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.TiposGasto)
                    .HasForeignKey(e => e.IdUsuario)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TipoTransaccion>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransaccion);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Credito>(entity =>
            {
                entity.HasKey(e => e.IdCredito);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Entidad).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Creditos)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CupoCredito>(entity =>
            {
                entity.HasKey(e => e.IdCupoCredito);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Credito)
                    .WithMany(c => c.CuposCredito)
                    .HasForeignKey(e => e.IdCredito)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Deuda>(entity =>
            {
                entity.HasKey(e => e.IdDeuda);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.MontoOriginal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SaldoActual).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TasaInteres).HasColumnType("decimal(5,2)");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Deudas)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasKey(e => e.IdPresupuesto);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Presupuestos)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PresupuestoItem>(entity =>
            {
                entity.HasKey(e => e.IdPresupuestoItem);
                entity.Property(e => e.TipoItem).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MontoObjetivo).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MontoReal).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Presupuesto)
                    .WithMany(p => p.Items)
                    .HasForeignKey(e => e.IdPresupuesto)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.CategoriaGasto)
                    .WithMany(c => c.PresupuestosItems)
                    .HasForeignKey(e => e.IdCategoriaGasto)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoGasto)
                    .WithMany(t => t.PresupuestosItems)
                    .HasForeignKey(e => e.IdTipoGasto)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleCuotas>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCuotas);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.MontoTotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MontoCuota).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Transacciones)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoTransaccion)
                    .WithMany(t => t.Transacciones)
                    .HasForeignKey(e => e.IdTipoTransaccion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MetodoPago)
                    .WithMany(m => m.Transacciones)
                    .HasForeignKey(e => e.IdMetodoPago)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CategoriaGasto)
                    .WithMany(c => c.Transacciones)
                    .HasForeignKey(e => e.IdCategoriaGasto)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoGasto)
                    .WithMany(t => t.Transacciones)
                    .HasForeignKey(e => e.IdTipoGasto)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DetalleCuotas)
                    .WithMany(d => d.Transacciones)
                    .HasForeignKey(e => e.IdDetalleCuotas)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Credito)
                    .WithMany(c => c.Transacciones)
                    .HasForeignKey(e => e.IdCredito)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Deuda)
                    .WithMany(d => d.Transacciones)
                    .HasForeignKey(e => e.IdDeuda)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // 3. INTEGRACIÓN CON AWS S3
            // ==========================================

            modelBuilder.Entity<SoporteFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);
                entity.Property(e => e.NombreArchivo).IsRequired().HasMaxLength(500);
                entity.Property(e => e.TipoMime).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(1000);

                entity.HasOne(e => e.Transaccion)
                    .WithOne(t => t.SoporteFactura)
                    .HasForeignKey<SoporteFactura>(e => e.IdTransaccion)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void AplicarAuditoria()
        {
            var usuario = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == TokenClaims.NombreUsuario)?.Value ?? "Sistema";

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var ahora = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaCreacion = ahora;
                    entry.Entity.CreadoPor = usuario;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaActualizacion = ahora;
                    entry.Entity.ActualizadoPor = usuario;
                }
            }
        }

        public async Task BulkUpdateWithAuditAsync<T>(IList<T> entities, BulkConfig? config = null) where T : AuditableEntity
        {
            var usuario = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == TokenClaims.NombreUsuario)?.Value ?? "Sistema";

            var fecha = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.ActualizadoPor = usuario;
                entity.FechaActualizacion = fecha;
            }

            config ??= new BulkConfig
            {
                BatchSize = 500,
                PreserveInsertOrder = true
            };

            await this.BulkUpdateAsync(entities, config);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entity.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Oracle.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Entity.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        // Constructor principal (para DI)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Constructor alternativo (para Factory y migrations)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _configuration = null; // Será null en contextos de design-time
        }

        // DBSETS PARA AGENDAMIENTO DE CITAS
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<TipoCita> TiposCita { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<HoraDisponible> HorasDisponibles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de tabla Clientes
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTES");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.NumeroCliente)
                    .HasColumnName("NUMERO_CLIENTE")
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.TipoDocumento)
                    .HasColumnName("TIPO_DOCUMENTO")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.NumeroDocumento)
                    .HasColumnName("NUMERO_DOCUMENTO")
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.NombreCompleto)
                    .HasColumnName("NOMBRE_COMPLETO")
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(c => c.Email)
                    .HasColumnName("EMAIL")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Telefono)
                    .HasColumnName("TELEFONO")
                    .HasMaxLength(20);

                entity.Property(c => c.Celular)
                    .HasColumnName("CELULAR")
                    .HasMaxLength(20);

                entity.Property(c => c.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(300);

                entity.Property(c => c.CreatedAt)
                    .HasColumnName("CREATED_AT")
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .HasColumnName("UPDATED_AT");

                entity.Property(c => c.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired()
                    .HasConversion<int>() // true -> 1, false -> 0
                    .HasDefaultValue(1);

                // Índices únicos
                entity.HasIndex(c => c.NumeroCliente).IsUnique().HasDatabaseName("IX_CLIENTES_NUMERO_CLIENTE");
                entity.HasIndex(c => c.NumeroDocumento).IsUnique().HasDatabaseName("IX_CLIENTES_NUMERO_DOCUMENTO");
                entity.HasIndex(c => c.Email).IsUnique().HasDatabaseName("IX_CLIENTES_EMAIL");
            });

            // Configuración de tabla Sedes
            modelBuilder.Entity<Sede>(entity =>
            {
                entity.ToTable("SEDES");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(s => s.Nombre)
                    .HasColumnName("NOMBRE")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Codigo)
                    .HasColumnName("CODIGO")
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(s => s.Direccion)
                    .HasColumnName("DIRECCION")
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(s => s.Telefono)
                    .HasColumnName("TELEFONO")
                    .HasMaxLength(20);

                entity.Property(s => s.Ciudad)
                    .HasColumnName("CIUDAD")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Departamento)
                    .HasColumnName("DEPARTAMENTO")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.EsPrincipal)
                    .HasColumnName("ES_PRINCIPAL")
                    .IsRequired()
                    .HasConversion<int>() // true -> 1, false -> 0
                    .HasDefaultValue(0);

                entity.Property(s => s.CreatedAt)
                    .HasColumnName("CREATED_AT")
                    .IsRequired();

                entity.Property(s => s.UpdatedAt)
                    .HasColumnName("UPDATED_AT");

                entity.Property(s => s.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired()
                    .HasConversion<int>() // true -> 1, false -> 0
                    .HasDefaultValue(1);

                // Índice único para código
                entity.HasIndex(s => s.Codigo).IsUnique().HasDatabaseName("IX_SEDES_CODIGO");
            });

            // Configuración de tabla TiposCita
            modelBuilder.Entity<TipoCita>(entity =>
            {
                entity.ToTable("TIPOS_CITA");
                entity.HasKey(tc => tc.Id);

                entity.Property(tc => tc.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(tc => tc.Nombre)
                    .HasColumnName("NOMBRE")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(tc => tc.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(500);

                entity.Property(tc => tc.Icono)
                    .HasColumnName("ICONO")
                    .HasMaxLength(50);

                entity.Property(tc => tc.TiempoEstimadoMinutos)
                    .HasColumnName("TIEMPO_ESTIMADO_MINUTOS")
                    .IsRequired()
                    .HasDefaultValue(120);

                entity.Property(tc => tc.RequiereDocumentacion)
                    .HasColumnName("REQUIERE_DOCUMENTACION")
                    .IsRequired()
                    .HasConversion<int>() // true -> 1, false -> 0
                    .HasDefaultValue(1);

                entity.Property(tc => tc.CreatedAt)
                    .HasColumnName("CREATED_AT")
                    .IsRequired();

                entity.Property(tc => tc.UpdatedAt)
                    .HasColumnName("UPDATED_AT");

                entity.Property(tc => tc.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired()
                    .HasConversion<int>() // true -> 1, false -> 0
                    .HasDefaultValue(1);
            });

            // Configuración de tabla Citas
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("CITAS");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.NumeroCita)
                    .HasColumnName("NUMERO_CITA")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.FechaCita)
                    .HasColumnName("FECHA_CITA")
                    .IsRequired();

                entity.Property(c => c.HoraCita)
                    .HasColumnName("HORA_CITA")
                    .IsRequired();

                entity.Property(c => c.Estado)
                    .HasColumnName("ESTADO")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("Pendiente");

                entity.Property(c => c.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(1000);

                entity.Property(c => c.FechaCompletada)
                    .HasColumnName("FECHA_COMPLETADA");


                entity.Property(c => c.ClienteId)
                    .HasColumnName("CLIENTE_ID")
                    .IsRequired();

                entity.Property(c => c.SedeId)
                    .HasColumnName("SEDE_ID")
                    .IsRequired();

                entity.Property(c => c.TipoCitaId)
                    .HasColumnName("TIPO_CITA_ID")
                    .IsRequired();

                entity.Property(c => c.CreatedAt)
                    .HasColumnName("CREATED_AT")
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .HasColumnName("UPDATED_AT");

                entity.Property(c => c.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired()
                    .HasConversion<int>() // ✔️ CONVERSIÓN REQUERIDA
                    .HasDefaultValue(1);  // ✔️ TRUE -> 1

                // Relación Cita -> Cliente
                entity.HasOne(c => c.Cliente)
                    .WithMany(cl => cl.Citas)
                    .HasForeignKey(c => c.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CITAS_CLIENTES");

                // Relación Cita -> Sede
                entity.HasOne(c => c.Sede)
                    .WithMany(s => s.Citas)
                    .HasForeignKey(c => c.SedeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CITAS_SEDES");

                // Relación Cita -> TipoCita
                entity.HasOne(c => c.TipoCita)
                    .WithMany(tc => tc.Citas)
                    .HasForeignKey(c => c.TipoCitaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CITAS_TIPOS_CITA");

                // Índice único para número de cita
                entity.HasIndex(c => c.NumeroCita).IsUnique().HasDatabaseName("IX_CITAS_NUMERO_CITA");

                // Índices para mejorar consultas
                entity.HasIndex(c => c.FechaCita).HasDatabaseName("IX_CITAS_FECHA_CITA");
                entity.HasIndex(c => c.Estado).HasDatabaseName("IX_CITAS_ESTADO");
                entity.HasIndex(c => new { c.ClienteId, c.FechaCita }).HasDatabaseName("IX_CITAS_CLIENTE_FECHA");
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Configuración de tabla HorasDisponibles
            modelBuilder.Entity<HoraDisponible>(entity =>
            {
                entity.ToTable("HORAS_DISPONIBLES");
                entity.HasKey(h => h.Id);

                entity.Property(h => h.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(h => h.Hora)
                    .HasColumnName("HORA")
                    .IsRequired();

                entity.Property(h => h.SedeId)
                    .HasColumnName("SEDE_ID")
                    .IsRequired();

                entity.Property(h => h.TipoCitaId)
                    .HasColumnName("TIPO_CITA_ID");

                entity.Property(h => h.CreatedAt)
                    .HasColumnName("CREATED_AT")
                    .IsRequired();

                entity.Property(h => h.UpdatedAt)
                    .HasColumnName("UPDATED_AT");

                entity.Property(h => h.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired()
                    .HasConversion<int>()
                    .HasDefaultValue(1);

                // Relación HoraDisponible -> Sede
                entity.HasOne(h => h.Sede)
                    .WithMany()
                    .HasForeignKey(h => h.SedeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HORASDISPONIBLES_SEDE");

                // Relación HoraDisponible -> TipoCita (opcional)
                entity.HasOne(h => h.TipoCita)
                    .WithMany()
                    .HasForeignKey(h => h.TipoCitaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HORASDISPONIBLES_TIPOCITA");

                // Índice para optimizar consultas por sede y hora
                entity.HasIndex(h => new { h.SedeId, h.Hora }).HasDatabaseName("IX_HORASDISPONIBLES_SEDE_HORA");
            });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                optionsBuilder.UseOracle(_configuration.GetConnectionString("DefaultConnection"));
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Entity.Base.BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity.Base.BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((Entity.Base.BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Entity.Contexts;
using Data.Interfaces;
using Data.Implement;
using Business.Interfaces;
using Business.Implement;
using Data.Mappings;

var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURACIÓN DE SERVICIOS =====

// Configuración de Entity Framework con Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuración de AutoMapper
builder.Services.AddAutoMapper(cfg => 
{
    cfg.AddProfile<MappingProfile>();
});

// ===== INYECCIÓN DE DEPENDENCIAS - DATA LAYER =====
builder.Services.AddScoped<IClienteData, ClienteData>();
builder.Services.AddScoped<ISedeData, SedeData>();
builder.Services.AddScoped<ITipoCitaData, TipoCitaData>();
builder.Services.AddScoped<ICitaData, CitaData>();
builder.Services.AddScoped<IHoraDisponibleData, HoraDisponibleData>();

// ===== INYECCIÓN DE DEPENDENCIAS - BUSINESS LAYER =====
builder.Services.AddScoped<IClienteBusiness, ClienteBusiness>();
builder.Services.AddScoped<ISedeBusiness, SedeBusiness>();
builder.Services.AddScoped<ITipoCitaBusiness, TipoCitaBusiness>();
builder.Services.AddScoped<ICitaBusiness, CitaBusiness>();
builder.Services.AddScoped<IHoraDisponibleBusiness, HoraDisponibleBusiness>();



// Configuración de controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    
    // CORS específico para producción con Vercel
    options.AddPolicy("Production",
        policy =>
        {
            policy
                .WithOrigins(
                    "https://*.vercel.app",
                    "https://pqr-agendamiento-citas.vercel.app", // Cambia por tu dominio real de Vercel
                    "http://localhost:3000", // Para desarrollo local del frontend
                    "https://*.up.railway.app" // Para Railway
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

// ===== CONFIGURACIÓN DEL PIPELINE =====

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElectroHuila API v1");
        c.RoutePrefix = string.Empty; // Swagger como página principal
    });
}

// Aplicar migraciones automáticamente en desarrollo
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            context.Database.EnsureCreated(); // Usar EnsureCreated en lugar de Migrate para simplicidad
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error al crear la base de datos");
        }
    }
}

app.UseHttpsRedirection();

// Configurar CORS según el entorno
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
}
else
{
    app.UseCors("Production");
}

// Configurar autorización
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Endpoint de health check
app.MapGet("/health", () => 
{
    return Results.Ok(new 
    { 
        Status = "Healthy", 
        Timestamp = DateTime.UtcNow,
        Service = "ElectroHuila - API Agendamiento de Citas"
    });
});

// Endpoint principal con información
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        Message = "ElectroHuila - API Agendamiento de Citas",
        Version = "1.0.0",
        Documentation = "/swagger",
        Health = "/health",
        Endpoints = new
        {
            Clientes = "/api/cliente",
            Sedes = "/api/sede", 
            TiposCita = "/api/tipocita",
            Citas = "/api/cita"
        }
    });
});

app.Run();
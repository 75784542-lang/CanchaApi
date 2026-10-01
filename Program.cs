var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("PermitirFrontend");

var listaCanchas = new List<Cancha>
{
    new Cancha { Id = 1, TipoCancha = "Sintética 7 vs 7", Fecha = "2026-10-01", Hora = "18:00", Estado = "Disponible", Precio = 80.00m },
    new Cancha { Id = 2, TipoCancha = "Gras Natural 11 vs 11", Fecha = "2026-10-01", Hora = "20:00", Estado = "Ocupado", Precio = 150.00m },
    new Cancha { Id = 3, TipoCancha = "Losa 5 vs 5", Fecha = "2026-10-02", Hora = "19:00", Estado = "Disponible", Precio = 50.00m },
    new Cancha { Id = 4, TipoCancha = "Sintética 5 vs 5", Fecha = "2026-10-02", Hora = "20:00", Estado = "Disponible", Precio = 60.00m },
    new Cancha { Id = 5, TipoCancha = "Sintética 7 vs 7", Fecha = "2026-10-03", Hora = "10:00", Estado = "Ocupado", Precio = 80.00m }
};

app.MapGet("/api/canchas", (string? fecha, string? estado) =>
{
    var resultado = listaCanchas.AsQueryable();

    if (!string.IsNullOrEmpty(fecha))
    {
        resultado = resultado.Where(c => c.Fecha == fecha);
    }

    if (!string.IsNullOrEmpty(estado))
    {
        resultado = resultado.Where(c => c.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(resultado.ToList());
});

app.Run();

public class Cancha
{
    public int Id { get; set; }
    public string TipoCancha { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Hora { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

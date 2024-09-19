using System;
using System.Linq;
using System.Text.Json;


using var db = new GamePartContext();

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajouter le service CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utiliser CORS
app.UseCors("AllowAll");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return Results.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapGet("/gameparts",() => {
    var gameparts = db.GameParts;
    return Results.Ok(gameparts);
})
.WithName("GetGameParts")
.WithOpenApi();

// Nouvel endpoint POST pour ajouter un produit
app.MapPost("/gameparts", async (HttpRequest request) =>
{

    // Lire le corps de la requête JSON
    using var reader = new StreamReader(request.Body);
    var jsonBody = await reader.ReadToEndAsync();

    // Désérialiser le JSON en objet GamePart
    var gamePart = JsonSerializer.Deserialize<GamePart>(jsonBody);

    // Vérifier si la désérialisation a réussi
    if (gamePart == null)
    {
        return Results.BadRequest("JSON invalide");
    }

    db.Add(gamePart);
    await db.SaveChangesAsync();
    return Results.Created($"/gameparts/{gamePart.GamePartId}", gamePart);
})
.WithName("CreateGamePart")
.WithOpenApi();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


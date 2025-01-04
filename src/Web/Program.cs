using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastucture.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using static Infrastructure.Services.AuthenticationService;

var port = Environment.GetEnvironmentVariable("PORT") ?? "7039";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls($"http://*:{port}");
// Add services to the container.

builder.Services.AddControllers()
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
       // Prevent reference cycles
       options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Convert Enums
   });

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("barbershopManagement", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "barbershopManagement" } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });

});

string connectionString = builder.Configuration["ConnectionStrings:BarberShopDBConnectionString"]!;

// Configure the SQLite connection
var connection = new SqliteConnection("Data Source= barbershop.db");
connection.Open();
builder.Services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));

builder.Services.Configure<AuthenticationServiceOptions>(
    builder.Configuration.GetSection(AuthenticationServiceOptions.AuthenticationService));

builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticaci�n que tenemos que elegir despu�s en PostMan para pasarle el token
    .AddJwtBearer(options => //Ac� definimos la configuraci�n de la autenticaci�n. le decimos qu� cosas queremos comprobar. La fecha de expiraci�n se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticacionService:Issuer"],
            ValidAudience = builder.Configuration["AuthenticacionService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticacionService:SecretForKey"]))
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy => policy.RequireRole("Client"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Barber", policy => policy.RequireRole("Barber"));
});



// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // URL del frontend
              .AllowAnyHeader() // Permite cualquier encabezado
              .AllowAnyMethod() // Permite cualquier m�todo HTTP (GET, POST, etc.)
              .AllowCredentials(); // Si utilizas cookies o autenticaci�n basada en sesiones
    });
});

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IServicesAndHaircutsService, ServicesAndHaircutsService>();
builder.Services.AddScoped<IServicesAndHaircutsRepository, ServicesAndHaircutsRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBarberShopRepository, BarberShopRepository>();
builder.Services.AddScoped<IBarberShopService, BarberShopService>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService,  ShiftService>();


builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate(); // Aplica las migraciones pendientes
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

// Usar CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Repository.Implementacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceReference;
using System.Text;
using static ServiceReference.Tipo_Cambio_BCNSoapClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Conexion SQL server
builder.Services.AddDbContext<GastosDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("SqlConexion"));
});

//JWT
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

});

//Automapper
builder.Services.AddAutoMapper(typeof(GastosAPI.Utilidades.AutoMapper));

//Repositorios
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ILugarRepository, LugarRepository>();
builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddScoped<ITasaCambioRepository, TasaCambioRepositoty>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<IIngresosRepository, IngresosRepository>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();


builder.Services.AddScoped(serviceProvider =>
{
    // Aquí puedes crear una instancia de Tipo_Cambio_BCNSoapClient con los parámetros necesarios
    // Puedes obtener otros servicios del contenedor si es necesario
    var endpointConfiguration = EndpointConfiguration.Tipo_Cambio_BCNSoap;
    return new Tipo_Cambio_BCNSoapClient(endpointConfiguration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

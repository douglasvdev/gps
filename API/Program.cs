using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<Contexto>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { 
            Title ="API GPS", 
            Version = "V1.1.0", 
            Description = "API de projeto de gestão de Futebol Amador",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "DAguiar",
                Email = "douglasv.dev@gmail.com"
            }
        });
});

//var tokenKey = "aqui minha chave privada";  //*******************************************
var key = Encoding.ASCII.GetBytes(Settings.Chave);  //*******************************************

builder.Services.AddAuthentication   //*******************************************
    (
        x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer
    (
        x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    );   //*******************************************

//builder.Services.AddSingleton<JWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));  //*******************************************

builder.Services.AddDbContext<Contexto>(); //Libera a injeção de dependência

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API GPS");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); //******************************************* Lembrar da ordem. Primeiro autenticação de pois autorização

app.UseAuthorization();

app.MapControllers();

app.Run();

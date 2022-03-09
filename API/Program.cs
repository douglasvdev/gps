using API.Data;

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

builder.Services.AddDbContext<Contexto>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();

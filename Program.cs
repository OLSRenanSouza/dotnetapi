var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) => 
{
  options.AddPolicy("DevCors", (corsBuilder) => 
  {
    corsBuilder.WithOrigins("http://localhost:3000")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
  });
  options.AddPolicy("ProdCors", (corsBuilder) => 
  {
    corsBuilder.WithOrigins("https://engenhariadeconcursos.com.br")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors("DevCors");
}

if (app.Environment.IsProduction())
{
  app.UseHttpsRedirection();
  app.UseCors("ProdCors");
}


app.UseAuthorization();

app.MapControllers();

app.Run();

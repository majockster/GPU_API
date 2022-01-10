using Microsoft.EntityFrameworkCore;
using GPU_api.Models;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<GPUContext>(opt => opt.UseInMemoryDatabase("GPU_info"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GPU API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.RoutePrefix = "";
    c.SwaggerEndpoint("/swagger/v1/swagger.json","GPU API");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

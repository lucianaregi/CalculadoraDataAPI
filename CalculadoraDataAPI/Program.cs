using CalculadoraDataAPI.Interfaces;
using CalculadoraDataAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddScoped<IConversorCalendarioService, ConversorCalendarioService>();
builder.Services.AddScoped<IDataCalculationService, DataCalculationService>();

// Configura��o do Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("calculadoradataapi", new OpenApiInfo
    {
        Title = "Calculadora Data API",
        Version = "1.0"
    });

    // Inclui coment�rios XML para o Swagger, se dispon�veis
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    options.IncludeXmlComments(xmlCommentsFullPath);
});

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/calculadoradataapi/swagger.json", "CalculadoraDataAPI - 2.0");
    options.RoutePrefix = ""; // Define a raiz como prefixo da UI do Swagger
});

app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Mapeia endpoints dos controladores

app.Run();

using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using steadyforum.Server.Controllers;
using steadyforum.Server.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<steadyforumServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("steadyforumServerContext") ?? throw new InvalidOperationException("Connection string 'steadyforumServerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v2", new OpenApiInfo { Title = "MVCCallWebAPI", Version = "v2" });});

builder.Services.AddTransient<NewsBrowser>();
/*
builder.Services.AddTransient<NewsBrowser>();
builder.Services.AddTransient<INewsBrowser, DorkBrowser>();
builder.Services.AddTransient<INewsBrowser, StockBrowser>();
*/

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {  c.SwaggerEndpoint("/swagger/v2/swagger.json", "MVCCallWebAPI"); }); 
}

// middleware

app.UseWebSockets();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

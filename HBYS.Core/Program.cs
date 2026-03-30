using HBYS.Core.API.Middleware;
using HBYS.Core.Application.Interfaces;
using HBYS.Core.Application.Services;
using HBYS.Core.Infrastructure.DbContext;
using HBYS.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "HBYS API",
        Version = "v1",
        Description = "Hastane Bilgi Yönetim Sistemi - REST API"
    });
});

// Database
builder.Services.AddDbContext<HbysDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

// Services
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<VisitService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<InvoiceService>();

builder.Services.AddCors(o => o.AddPolicy("AllowAll",
    b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Auto Migration
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HbysDbContext>();
    db.Database.Migrate();
    Log.Information("Veritabanı migration tamamlandı.");
}
catch (Exception ex)
{
    Log.Warning(ex, "Migration başarısız - devam ediliyor.");
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HBYS API v1"));

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

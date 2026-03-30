using HBYS.Integration.ENabiz;
using HBYS.Integration.HL7;
using HBYS.Integration.Medula;
using HBYS.Integration.Queue;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Services.AddSerilog();

builder.Services.AddSingleton<QueueService>();
builder.Services.AddSingleton<HL7Parser>();
builder.Services.AddSingleton<HL7AckBuilder>();
builder.Services.AddHostedService<HL7ListenerService>();
builder.Services.AddHttpClient<MedulaService>();
builder.Services.AddHttpClient<ENabizService>();

var host = builder.Build();
await host.RunAsync();

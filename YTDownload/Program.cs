using YoutubeExplode;
using Application.Core.Services;
using Application.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<YoutubeClient>();
builder.Services.AddScoped<IVideoService, VideoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

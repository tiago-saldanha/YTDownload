using YTDownload.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ProvideServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("angular");
}

app.UseAuthorization();

app.MapControllers();

app.Run();

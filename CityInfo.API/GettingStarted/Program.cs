var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Order of registering middleware is important!
    app.UseSwagger(); // Injecting specifications
    app.UseSwaggerUI(); // Constructing UI from specifications
}

// Just a test run!
app.Run(async (context) => {
    await context.Response.WriteAsync("Hello World");
});

app.Run();

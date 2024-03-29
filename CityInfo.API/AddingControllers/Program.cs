var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => {
    x.ReturnHttpNotAcceptable = true; // Returns 406 status if not supported
}).AddXmlDataContractSerializerFormatters(); // Content Negotiation if application/xml is provided in "Accept" header

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

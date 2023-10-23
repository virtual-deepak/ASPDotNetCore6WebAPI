var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => {
    x.ReturnHttpNotAcceptable = true; // Returns 406 status if not supported
}).AddNewtonsoftJson() // Overrides default JSON formatter to NewtonsoftJson required for JsonPatchDocument
.AddXmlDataContractSerializerFormatters(); // Content Negotiation if application/xml is provided in "Accept" header

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options => 
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
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

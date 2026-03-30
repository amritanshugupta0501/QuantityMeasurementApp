using Microsoft.EntityFrameworkCore; 
using System.Text.Json.Serialization; 
using QuantityMeasurementModel;
using QuantityMeasurementRepository;
using QuantityMeasurementService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddMemoryCache(); 
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IQuantityMeasurementRepo, QuantityMeasurementSQLRepository>();
builder.Services.AddTransient<IQuantityMeasurementService, QuantityMeasurementServices>();
var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
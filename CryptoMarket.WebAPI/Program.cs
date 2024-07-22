using CryptoMarket.Business;
using CryptoMarket.Core;
using CryptoMarket.Core.CrossCuttingConcerns.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddCoreServices();

var app = builder.Build();

app.ValidateCoinMarketCapConfig();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//if (app.Environment.IsProduction())
//{
    app.ConfigureCustomExceptionMiddleware();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

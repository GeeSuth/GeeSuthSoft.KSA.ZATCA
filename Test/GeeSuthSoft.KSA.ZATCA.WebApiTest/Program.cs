using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Extensions;
using GeeSuthSoft.KSA.ZATCA.WebApiTest;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.PropertyNameCaseInsensitive = false;
});



builder.Services.AddZatca(o =>
{
    o.Environment = EnvironmentType.NonProduction;
    o.LogRequestAndResponse = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(opt =>
{
    opt.RouteTemplate = "openapi/{documentName}.json";
});
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar Example";
    opt.Theme = ScalarTheme.Mars;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    opt.Servers = new List<ScalarServer>()
    {
        new ScalarServer("https://localhost:7235/", "Main")
    };
});


app.Map("/", () => Results.Redirect("/scalar/v1"));
app.MapTestOnBoarding();
app.MapTestSimpleInvoiceApi();
app.MapTestComplaince();

app.Run();


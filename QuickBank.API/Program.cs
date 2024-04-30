using QuickBank.Data;
using QuickBank.Core;
using QuickBank.API.AppStartup;
using QuickBank.Business;
using System.Reflection;
using FluentValidation.AspNetCore;
using QuickBank.API;
using System.Collections.Specialized;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext(builder.Configuration);

// Add Repositories
builder.Services.AddRepositories();

// Add Core Services
builder.Services.AddCoreServices();

// Add Business Services and ServiceValidators.
builder.Services.AddServices();
builder.Services.AddServiceValidators();

// Add Identity Configuration
IdentityConfigurator.Configure(builder.Services);

// Add Authentication Configuration
AuthenticationConfigurator.Configure(builder.Services);

void FluentValidationAction(FluentValidationMvcConfiguration fv)
{
    var assemblies = new List<Assembly>() { };

    fv.RegisterValidatorsFromAssemblies(assemblies);

    fv.DisableDataAnnotationsValidation = true;
    fv.ImplicitlyValidateChildProperties = true;
}

builder.Services.RegisterBaseDependencies(FluentValidationAction);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddJsonOptions(
        opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Security.Cryptography;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var keyVaultUrl = "https://verklig-ventixe-keyvault.vault.azure.net/";
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
KeyVaultSecret dbSecret = await client.GetSecretAsync("DbConnectionString-Ventixe");
KeyVaultSecret jwtKeySecret = await client.GetSecretAsync("JwtPublicKey");

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(dbSecret.Value));

var rsa = RSA.Create();
rsa.ImportFromPem(jwtKeySecret.Value.ToCharArray());

var issuer = builder.Configuration["JwtIssuer"];
var audience = builder.Configuration["JwtAudience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = issuer,
    ValidAudience = audience,
    IssuerSigningKey = new RsaSecurityKey(rsa)
  };
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IEventPackageRepository, EventPackageRepository>();

builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service API");
  c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
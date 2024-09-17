using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using beakiebot_server.Data;
using Microsoft.EntityFrameworkCore;

/////////////////////////////////////////////////////////////////
//               Add services to the container.                //
/////////////////////////////////////////////////////////////////
#region
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVault:KeyVaultUrl").Value!);
var azureCredentials = new DefaultAzureCredential();

builder.Configuration.AddAzureKeyVault(keyVaultUrl, azureCredentials);

//Prod loads Azure DB
if (builder.Environment.IsProduction())
{
    var connString = builder.Configuration.GetSection("ProdConn").Value;
    builder.Services.AddDbContext<UserContext>(opt => opt.UseSqlServer(connString));
}

//Dev loads local DB
if (builder.Environment.IsDevelopment())
{
    var connString = builder.Configuration.GetSection("LocalDb:ConnString").Value;
    builder.Services.AddDbContext<UserContext>(opt => opt.UseSqlServer(connString));
}

#endregion
// End of Services /////////////////////////////////////////////

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

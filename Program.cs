using Azure.Identity;
using beakiebot_server.Clients;
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


if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration.GetSection("KeyVault:KeyVaultUrl").Value!), new DefaultAzureCredential());
    builder.Services.AddDbContext<UserContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("ProdConn").Value));
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<UserContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("LocalDb:ConnString").Value));
}

builder.Services.AddTransient<AzureKeyVaultClient>();
builder.Services.AddTransient<TwitchUserClient>();
builder.Services.AddTransient<IStorage, Storage>();
#endregion
// End of Services /////////////////////////////////////////////

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

using linksnews2API.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IAccountService>(options =>
{
    var section = builder.Configuration.GetSection("AzureCosmosDbSettings");
    string url = section.GetValue<string>("URL");
    string primaryKey = section.GetValue<string>("PrimaryKey");
    string dbName = section.GetValue<string>("DatabaseName");
    string accountId = section.GetValue<string>("AccountId");
    string containerName = "Account";
	
    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );
	
    return new AccountService(cosmosClient, dbName, containerName, accountId);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               )
           .UseHttpsRedirection();
app.Run();

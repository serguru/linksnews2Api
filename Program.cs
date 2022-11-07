using FiltersSample.Filters;
using linksnews2API.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IUserService>(options =>
{
    var section = builder.Configuration.GetSection("AzureCosmosDbSettings");
    string url = section.GetValue<string>("URL");
    string primaryKey = section.GetValue<string>("PrimaryKey");
    string dbName = section.GetValue<string>("DatabaseName");
    string containerName = "User";

    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );

    return new UserService(cosmosClient, dbName, containerName);
});

builder.Services.AddSingleton<IAccountService>(options =>
{
    var section = builder.Configuration.GetSection("AzureCosmosDbSettings");
    string url = section.GetValue<string>("URL");
    string primaryKey = section.GetValue<string>("PrimaryKey");
    string dbName = section.GetValue<string>("DatabaseName");
    string containerName = "Account";

    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );

    return new AccountService(cosmosClient, dbName, containerName);
});


builder.Services.AddControllers(options =>
{
    // GlobalActionFilter : IActionFilter did not work properly because on calling
    // it returned the excution flow to the controller
    // 
    // options.Filters.Add<GlobalActionFilter>();
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

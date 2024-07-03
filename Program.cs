using Microsoft.Extensions.DependencyInjection;
using SimpleInventoryManagementSystem.DataBase;
using SimpleInventoryManagementSystem.ProductManagement.Entity;
using SimpleInventoryManagementSystem.ProductManagement.Repository;
using SimpleInventoryManagementSystem.Utilities;

 //UseSQLDB();

UseMongoDB();



static void UseSQLDB()
{
    String connectionString = "Server=DESKTOP-4J9GHFA;Database=SimpleInventoryManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;";

    var serviceProvider = new ServiceCollection()
        .AddSingleton<DatabaseInitializer>(sp => new DatabaseInitializer(connectionString))
        .AddSingleton<IProductRepository>(sp => new SQLProductRepository(connectionString))
        .AddSingleton<Inventory>()
        .AddSingleton<Utilities>()
        .BuildServiceProvider();
    var dataBaseInitializer = serviceProvider.GetService<DatabaseInitializer>();
    dataBaseInitializer.Initialize();
    var utilities = serviceProvider.GetService<Utilities>();
    utilities.ShowMainMenu();
}

static void UseMongoDB()
{
    string connectionString = "mongodb://localhost:27017";
    string databaseName = "InventoryDB";
    string collectionName = "Products";

    var serviceProvider = new ServiceCollection()
      .AddSingleton<IProductRepository>(sp => new MongoProductRepository(connectionString, databaseName, collectionName))
      .AddSingleton<Inventory>()
      .AddSingleton<Utilities>()
      .BuildServiceProvider();
    var utilities = serviceProvider.GetService<Utilities>();
    utilities.ShowMainMenu();
}


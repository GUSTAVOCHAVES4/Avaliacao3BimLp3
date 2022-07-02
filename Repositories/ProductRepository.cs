using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public IEnumerable<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products");

        return products;
    }

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active);", product);

        return product;
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Products WHERE Id = @Id", new { Id = id });

        return result;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });

    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active=true WHERE id=@Id", new {Id = id});

    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active=false WHERE id=@Id", new {Id = id});
    }

    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
     {
         using var connection = new SqliteConnection(databaseConfig.ConnectionString);
         connection.Open();

         var products = connection.Query<Product>("SELECT * FROM Products WHERE price BETWEEN @InitialPrice AND @EndPrice ",new {InitialPrice = initialPrice, EndPrice = endPrice}).ToList();
         return products;
     }

    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price > @Price", new { Price = price }).ToList();

        return products;
    }

    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new { Price = price }).ToList();

        return products;
    }

    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var mediaPrices = connection.ExecuteScalar<Double>("SELECT AVG(price) FROM Products");

        return mediaPrices;
    }
}
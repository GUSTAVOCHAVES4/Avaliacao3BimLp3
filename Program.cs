using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;
using Microsoft.Data.Sqlite;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var productRepository = new ProductRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product")
{
    if(modelAction == "List")
    {
        if(productRepository.GetAll().Count() != 0){
            foreach(var product in productRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
        
    }
}

if(modelAction == "New")
{
    int id = Convert.ToInt32(args[2]);
    string name = args[3];
    double price = Convert.ToDouble(args[4]);
    bool active = Convert.ToBoolean(args[5]);

    if(productRepository.ExistsById(id))
    {
        Console.WriteLine($"Produto com id {id} já existe");
    }
    else 
    {
        var product = new Product(id, name, price, active); 
        productRepository.Save(product);
        Console.WriteLine($"Produto {name} cadastrado com sucesso!");
    }
}

if(modelAction == "Delete")
{
    int id = Convert.ToInt32(args[2]);

    if(productRepository.ExistsById(id))
    {   
        productRepository.Delete(id);
        Console.WriteLine($"Produto {id} foi removido com sucesso");
    }
    else
    { 
        Console.WriteLine($"Produto {id} não encontrado");
        
    }
}

if (modelAction == "Enable")
{
    int id = Convert.ToInt32(args[2]);

    if (productRepository.ExistsById(id))
    {
        productRepository.Enable(id);
        Console.WriteLine($"Produto {id} habilitado com sucesso");
        
    }
    else
    {  
        Console.WriteLine($"Produto {id} não encontrado");
    }
}

if (modelAction == "Disable")
{
    int id = Convert.ToInt32(args[2]);

    if (productRepository.ExistsById(id))
    {   
        productRepository.Disable(id);
        Console.WriteLine($"Produto {id} desabilitado com sucesso");
    }
    else{
        Console.WriteLine($"Produto {id} não encontrado");
    }
}

if(modelAction == "PriceBetween")
{
    double initialPrice = Convert.ToDouble(args[2]);
    double endPrice = Convert.ToDouble(args[3]);

    if (productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Count != 0)
    {
        foreach(var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
        }
    }
    else
    {
        Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço R${initialPrice} e R${endPrice}");
    }
}

if(modelAction == "PriceLowerThan")
{
    double price = Convert.ToDouble(args[2]);

    if(productRepository.GetAllWithPriceLowerThan(price).Count() != 0)
    {
        foreach(var product in productRepository.GetAllWithPriceLowerThan(price))
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
        }
    }
    else
    {
        Console.WriteLine($"Nenhum produto encontrado com preço menor que R$ {price}");
    }
}

if(modelAction == "PriceHigherThan")
{
    double price = Convert.ToDouble(args[2]);

    if(productRepository.GetAllWithPriceHigherThan(price).Count() != 0)
    {
        foreach(var product in productRepository.GetAllWithPriceHigherThan(price))
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
        }
    }
    else
    {
        Console.WriteLine($"Nenhum produto encontrado com preço maior que R$ {price}");
    }
}

if(modelAction == "AveragePrice")
{
    if(productRepository.GetAll().Count() != 0)
    {
        Console.WriteLine("A média dos preços é {0}", productRepository.GetAveragePrice().ToString("C"));
    }
    else
    {
        Console.WriteLine("Nenhum produto cadastrado");
    }
}


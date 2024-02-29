namespace Sakila.ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var uri = "http://localhost:5220/";
        swaggerClient client = new swaggerClient(uri, new HttpClient());

         var customers = await client.CustomersAllAsync();
    
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.FirstName);
        }
    }
}

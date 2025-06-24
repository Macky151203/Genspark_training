public class Customer
{
    public string? Name { get; set; }
    public int Amount { get; set; }
}

interface IPrice
{
    double GetPrice(Customer customer); 
}

class GoldPriceCal : IPrice
{
    public double GetPrice(Customer customer) 
    {
        return customer.Amount * 0.8;
    }
}

class SilverPriceCal : IPrice
{
    public double GetPrice(Customer customer) 
    {
        return customer.Amount * 0.9;
    }
}

class PlatinumPriceCal : IPrice
{
    public double GetPrice(Customer customer) 
    {
        return customer.Amount * 0.7;
    }
}

class Payment
{
    public double CalculatePrice(IPrice priceCalculator, Customer customer)
    {
        return priceCalculator.GetPrice(customer);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // OCP
        var customer = new Customer { Name = "John", Amount = 1000 };
        // IPrice priceCalculator = new GoldPriceCal();
        // IPrice priceCalculator2 = new SilverPriceCal();
        IPrice priceCalculator3 = new PlatinumPriceCal();
        Payment payment = new Payment();
        double price = payment.CalculatePrice(priceCalculator3, customer);
        Console.WriteLine($"Price for {customer.Name} is {price}");
    }
}
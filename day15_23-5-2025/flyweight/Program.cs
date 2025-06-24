public interface ICarModel
{
    void display(string number, string location);//The unique part
}
public class CarModel : ICarModel
{
    private string modelName { get; set; } = "";
    private string Color { get; set; } = "";
    public CarModel(string modelName, string Color)
    {
        this.modelName = modelName;
        this.Color = Color;
    }
    public void display(string number, string location)
    {
        Console.WriteLine($"Car Model: {modelName}, Color:{Color} Number: {number}, Location: {location}");
    }
}

public class CarModelFactory
{
    private Dictionary<string, ICarModel> carModels = new Dictionary<string, ICarModel>();
    public ICarModel GetCarModel(string modelName, string color)
    {
        string key = $"{modelName}_{color}";
        if (!carModels.ContainsKey(key))
        {
            carModels[key] = new CarModel(modelName, color);
        }
        return carModels[key];
    }
}
class Program
{
    static void Main(string[] args)
    {
        CarModelFactory factory = new CarModelFactory();
        ICarModel car1 = factory.GetCarModel("BMW", "Red");
        ICarModel car2 = factory.GetCarModel("BMW", "Red");
        ICarModel car3 = factory.GetCarModel("Audi", "Blue");
        ICarModel car4 = factory.GetCarModel("Audi", "Blue");

        car1.display("1234", "New York");
        car2.display("5678", "Los Angeles");
        car3.display("91011", "Chicago");
        car4.display("121314", "Houston");

        Console.WriteLine($"car1 and car2 are the same instance: {car1==car2}");
        Console.WriteLine($"car3 and car4 are the same instance: {car3==car4}");
    }
}
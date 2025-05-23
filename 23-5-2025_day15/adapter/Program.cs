public interface INewObj
{
    int a { get; set; }
    int b { get; set; }
    int product { get; set; }
    string ToString();
}
public interface IOldObj
{
    int a { get; set; }
    int b { get; set; }
}
public class NewObj :INewObj
{
    public int a { get; set; }
    public int b { get; set; }
    public int product { get; set; }

    public override string ToString()
    {
        return $"a: {a}, b: {b}, product: {product}";
    }

}

public class OldObj :IOldObj {
    public int a { get; set; }
    public int b { get; set; }
}
public interface INum
{
    INewObj calc();

}

class Adaptee
{
    private IOldObj _oldObj;
    public Adaptee(int a,int b)
    {
        _oldObj = new OldObj { a = a, b = b };
    }
    public IOldObj GetOldObj()
    {
        return _oldObj;
    }
}

class Adapter : INum
{
    private readonly Adaptee _adaptee;
    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }
    public INewObj calc()
    {
        var oldObj = _adaptee.GetOldObj();
        var obj = new NewObj { a = oldObj.a, b = oldObj.b, product = oldObj.a * oldObj.b };
        // Console.WriteLine($"Product: {obj.ToString()}");
        return obj;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Adapter Pattern
        var adaptee = new Adaptee(5,10);
        INum adapter = new Adapter(adaptee);
        INewObj result = adapter.calc();
        Console.WriteLine(result.ToString());
    }
}

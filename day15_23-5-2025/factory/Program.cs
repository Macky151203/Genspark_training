using System;
using System.IO;
//logging payment records to a file using singleton pattern and implementing factory pattern for the types of transactions
class FileManager
{
    private static FileManager? instance;
    private string filePath = "";
    private bool isOpen = false;

    private FileManager() { }

    public static FileManager GetInstance()
    {
        if (instance == null)
        {
            instance = new FileManager();
        }
        return instance;
    }

    public void Open(string path)
    {
        if (!isOpen)
        {
            filePath = path;
            isOpen = true;
        }
    }

    public void Write(string content)
    {
        if (isOpen && !string.IsNullOrEmpty(filePath))
        {
            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(content);
            }
        }
    }

    public void ReadAll()
    {
        if (isOpen && !string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }

    public void Close()
    {
        isOpen = false;
    }
}


public interface IPayment
{
    void pay(int amount);
}

public class CreditCardPayment : IPayment
{
    public void pay(int amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card.");
        string path = "data.txt";
        var fileManager = FileManager.GetInstance();

        fileManager.Open(path);

        fileManager.Write($"Paid {amount} using Credit Card.");

        fileManager.Close();
    }
}

public class PayPalPayment : IPayment
{
    public void pay(int amount)
    {
        Console.WriteLine($"Paid {amount} using PayPal.");
        string path = "data.txt";
        var fileManager = FileManager.GetInstance();

        fileManager.Open(path);

        fileManager.Write($"Paid {amount} using Paypal.");

        fileManager.Close();
    }
}

public abstract class PaymentProcessor
{
    public abstract IPayment PaymentType();
    public void MakePayment(int amount)
    {
        IPayment payment = PaymentType();
        payment.pay(amount);
    }
}

public class CreditCardPaymentProcessor : PaymentProcessor
{
    public override IPayment PaymentType()
    {
        return new CreditCardPayment();
    }
}

public class PayPalPaymentProcessor : PaymentProcessor
{
    public override IPayment PaymentType()
    {
        return new PayPalPayment();
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        PaymentProcessor creditCardProcessor = new CreditCardPaymentProcessor();
        creditCardProcessor.MakePayment(10);

        PaymentProcessor payPalProcessor = new PayPalPaymentProcessor();
        payPalProcessor.MakePayment(20);
    }
}
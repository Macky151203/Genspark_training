class EmployeePromotion
{
    List<string> promotionlist = new List<string>();

    public void AddEmployee()
    {
        Console.WriteLine("Please enter the no. employee for promotion- ");
        int totalnames = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < totalnames; i++)
        {
            Console.WriteLine($"Please enter the employee {i + 1} name");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter a valid name");
                i--;
            }
            else
            {
                promotionlist.Add(name);
            }
        }

    }

    public void printemp()
    {
        if (promotionlist.Count == 0)
        {
            Console.WriteLine("No employees in the promotion list");
            return;
        }
        else
        {
            Console.WriteLine("Employee names in the promotion list are- ");
            for (int i = 0; i < promotionlist.Count; i++)
            {
                Console.WriteLine($"Employee {i + 1} name is {promotionlist[i]}");
            }
        }
    }

    public void findposition(string name)
    {
        if (promotionlist.Count == 0)
        {
            Console.WriteLine("No employees in the promotion list");
            return;
        }
        else
        {
            for (int i = 0; i < promotionlist.Count; i++)
            {
                if (promotionlist[i] == name)
                {
                    Console.WriteLine($"Employee {name} is in the promotion list at position {i + 1}");
                    return;
                }
            }
            Console.WriteLine("Employee name not found");
            return;
        }
    }

    public void optmize()
    {
        Console.WriteLine($"The current size of the collection is {promotionlist.Capacity}");
        promotionlist.TrimExcess();
        Console.WriteLine($"The size after removing the extra space is {promotionlist.Capacity}");
    }

    public void promote_all()
    {
        if (promotionlist.Count == 0)
        {
            Console.WriteLine("No employees in the promotion list");
            return;
        }
        else
        {
            var promoted_list_byname = promotionlist.OrderBy(x => x).ToList();
            Console.WriteLine("The promoted list in alphabetical order is- ");
            foreach (var i in promoted_list_byname)
            {
                Console.WriteLine(i);
            }
            promotionlist.Clear();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        EmployeePromotion emp = new EmployeePromotion();
        int choice;
        do
        {
            Console.WriteLine("Enter 1 for adding employee, 2 for finding , 3 to view all , 4 to optimize , 5 to promote all and 6 to exit");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    emp.AddEmployee();
                    break;
                case 2:
                    Console.WriteLine("Enter the employee name to find");
                    string name = Console.ReadLine();
                    emp.findposition(name);
                    break;
                case 3:
                    emp.printemp();
                    break;
                case 4:
                    emp.optmize();
                    break;
                case 5:
                    emp.promote_all();
                    break;
                case 6:
                    Console.WriteLine("Exiting the program");
                    choice = 0;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        } while (choice != 0);
        //emp.AddEmployee();
        //emp.findposition("a");
        //emp.printemp();

    }
}
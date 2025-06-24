class Program
{
    static void Main(string[] args)
    {
        //ques7
        //  7) create a program to rotate the array to the left by one position.

        int[] arr = { 10, 20, 30, 40, 50 };
        int temp = arr[0];
        for (int i = 0; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];
        }
        arr[arr.Length - 1] = temp;
        foreach (int i in arr)
        {
            Console.WriteLine(i);
        }

    }
}

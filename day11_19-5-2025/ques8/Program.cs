class Program
{
    static void Main(string[] args)
    {
        //Ques8
        //8) Given two integer arrays, merge them into a single array.
        // 1,3,5 and 2,4,6
        int[] arr1 = { 1, 3, 5 };
        int[] arr2 = { 2, 4, 6 };
        int[] newarr = new int[arr1.Length + arr2.Length];
        for(int i= 0; i < arr1.Length; i++)
        {
            newarr[i] = arr1[i];
        }
        for(int i= arr1.Length;i<arr1.Length + arr2.Length; i++)
        {
            newarr[i] = arr2[i - arr1.Length];
        }
        foreach (int i in newarr)
        {
            Console.WriteLine(i);
        }

    }
}

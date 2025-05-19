class Program
{
    static void Main(string[] args)
    {
        //ques 6
        //6) Count the Frequency of Each Element
        //Given an array, count the frequency of each element and print the result.
        //Input: {1, 2, 2, 3, 4, 4, 4}
        int[] nums = { 1, 2, 2, 3, 4, 4, 4 };
        Dictionary<int, int> freq = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            if (freq.ContainsKey(num))
            {
                freq[num]++;
            }
            else
            {
                freq[num] = 1;
            }
        }
        foreach (var i in freq) {
            Console.WriteLine($"Number {i.Key} occurs {i.Value} times");
        }
    }
}
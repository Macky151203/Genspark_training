class Program
{
    public static bool checkvalid(int[] row)
    {
        bool[] visited = new bool[9];
        for (int i = 0; i < row.Length; i++)
        {
            if (visited[row[i] - 1] == false)
            {
                visited[row[i] - 1] = true;
            }
            else
            {
                // Console.WriteLine("Invalid");
                return false;
            }
        }
        return true;
    }

    public static bool checkrows(int[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            int[] row = new int[9];
            for (int j = 0; j < 9; j++)
            {
                row[j] = board[i, j];
            }
            if (!checkvalid(row))
            {
                return false;
            }
        }
        return true;
    }
    public static bool checkcols(int[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            int[] col = new int[9];
            for (int j = 0; j < 9; j++)
            {
                col[j] = board[j, i];
            }
            if (!checkvalid(col))
            {
                return false;
            }
        }
        return true;
    }

    public static bool checkblocks(int[,] board)
    {
        for (int blockRow = 0; blockRow < 3; blockRow++)
        {
            for (int blockCol = 0; blockCol < 3; blockCol++)
            {
                int[] block = new int[9];
                int pos = 0;
                for (int i = blockRow * 3; i < blockRow * 3 + 3; i++)
                {
                    for (int j = blockCol * 3; j < blockCol * 3 + 3; j++)
                    {
                        block[pos++] = board[i, j];
                    }
                }
                if (!checkvalid(block))
                {
                    return false;
                }
            }
        }
        return true;
    }
    static void Main(string[] args)
        {
            int[,] board = new int[9, 9]
        {
            {5, 3, 4, 6, 7, 8, 9, 1, 2},
            {6, 7, 2, 1, 9, 5, 3, 4, 8},
            {1, 9, 8, 3, 4, 2, 5, 6, 7},
            {8, 5, 9, 7, 6, 1, 4, 2, 3},
            {4, 2, 6, 8, 5, 3, 7, 9, 1},
            {7, 1, 3, 9, 2, 4, 8, 5, 6},
            {9, 6, 1, 5, 3, 7, 2, 8, 4},
            {2, 8, 7, 4, 1, 9, 6, 3, 5},
            {3, 4, 5, 2, 8, 6, 1, 7, 9}
        };
            bool rowcheck = checkrows(board);
            bool colcheck = checkcols(board);
            bool gridcheck = checkblocks(board);
            if (rowcheck && colcheck && gridcheck)
        {
            Console.WriteLine("Valid");
        }
        else
        {
            Console.WriteLine("Invalid");
        }
        }
}





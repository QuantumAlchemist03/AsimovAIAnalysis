using System;

class HollowSquare
{
    static void Main(string[] args)
    {
        int size = 5;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                
                if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}

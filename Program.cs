using System;
using System.Text;
using System.Collections.Generic;

namespace Conway
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SetBoardSize();
            StartConfig(new Dictionary<int, int>() {{0, 0}});
            Draw();
            Console.ReadKey(true);
        }
        //dimensions; first value is x (width), second value is y (height)
        static int width, height; //dimensions of board
        static bool[,] board; //Boolean representation of board
        //sets size of board (duh)
        static void SetBoardSize(int x = 32, int y = 32)
        {
            Console.Clear();
            width = x; height = y;

            Console.SetWindowSize(x*2+1, y+1); 
            Console.SetBufferSize(x*2+1, y+1);
        }

        //draws results to console
        static void Draw()
        {
            var output = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    output.Append(board[x, y] ? "  ": "\u2588\u2588"); //Blank = live, block = dead         
                }
                output.Append('\n');                
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(output.ToString());
        }
        //determines the starting configuration
        static void StartConfig(Dictionary<int, int> input)
        {
            board = new bool[width, height];
            foreach (KeyValuePair<int, int> coords in input)
            {
                int x = coords.Key;
                int y = coords.Value;
                board[x, y] = true;
            }
        }
    }
}

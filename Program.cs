using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace Conway
{
    class Program
    {
        public static void Main(string[] args)
        {

            SetBoardSize(44, 44);
            StartConfig(new int[,] {
                {1, 1},
                {2, 3},
                {4, 5}
            });

            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.F)
            {
                Draw();
                Update();
                Thread.Sleep(250);
            }
        }
        //dimensions; first value is x (width), second value is y (height)
        private static int width, height; //dimensions of board
        private static bool[,] board; //Boolean representation of board
        //sets size of board (duh)
        private static void SetBoardSize(int x = 32, int y = 32)
        {
            Console.Clear();
            width = x; height = y;
            //x is multiplied by two because each width is two characters
            //there's a "+2" at the end for buffer
            // Console.SetWindowSize(x*2+1, y+1); 
            // Console.SetBufferSize(x*2+1, y+1);
        }

        //draws results to console
        private static void Draw()
        {
            var output = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    output.Append(board[x, y] ? "  " : "\u2588\u2588"); //Blank = live, block = dead         
                }
                output.Append('\n');
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(output.ToString());
        }
        //determines the starting configuration
        private static void StartConfig(int[,] input)
        {
            board = new bool[width, height];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                int x = input[i, 0];
                int y = input[i, 1];
                board[x, y] = true;
            }
        }
        //Count the neighbors surrounding them
        private static int CountNeighbor(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                int k = (y + i + Program.height) % Program.height;

                for (int j = -1; j <= 1; j++)
                {
                    if (j == 0 && i == 0)
                    {
                        continue;
                    }

                    int h = (x + j + Program.width) % Program.width;
                    count += Program.board[h, k] ? 1 : 0;
                }
            }
            return count;
        }
        private static void Update()
        {
            bool[,] newBoard = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int n = CountNeighbor(x, y);
                    var c = board[x, y];

                    newBoard[x, y] = c && (n == 2 || n == 3) || !c && n == 3;
                }
            }
            board = newBoard;
        }
    }
}

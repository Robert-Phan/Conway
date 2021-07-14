using System;
using System.Text;

namespace Life
{
    class Game
    {
        public static int width, height; //dimensions of board
        public static bool[,] board; //Boolean representation of board
        //sets size of board (duh)
        public static void SetBoardSize(int x = 32, int y = 32)
        {
            width = x; height = y;
            //x is multiplied by two because each width is two characters
            //there's a "+2" at the end for buffer
            // Console.SetWindowSize(x*2+1, y+1); 
            // Console.SetBufferSize(x*2+1, y+1);
        }

        //draws results to console
        public static void Draw()
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
        public static void StartConfig(int[,] input)
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
                int k = (y + i + Game.height) % Game.height;

                for (int j = -1; j <= 1; j++)
                {
                    if (j == 0 && i == 0) continue;

                    int h = (x + j + Game.width) % Game.width;
                    count += Game.board[h, k] ? 1 : 0;
                }
            }
            return count;
        }
        public static void Update()
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
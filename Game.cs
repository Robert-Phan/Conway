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
        }

        public static void CreateBoard(int[,] input)
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
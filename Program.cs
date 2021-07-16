using System;
using System.Threading;
using Life;

namespace Conway
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game.SetBoardSize(20, 20);
            Game.StartConfig(new int[,] {
                {1, 1},
                {1, 0},
                {1, 2}
            });
            Console.Clear();
            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Game.Draw();
                Game.Update();
                Thread.Sleep(250);
            }
        }
    }
}
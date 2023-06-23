using System;

namespace ConnectFour
{
    interface IPlayer
    {
        int GetMove();
    }

    class HumanPlayer : IPlayer
    {
        public int GetMove()
        {
            Console.Write("Enter the column number (1-7): ");
            int col = int.Parse(Console.ReadLine());
            return col;
        }
    }

   
    



    class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.StartGame();
        }
    }
}


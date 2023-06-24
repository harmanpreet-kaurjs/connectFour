using System;

//Logic
namespace ConnectFour
{

    //player interface
    interface IPlayer
    {
        int GetMove();
    }


    //class for player Human ... computer player option will be added soon
    class HumanPlayer : IPlayer
    {

        //When player "X or O" is moving
        public int GetMove()
        {
            int col = 0;
            bool isValidMove = false;
            while (!isValidMove)
            {
                Console.Write("Enter the column number (1-7): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out col))
                {
                    if (col >= 1 && col <= 7)
                    {
                        isValidMove = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Column number should be between 1 and 7.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid column number.");
                }
            }

            return col;
        }
    }
    


    //It is model which connects all the features 
    class ConnectFourModel
    {
        private const int Rows = 6;
        private const int Columns = 7;

        private char[,] board;
        private char currentPlayer;


        //constructor call
        public ConnectFourModel()
        {
            board = new char[Rows, Columns];
            currentPlayer = 'X';
            InitializeBoard();
        }

        //intialization of game board
        private void InitializeBoard()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        //use of encapsulation
        public char[,] GetBoard()
        {
            return board;
        }

        //if the move is valid update
        public bool IsValidMove(int col)
        {
            if (col < 1 || col > Columns)
                return false;

            return board[0, col - 1] == ' ';
        }


        // where the value should be placed
        public void PlaceToken(int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, col - 1] == ' ')
                {
                    board[row, col - 1] = currentPlayer;
                    break;
                }
            }
        }


        //checking if player X or O is winner
        public bool CheckWin()
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // Check for four-in-a-row
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col <= cols - 4; col++)
                {
                    char token = board[row, col];
                    if (token != ' ' &&
                        token == board[row, col + 1] &&
                        token == board[row, col + 2] &&
                        token == board[row, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check for four-in-a-column
            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row <= rows - 4; row++)
                {
                    char token = board[row, col];
                    if (token != ' ' &&
                        token == board[row + 1, col] &&
                        token == board[row + 2, col] &&
                        token == board[row + 3, col])
                    {
                        return true;
                    }
                }
            }

            // Check for four-in-a-diagonal (top-left to bottom-right)
            for (int row = 0; row <= rows - 4; row++)
            {
                for (int col = 0; col <= cols - 4; col++)
                {
                    char token = board[row, col];
                    if (token != ' ' &&
                        token == board[row + 1, col + 1] &&
                        token == board[row + 2, col + 2] &&
                        token == board[row + 3, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check for four-in-a-diagonal (top-right to bottom-left)
            for (int row = 0; row <= rows - 4; row++)
            {
                for (int col = cols - 1; col >= 3; col--)
                {
                    char token = board[row, col];
                    if (token != ' ' &&
                    token == board[row + 1, col - 1] &&
                    token == board[row + 2, col - 2] &&
                    token == board[row + 3, col - 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        //if board is full draw
        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[0, col] == ' ')
                    return false;
            }
            return true;
        }


        //to get cuurent player
        public char GetCurrentPlayer()
        {
            return currentPlayer;
        }


        //to swictch the player if one player turn over
        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }



    //controller class in which controls and starting of the game is defined
    class GameController
    {
        private ConnectFourModel model;
        private IPlayer player1;
        private IPlayer player2;
        private string currentPlayerName;

        //constructor
        public GameController()
        {
            model = new ConnectFourModel();
            player1 = new HumanPlayer();
            player2 = new HumanPlayer();
        }


        //how game starts and flow of game
        public void StartGame()
        {
            bool playAgain = true;
            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine("===== CONNECT FOUR =====");
                Console.WriteLine("Code Owner: Harmanpreet Kaur"); 
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Quit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayGame();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Thank you for playing!");
                        Console.WriteLine("Code Owner: Harmanpreet kaur"); 
                        playAgain = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }


        //defines turn and winner
        private void PlayGame()
        {
            model = new ConnectFourModel();
            bool gameOver = false;

            while (!gameOver)
            {
                Console.Clear();
                char[,] board = model.GetBoard();
                DisplayBoard(board);

                char currentPlayer = model.GetCurrentPlayer();
                currentPlayerName = (currentPlayer == 'X') ? "Player 1" : "Player 2";
                Console.WriteLine("It is " + currentPlayerName + "'s turn.");

                IPlayer currentPlayerObj = (currentPlayer == 'X') ? player1 : player2;
                int move = currentPlayerObj.GetMove();

                if (model.IsValidMove(move))
                {
                    model.PlaceToken(move);

                    if (model.CheckWin())
                    {
                        Console.Clear();
                        DisplayBoard(board);
                        Console.WriteLine(currentPlayerName + " wins!");
                        gameOver = true;
                    }
                    else if (model.IsBoardFull())
                    {
                        Console.Clear();
                        DisplayBoard(board);
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }

                    model.SwitchPlayer();
                }
                else
                {
                    Console.WriteLine("Invalid move! Please try again.");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Press R to restart or any other key to exit.");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.R)
            {
                StartGame();
            }
        }


        //display board on console
        private void DisplayBoard(char[,] board)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Console.Write("| " + board[row, col] + " ");
                }
                Console.WriteLine("|");
            }

            for (int col = 0; col < columns; col++)
            {
                Console.Write("---");
            }
            Console.WriteLine(); for (int col = 0; col < columns; col++)
            {
                Console.Write("  " + (col + 1) + " ");
            }
            Console.WriteLine();
        }
    }



    //main class
    class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.StartGame();
        }
    }
}


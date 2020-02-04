using System;
using System.Collections.Generic;
using ConsoleUserInt.ConsoleUI;
using Engine;
using Game;
using GameEngine;
using MenuSystem;
using static System.Console;

namespace IDoMinesweeper
{
    class Program
    {

        private static GameSettings _settings;
        private static CellState[,] _boardState;

        static void Main(string[] args)
        {
            Clear();

            WriteLine("Welcome to Minesweeper");
           

            _settings = GameConfigHandler.LoadConfig();


            var gameMenu = new Menu(1)
            {
                Title = "New Game",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Play now",
                            CommandToExecute = TestGame
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Level 2",
                            CommandToExecute = TestGame
                        }
                    },
                    

                    {
                        "3", new MenuItem()
                        {
                            Title = "Customize Your Board size",
                            CommandToExecute = SaveSettings
                        }
                    },

                }
            };

            var menu0 = new Menu(0) //main menu
            {
                Title = "Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "S", new MenuItem()
                        {
                            Title = "Start game",
                            CommandToExecute = gameMenu.Run
                        }

                    },
                    {

                        "J", new MenuItem()
                        {
                            Title = "Set default board size for game (save to JSON)",
                            CommandToExecute = SaveSettings

                        }
                    },


                    {
                        "D", new MenuItem()
                        {
                            Title = "Donate",
                            CommandToExecute = null
                        }
                    }
                }
            };

            menu0.Run();



        }


        static string SaveSettings()
        {
            Console.Clear();

            var boardWidth = 0;
            var boardHeight = 0;
            var userCanceled = false;

            (boardWidth, userCanceled) = GetUserIntInput("Enter board width", 8, 30, 0);
            if (userCanceled) return "";

            (boardHeight, userCanceled) = GetUserIntInput("Enter board height", 8, 30, 0);
            if (userCanceled) return "";

            _settings.BoardHeight = boardHeight;
            _settings.BoardWidth = boardWidth;
            GameConfigHandler.SaveConfig(_settings);

            return "";
        }



        

        static string TestGame()
        {


            var game = new BoardDim(_settings);
            Console.WriteLine();
            
            var done = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                BoardDim.Mines.MinesSetter(_settings.BoardHeight, _settings.BoardWidth);

                var userXint = 0;
                var userYint = 0;
                var userCanceled = false;

                (userYint, userCanceled) = GetUserIntInput("Enter Y coordinate", 1, 30, 0);
                if (!userCanceled)
                {
                    (userXint, userCanceled) = GetUserIntInput("Enter X coordinate", 1, 30, 0);
                }

                if (userYint > _settings.BoardHeight || userXint > _settings.BoardWidth)
                {
                    Console.WriteLine("Out of Range");
                }

                if (userCanceled)
                {
                    done = true;
                }
                else
                {
                    game.Move(userYint - 1, userXint - 1);
                }

                var h = Engine.BoardDim.Board;
                _boardState = BoardDim.Board;
                GameConfigHandler.SaveCellState(_boardState);


            } while (!done);



            return "GAME OVER!!";




        }

       /* static string OldTestGame()
        {


            Console.Clear();
            WriteLine("Board height (Min. 8): ");
            //WriteLine(">");
            var checkHeight = ReadLine();
            int height = 0;
            if (!int.TryParse(checkHeight, out height))
            {
                WriteLine("Not an integer");
            }

            WriteLine("Board width (Min. 8): ");
            //WriteLine(">");
            var checkWidth = ReadLine();
            int width = 0;
            if (!int.TryParse(checkWidth, out width))
            {
                Console.WriteLine("Not an integer");
            }

            width = Convert.ToInt32(checkWidth);
            height = Convert.ToInt32(checkHeight);

            var game2 = new BoardDimOld(height, width);
            var doIt = false;
            do
            {


                if (height < 8 || width < 8)
                {
                    Console.WriteLine("Too small. Minimal board size is 8x8");
                }

                else if (height > 30 || width > 30)
                {
                    Console.WriteLine("Too big. Maximum board size is 30x30");
                }


            } while (!doIt);
            
            return "GAME OVER!!";




        }
        */

       static string SaveGameState()
       {
           var game = new BoardDim(_settings);
           BoardDim.Mines.MinesSetter(_settings.BoardHeight, _settings.BoardWidth);
           return "";

       }

        public static (int result, bool wasCanceled) GetUserIntInput(string prompt, int min, int max,
            int? cancelIntValue = null, string cancelStrValue = "")
        {
            do
            {
                Console.WriteLine(prompt);
                if (cancelIntValue.HasValue || !string.IsNullOrWhiteSpace(cancelStrValue) )
                {
                    Console.WriteLine($"To cancel input enter: {cancelIntValue}" +
                                      $"{(cancelIntValue.HasValue && !string.IsNullOrWhiteSpace(cancelStrValue) ? " or " : "")}" +
                                      $"{cancelStrValue}");
                }
                

                Console.Write(">");
                var consoleLine = Console.ReadLine();
                

                if (consoleLine == cancelStrValue) return (0, true);
                
               

                if (int.TryParse(consoleLine, out var userInt)  )
                {
                    return userInt == cancelIntValue ? (userInt, true) : (userInt, false);
                }

                Console.WriteLine($"'{consoleLine}' cant be converted to int value!");
            } while (true);
        }


        

    }

    internal class BoardDimOld
    {
        

        private static CellState[,] Board { get; set; }

        public int BoardWidth { get; }
        public int BoardHeight { get; }



        public BoardDimOld(int boardHeight, int boardWidth)
        {

            if (boardHeight > 7 || boardWidth > 7)
            {
                BoardHeight = boardHeight;
                BoardWidth = boardWidth;
                // initialize the board
                Board = new CellState[boardHeight, boardWidth];

            }


            else
            {
                Console.WriteLine($"{boardHeight} & {boardWidth} is too small! Minimum 8x8");
                throw new ArgumentException("Board size has to be at least 8x8!");


            }



        }





        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;

        }


    }
}




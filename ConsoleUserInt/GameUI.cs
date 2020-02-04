using System;
using System.ComponentModel;
using Engine;
using Game;
using GameEngine;

namespace ConsoleUserInt
{
    namespace ConsoleUI
    {
        public static class GameUI
        {
            
            private static readonly string _verticalSeparator = "┇";
            private static readonly string _horizontalSeparator = "-";
            private static readonly string _centerSeparator = "◉";
            
            /*
            public static void PrintBoard(Engine.BoardDim game)
            {
                var g = new Engine.BoardDim.Mines();
               // g.GetMineCount();
                    
                    
                    
                    
                    
                var board = game.GetBoard();
                for (int yIndex = 0; yIndex < game.BoardHeight; yIndex++)
                {
                    var line = "";
                    for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                    {
                    
                        line = line + " " + GetSingleState(board[yIndex, xIndex]) + " ";
                        if (xIndex < game.BoardWidth - 1)
                        {
                            line = line + _verticalSeparator;
                        }
                    }
                
                    Console.WriteLine(line);

                    if (yIndex < game.BoardHeight - 1)
                    {
                        line = "";
                        for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                        {
                            line = line + _horizontalSeparator+ _horizontalSeparator+ _horizontalSeparator;
                            if (xIndex < game.BoardWidth - 1)
                            {
                                line = line +_centerSeparator;
                            }
                        }
                        Console.WriteLine(line);
                    }

                
                }
                
            }
            */
            
            public static void PrintBoard(BoardDim game)
            {
                var board = game.GetBoard();

                var line = "      ";
                for (int i = 0; i < game.BoardWidth; i++)
                {
                    line += (i + 1).ToString().PadRight(4, ' ');
                }
                Console.WriteLine(line);
                for (int yIndex = 0; yIndex < game.BoardHeight + 1; yIndex++)
                {
                    line = "    |";
                    for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++) {
                        line += _horizontalSeparator + _horizontalSeparator + _horizontalSeparator;
                        if (xIndex < game.BoardWidth - 1)
                        {
                            line += _centerSeparator;
                        }
                        else
                        {
                            line += _verticalSeparator;
                        }
                    } 
                    Console.WriteLine(line);
                    if (yIndex < game.BoardHeight)
                    {
                        line = "";
                        line = line + (yIndex + 1).ToString().PadLeft(3, ' ') + " |";
                        for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                        {
                            line = line + " " + GetSingleState(board[yIndex, xIndex]) + " ";

                            if (xIndex < game.BoardWidth)
                            {
                                line += _verticalSeparator;
                            }
                        }

                        line = line + " " + (yIndex + 1).ToString();

                        Console.WriteLine(line);
                    }
                }
                line = "      ";
                for (int i = 0; i < game.BoardWidth; i++)
                {
                    line += (i + 1).ToString().PadRight(4, ' ');
                }
                Console.WriteLine(line);
            }

            public static string GetSingleState(CellState state)
            {
                

                switch (state)
                {
                   case CellState.Empty:
                       return " ";
                    case CellState.Mine:
                        ConsoleColor mineColor = (Console.ForegroundColor = ConsoleColor.Red);
                        return "☢";
                    case CellState.Flag:
                        return "±";
                    case CellState.Hidden:
                        return "-";
                    case CellState.Open:
                        return "◦";
                    default:
                        throw new InvalidEnumArgumentException("Unknown enum option!");
                }
            
            }
            
            
            
            
            //so, randomizer should generate an array in that range where the mines are stored. If they insert y/x where mine is stored, game ends
        }
    }
}
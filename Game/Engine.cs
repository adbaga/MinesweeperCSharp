using System;
using System.Runtime.Versioning;
using Game;
using GameEngine;

namespace Engine

{
    
    
    public class BoardDim
    {
        public static CellState[,] Board { get; set; }

        public int BoardWidth { get; }
        public int BoardHeight { get; }
        private static int[,]  MineLoc { get; set; }
        public bool mineHere { get; set; }
        
        private int CellNumb { get; }
        
        
        

        public BoardDim(GameSettings settings)
        {
           
                if (settings.BoardHeight > 7 || settings.BoardWidth > 7) 
                {
                    BoardHeight = settings.BoardHeight;
                    BoardWidth = settings.BoardWidth;
                    // initialize the board
                    Board = new CellState[BoardHeight, BoardWidth];
                    
                }


                else
                {
                    Console.WriteLine($"{BoardHeight} & {BoardWidth} is too small! Minimum 8x8");
                    throw new ArgumentException("Board size has to be at least 8x8!");

                }
                


        }


        
        

        



        public void Move(int posY, int posX)
        {
            var h = Board[posY, posX];
            if (MineLoc[posY, posX] == 1)
            {
                Board[posY, posX] = CellState.Mine;

               for (int i = 0; i < BoardWidth; i++)
                {
                    for (int j = 0; j < BoardHeight; j++)
                    {
                        if (MineLoc[i, j] == 1)
                        {
                            Board[i, j] = CellState.Mine;
                            
                        }
                        
                    }
                }
                
                
                
            }

            mineHere = Board[posY, posX] == (CellState) MineLoc[posY,posX];
            
            if (Board[posY, posX] != CellState.Empty)
            {
               return;
            }

           
            Board[posY, posX] = CellState.Open;
            


        }

        
       

        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;

        }
        
        public class Mines
        {
        
        
        
            public int minePosY { get; set; }
            public int minePosX { get; set; }
            
        
            private static  Random random = new Random();
        
            public static int[,] MinesSetter(int height, int width)       
            {
                int mineCount = 0;
                int numOfMines = height * width / 6;
                //int[,] mineLoc = new int[width, height];

            
            
                MineLoc = new int[width, height];
                {
                    do
                    {
                        int minePosY = random.Next(width);
                        int minePosX = random.Next(height);

                        // Make sure we haven't already set this position

                        if (MineLoc[minePosY, minePosX] != 1)
                        {
                            MineLoc[minePosY, minePosX] = 1;
                            mineCount++;

                        }

                    } while (mineCount <= numOfMines);
                }
                Console.WriteLine($"There are {numOfMines} mines");
                Console.WriteLine(MineLoc.Length);

                return MineLoc;
            }
            
           
            
            public int GetMineCount(int posY, int posX)
            {
                int count = 0;
                if (Board[posY-1, posX-1] == CellState.Mine)
                    count++;
                if (Board[posY, posX-1] == CellState.Mine)
                    count++;
                if (Board[posY + 1, posX - 1] == CellState.Mine)
                    count++;
            
                if (Board[posY + 1, posX] == CellState.Mine)
                    count++;
            
                if (Board[posY + 1, posX + 1] == CellState.Mine)
                    count++;
                if (Board[posY, posX+1] == CellState.Mine)
                    count++;
            
                if (Board[posY-1, posX+1] == CellState.Mine)
                    count++;
            
                if (Board[posY-1, posX] == CellState.Mine)
                    count++;

                return count;
                

            }
            
        }

        

        
        
     
       
        
    }
      


        

    }
    
    


    

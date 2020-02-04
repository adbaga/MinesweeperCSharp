using System.Text.Json;
using GameEngine;

namespace Game
{
    public static class GameConfigHandler
    {
        private const string FileName = "gamesettings.json";
        
        public static void SaveConfig(GameSettings settings, string fileName = FileName)
        {
            using (var writer = System.IO.File.CreateText(fileName))
            {
                var jsonString = JsonSerializer.Serialize(settings);
                writer.Write(jsonString);
            }
        }

        public static GameSettings LoadConfig(string fileName = FileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                var res = JsonSerializer.Deserialize<GameSettings>(jsonString);
                return res;
            }
            
            return new GameSettings();
        }

        public static CellState[,] SaveCellState(CellState[,] gameState, string filename = FileName)
        {
            using (var writer = System.IO.File.CreateText(FileName))
            {
                var jsonString = JsonSerializer.Serialize(gameState);
                writer.Write(jsonString);

                return gameState;
            }
            
        }

        public static CellState[,] LoadCellState(string fileName = FileName )
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                CellState[,] res = JsonSerializer.Deserialize<CellState[,]>(jsonString);
                return res;
            }
            
            return new CellState[,];

            
        }
    }
}
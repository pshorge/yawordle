using Yawordle.Core;
using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using static System.DateTime;

namespace Yawordle.Infrastructure
{
    public class JsonStatsService : IStatsService
    {
        public PlayerStats CurrentStats { get; private set; }
        private readonly string _savePath = Path.Combine(Application.persistentDataPath, "playerstats.json");

        public JsonStatsService()
        {
            LoadStats();
        }

        private void LoadStats()
        {
            CurrentStats = File.Exists(_savePath) 
                ? JsonConvert.DeserializeObject<PlayerStats>(File.ReadAllText(_savePath)) : new PlayerStats();
        }
        
        public void RecordGameResult(bool isWin, int attempt, bool isDaily)
        {
            CurrentStats.GamesPlayed++;

            if (isWin)
            {
                CurrentStats.GamesWon++;
                CurrentStats.CurrentStreak++;
                if (CurrentStats.CurrentStreak > CurrentStats.MaxStreak)
                {
                    CurrentStats.MaxStreak = CurrentStats.CurrentStreak;
                }

                if (attempt >= 0 && attempt < CurrentStats.GuessDistribution.Length)
                {
                    CurrentStats.GuessDistribution[attempt]++;
                }
            }
            else
                CurrentStats.CurrentStreak = 0;

            if (isDaily) 
                CurrentStats.LastCompletedDaily = UtcNow.ToString("yyyy-MM-dd");
            
            SaveStatsAsync().Forget();
        }
        
        public async UniTask SaveStatsAsync()
        {
            await File.WriteAllTextAsync(_savePath, JsonConvert.SerializeObject(CurrentStats, Formatting.Indented));
            Debug.Log("<color=lime>Player stats saved.</color>");
        }
    }
}
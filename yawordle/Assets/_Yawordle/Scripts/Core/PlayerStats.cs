using System;

namespace Yawordle.Core
{
    [Serializable]
    public class PlayerStats
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int CurrentStreak { get; set; }
        public int MaxStreak { get; set; }
        public int[] GuessDistribution { get; set; } = new int[6]; 
        
        public string LastCompletedDaily { get; set; }
    }
}
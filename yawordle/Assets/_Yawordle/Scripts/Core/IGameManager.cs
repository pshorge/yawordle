using System;

namespace Yawordle.Core
{
    
    public readonly struct GameResult
    {
        public readonly bool IsWin;
        public readonly int FinalAttempt; 
    
        public GameResult(bool isWin, int finalAttempt)
        {
            IsWin = isWin;
            FinalAttempt = finalAttempt;
        }
    }
    
    public interface IGameManager
    {
        
        
        event Action<GuessValidationError> OnGuessValidationFailed;
        event Action<int, string> OnGuessUpdated;
        event Action<int, LetterState[]> OnGuessEvaluated;
        event Action<GameResult> OnGameFinished; 

        int MaxAttempts { get; }
        int CurrentAttempt { get; }
        
        string TargetWord { get; }
        void StartNewGame(string targetWord);
        void TypeLetter(char letter);
        void DeleteLetter();
        void SubmitGuess();
    }
}
using VContainer.Unity;
using Yawordle.Core;

namespace Yawordle.DI
{
    public class StatsHandler : IStartable
    {
        private readonly IGameManager _gameManager;
        private readonly ISettingsService _settingsService;
        private readonly IStatsService _statsService;

        public StatsHandler(IGameManager gameManager, ISettingsService settingsService, IStatsService statsService)
        {
            _gameManager = gameManager;
            _settingsService = settingsService;
            _statsService = statsService;
        }

        public void Start()
        {
            _gameManager.OnGameFinished += HandleGameFinished;
        }

        private void HandleGameFinished(GameResult result)
        {
            _gameManager.OnGameFinished -= HandleGameFinished; 
            
            var isDaily = _settingsService.CurrentSettings.Mode == GameMode.Daily;
            _statsService.RecordGameResult(result.IsWin, result.FinalAttempt, isDaily);
        }
    }
}
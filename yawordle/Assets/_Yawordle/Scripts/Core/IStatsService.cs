using Cysharp.Threading.Tasks;

namespace Yawordle.Core
{
    public interface IStatsService
    {
        PlayerStats CurrentStats { get; }
        void RecordGameResult(bool isWin, int attemptIndex, bool isDaily);
        UniTask SaveStatsAsync();
    }
}
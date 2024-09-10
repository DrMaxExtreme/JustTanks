using Agava.YandexGames;
using JustTanks.GameLogic;

namespace JustTanks.Boosts
{
    public class AdManager : IAdManager
    {
        private GameFocusManager _gameFocusManager;

        public AdManager(GameFocusManager gameFocusManager)
        {
            _gameFocusManager = gameFocusManager;
        }

        public void ShowAd()
        {
            VideoAd.Show(PauseGame, ContinueGame);
        }

        private void PauseGame()
        {
            _gameFocusManager.SetOpenAdMarker(true);
        }

        private void ContinueGame()
        {
            _gameFocusManager.SetOpenAdMarker(false);
        }
    }
}

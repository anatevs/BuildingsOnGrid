using SaveLoad;
using UI;

namespace GameManagement
{
    public sealed class StartGameManager
    {
        private readonly SaveLoadManager _saveLoadManager;

        public StartGameManager(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        public void StartGame()
        {
            _saveLoadManager.LoadAll();
        }
    }
}
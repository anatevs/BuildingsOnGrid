using SaveLoad;
namespace GameManagement
{
    public sealed class EndGameManager
    {
        private readonly SaveLoadManager _saveLoadManager;

        private readonly SaveLoadStorage _saveLoadStorage;

        private readonly ApplicationShutdown _applicationShutdown;

        public EndGameManager(SaveLoadManager saveLoadManager,
            SaveLoadStorage saveLoadStorage,
            ApplicationShutdown applicationShutdown)
        {
            _saveLoadManager = saveLoadManager;
            _saveLoadStorage = saveLoadStorage;
            _applicationShutdown = applicationShutdown;
        }

        public void EndGame()
        {
            _saveLoadManager.SaveAll();

            _saveLoadStorage.SaveState();

            _applicationShutdown.QuitApp();
        }
    }
}
using SaveLoad;
namespace GameManagement
{
    public class EndGameManager
    {
        private readonly SaveLoadManager _saveLoadManager;

        private readonly ApplicationShutdown _applicationShutdown;

        public EndGameManager(SaveLoadManager saveLoadManager,
            ApplicationShutdown applicationShutdown)
        {
            _saveLoadManager = saveLoadManager;
            _applicationShutdown = applicationShutdown;
        }

        public void EndGame()
        {
            _saveLoadManager.SaveAll();

            _applicationShutdown.QuitApp();
        }
    }
}
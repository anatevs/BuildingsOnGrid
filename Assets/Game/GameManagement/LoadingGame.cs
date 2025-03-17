using Cysharp.Threading.Tasks;
using SaveLoad;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace GameManagement
{
    public sealed class LoadingGame :
        IInitializable
    {
        private readonly SaveLoadStorage _saveLoadStorage;

        private readonly int _gameSceneID = 1;

        public LoadingGame(SaveLoadStorage saveLoadStorage)
        {
            _saveLoadStorage = saveLoadStorage;
        }

        async void IInitializable.Initialize()
        {
            _saveLoadStorage.LoadState();

            //SceneManager.LoadScene(_gameSceneID);

            await SceneManager.LoadSceneAsync(_gameSceneID);
        }
    }
}
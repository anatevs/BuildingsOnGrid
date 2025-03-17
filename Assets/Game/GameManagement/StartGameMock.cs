using GameManagement;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public sealed class StartGameMock : MonoBehaviour
    {
        private StartGameManager _startGameManager;

        [Inject]
        private void Construct(StartGameManager startGameManager)
        {
            _startGameManager = startGameManager;
        }

        private void Start()
        {
            _startGameManager.StartGame();
        }
    }
}
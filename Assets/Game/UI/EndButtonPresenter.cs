using GameManagement;
using System;
using UnityEngine.UI;
using VContainer.Unity;

namespace UI
{
    public sealed class EndButtonPresenter :
        IInitializable,
        IDisposable
    {
        private readonly Button _endGameButton;

        private readonly EndGameManager _endGameManager;

        public EndButtonPresenter(Button endGameButton,
            EndGameManager endGameManager)
        {
            _endGameButton = endGameButton;
            _endGameManager = endGameManager;
        }

        void IInitializable.Initialize()
        {
            _endGameButton.onClick.AddListener(_endGameManager.EndGame);
        }

        void IDisposable.Dispose()
        {
            _endGameButton.onClick.RemoveListener(_endGameManager.EndGame);
        }
    }
}
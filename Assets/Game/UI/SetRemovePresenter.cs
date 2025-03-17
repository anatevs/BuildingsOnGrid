using GameCore;
using System;
using VContainer.Unity;

namespace UI
{
    public sealed class SetRemovePresenter :
        IInitializable,
        IDisposable
    {
        private readonly SetRemoveView _view;

        private readonly ItemsManager _itemsManager;

        private readonly Player _player;

        public SetRemovePresenter(SetRemoveView view,
            ItemsManager itemsManager, Player player)
        {
            _view = view;
            _itemsManager = itemsManager;
            _player = player;
        }

        void IInitializable.Initialize()
        {
            _view.OnSetClicked += _itemsManager.SetItem;
            _view.OnRemoveClicked += ClickRemoveButton;
        }
        void IDisposable.Dispose()
        {
            _view.OnSetClicked -= _itemsManager.SetItem;
            _view.OnRemoveClicked -= ClickRemoveButton;
        }

        private void ClickRemoveButton()
        {
            _player.IsRemoving = true;
        }
    }
}
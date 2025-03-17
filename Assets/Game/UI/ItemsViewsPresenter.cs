using GameCore;
using UnityEngine;
using VContainer.Unity;
using System;

namespace UI
{
    public sealed class ItemsViewsPresenter : IInitializable,
        IDisposable
    {
        public event Action<int> OnViewClicked;

        private readonly AllItemsConfig _allItemsConfig;

        private readonly Transform _viewsParent;

        private readonly ItemsManager _itemsManager;

        private ItemViewPresenter[] _presenters;

        public ItemsViewsPresenter(AllItemsConfig allItemsConfig,
            Transform viewsParent, ItemsManager itemsManager)
        {
            _allItemsConfig = allItemsConfig;
            _viewsParent = viewsParent;
            _itemsManager = itemsManager;
        }

        void IInitializable.Initialize()
        {
            (int start, int end) = _allItemsConfig.IDInterval;

            _presenters = new ItemViewPresenter[end - start + 1];

            for (int id = start; id <= end; id++)
            {
                var view = GameObject.Instantiate(_allItemsConfig.ViewPrefab);

                view.transform.SetParent(_viewsParent);

                var itemPrefab = _allItemsConfig.GetPrefab(id);

                var sprites = _allItemsConfig.GetViewSprite(itemPrefab.TypeName, itemPrefab.Name);

                var presenter = new ItemViewPresenter(view, id);

                presenter.SetViewSprite(sprites);

                _presenters[id - start] = presenter;
            }

            Show();
        }

        void IDisposable.Dispose()
        {
            Hide();
        }


        public void Show()
        {
            foreach (var presenter in _presenters)
            {
                presenter.OnItemSelected += ClickView;
                presenter.Show();
            }

            OnViewClicked += _itemsManager.SetCurrentSelected;
        }

        public void Hide()
        {
            foreach (var presenter in _presenters)
            {
                presenter.OnItemSelected -= ClickView;
                presenter.Hide();
            }

            OnViewClicked -= _itemsManager.SetCurrentSelected;
        }

        private void ClickView(int id)
        {
            OnViewClicked?.Invoke(id);
        }
    }
}
using System;
using UnityEngine;

namespace UI
{
    public sealed class ItemViewPresenter
    {
        public event Action<int> OnItemSelected;

        private readonly ItemView _view;

        private readonly int _id;

        public ItemViewPresenter(ItemView view, int id)
        {
            _view = view;
            _id = id;
        }

        public void Show()
        {
            _view.OnClick += MakeOnSelect;
        }

        public void Hide()
        {
            _view.OnClick -= MakeOnSelect;
        }

        public void SetViewSprite((Sprite main, Sprite active) sprites)
        {
            _view.SetButtonSprite(sprites.main);
            _view.SetActiveSprite(sprites.active);
        }

        private void MakeOnSelect()
        {
            OnItemSelected?.Invoke(_id);
        }
    }
}
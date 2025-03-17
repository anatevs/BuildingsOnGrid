using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class ItemView : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField]
        private Button _selectButton;

        public void SetButtonSprite(Sprite sprite)
        {
            _selectButton.image.sprite = sprite;
        }

        public void SetActiveSprite(Sprite sprite)
        {
            var state = new SpriteState();

            state.selectedSprite = sprite;
            state.pressedSprite = sprite;
            state.highlightedSprite = sprite;

            _selectButton.spriteState = state;
            
        }

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(MakeOnClick);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(MakeOnClick);
        }

        private void MakeOnClick()
        {
            OnClick?.Invoke();
        }
    }
}
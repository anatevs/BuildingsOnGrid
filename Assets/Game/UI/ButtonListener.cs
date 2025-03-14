using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class ButtonListener : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(MakeOnClick);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(MakeOnClick);
        }

        private void MakeOnClick()
        {
            OnClick?.Invoke();
        }
    }
}
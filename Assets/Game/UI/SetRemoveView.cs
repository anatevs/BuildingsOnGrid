using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class SetRemoveView : MonoBehaviour
    {
        public event Action OnRemoveClicked;

        public event Action OnSetClicked;

        [SerializeField]
        private Button _removeButton;

        [SerializeField]
        private Button _setButton;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(ClickRemove);
            _setButton.onClick.AddListener(ClickSet);
        }
        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(ClickRemove);
            _setButton.onClick.RemoveListener(ClickSet);
        }

        private void ClickRemove()
        {
            OnRemoveClicked?.Invoke();
        }

        private void ClickSet()
        {
            OnSetClicked?.Invoke();
        }
    }
}
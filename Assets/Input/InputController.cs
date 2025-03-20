using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Assets.Input
{
    public class InputController :
        IInitializable,
        IDisposable
    {
        public event Action<Vector2> OnClickGO;

        public Vector2 PointerPosition => _pointerPosition;

        private GameControls _controls;

        private Vector2 _pointerPosition;

        private readonly List<RaycastResult> _uiRaycastResults = new();

        private readonly PointerEventData _pointerEventData = new(EventSystem.current);

        public InputController()
        {
            _controls = new GameControls();
        }

        void IInitializable.Initialize()
        {
            _controls.Enable();

            _controls.Gameplay.Click.performed += ClickGO;

            _controls.Gameplay.FollowPointer.performed += Follow;
        }

        void IDisposable.Dispose()
        {
            _controls.Gameplay.Click.performed -= ClickGO;

            _controls.Gameplay.FollowPointer.performed -= Follow;

            _controls.Disable();
        }

        private void Follow(InputAction.CallbackContext context)
        {
            _pointerPosition = context.ReadValue<Vector2>();
        }

        private void ClickGO(InputAction.CallbackContext context)
        {
            _pointerPosition = context.ReadValue<Vector2>();

            if (!IsPointerOverUI(_pointerPosition))
            {
                OnClickGO?.Invoke(_pointerPosition);
            }
        }

        private bool IsPointerOverUI(Vector2 pos)
        {
            _pointerEventData.position = pos;

            _uiRaycastResults.Clear();

            EventSystem.current.RaycastAll(_pointerEventData, _uiRaycastResults);

            return _uiRaycastResults.Count > 0;
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Assets.Input
{
    public class InputController :
        IInitializable,
        IDisposable
    {
        public event Action<Vector2> OnClick;

        public Vector2 PointerPosition => _pointerPosition;

        private GameControls _controls;

        private Vector2 _pointerPosition;

        public InputController()
        {
            _controls = new GameControls();
        }

        void IInitializable.Initialize()
        {
            _controls.Enable();

            _controls.Gameplay.Click.performed += Click;

            _controls.Gameplay.FollowPointer.performed += Follow;
        }

        void IDisposable.Dispose()
        {
            _controls.Gameplay.Click.performed -= Click;

            _controls.Gameplay.FollowPointer.performed -= Follow;

            _controls.Disable();
        }

        private void Follow(InputAction.CallbackContext context)
        {
            _pointerPosition = context.ReadValue<Vector2>();
        }

        private void Click(InputAction.CallbackContext context)
        {
            _pointerPosition = context.ReadValue<Vector2>();

            OnClick?.Invoke(_pointerPosition);
        }
    }
}
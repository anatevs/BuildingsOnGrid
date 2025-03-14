using Assets.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private bool _isSticking;

        private InputController _input;

        [Inject]
        private void Construct(InputController input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.OnClick += Click;
        }

        private void OnDisable()
        {
            _input.OnClick -= Click;
        }

        private void Update()
        {
            if (_isSticking)
            {
                var pos = _input.PointerPosition;
                Debug.Log(pos);
            }
        }

        private void Click(Vector2 pos)
        {
            Debug.Log($"click at {pos}");
        }
    }
}
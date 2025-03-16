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

        [SerializeField]
        private LayerMask _groundLayer;

        private PlayingGrid _playingGrid;

        private InputController _input;

        private Camera _camera;

        [Inject]
        private void Construct(InputController input, PlayingGrid playingGrid)
        {
            _input = input;
            _playingGrid = playingGrid;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _input.OnClick += Click;
        }

        private void OnDisable()
        {
            _input.OnClick -= Click;
        }


        [SerializeField]
        private MapItem _testItem;

        private void Update()
        {
            if (_isSticking)
            {
                var screenPos = _input.PointerPosition;

                if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, _groundLayer))
                {
                    var pos = hit.point;
                    var originPosInt = _playingGrid.GetTileOriginCoordinate(pos);

                    if (_playingGrid.IsAreaFree(_playingGrid.GetTileIndex(originPosInt), _testItem.Size))
                    {
                        _testItem.SetPosition(originPosInt);
                    }
                }
            }
        }

        private void Click(Vector2 screenPos)
        {
            if (IsPointerHitGround(screenPos, out var pos))
            {
                var posInt = _playingGrid.GetTileOriginCoordinate(pos);
            }
        }

        private bool IsPointerHitGround(Vector2 screenPos, out Vector3 clickPos)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, _groundLayer))
            {
                clickPos = hit.point;
                return true;
            }

            clickPos = Vector3.zero;
            return false;
        }
    }
}
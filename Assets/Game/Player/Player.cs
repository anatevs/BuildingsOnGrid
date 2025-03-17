using Assets.Input;
using System;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class Player : MonoBehaviour
    {
        public event Action<MapItem> OnItemRemoved;

        public bool IsSticking => _isSticking;

        public bool IsRemoving
        {
            get => _isRemoving;
            set => _isRemoving = value;
        }

        [SerializeField]
        private LayerMask _groundLayer;

        [SerializeField]
        private LayerMask _itemLayer;

        [SerializeField]
        private float _rayCastMaxDistance = 100f;

        private bool _isSticking;

        private bool _isRemoving;

        private PlayingGrid _playingGrid;

        private InputController _input;

        private Camera _camera;

        private MapItem _currentSticked;

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
            _input.OnClickGO += Click;
        }

        private void OnDisable()
        {
            _input.OnClickGO -= Click;
        }

        private void Update()
        {
            if (_isSticking)
            {
                var screenPos = _input.PointerPosition;

                if (IsPointerHitLayer(screenPos, _groundLayer, out var hit))
                {
                    var pos = hit.point;
                    var originPosInt = _playingGrid.GetTileOriginCoordinate(pos);
                    var originIndex = _playingGrid.GetTileIndex(originPosInt);

                    if (_playingGrid.IsAreaFree(originIndex, _currentSticked.Size))
                    {
                        _currentSticked.SetPosition(originPosInt, originIndex);
                    }
                }
            }
        }

        public void SetCurrentSticking(MapItem item)
        {
            _isSticking = true;
            _currentSticked = item;
        }

        public void UnsetCurrentSticking(out MapItem current)
        {
            current = _currentSticked;

            _isSticking = false;
            _currentSticked = null;
        }

        private void Click(Vector2 screenPos)
        {
            if (_isSticking)
            {
                if (IsPointerHitLayer(screenPos, _groundLayer, out _))
                {
                    _playingGrid.SetArea(_currentSticked.OriginIndex,
                        _currentSticked.Size, _currentSticked.ID);

                    _currentSticked = null;
                    _isSticking = false;
                }

                return;
            }
            if (_isRemoving)
            {
                if (IsPointerHitLayer(screenPos, _itemLayer, out var hit))
                {
                    var item = hit.collider.GetComponent<MapItem>();

                    _playingGrid.ClearArea(item.OriginIndex, item.Size);

                    OnItemRemoved?.Invoke(item);
                }
            }
        }

        private bool IsPointerHitLayer(Vector2 screenPos, LayerMask layerMask, out RaycastHit hit)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out hit, _rayCastMaxDistance, layerMask))
            {
                return true;
            }

            return false;
        }
    }
}
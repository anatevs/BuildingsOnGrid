using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public abstract class MapItem : MonoBehaviour
    {
        public int ID => _id;

        public string TypeName => _typeName;

        public string Name => _name;

        public (int x, int y) Size => _size;

        public (int x, int z) Position { get; set; }

        public (int x, int y) OriginIndex { get; set; }

        [SerializeField]
        private int[] _maxSizeXY = new int[2];

        [SerializeField]
        private string _name;

        [SerializeField]
        private GameObject _view;

        private int _id;

        private (int x, int y) _size;

        private string _typeName;

        private Bounds _bounds;

        private (float x, float z) _shift;

        private void Awake()
        {
            Init(0, "test");
        }

        public virtual void Init(int id, string typeName)
        {
            _id = id;
            _typeName = typeName;
            _size = (_maxSizeXY[0], _maxSizeXY[1]);

            _shift = ((float)_size.x / 2, (float)_size.y / 2);

            _bounds = ItemViewSize.GetBounds(_view);

            if (_view != null)
            {
                if (!(ItemViewSize.IsBoundsHaveSize(_bounds, _size, out var objectSize)))
                {
                    throw new System.Exception($"the size {_size} is not equals to this object's view size {objectSize}");
                }
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = new Vector3(
                position.x + _shift.x,
                transform.position.y,
                position.z + _shift.z
                );
        }

        public void SetPosition((int x, int z) position)
        {
            transform.position = new Vector3(
                position.x + _shift.x,
                transform.position.y,
                position.z + _shift.z
                );
        }

        public (int x, int y) GetOriginIndex()
        {
            return ((int)(transform.position.x - _shift.x),
                (int)(transform.position.z - _shift.z));
        }
    }
}
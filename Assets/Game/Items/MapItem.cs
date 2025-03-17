using UnityEngine;

namespace GameCore
{
    public abstract class MapItem : MonoBehaviour
    {
        public int ID => _id;

        public string TypeName => _typeName;

        public string Name => _name;

        public (int x, int y) Size => _size;

        public (int x, int z) OriginPosition => _originPos;

        public (int x, int y) OriginIndex => _originIndex;

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

        private (int x, int z) _originPos;

        private (int x, int y) _originIndex;

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

        public void SetPosition((int x, int z) originPosition, (int x, int y) originIndex)
        {
            transform.position = new Vector3(
                originPosition.x + _shift.x,
                transform.position.y,
                originPosition.z + _shift.z
                );

            _originPos = originPosition;
            _originIndex = originIndex;
        }

        public (int x, int y) GetOriginIndex()
        {
            return ((int)(transform.position.x - _shift.x),
                (int)(transform.position.z - _shift.z));
        }
    }
}
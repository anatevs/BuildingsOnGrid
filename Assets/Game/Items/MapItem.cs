using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public abstract class MapItem : MonoBehaviour
    {
        public int ID => _id;

        public string TypeID => _typeID;

        public (int x, int y) Size => _size;

        [SerializeField]
        private int[] _maxSizeXY = new int[2];

        [SerializeField]
        private string _name;

        [SerializeField]
        private GameObject _view;

        private int _id;

        private (int x, int y) _size;

        protected string _typeID;

        public virtual void Init(int id, string nameID)
        {
            _id = id;
            _typeID = nameID;
            _size = (_maxSizeXY[0], _maxSizeXY[1]);

            if (_view != null)
            {
                if (!(ItemViewSize.IsObjectHasSize(_view, _size, out var objectSize)))
                {
                    throw new System.Exception($"the size {_size} is not equals to this object's view size {objectSize}");
                }
            }
        }

        private void OnEnable()
        {
            Init(0, "b1");
        }
    }
}
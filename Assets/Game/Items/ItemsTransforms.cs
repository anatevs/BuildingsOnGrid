using UnityEngine;

namespace GameCore
{
    public class ItemsTransforms : MonoBehaviour
    {
        public Transform PoolParent => _poolParent;

        public Transform MapItemsParent => _mapItemsParent;

        [SerializeField]
        private Transform _poolParent;

        [SerializeField]
        private Transform _mapItemsParent;
    }
}
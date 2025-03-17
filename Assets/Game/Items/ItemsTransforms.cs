using UnityEngine;

namespace GameCore
{
    public sealed class ItemsTransforms : MonoBehaviour
    {
        public Transform PoolParent => _poolParent;

        public Transform MapItemsParent => _mapItemsParent;

        public Transform ViewsParent => _viewsParent;

        [SerializeField]
        private Transform _poolParent;

        [SerializeField]
        private Transform _mapItemsParent;

        [SerializeField]
        private Transform _viewsParent;
    }
}
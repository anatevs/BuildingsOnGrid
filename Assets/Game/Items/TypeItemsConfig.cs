using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "ItemsConfig",
        menuName = "Configs/TypeItemsConfig")]
    public sealed class TypeItemsConfig : ScriptableObject
    {
        public int Length => _itemsPrefabs.Length;

        public string TypeName => _typeName;

        [SerializeField]
        private string _typeName;

        [SerializeField]
        private MapItem[] _itemsPrefabs;

        public void Init(int count, out int id)
        {
            id = 0;

            for (int i = 0; i < _itemsPrefabs.Length; i++)
            {
                id = i + count;
                _itemsPrefabs[i].Init(id, _typeName);
            }
        }

        private void OnEnable()
        {
            if (_itemsPrefabs.Length > 0)
            {
                var firstType = _itemsPrefabs[0].GetType();

                foreach (var item in _itemsPrefabs)
                {
                    if (item.GetType() != firstType)
                    {
                        throw new System.Exception($"not all items in {this} config have the same type!!");
                    }
                }
            }
        }
    }
}
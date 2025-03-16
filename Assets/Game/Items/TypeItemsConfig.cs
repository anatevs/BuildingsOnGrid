using System.Collections.Generic;
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

        private readonly Dictionary<string, MapItem> _prefabs = new();

        private readonly Dictionary<int, string> _idNames = new();

        public void Init(int count, out int id)
        {
            id = 0;

            for (int i = 0; i < _itemsPrefabs.Length; i++)
            {
                id = i + count;
                _itemsPrefabs[i].Init(id, _typeName);

                _prefabs.Add(_itemsPrefabs[i].Name, _itemsPrefabs[i]);

                _idNames.Add(id, _itemsPrefabs[i].Name);
            }
        }

        public MapItem GetPrefab(string name)
        {
            return _prefabs[name];
        }

        public MapItem GetPrefab(int id)
        {
            return _prefabs[_idNames[id]];
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
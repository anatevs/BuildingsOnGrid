using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "AllItemsConfig",
        menuName = "Configs/AllItemsConfig")]
    public sealed class AllItemsConfig : ScriptableObject
    {
        [SerializeField]
        TypeItemsConfig[] _typeConfigs;

        private readonly Dictionary<int, MapItem> _idPrefabs = new();

        private readonly Dictionary<string, TypeItemsConfig> _typeItemsConfigs = new();

        public MapItem GetPrefab(string typeName, string name) //load
        {
            return _typeItemsConfigs[typeName].GetPrefab(name);
        }

        public MapItem GetPrefab(int id) //in game session
        {
            return _idPrefabs[id];
        }

        public void Init()
        {
            int count = 1;
            for (int i = 0; i < _typeConfigs.Length; i++)
            {
                _typeConfigs[i].Init(count, out var id);

                _idPrefabs.Add(id, _typeConfigs[i].GetPrefab(id));

                _typeItemsConfigs.Add(_typeConfigs[i].TypeName, _typeConfigs[i]);

                count += _typeConfigs.Length;
            }
        }
    }
}
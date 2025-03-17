using System.Collections.Generic;
using UI;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "AllItemsConfig",
        menuName = "Configs/AllItemsConfig")]
    public sealed class AllItemsConfig : ScriptableObject
    {
        public (int start, int end) IDInterval => _idInterval;

        public ItemView ViewPrefab => _viewPrefab;

        [SerializeField]
        private TypeItemsConfig[] _typeConfigs;

        [SerializeField]
        private ItemView _viewPrefab;

        private readonly Dictionary<int, MapItem> _idPrefabs = new();

        private readonly Dictionary<string, TypeItemsConfig> _typeItemsConfigs = new();

        private (int start, int end) _idInterval;

        public MapItem GetPrefab(string typeName, string name) //load
        {
            return _typeItemsConfigs[typeName].GetPrefab(name);
        }

        public (Sprite main, Sprite active) GetViewSprite(string typeName, string name)
        {
            return _typeItemsConfigs[typeName].GetMenuSprite(name);
        }

        public MapItem GetPrefab(int id) //in game session
        {
            return _idPrefabs[id];
        }

        public void Init()
        {
            _idInterval.start = 1;

            int count = _idInterval.start;

            for (int i = 0; i < _typeConfigs.Length; i++)
            {
                _typeConfigs[i].Init(count, out var idNumbers);

                foreach (var id in idNumbers)
                {
                    _idPrefabs.Add(id, _typeConfigs[i].GetPrefab(id));

                    _idInterval.end = id;
                }

                _typeItemsConfigs.Add(_typeConfigs[i].TypeName, _typeConfigs[i]);

                count += _typeConfigs.Length;
            }
        }
    }
}
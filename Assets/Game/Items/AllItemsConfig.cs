using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "ItemsConfig",
        menuName = "Configs/AllItemsConfig")]
    public sealed class AllItemsConfig : ScriptableObject
    {
        [SerializeField]
        TypeItemsConfig[] _typeConfigs;

        private readonly Dictionary<int, string> _idItemTypeInfo = new Dictionary<int, string>();

        public bool TryGetItemType(int id, out string itemType)
        {
            return _idItemTypeInfo.TryGetValue(id, out itemType);
        }

        private void OnEnable()
        {
            int count = 0;
            for (int i = 0;  i < _typeConfigs.Length; i++)
            {
                _typeConfigs[i].Init(count, out var id);

                _idItemTypeInfo.Add(id, _typeConfigs[i].TypeName);

                count += _typeConfigs.Length;
            }
        }
    }
}
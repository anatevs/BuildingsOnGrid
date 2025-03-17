using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "ItemsConfig",
        menuName = "Configs/TypeItemsConfig")]
    public sealed class TypeItemsConfig : ScriptableObject
    {
        public int Length => _itemConfigDatas.Length;

        public string TypeName => _typeName;

        [SerializeField]
        private string _typeName;

        [SerializeField]
        private ItemConfigData[] _itemConfigDatas;

        private readonly Dictionary<string, MapItem> _prefabs = new();

        private readonly Dictionary<int, string> _idNames = new();

        private readonly Dictionary<string, (Sprite main, Sprite active)> _nameSprites = new();

        public void Init(int count, out int[] idNumbers)
        {
            idNumbers = new int[_itemConfigDatas.Length];

            var id = count;

            for (int i = 0; i < _itemConfigDatas.Length; i++)
            {
                id = i + count;

                idNumbers[i] = id;

                _itemConfigDatas[i].Item.Init(id, _typeName);

                _prefabs.TryAdd(_itemConfigDatas[i].Item.Name, _itemConfigDatas[i].Item);

                _idNames.TryAdd(id, _itemConfigDatas[i].Item.Name);

                _nameSprites.TryAdd(_itemConfigDatas[i].Item.Name,
                    (_itemConfigDatas[i].MenuSprite, _itemConfigDatas[i].ActiveSprite));
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

        public (Sprite main, Sprite active) GetMenuSprite(string name)
        {
            return _nameSprites[name];
        }

        private void OnEnable()
        {
            if (_itemConfigDatas.Length > 0)
            {
                var firstType = _itemConfigDatas[0].Item.GetType();

                foreach (var data in _itemConfigDatas)
                {
                    if (data.Item.GetType() != firstType)
                    {
                        throw new System.Exception($"not all items in {this} config have the same type!!");
                    }
                }
            }
        }
    }

    [Serializable]
    public struct ItemConfigData
    {
        public MapItem Item;

        public Sprite MenuSprite;

        public Sprite ActiveSprite;
    }
}
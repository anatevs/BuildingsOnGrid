using GameCore;
using UnityEngine;

namespace SaveLoad
{
    public class MapItemsSaveLoad :
        ISaveLoad
    {
        private readonly SaveLoadStorage _saveLoadStorage;

        private readonly MapItemSpawner _mapItemSpawner;

        private readonly Transform _mapItemsTransform;

        public MapItemsSaveLoad(SaveLoadStorage saveLoadStorage,
            MapItemSpawner mapItemSpawner, Transform mapItemsTransform)
        {
            _saveLoadStorage = saveLoadStorage;
            _mapItemSpawner = mapItemSpawner;
            _mapItemsTransform = mapItemsTransform;
        }

        public void LoadDefault()
        {
        }

        public void Load()
        {
            if (_saveLoadStorage.TryGetData<MapItemsData>(out var mapItemsData))
            {
                foreach (var data in mapItemsData.Data)
                {
                    _mapItemSpawner.SpawnAndSet(data);
                }

                return;
            }

            LoadDefault();
        }

        public void Save()
        {
            var data = GetMapItemsData();

            _saveLoadStorage.SetData(data);
        }

        private MapItemsData GetMapItemsData()
        {
            var items = _mapItemsTransform.GetComponentsInChildren<MapItem>();

            var itemsData = new OneMapItemData[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                itemsData[i] = GetItemData(items[i]);
            }

            return new MapItemsData(itemsData);
        }

        private OneMapItemData GetItemData(MapItem item)
        {
            return new OneMapItemData(
                item.TypeName,
                item.Name,
                item.Position,
                item.OriginIndex
                );
        }
    }
}
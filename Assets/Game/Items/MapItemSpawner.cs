using SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class MapItemSpawner
    {
        private readonly PlayingGrid _playingGrid;

        private readonly AllItemsConfig _itemsConfig;

        private readonly ItemsTransforms _transforms;

        private readonly Dictionary<int, Queue<MapItem>> _pools = new();

        public MapItemSpawner(PlayingGrid playingGrid,
            AllItemsConfig itemsConfig,
            ItemsTransforms transforms)
        {
            _playingGrid = playingGrid;
            _itemsConfig = itemsConfig;
            _transforms = transforms;
        }

        public void SpawnAndSet(OneMapItemData data)
        {
            var item = InstantiateItem(data.TypeName, data.Name);

            InitSpawnedItem(item, _transforms.MapItemsParent);
            item.transform.position = new Vector3(
            data.Position.x, item.transform.position.y, data.Position.z);

            item.Position = data.Position;
            item.OriginIndex = data.OriginIndex;
            _playingGrid.SetArea(data.OriginIndex, item.Size, item.ID);
        }

        public MapItem Spawn(int id)
        {
            var item = GetItem(id);

            InitSpawnedItem(item, _transforms.MapItemsParent);

            return item;
        }

        public void Unspawn(MapItem item)
        {
            item.gameObject.SetActive(false);

            item.transform.SetParent(_transforms.PoolParent);

            if (!_pools.TryGetValue(item.ID, out var queue))
            {
                _pools.Add(item.ID, queue);
            }

            queue.Enqueue(item);
        }

        public MapItem InstantiateItem(string typeName, string name)
        {
            var item = GameObject.Instantiate(_itemsConfig.GetPrefab(typeName, name));
            return item;
        }

        private MapItem GetItem(int id)
        {
            if (!TryGetPrefabFromPool(id, out var item))
            {
                item = GameObject.Instantiate(_itemsConfig.GetPrefab(id));
            }

            return item;
        }

        private bool TryGetPrefabFromPool(int id, out MapItem item)
        {
            if (_pools.TryGetValue(id, out var queue))
            {
                return queue.TryDequeue(out item);
            }

            item = null;
            return false;
        }

        private void InitSpawnedItem(MapItem item, Transform parent)
        {
            item.transform.SetParent(parent);

            item.gameObject.SetActive(true);
        }
    }
}
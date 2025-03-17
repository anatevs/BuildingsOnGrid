using SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class MapItemSpawner
    {
        private readonly PlayingGrid _playingGrid;

        private readonly AllItemsConfig _allItemsConfig;

        private readonly ItemsTransforms _transforms;

        private readonly Dictionary<int, Queue<MapItem>> _pools = new();

        public MapItemSpawner(PlayingGrid playingGrid,
            AllItemsConfig itemsConfig,
            ItemsTransforms transforms)
        {
            _playingGrid = playingGrid;
            _allItemsConfig = itemsConfig;
            _transforms = transforms;

            (int start, int end) = _allItemsConfig.IDInterval;

            for (int i = start; i <= end; i++)
            {
                _pools.Add(i, new Queue<MapItem>());
            }
        }

        public void SpawnAndSet(OneMapItemData data)
        {
            var item = InstantiateItem(data.TypeName, data.Name);

            InitSpawnedItem(item, _transforms.MapItemsParent);

            item.SetPosition(data.Position, data.OriginIndex);

            //item.OriginPosition = data.Position;
            //item.OriginIndex = data.OriginIndex;
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

            _pools[item.ID].Enqueue(item);
        }

        public MapItem InstantiateItem(string typeName, string name)
        {
            var prefab = _allItemsConfig.GetPrefab(typeName, name);

            var item = GameObject.Instantiate(prefab);

            item.Init(prefab.ID, typeName);

            return item;
        }

        private MapItem GetItem(int id)
        {
            if (!TryGetPrefabFromPool(id, out var item))
            {
                var prefab = _allItemsConfig.GetPrefab(id);

                item = GameObject.Instantiate(prefab);

                item.Init(id, prefab.TypeName);
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
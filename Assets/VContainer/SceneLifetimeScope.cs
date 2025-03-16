using GameCore;
using SaveLoad;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private int[] _gridSize = new int[2] { 1, 1 };

        [SerializeField]
        private AllItemsConfig _allItemsConfig;

        [SerializeField]
        private ItemsTransforms _itemsTransforms;


        protected override void Configure(IContainerBuilder builder)
        {
            _allItemsConfig.Init();

            RegisterGrid(builder);

            RegisterSpawner(builder);

            RegisterSaveLoad(builder);
        }

        private void RegisterGrid(IContainerBuilder builder)
        {
            builder.Register<PlayingGrid>(Lifetime.Singleton)
                .WithParameter(_gridSize);
        }

        private void RegisterSpawner(IContainerBuilder builder)
        {
            builder.Register<MapItemSpawner>(Lifetime.Singleton)
                .WithParameter(_allItemsConfig)
                .WithParameter(_itemsTransforms);
        }

        private void RegisterSaveLoad(IContainerBuilder builder)
        {
            builder.Register<MapItemsSaveLoad>(Lifetime.Singleton)
                .WithParameter(_allItemsConfig)
                .AsImplementedInterfaces()
                .AsSelf();


            builder.Register<SaveLoadManager>(Lifetime.Singleton);
        }
    }
}
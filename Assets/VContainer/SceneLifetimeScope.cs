using GameCore;
using GameManagement;
using SaveLoad;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private int[] _gridSize = new int[2] { 100, 100 };

        [SerializeField]
        private AllItemsConfig _allItemsConfig;

        [SerializeField]
        private ItemsTransforms _itemsTransforms;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private SetRemoveView _setRemoveView;

        [SerializeField]
        private Button _endButton;


        protected override void Configure(IContainerBuilder builder)
        {
            _allItemsConfig.Init();

            RegisterGridAndPlayer(builder);

            RegisterSpawner(builder);

            RegisterSaveLoad(builder);

            RegisterGameManagement(builder);

            RegisterUI(builder);
        }

        private void RegisterGridAndPlayer(IContainerBuilder builder)
        {
            builder.Register<PlayingGrid>(Lifetime.Singleton)
                .WithParameter(_gridSize);

            builder.RegisterComponent(_player);
        }

        private void RegisterSpawner(IContainerBuilder builder)
        {
            builder.Register<MapItemSpawner>(Lifetime.Singleton)
                .WithParameter(_allItemsConfig)
                .WithParameter(_itemsTransforms);

            builder.RegisterEntryPoint<ItemsManager>()
                .AsSelf();
        }

        private void RegisterSaveLoad(IContainerBuilder builder)
        {
            builder.Register<MapItemsSaveLoad>(Lifetime.Singleton)
                .WithParameter(_allItemsConfig)
                .WithParameter(_itemsTransforms.MapItemsParent)
                .AsImplementedInterfaces()
                .AsSelf();


            builder.Register<SaveLoadManager>(Lifetime.Singleton);
        }

        private void RegisterGameManagement(IContainerBuilder builder)
        {
            builder.Register<EndGameManager>(Lifetime.Singleton);

            builder.Register<StartGameManager>(Lifetime.Singleton);
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            builder.Register<ItemsViewsPresenter>(Lifetime.Singleton)
                .WithParameter(_allItemsConfig)
                .WithParameter(_itemsTransforms.ViewsParent)
                .AsImplementedInterfaces().AsSelf();

            builder.RegisterEntryPoint<SetRemovePresenter>()
                .WithParameter(_setRemoveView)
                .AsSelf();

            builder.RegisterEntryPoint<EndButtonPresenter>()
                .WithParameter(_endButton)
                .AsSelf();
        }
    }
}
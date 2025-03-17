using Assets.Input;
using GameManagement;
using SaveLoad;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public sealed class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInput(builder);

            RegisterSaveLoad(builder);

            RegisterGameManagement(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputController>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        private void RegisterSaveLoad(IContainerBuilder builder)
        {
            builder.Register<SaveLoadStorage>(Lifetime.Singleton);

            builder.RegisterEntryPoint<LoadingGame>()
                .AsSelf();
        }

        private void RegisterGameManagement(IContainerBuilder builder)
        {
            builder.Register<ApplicationShutdown>(Lifetime.Singleton);
        }
    }
}
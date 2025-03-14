using Assets.Input;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public sealed class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInput(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputController>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
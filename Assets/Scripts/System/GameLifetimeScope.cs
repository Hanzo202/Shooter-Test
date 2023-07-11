using VContainer;
using VContainer.Unity;
using UnityEngine;
using Canvas;


namespace GameSystems
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] AndroidButtons androidButtons;
        [SerializeField] CanvasController canvasController;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyBoardInput>(Lifetime.Singleton);
            builder.RegisterComponent(androidButtons);
            builder.RegisterComponent(canvasController);
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
        }
    }
}


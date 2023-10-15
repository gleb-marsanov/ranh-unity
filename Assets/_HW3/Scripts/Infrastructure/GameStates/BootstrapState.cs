using _HW3.Infrastructure.Bootstrap;
using _HW3.Infrastructure.Services;
using _HW3.Infrastructure.Services.Containers;
using _HW3.Infrastructure.Services.GameFactory;
using _HW3.Infrastructure.Services.GameTime;
using _HW3.Infrastructure.Services.Ui;
using _HW3.Infrastructure.Services.Ui.Factories;
using _HW3.Infrastructure.Services.Ui.LoadingCurtain;
using _HW3.Infrastructure.StaticData;

namespace _HW3.Infrastructure.GameStates
{
    public class BootstrapState : IGameState
    {
        private readonly GameConfig _gameConfig;
        private readonly DiContainer _container;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, DiContainer container, SceneLoader sceneLoader, GameConfig gameConfig)
        {
            _container = container;
            _sceneLoader = sceneLoader;
            _gameConfig = gameConfig;
            _stateMachine = stateMachine;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(_gameConfig.InitialScene, EnterLoadLevelState);

        private void RegisterServices()
        {
            _container.Bind<IGameTimeService>(new GameTimeService());
            _container.Bind<IGameFactory>(new GameFactory(_gameConfig));
            _container.Bind<ILevelContainer>(new LevelContainer());
            _container.Bind<IUiFactory>(CreateUiFactory());
            _container.Bind<ILoadingCurtainService>(CreateLoadingCurtainService());
            _container.Bind<IUiService>(CreateUiService());
        }

        private UiFactory CreateUiFactory()
        {
            return new UiFactory(
                gameConfig: _gameConfig,
                gameTimeService: _container.Resolve<IGameTimeService>(),
                gameStateMachine: _stateMachine,
                levelContainer: _container.Resolve<ILevelContainer>()
            );
        }

        private UiService CreateUiService()
        {
            return new UiService(
                gameConfig: _gameConfig,
                uiFactory: _container.Resolve<IUiFactory>(),
                levelContainer: _container.Resolve<ILevelContainer>()
            );
        }

        private LoadingCurtainService CreateLoadingCurtainService() =>
            new LoadingCurtainService(_container.Resolve<IUiFactory>());

        private void EnterLoadLevelState() =>
            _stateMachine.Enter<LoadLevelState>();
    }
}
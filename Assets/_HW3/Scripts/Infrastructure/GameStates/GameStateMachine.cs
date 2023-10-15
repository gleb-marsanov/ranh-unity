using System;
using System.Collections.Generic;
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
    public class GameStateMachine : IService, IUpdatable
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer, GameConfig gameConfig)
        {
            _states = new Dictionary<Type, IGameState>()
            {
                [typeof(BootstrapState)] = new BootstrapState
                (
                    stateMachine: this,
                    container: diContainer,
                    sceneLoader: sceneLoader,
                    gameConfig
                ),

                [typeof(LoadLevelState)] = new LoadLevelState
                (
                    stateMachine: this,
                    loadingCurtain: diContainer.Resolve<ILoadingCurtainService>(),
                    sceneLoader: sceneLoader,
                    gameConfig: gameConfig,
                    uiService: diContainer.Resolve<IUiService>(),
                    uiFactory: diContainer.Resolve<IUiFactory>(),
                    timeService: diContainer.Resolve<IGameTimeService>(),
                    gameFactory: diContainer.Resolve<IGameFactory>(),
                    levelContainer: diContainer.Resolve<ILevelContainer>()
                ),

                [typeof(GameLoopState)] = new GameLoopState
                (
                    timeService: diContainer.Resolve<IGameTimeService>(),
                    uiService: diContainer.Resolve<IUiService>(),
                    levelContainer: diContainer.Resolve<ILevelContainer>()
                )
            };
        }

        public void Update()
        {
            if (_activeState is IUpdatable updatableState)
                updatableState.Update();
        }

        public void Enter<TState>() where TState : class, IGameState
        {
            IGameState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IGameState
        {
            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class =>
            _states[typeof(TState)] as TState;
    }
}
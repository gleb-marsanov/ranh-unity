using _HW3.Infrastructure.GameStates;
using _HW3.Infrastructure.Services.Containers;
using _HW3.Infrastructure.Services.GameTime;
using _HW3.Infrastructure.StaticData;
using _HW3.UI;
using UnityEngine;

namespace _HW3.Infrastructure.Services.Ui.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILevelContainer _levelContainer;
        private readonly IGameTimeService _gameTimeService;
        private Transform _uiRoot;

        public UiFactory(GameConfig gameConfig, IGameTimeService gameTimeService, GameStateMachine gameStateMachine, ILevelContainer levelContainer)
        {
            _gameConfig = gameConfig;
            _gameTimeService = gameTimeService;
            _gameStateMachine = gameStateMachine;
            _levelContainer = levelContainer;
        }

        public UI.LoadingCurtain CreateLoadingCurtain() =>
            Object.Instantiate(_gameConfig.LoadingCurtain);

        public GameHud CreateGameHud()
        {
            GameHud gameHud = Object.Instantiate(_gameConfig.GameHud, _uiRoot);
            gameHud.TimeDisplay.Construct(_gameTimeService);
            return gameHud;
        }

        public GameOverWindow CreateGameOverWindow()
        {
            GameOverWindow gameOverWindow = Object.Instantiate(_gameConfig.GameOverWindow, _uiRoot);
            gameOverWindow.Construct(_gameStateMachine);
            return gameOverWindow;
        }

        public void CreateUiRoot() => 
            _uiRoot = Object.Instantiate(_gameConfig.UiRoot).transform;

        public VictoryWindow CreateVictoryWindow()
        {
            VictoryWindow victoryWindow = Object.Instantiate(_gameConfig.VictoryWindow, _uiRoot);
            victoryWindow.Construct(_gameStateMachine);
            return victoryWindow;
        }
    }
}
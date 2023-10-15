using System;
using System.Threading.Tasks;
using _HW3.Infrastructure.Bootstrap;
using _HW3.Infrastructure.DataModels;
using _HW3.Infrastructure.Services.Containers;
using _HW3.Infrastructure.Services.GameFactory;
using _HW3.Infrastructure.Services.GameTime;
using _HW3.Infrastructure.Services.Ui;
using _HW3.Infrastructure.Services.Ui.Factories;
using _HW3.Infrastructure.Services.Ui.LoadingCurtain;
using _HW3.Infrastructure.StaticData;

namespace _HW3.Infrastructure.GameStates
{
    public class LoadLevelState : IGameState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly GameConfig _gameConfig;
        private readonly IUiService _uiService;
        private readonly IUiFactory _uiFactory;
        private readonly IGameTimeService _timeService;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelContainer _levelContainer;
        private readonly ILoadingCurtainService _loadingCurtain;

        public LoadLevelState
        (
            GameStateMachine stateMachine,
            ILoadingCurtainService loadingCurtain,
            SceneLoader sceneLoader,
            GameConfig gameConfig,
            IUiService uiService,
            IUiFactory uiFactory,
            IGameTimeService timeService,
            IGameFactory gameFactory,
            ILevelContainer levelContainer
        )
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _gameConfig = gameConfig;
            _uiService = uiService;
            _uiFactory = uiFactory;
            _timeService = timeService;
            _gameFactory = gameFactory;
            _levelContainer = levelContainer;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _timeService.SetTime(_gameConfig.GameLoopTime);
            _sceneLoader.Load(_gameConfig.GameplayScene, OnLoaded);
        }

        private async void OnLoaded()
        {
            await LoadGameLevel();
            LoadUi();

            _loadingCurtain.Hide();
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task LoadGameLevel()
        {
            var loadingProgress = 0f;

            Door door = _gameFactory.CreateDoor();
            _levelContainer.Initialize(door);

            loadingProgress += 0.1f;
            await LoadTheDungeon(loadingProgress);
            loadingProgress += 0.1f;
            await SpawnMonsters(loadingProgress);
            loadingProgress += 0.1f;
            await LoadingDebug(loadingProgress);
        }

        private void LoadUi()
        {
            _uiFactory.CreateUiRoot();
            _uiService.ShowGameHud();
        }

        private async Task SpawnMonsters(float loadingProgress)
        {
            _loadingCurtain.SetProgress(loadingProgress, "Spawning monsters");
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }

        private async Task LoadTheDungeon(float loadingProgress)
        {
            _loadingCurtain.SetProgress(loadingProgress, "Entering dungeon");
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }

        private async Task LoadingDebug(float currentProgress)
        {
            const float loadingStep = 0.1f;
            float loadingTime = _gameConfig.LoadingTime * (1 - currentProgress);
            float delay = loadingStep / loadingTime;

            for (float i = 0; i < loadingTime; i += delay)
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));
                float progress = i / loadingTime;
                _loadingCurtain.SetProgress(currentProgress + progress, "Looting chests");
            }
        }
    }

}
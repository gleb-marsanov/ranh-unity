using _HW3.Infrastructure.Services.Containers;
using _HW3.Infrastructure.Services.Ui.Factories;
using _HW3.Infrastructure.StaticData;
using _HW3.UI;

namespace _HW3.Infrastructure.Services.Ui
{
    public class UiService : IUiService
    {
        private readonly IUiFactory _uiFactory;
        private readonly ILevelContainer _levelContainer;
        private readonly GameConfig _gameConfig;

        private GameHud _gameHud;
        private GameOverWindow _gameOverWindow;

        public UiService(GameConfig gameConfig, IUiFactory uiFactory, ILevelContainer levelContainer)
        {
            _uiFactory = uiFactory;
            _gameConfig = gameConfig;
            _levelContainer = levelContainer;
        }

        public void ShowGameHud()
        {
            _gameHud = _uiFactory.CreateGameHud();
            _gameHud.SetDoorPins(_levelContainer.Door.Pin1, _levelContainer.Door.Pin2, _levelContainer.Door.Pin3);
            _gameHud.OnToolUsed += OnToolUsed;
        }

        private void OnToolUsed(int toolIndex)
        {
            _levelContainer.Door.ApplyTool(_gameConfig.Tools[toolIndex]);
            _gameHud.SetDoorPins(_levelContainer.Door.Pin1, _levelContainer.Door.Pin2, _levelContainer.Door.Pin3);
        }

        public void ShowGameOverWindow()
        {
            _gameOverWindow = _uiFactory.CreateGameOverWindow();
            _gameOverWindow.Show();
        }

        public void ShowVictoryWindow()
        {
            VictoryWindow victoryWindow = _uiFactory.CreateVictoryWindow();
            victoryWindow.Show();
        }
    }
}
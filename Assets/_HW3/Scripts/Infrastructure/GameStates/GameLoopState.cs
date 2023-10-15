using _HW3.Infrastructure.Services.Containers;
using _HW3.Infrastructure.Services.GameTime;
using _HW3.Infrastructure.Services.Ui;

namespace _HW3.Infrastructure.GameStates
{
    public class GameLoopState : IGameState, IUpdatable
    {
        private readonly ILevelContainer _levelContainer;
        private readonly IGameTimeService _timeService;
        private readonly IUiService _uiService;

        private bool _gameOver;

        public GameLoopState(IGameTimeService timeService, IUiService uiService, ILevelContainer levelContainer)
        {
            _timeService = timeService;
            _uiService = uiService;
            _levelContainer = levelContainer;
        }

        public void Enter() =>
            _gameOver = false;

        public void Update()
        {
            if (_gameOver)
                return;

            if (_levelContainer.Door.IsOpen)
                ProcessVictory();

            if (_timeService.TimeLeft <= 0)
                ProcessDefeat();

            _timeService.Update();
        }

        private void ProcessDefeat()
        {
            _uiService.ShowGameOverWindow();
            _gameOver = true;
        }

        private void ProcessVictory()
        {
            _uiService.ShowVictoryWindow();
            _gameOver = true;
        }
    }
}
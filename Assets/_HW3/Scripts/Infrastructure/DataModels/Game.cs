using _HW3.Infrastructure.Bootstrap;
using _HW3.Infrastructure.GameStates;
using _HW3.Infrastructure.Services;
using _HW3.Infrastructure.StaticData;

namespace _HW3.Infrastructure.DataModels
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, GameConfig config)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), DiContainer.Container, config);
        }
    }
}
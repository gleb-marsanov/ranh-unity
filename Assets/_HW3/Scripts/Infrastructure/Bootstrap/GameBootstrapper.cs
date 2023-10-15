using _HW3.Infrastructure.DataModels;
using _HW3.Infrastructure.GameStates;
using _HW3.Infrastructure.StaticData;
using UnityEngine;

namespace _HW3.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameConfig _gameConfig;

        private Game _game;

        private void Start()
        {
            _game = new Game(this, _gameConfig);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _game.StateMachine.Update();
        }
    }
}
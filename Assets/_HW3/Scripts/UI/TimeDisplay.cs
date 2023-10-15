using _HW3.Infrastructure.Services.GameTime;
using TMPro;
using UnityEngine;

namespace _HW3.UI
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IGameTimeService _gameTimeService;

        public void Construct(IGameTimeService gameTimeService) => 
            _gameTimeService = gameTimeService;

        private void Update() => 
            _text.text = (_gameTimeService.TimeLeft).ToString("0");
    }

}
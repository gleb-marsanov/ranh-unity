using UnityEngine;

namespace _HW3.Infrastructure.Services.GameTime
{
    public class GameTimeService : IGameTimeService
    {
        public float TimeLeft { get; private set; }

        public void SetTime(float value) => 
            TimeLeft = Mathf.Max(0, value);

        public void Update() => 
            TimeLeft = Mathf.Max(0, TimeLeft - Time.deltaTime);
    }
}
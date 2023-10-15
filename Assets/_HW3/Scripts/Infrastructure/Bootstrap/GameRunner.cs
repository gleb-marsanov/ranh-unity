using UnityEngine;

namespace _HW3.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            Instantiate(BootstrapperPrefab);
        }
    }
}
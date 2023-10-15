using _HW3.Infrastructure.DataModels;

namespace _HW3.Infrastructure.Services.Containers
{
    public class LevelContainer : ILevelContainer
    {
        public Door Door { get; private set; }
        
        public void Initialize(Door door) => 
            Door = door;
    }
}
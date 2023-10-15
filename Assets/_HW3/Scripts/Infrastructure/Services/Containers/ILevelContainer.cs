using _HW3.Infrastructure.DataModels;

namespace _HW3.Infrastructure.Services.Containers
{
    public interface ILevelContainer : IService
    {
        Door Door { get; }
        void Initialize(Door createDoor);
    }
}
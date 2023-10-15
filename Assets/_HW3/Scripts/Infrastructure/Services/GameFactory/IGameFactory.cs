using _HW3.Infrastructure.DataModels;

namespace _HW3.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        Door CreateDoor();
    }

}
namespace _HW3.Infrastructure.Services.GameTime
{
    public interface IGameTimeService : IService, IUpdatable
    {
        float TimeLeft { get; }
        void SetTime(float value);
    }

}
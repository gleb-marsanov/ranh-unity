namespace _HW3.Infrastructure.Services.Ui
{
    public interface IUiService : IService
    {
        void ShowGameHud();
        void ShowGameOverWindow();
        void ShowVictoryWindow();
    }
}
using _HW3.UI;

namespace _HW3.Infrastructure.Services.Ui.Factories
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        GameHud CreateGameHud();
        VictoryWindow CreateVictoryWindow();
        GameOverWindow CreateGameOverWindow();
        UI.LoadingCurtain CreateLoadingCurtain();
    }

}
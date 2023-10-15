namespace _HW3.Infrastructure.Services.Ui.LoadingCurtain
{
    public interface ILoadingCurtainService : IService
    {

        void Show();
        void Hide();
        void SetProgress(float value, string description = "");
    }

}
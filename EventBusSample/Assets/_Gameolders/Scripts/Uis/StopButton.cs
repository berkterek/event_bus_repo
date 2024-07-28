using EventBusSample.Managers;

namespace EventBusSample
{
    public class StopButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            GameManager.Stop();
        }
    }
}
namespace UnityPureMVC.Core.View.Components
{
    interface IDialog
    {
        void SetTitleText(string title);
        void SetMessageText(string message);
        void SetButtonText(string buttonText);
        void SetButtonActive(bool active);
    }
}

using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPureMVC.Core.View.Components
{
    internal class CoreDialogComponent : MonoBehaviour, IDialog
    {
        internal delegate void ButtonClickEvent();
        internal ButtonClickEvent OnDialogButtonClick;

        // Inspector parameters
        public Text titleText;
        public Text messageText;
        public Button button;

        private void Start()
        {
            if (button == null)
            {
                DebugLogger.LogWarning("Title text not found on CoreDialogComponent");
                return;
            }

            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnDialogButtonClick?.Invoke();
        }

        public void SetTitleText(string value)
        {
            if (titleText == null)
            {
                DebugLogger.LogWarning("Title text not found on CoreDialogComponent");
                return;
            }
            titleText.text = value ?? "Error";
        }

        public void SetMessageText(string value)
        {
            if (messageText == null)
            {
                DebugLogger.LogWarning("Message text not found on CoreDialogComponent");
                return;
            }
            messageText.text = value ?? "There was an error with your request. Please try again.";
        }

        public void SetButtonText(string value)
        {
            if (button == null)
            {
                DebugLogger.LogWarning("Button not found on CoreDialogComponent");
                return;
            }
            button.GetComponentInChildren<Text>().text = value ?? "OK";
        }

        public void SetButtonActive(bool value)
        {
            if (button == null)
            {
                DebugLogger.LogWarning("Button not found on CoreDialogComponent");
                return;
            }
            button.gameObject.SetActive(value);
        }
    }
}

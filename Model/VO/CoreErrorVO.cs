using UnityPureMVC.Core.View.Components;
using UnityEngine.Events;

namespace UnityPureMVC.Core.Model.VO
{
    internal sealed class CoreErrorVO
    {
        internal string template = "Core/CoreErrorDialog";

        internal string errorType = "DefaultError";
        internal string title = "Error";
        internal string message = "There was an error!";
        internal string buttonText = "OK";
        internal UnityAction callback = null;
        internal bool buttonActive = true;
        internal CoreDialogComponent coreDialogComponent = null;
    }
}

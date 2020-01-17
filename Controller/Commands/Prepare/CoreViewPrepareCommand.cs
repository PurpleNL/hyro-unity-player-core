namespace UnityPureMVC.Core.Controller.Commands.Prepare
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.View.Mediators;

    internal sealed class CoreViewPrepareCommand : SimpleCommand
    {
        /// <summary>
        /// Prepare view command.
        /// 
        /// The body of this notification is the ApplicationBehaviour
        /// It makes sense to assign the CoreBehaviour as the view component of our ApplicationMediator
        /// </summary>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("CoreViewPrepareCommand::Execute");

            Facade.RegisterMediator(new CoreMediator(notification.Body as CoreBehaviour));
        }
    }
}
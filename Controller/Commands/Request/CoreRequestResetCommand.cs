namespace UnityPureMVC.Core.Controller.Commands.Request
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Model.Proxies;

    internal sealed class CoreRequestResetCommand : SimpleCommand
    {
        /// <summary>
        /// Reset the the application.
        /// 
        /// Currently we just load the default environment
        /// This command will likely grow as the application does
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void Execute(INotification notification)
        {
            CoreDataProxy data = (CoreDataProxy)Facade.RetrieveProxy(CoreDataProxy.NAME);

            SendNotification(CoreNote.REQUEST_LOAD_ENVIRONMENT, data.DefaultEnvironment);
        }
    }
}
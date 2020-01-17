namespace UnityPureMVC.Core.Controller.Commands
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.Proxies;

    internal sealed class CoreReadyCommand : SimpleCommand
    {
        /// <summary>
        /// Prepare view command.
        /// </summary>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("CoreReadyCommand::Execute");

            // Retrieve data proxy
            CoreDataProxy coreDataProxy = (Facade.RetrieveProxy(CoreDataProxy.NAME) as CoreDataProxy);

            // Load Default environment
            SendNotification(CoreNote.REQUEST_LOAD_ENVIRONMENT, coreDataProxy.DefaultEnvironment);

            // Notify any subscribers that the application is ready
            SendNotification(CoreNote.READY);
        }
    }
}
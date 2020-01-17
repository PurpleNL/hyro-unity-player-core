namespace UnityPureMVC.Core.Controller.Commands.Prepare
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.Proxies;

    internal sealed class CoreModelPrepareCommand : SimpleCommand
    {
        /// <summary>
        /// Execute the specified aNote.
        /// </summary>
        /// <param name="aNote">A note.</param>
        public override void Execute(INotification aNote)
        {
            DebugLogger.Log("CoreModelPrepareCommand::Execute");

            Facade.RegisterProxy(new CoreDataProxy());
        }
    }
}
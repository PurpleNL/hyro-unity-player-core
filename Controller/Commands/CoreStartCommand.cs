namespace UnityPureMVC.Core.Controller.Commands
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Controller.Commands.Prepare;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

    internal sealed class CoreStartCommand : MacroCommand
    {
        /// <summary>
        /// Initializes the macro command.
        /// </summary>
        protected override void InitializeMacroCommand()
        {
            DebugLogger.Log("CoreStartCommand::InitializeMacroCommand");

            AddSubCommand(typeof(CoreModelPrepareCommand));
            AddSubCommand(typeof(CoreViewPrepareCommand));
            AddSubCommand(typeof(CoreControllerPrepareCommand));
            AddSubCommand(typeof(CoreReadyCommand));
        }

        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            Facade.RemoveCommand(CoreNote.START);
        }
    }
}
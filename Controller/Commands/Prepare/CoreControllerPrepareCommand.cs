/***********************************************************************************
 * 
 * PrepareControllerCommand.cs 
 * 
 * Register all commands required on start
 * 
 * Facade.GetInstance().RegisterCommand(CoreNote.NOTE_TO_REGISTER, typeof(NameOfCommand));
 * 
 ***********************************************************************************/

namespace UnityPureMVC.Core.Controller.Commands.Prepare
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Controller.Commands.Request;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

    internal sealed class CoreControllerPrepareCommand : SimpleCommand
    {
        /// <summary>
        /// Execute the specified aNote.
        /// </summary>
        /// <param name="notification">A note.</param>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("CoreControllerPrepareCommand::Execute");

            Facade.RegisterCommand(CoreNote.REQUEST_LOAD_ENVIRONMENT, typeof(CoreRequestLoadEnvironmentCommand));
            Facade.RegisterCommand(CoreNote.REQUEST_RESET, typeof(CoreRequestResetCommand));
            Facade.RegisterCommand(CoreNote.REQUEST_BLACKOUT, typeof(CoreRequestBlackoutCommand));
        }
    }
}
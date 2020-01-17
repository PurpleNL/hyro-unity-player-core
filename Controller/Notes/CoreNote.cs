namespace UnityPureMVC.Core.Controller.Notes
{
    internal static class CoreNote
    {
        // Initial Command called from Core Facade to kick start the application
        // This is called internally.
        internal const string START = "Core/start";

        // Called after core start, once core mediators have been registered
        // This is called internally. 
        internal const string READY = "Core/ready";

        // Make a request for the application to reset
        internal const string REQUEST_RESET = "Core/requestReset";

        /// <summary>
        /// Back button 'ESC' pressed
        /// </summary>
        internal const string BACK_BUTTON_PRESSED = "Core/BackButtonPressed";

        // Make a request to load a new environment scene
        internal const string REQUEST_LOAD_ENVIRONMENT = "Core/requestLoadEnvironment";

        // Make a request to show / hide blackout canvas
        internal const string REQUEST_BLACKOUT = "Core/requestBlackout";

        // New environment scene has been successfully loaded
        internal const string ENVIRONMENT_LOADED = "Core/environmentLoaded";

        // Start a coroutine from outside a MonoBehvaiour - e.q from an PureMVC Command
        internal const string REQUEST_START_COROUTINE = "Core/requestStartCoroutine";

        internal const string REQUEST_STOP_COROUTINE = "Core/requestStopCoroutine";

        internal const string REQUEST_STOP_ALL_COROUTINES = "Core/requestStopAllCoroutines";

        internal const string REQUEST_UPDATE_APPLICAITON_TIMESCALE = "Core/requestUpdateApplicationTimeScale";

        /// <summary>
        /// Notify system of an error. Pass a CoreErrorVO object as parameter
        /// </summary>
        internal const string ERROR = "Core/ERROR";

        /// <summary>
        /// Notify the system that an error dialog has been created. Passes a CoreErrorVO as parameter.
        /// </summary>
        internal const string ERROR_DIALOG_CREATED = "Core/errorDialogCreated";
    }
}
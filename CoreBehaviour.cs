/*
 * 																					
 * CoreBehaviour.cs															
 * 																					
 * This behaviour should be attached directly to the main Application GameObject.
 * This is used as an entry point for establishing a concrete application facade.	
 * 																					
 * Boots up the Core system, which then loads up the Application Prepare commands in Application namespace
 * 
 */
namespace UnityPureMVC.Core
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Facade;
    using System;
    using UnityEngine;
    using UnityPureMVC.Core.Controller.Commands;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

    internal class CoreBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Application reset event.
        /// </summary>
        internal delegate void CoreResetEvent();
        internal event CoreResetEvent OnCoreReset;

        internal delegate void CoreButtonEvent();
        internal event CoreButtonEvent OnBackButtonPressed;

        // Updae app version from Main scene in editor
        public string appVersion = "1.0.0";

        // Set internal so it can be set in Editor
        public string DefaultEnvironmentScene = "Environment";

        /// <summary>
        /// Log to console.
        /// </summary>
        public bool debug = true;

        /// <summary>
        /// The core facade.
        /// </summary>
        private IFacade coreFacade;

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {
            DebugLogger.LogTexts = debug;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        protected virtual void Start()
        {
            try
            {
                coreFacade = Facade.GetInstance("Core");
                coreFacade.RegisterCommand(CoreNote.START, typeof(CoreStartCommand));
                coreFacade.SendNotification(CoreNote.START, this);
            }
            catch (Exception exception)
            {
                throw new UnityException("Unable to initiate Facade", exception);
            }
        }

        /// <summary>
        /// Check for key press and fire an Application reset event
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnCoreReset?.Invoke();
            }

            // Exit application on Escape key press.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DebugLogger.Log("BACK");
                OnBackButtonPressed?.Invoke();
            }
        }

        /// <summary>
        /// On destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (coreFacade != null)
            {
                coreFacade.Dispose();
                coreFacade = null;
            }
        }
    }
}
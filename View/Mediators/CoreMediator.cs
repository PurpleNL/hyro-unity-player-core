namespace UnityPureMVC.Core.View.Mediators
{
    using PureMVC.Interfaces;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.VO;
    using UnityPureMVC.Core.View.Components;

    internal sealed class CoreMediator : CoreBaseMediator
    {
        /// <summary>
        /// The name of this mediator - for use internally in PureMVC
        /// </summary>
        new internal const string NAME = "CoreMediator";

        /************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Core.View.Mediators.CoreMediator"/> class.
        /// </summary>
        internal CoreMediator(CoreBehaviour coreBehaviour) : base(NAME)
        {
            m_viewComponent = coreBehaviour;
        }

        /// <summary>
        /// On register.
        /// </summary>
        public override void OnRegister()
        {
            DebugLogger.Log(NAME + "::OnRegister");

            // Check if the application contains an EventSystem
            CheckEventSystemExists();

            // Listen for core reset event
            (m_viewComponent as CoreBehaviour).OnCoreReset += OnRequestCoreReset;
            (m_viewComponent as CoreBehaviour).OnBackButtonPressed += OnCoreBackButtonPressed;
        }

        /// <summary>
        /// Gets the list notification interests.
        /// </summary>
        /// <value>The list notification interests.</value>
        public override IEnumerable<string> ListNotificationInterests
        {
            get
            {
                return new List<string>(base.ListNotificationInterests)
                {
                    CoreNote.REQUEST_START_COROUTINE,
                    CoreNote.REQUEST_STOP_COROUTINE,
                    CoreNote.REQUEST_STOP_ALL_COROUTINES,
                };
            }
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case CoreNote.REQUEST_START_COROUTINE:
                    OnRequestStartCoroutine(notification.Body);

                    break;
                case CoreNote.REQUEST_STOP_COROUTINE:
                    OnRequestStopCoroutine(notification.Body as IEnumerator);

                    break;

                case CoreNote.REQUEST_STOP_ALL_COROUTINES:
                    OnRequestStopAllCoroutines();

                    break;

                default: break;
            }
        }

        /// <summary>
        /// Handle receiveing / displaying a system error
        /// </summary>
        /// <param name="coreErrorVO"></param>
        protected override void OnCoreError(CoreErrorVO coreErrorVO)
        {
            base.OnCoreError(coreErrorVO);

            // Create an error dialog
            GameObject errorDialogGameObject = GameObject.Instantiate(Resources.Load(coreErrorVO.template) as GameObject);

            // Get the error dialog component
            CoreDialogComponent coreDialogComponent = errorDialogGameObject.GetComponent<CoreDialogComponent>();

            if (coreDialogComponent == null)
            {
                DebugLogger.LogError("Error dialog component does not contain CoreErrorDialogComponent");
                return;
            }

            coreDialogComponent.SetTitleText(coreErrorVO.title);
            coreDialogComponent.SetMessageText(coreErrorVO.message);
            coreDialogComponent.SetButtonText(coreErrorVO.buttonText);
            coreDialogComponent.SetButtonActive(coreErrorVO.buttonActive);

            // Destroy dialog on button click
            coreDialogComponent.OnDialogButtonClick += () =>
            {
                GameObject.Destroy(errorDialogGameObject);

                if (coreErrorVO.callback != null)
                {
                    coreErrorVO.callback.Invoke();
                };
            };

            // Add the dialog component to the VO
            coreErrorVO.coreDialogComponent = coreDialogComponent;

            // Send it back to the system
            SendNotification(CoreNote.ERROR_DIALOG_CREATED, coreErrorVO);
        }

        /// <summary>
        /// Attempts to start a coroutine
        /// </summary>
        /// <param name="requestStartCoroutineVO"></param>
        private void OnRequestStartCoroutine(object notificationBody)
        {
            IEnumerator coroutine = null;
            if (notificationBody is RequestStartCoroutineVO)
            {
                coroutine = (notificationBody as RequestStartCoroutineVO).coroutine;
            }
            if (notificationBody is IEnumerator)
            {
                coroutine = notificationBody as IEnumerator;
            }

            (m_viewComponent as CoreBehaviour).StartCoroutine(coroutine);
        }

        /// <summary>
        /// Attempts to stop a coroutine
        /// </summary>
        /// <param name="requestStartCoroutineVO"></param>
        private void OnRequestStopCoroutine(IEnumerator coroutine)
        {
            (m_viewComponent as CoreBehaviour).StopCoroutine(coroutine);
        }

        /// <summary>
        /// Attempts to stop all coroutines
        /// </summary>
        private void OnRequestStopAllCoroutines()
        {
            (m_viewComponent as CoreBehaviour).StopAllCoroutines();
        }

        /// <summary>
        /// Checks if an EventSystem exists in heirarchy, if none found, we add one
        /// </summary>
        private void CheckEventSystemExists()
        {
            // Check if we have an EventSystem already
            if (GameObject.FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnRequestCoreReset()
        {
            base.OnRequestCoreReset();
            SendNotification(CoreNote.REQUEST_RESET);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnCoreBackButtonPressed()
        {
            base.OnCoreBackButtonPressed();
            SendNotification(CoreNote.BACK_BUTTON_PRESSED);
        }
    }
}
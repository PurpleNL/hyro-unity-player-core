namespace UnityPureMVC.Core.View.Mediators
{
    using Model.Proxies;
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Mediator;
    using System.Collections.Generic;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Model.VO;

    internal abstract class CoreBaseMediator : Mediator
    {
        /// <summary>
        /// The name of this mediator - for use internally in PureMVC
        /// </summary>
        new internal string NAME = "CoreBaseMediator";

        protected CoreDataProxy coreDataProxy;

        /************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Core.View.Mediators.CoreBaseMediator"/> class.
        /// </summary>
        internal CoreBaseMediator(string _NAME) : base(_NAME) { NAME = _NAME; }

        /// <summary>
        /// On register.
        /// </summary>
        public override void OnRegister()
        {
            coreDataProxy = Facade.RetrieveProxy(CoreDataProxy.NAME) as CoreDataProxy;
        }

        /// <summary>
        /// Gets the list notification interests.
        /// </summary>
        /// <value>The list notification interests.</value>
        public override IEnumerable<string> ListNotificationInterests
        {
            get
            {
                return new List<string>
                {
                    CoreNote.ERROR,
                    CoreNote.READY,
                    CoreNote.ENVIRONMENT_LOADED,
                    CoreNote.REQUEST_UPDATE_APPLICAITON_TIMESCALE
                };
            }
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case CoreNote.ERROR:
                    OnCoreError(notification.Body as CoreErrorVO);
                    break;

                case CoreNote.READY:
                    OnCoreReady();
                    break;

                case CoreNote.ENVIRONMENT_LOADED:
                    OnEnvironmentLoaded();
                    break;

                case CoreNote.REQUEST_UPDATE_APPLICAITON_TIMESCALE:
                    OnRequestUpdateApplicationTimeScale(notification.Body as RequestUpdateApplicationTimeScaleVO);
                    break;
            }
        }

        /// <summary>
        /// Handle receieving errors
        /// </summary>
        /// <param name="coreErrorVO"></param>
        protected virtual void OnCoreError(CoreErrorVO coreErrorVO)
        {

        }

        /// <summary>
        /// Called on application init, after PureMVC has been initiated
        /// </summary>
        protected virtual void OnCoreReady()
        {

        }

        /// <summary>
        /// Setup this instance.
        /// </summary>
        protected virtual void OnEnvironmentLoaded()
        {

        }

        /// <summary>
        /// Ons the application reset.
        /// </summary>
        protected virtual void OnRequestCoreReset()
        {

        }

        /// <summary>
        /// Back Button pressed (ESC)
        /// </summary>
        protected virtual void OnCoreBackButtonPressed()
        {
        }

        /// <summary>
        /// Pause the application by adjusting the application timescale.
        /// </summary>
        /// <param name="requestUpdateApplicationTimeScaleVO"></param>
        protected virtual void OnRequestUpdateApplicationTimeScale(RequestUpdateApplicationTimeScaleVO requestUpdateApplicationTimeScaleVO)
        {

        }
    }
}
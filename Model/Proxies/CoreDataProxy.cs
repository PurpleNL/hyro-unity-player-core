namespace UnityPureMVC.Core.Model.Proxies
{
    using PureMVC.Patterns.Proxy;
    using UnityEngine;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.VO;

    internal sealed class CoreDataProxy : Proxy
    {
        new internal const string NAME = "CoreDataProxy";

        protected CoreDataVO dataVO { get { return Data as CoreDataVO; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Core.Model.Proxies.DataProxy"/> class.
        /// Create a new DataVO object
        /// Call method to set ay DataVO default values
        /// </summary>
        internal CoreDataProxy() : base(NAME)
        {
            DebugLogger.Log(NAME + "::__Contstruct");

            Data = new CoreDataVO();

            SetDefaultValues();
        }

        /**************/

        /// <summary>
        /// Sets the default values of DataVO
        /// </summary>
        private void SetDefaultValues()
        {
            dataVO.version = "1.0.0";
            dataVO.currentEnvironment = "";
            dataVO.defaultEnvironment = "Environment";

            // Search for a GameObject with CoreBehaviour componenent attached
            CoreBehaviour coreBehaviour = GameObject.FindObjectOfType<CoreBehaviour>();
            if (coreBehaviour != null)
            {
                dataVO.version = coreBehaviour.appVersion;
                dataVO.defaultEnvironment = coreBehaviour.DefaultEnvironmentScene;
                dataVO.isDebug = coreBehaviour.debug;
            }
        }

        /**************/

        internal string AppVersion
        {
            get
            {
                return dataVO.version;
            }
            set
            {
                dataVO.version = value;
            }
        }

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        /// <returns>The current environment.</returns>
        internal string CurrentEnvironment
        {
            get
            {
                return dataVO.currentEnvironment;
            }
            set
            {
                dataVO.currentEnvironment = value;
            }
        }

        internal string DefaultEnvironment
        {
            get
            {
                return dataVO.defaultEnvironment;
            }
        }

        internal bool IsDebug
        {
            get
            {
                return dataVO.isDebug;
            }
        }

        internal BlackoutStatus BlackoutStatus
        {
            get
            {
                return dataVO.blackoutStatus;
            }
            set
            {
                dataVO.blackoutStatus = value;
            }
        }
    }
}
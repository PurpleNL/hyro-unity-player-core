namespace UnityPureMVC.Core.Controller.Commands.Request
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.Proxies;
    using UnityEngine.SceneManagement;

    internal sealed class CoreRequestLoadEnvironmentCommand : SimpleCommand
    {
        /// <summary>
        /// Name of scene to load
        /// </summary>
        private string sceneName = "Environment";

        /// <summary>
        /// The current scene.
        /// </summary>
        private Scene currentScene;

        /// <summary>
        /// Store a reference to the core data proxy
        /// For access to currently loaded scene
        /// </summary>
        private CoreDataProxy coreDataProxy;

        /// <summary>
        /// Execute the specified notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("LoadEnvironmentCommand::Execute -> " + notification.Body.ToString());

            coreDataProxy = (Facade.RetrieveProxy(CoreDataProxy.NAME) as CoreDataProxy);

            currentScene = SceneManager.GetSceneByName(coreDataProxy.CurrentEnvironment);

            DebugLogger.Log("Current loaded Scene : " + currentScene.name);

            sceneName = notification.Body.ToString();

            if (currentScene.name != sceneName && SceneUtility.GetBuildIndexByScenePath(sceneName) >= 0)
            {
                LoadEnvironment();
            }
            else if (currentScene.name == sceneName && SceneUtility.GetBuildIndexByScenePath(sceneName) >= 0)
            {
                SendNotification(CoreNote.ENVIRONMENT_LOADED, sceneName);
            }

        }

        /// <summary>
        /// Attempts to unload any existing environment then additivily loads the passed scene name.
        /// </summary>
        /// <param name="environment">Environment (Scene name) to load.</param>
        private void LoadEnvironment()
        {
            // Check if environment exists in build settings
            if (SceneUtility.GetBuildIndexByScenePath(sceneName) >= 0)
            {
                UnloadEnvironment();
                SceneManager.sceneLoaded += SceneLoaded;
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                DebugLogger.LogWarning("Scene {0} does not exist. Check build settings", sceneName);
            }
        }

        /// <summary>
        /// Scene loaded event.
        /// </summary>
        /// <param name="scene">Scene.</param>
        /// <param name="loadMode">Load mode.</param>
        private void SceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            if (scene.name == sceneName)
            {
                DebugLogger.Log("Scene " + scene.name + " Loaded");

                coreDataProxy.CurrentEnvironment = scene.name;
                SceneManager.sceneLoaded -= SceneLoaded;
                SceneManager.SetActiveScene(scene);
                SendNotification(CoreNote.ENVIRONMENT_LOADED, scene.name);
            }
        }

        /// <summary>
        /// Unloads any existing environment.
        /// </summary>
        private void UnloadEnvironment()
        {
            if (currentScene.isLoaded)
            {
                DebugLogger.Log("Unloading scene " + currentScene.name);
                SceneManager.sceneUnloaded += SceneUnloaded;
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        /// <summary>
        /// Scene has successfully unloaded.
        /// </summary>
        /// <param name="scene">Scene.</param>
        private void SceneUnloaded(Scene scene)
        {
            DebugLogger.Log("Scene " + scene.name + " Unloaded");
            SceneManager.sceneUnloaded -= SceneUnloaded;
        }
    }
}
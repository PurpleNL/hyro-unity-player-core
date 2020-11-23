namespace UnityPureMVC.Core.Controller.Commands.Request
{
    using Model.VO;
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using UnityPureMVC.Core.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
    using UnityPureMVC.Core.Model.Proxies;

    internal sealed class CoreRequestBlackoutCommand : SimpleCommand
    {
        /// <summary>
        /// Store a reference to canvasgroup for fading to black
        /// </summary>
        private CanvasGroup canvasGroup;

        private CoreDataProxy coreDataProxy;

        /// <summary>
        /// Execute the specified notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("CoreRequestBlackoutCommand::Execute");

            RequestBlackoutVO requestBlackoutVO = notification.Body as RequestBlackoutVO;

            // Get the data proxy to check blackoutstatus
            coreDataProxy = Facade.RetrieveProxy(CoreDataProxy.NAME) as CoreDataProxy;

            if (coreDataProxy.BlackoutStatus != BlackoutStatus.NONE)
            {
                return;
            }

            // First see if blackout canvas already exists in scene
            if (!GameObject.Find("BlackoutCanvas"))
            {
                // Create a canvas to fade to black
                GameObject fadeCanvas = new GameObject("BlackoutCanvas");
                GameObject.DontDestroyOnLoad(fadeCanvas);
                fadeCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGroup = fadeCanvas.AddComponent<CanvasGroup>();

                // Create an image component as child of fadeCanvas
                GameObject image = new GameObject("Image");
                image.transform.SetParent(fadeCanvas.transform);
                Image imageComponent = image.AddComponent<Image>();
                imageComponent.color = Color.black;
                RectTransform rectTransform = image.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
            }
            else
            {
                canvasGroup = GameObject.Find("BlackoutCanvas").GetComponent<CanvasGroup>();
            }

            // Request a coroutine to fade in blackout canvas
            RequestStartCoroutineVO requestStartCoroutineVO = new RequestStartCoroutineVO();

            // Check fade direction
            if (requestBlackoutVO.fadeDirection == FadeDirection.IN)
            {
                coreDataProxy.BlackoutStatus = BlackoutStatus.IN;

                requestStartCoroutineVO.coroutine = DoFadeIn(requestBlackoutVO.delay, requestBlackoutVO.callback, requestBlackoutVO.auto);
            }
            else
            {
                coreDataProxy.BlackoutStatus = BlackoutStatus.OUT;

                SendNotification(CoreNote.REQUEST_STOP_COROUTINE, DoFadeIn());

                requestStartCoroutineVO.coroutine = DoFadeOut(requestBlackoutVO.delay, requestBlackoutVO.callback);
            }

            SendNotification(CoreNote.REQUEST_START_COROUTINE, requestStartCoroutineVO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected IEnumerator DoFadeIn(float delay = 0, UnityAction callback = null, bool auto = false)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            float time = .3f;
            float elapsed = 0;
            while (elapsed < time)
            {
                if (canvasGroup == null)
                {
                    break;
                }

                canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / time);

                yield return new WaitForEndOfFrame();

                elapsed += Time.deltaTime;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
            }

            coreDataProxy.BlackoutStatus = BlackoutStatus.NONE;

            callback?.Invoke();

            if (auto)
            {
                // Request a coroutine to fade out blackout canvas
                coreDataProxy.BlackoutStatus = BlackoutStatus.OUT;
                SendNotification(CoreNote.REQUEST_STOP_COROUTINE, DoFadeIn());
                SendNotification(CoreNote.REQUEST_START_COROUTINE, new RequestStartCoroutineVO
                {
                    coroutine = DoFadeOut(delay)
                });

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected IEnumerator DoFadeOut(float delay = 0, UnityAction callback = null)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            float time = .6f;
            float elapsed = 0;
            while (elapsed < time)
            {
                if (canvasGroup == null)
                {
                    break;
                }

                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / time);

                yield return new WaitForEndOfFrame();

                elapsed += Time.deltaTime;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                GameObject.Destroy(canvasGroup.gameObject);
            }

            coreDataProxy.BlackoutStatus = BlackoutStatus.NONE;

            callback?.Invoke();
        }
    }
}
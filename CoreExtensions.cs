using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPureMVC.Core
{
    internal static class CoreExtensions
    {
        /// <summary>
        /// Extension method for GameObject to recursively change the active state of object and children
        /// </summary>
        /// <param name="go"></param>
        /// <param name="active"></param>
        internal static void SetActiveRecursive(this GameObject go, bool active)
        {
            go.SetActive(active);

            foreach (Transform child in go.transform)
            {
                SetActiveRecursive(child.gameObject, active);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="go"></param>
        /// <param name="layer"></param>
        internal static void SetLayerRecursively(this GameObject go, int layer)
        {
            go.layer = layer;

            foreach (Transform child in go.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }

        /// <summary>
        /// AddListener to pointer events
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        internal static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> listener)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(data => listener.Invoke((BaseEventData)data));
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        internal static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
            trigger.triggers.Add(entry);
        }

        /// <summary>
        /// Map a float from one range to another
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        internal static float Map(this float x, float x1, float x2, float y1, float y2)
        {
            var m = (y2 - y1) / (x2 - x1);
            var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

            return m * x + c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cg"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        internal static IEnumerator AlphaTween(this CanvasGroup cg, float from, float to, float time, float delay)
        {
            cg.alpha = 0;
            yield return new WaitForSeconds(delay);

            float elapsed = 0.0f;
            cg.gameObject.SetActive(true);

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Lerp(from, to, elapsed / time);
                yield return new WaitForEndOfFrame();
            }
            cg.alpha = to;
        }
    }
}
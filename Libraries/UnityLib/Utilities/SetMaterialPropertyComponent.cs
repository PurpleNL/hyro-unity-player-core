using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities
{
    public class SetMaterialPropertyComponent : MonoBehaviour
    {
        // Store all materials in a list
        private List<Renderer> renderers;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            renderers = GetComponentsInChildren<Renderer>().ToList();
        }

        /// <summary>
        /// Sets float on all materials on all renderers
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetAllFloat(string property, float value)
        {
            renderers.ForEach((r) =>
                {
                    r.materials.ToList().ForEach(m => m.SetFloat(property, value));
                });
        }

        /// <summary>
        /// Sets vector on all materials on all renderers
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetAllVector(string property, Vector4 value)
        {
            renderers.ForEach((r) =>
            {
                r.materials.ToList().ForEach(m => m.SetVector(property, value));
            });
        }

        /// <summary>
        /// Sets float on all materials on the renderer of name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetSingleFloat(string name, string property, float value)
        {
            renderers.FirstOrDefault(r => r.name == name)
                .materials.ToList().ForEach(m => m.SetFloat(property, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public void LerpSingleFloat(string name, string property, float value, float time = 1.0f)
        {
            StartCoroutine(DoLerpSingleFloat(name, property, value, time));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator DoLerpSingleFloat(string name, string property, float value, float time)
        {
            List<Material> mats = renderers.FirstOrDefault(r => r.name.Contains(name)).materials.ToList();

            float from = mats[0].GetFloat(property);
            float elapsed = 0.0f;

            while (elapsed < time)
            {
                float newValue = Mathf.Lerp(from, value, elapsed / time);
                mats.ForEach(m => m.SetFloat(property, newValue));
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            mats.ForEach(m => m.SetFloat(property, value));
        }

        /// <summary>
        /// Sets vector on all materials on the renderer of name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetSingleVector(string name, string property, Vector4 value)
        {
            renderers.FirstOrDefault(r => r.name.Contains(name))
                .materials.ToList().ForEach(m => m.SetVector(property, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public void LerpSingleVector(string name, string property, Vector4 value, float time = 1.0f)
        {
            StartCoroutine(DoLerpSingleVector(name, property, value, time));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator DoLerpSingleVector(string name, string property, Vector4 value, float time)
        {
            List<Material> mats = renderers.FirstOrDefault(r => r.name.Contains(name)).materials.ToList();

            Vector4 from = mats[0].GetVector(property);
            float elapsed = 0.0f;

            while (elapsed < time)
            {
                Vector4 newValue = Vector4.Lerp(from, value, elapsed / time);
                mats.ForEach(m => m.SetVector(property, newValue));
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            mats.ForEach(m => m.SetVector(property, value));
        }
    }
}
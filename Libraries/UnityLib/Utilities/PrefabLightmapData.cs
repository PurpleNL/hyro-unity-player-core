using System.Collections.Generic;
using UnityEngine;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities
{
    public class PrefabLightmapData : MonoBehaviour
    {
        [System.Serializable]
        public struct RendererInfo
        {
            public Renderer renderer;
            public int lightmapIndex;
            public Vector4 lightmapOffsetScale;
        }

        [SerializeField]
        RendererInfo[] m_RendererInfo;

        [SerializeField]
        public Transform[] lodRoots;

        [SerializeField]
        public List<List<RendererInfo>> lodRendererInfo;

        [SerializeField]
        public Texture2D[] m_Lightmaps;

        [SerializeField]
        public Texture2D[] m_Dirmaps;

        void Start()
        {
            lodRendererInfo = new List<List<RendererInfo>>();
            foreach (RendererInfo info in m_RendererInfo)
            {
                foreach (Transform lod in lodRoots)
                {
                    List<RendererInfo> rendererInfos = new List<RendererInfo>();

                    foreach (Transform child in lod)
                    {
                        if (child.name.Contains(info.renderer.name))
                        {
                            rendererInfos.Add(new RendererInfo
                            {
                                lightmapIndex = info.lightmapIndex,
                                lightmapOffsetScale = info.lightmapOffsetScale,
                                renderer = child.GetComponent<Renderer>()
                            });
                        }
                    }
                    lodRendererInfo.Add(rendererInfos);
                }
            }

            SetLightmaps();
        }

        public void SetLightmaps()
        {

            if (m_RendererInfo == null || m_RendererInfo.Length == 0)
                return;

            var lightmaps = LightmapSettings.lightmaps;
            var combinedLightmaps = new LightmapData[lightmaps.Length + m_Lightmaps.Length];

            lightmaps.CopyTo(combinedLightmaps, 0);
            for (int i = 0; i < m_Lightmaps.Length; i++)
            {
                combinedLightmaps[i + lightmaps.Length] = new LightmapData
                {
                    lightmapColor = m_Lightmaps[i],
                    lightmapDir = m_Dirmaps[i]
                };
            }

            ApplyRendererInfo(m_RendererInfo, lightmaps.Length);
            DebugLogger.Log("Applying Renderer Info for {0}", name);
            DebugLogger.Log("lm : {0} - dm : {1}", m_Lightmaps[0], m_Dirmaps[0]);
            DebugLogger.Log("{0}x{1}", m_Lightmaps[0].width, m_Lightmaps[0].height);

            foreach (List<RendererInfo> rendererInfos in lodRendererInfo)
            {
                ApplyRendererInfo(rendererInfos.ToArray(), lightmaps.Length);
            }
            LightmapSettings.lightmaps = combinedLightmaps;

            LightmapSettings.lightmapsMode = LightmapsMode.CombinedDirectional;

            Debug.Log(LightmapSettings.lightmaps);
            Debug.Log(LightmapSettings.lightmaps.Length);
        }


        private void ApplyRendererInfo(RendererInfo[] infos, int lightmapOffsetIndex)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                var info = infos[i];
                info.renderer.lightmapIndex = info.lightmapIndex + lightmapOffsetIndex;
                info.renderer.lightmapScaleOffset = info.lightmapOffsetScale;

            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Bake Prefab Lightmaps")]
        static void GenerateLightmapInfo()
        {
            if (UnityEditor.Lightmapping.giWorkflowMode != UnityEditor.Lightmapping.GIWorkflowMode.OnDemand)
            {
                Debug.LogError("ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.");
                return;
            }
            //UnityEditor.Lightmapping.Bake();

            PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();

            foreach (var instance in prefabs)
            {
                var gameObject = instance.gameObject;
                var rendererInfos = new List<RendererInfo>();
                var lightmaps = new List<Texture2D>();
                var dirmaps = new List<Texture2D>();


                GenerateLightmapInfo(gameObject, rendererInfos, lightmaps, dirmaps);

                instance.m_RendererInfo = rendererInfos.ToArray();
                instance.m_Lightmaps = lightmaps.ToArray();
                instance.m_Dirmaps = dirmaps.ToArray();

                var targetPrefab = UnityEditor.PrefabUtility.GetPrefabParent(gameObject) as GameObject;
                if (targetPrefab != null)
                {
                    //UnityEditor.Prefab
                    UnityEditor.PrefabUtility.ReplacePrefab(gameObject, targetPrefab);
                }
            }
        }

        static void GenerateLightmapInfo(GameObject root, List<RendererInfo> rendererInfos, List<Texture2D> lightmaps, List<Texture2D> dirmaps)
        {
            var renderers = root.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                if (renderer.lightmapIndex != -1)
                {
                    RendererInfo info = new RendererInfo
                    {
                        renderer = renderer,
                        lightmapOffsetScale = renderer.lightmapScaleOffset
                    };

                    Texture2D lightmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapColor;
                    Texture2D dirmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapDir;

                    info.lightmapIndex = lightmaps.IndexOf(lightmap);
                    if (info.lightmapIndex == -1)
                    {
                        info.lightmapIndex = lightmaps.Count;
                        lightmaps.Add(lightmap);
                        dirmaps.Add(dirmap);
                    }

                    rendererInfos.Add(info);
                }
            }
        }
#endif

    }

}
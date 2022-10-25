using UnityEngine;
using System.Collections;
using dnSR_Coding.Utilities;
using NaughtyAttributes;
using UnityEngine.Rendering.Universal;

namespace dnSR_Coding
{
    ///<summary> PixelizeFeature description <summary>
    public class PixelizeFeature : ScriptableRendererFeature
    {
        [SerializeField] private CustomPassSettings settings;
        private PixelizePass _customPass;

        [System.Serializable]
        public class CustomPassSettings
        {
            public RenderPassEvent RenderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            [Range( 1, 500 )] public int PixelSize = 144;
        }        

        public override void Create()
        {
            _customPass = new PixelizePass( settings );
        }

        public override void AddRenderPasses( ScriptableRenderer renderer, ref RenderingData renderingData )
        {
#if UNITY_EDITOR
            if ( renderingData.cameraData.isSceneViewCamera ) return;
#endif
            renderer.EnqueuePass( _customPass );
        }

        public CustomPassSettings GetSettings() { return settings; }
    }
}
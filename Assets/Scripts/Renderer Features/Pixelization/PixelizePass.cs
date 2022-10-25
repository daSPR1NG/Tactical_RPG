using UnityEngine;
using System.Collections;
using dnSR_Coding.Utilities;
using NaughtyAttributes;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace dnSR_Coding
{
    ///<summary> PixelizePass description <summary>
    public class PixelizePass : ScriptableRenderPass
    {
        private PixelizeFeature.CustomPassSettings _settings;

        private RenderTargetIdentifier _colorBuffer, _pixelBuffer;
        private int _pixelBufferID = Shader.PropertyToID("_PixelBuffer");

        //private RenderTargetIdentifier _pointBuffer;
        //private int _pointBufferID = Shader.PropertyToID( "_PointBuffer" );

        private Material _material;
        private int pixelScreenHeight, pixelScreenWidth;

        public PixelizePass( PixelizeFeature.CustomPassSettings settings )
        {
            _settings = settings;
            renderPassEvent = settings.RenderPassEvent;
            if ( _material.IsNull() ) _material = CoreUtils.CreateEngineMaterial( "Hidden/Pixelize" );
        }

        public override void OnCameraSetup( CommandBuffer cmd, ref RenderingData renderingData )
        {
            _colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

            //cmd.GetTemporaryRT( _pointBufferID, descriptor.width, descriptor.height, 0, FilterMode.Point );
            //_pointBuffer = new RenderTargetIdentifier( _pointBufferID );

            pixelScreenHeight = _settings.PixelSize;
            pixelScreenWidth = ( int ) ( pixelScreenHeight * renderingData.cameraData.camera.aspect + .5f );

            _material.SetVector( "_BlockCount", new Vector2( pixelScreenWidth, pixelScreenHeight ) );
            _material.SetVector( "_BlockSize", new Vector2( 1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight ) );
            _material.SetVector( "_HalfBlockSize", new Vector2( .5f / pixelScreenWidth, .5f / pixelScreenHeight ) );

            descriptor.height = pixelScreenHeight;
            descriptor.width = pixelScreenWidth;

            cmd.GetTemporaryRT( _pixelBufferID, descriptor, FilterMode.Point );
            _pixelBuffer = new RenderTargetIdentifier( _pixelBufferID );
        }

        public override void Execute( ScriptableRenderContext context, ref RenderingData renderingData )
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            using ( new ProfilingScope( cmd, new ProfilingSampler( "Pixelize Pass" ) ) )
            {
                //Blit( cmd, _colorBuffer, _pointBuffer );
                //Blit( cmd, _pointBuffer, _pixelBuffer );
                //Blit( cmd, _pixelBuffer, _colorBuffer );


                Blit( cmd, _colorBuffer, _pixelBuffer, _material );
                Blit( cmd, _pixelBuffer, _colorBuffer );
            }

            context.ExecuteCommandBuffer( cmd );
            CommandBufferPool.Release( cmd );
        }

        public override void OnCameraCleanup( CommandBuffer cmd )
        {
            if ( cmd.IsNull() ) throw new System.ArgumentNullException( "cmd" );
            cmd.ReleaseTemporaryRT( _pixelBufferID );
            //cmd.ReleaseTemporaryRT( _pointBufferID );
        }
    }
}
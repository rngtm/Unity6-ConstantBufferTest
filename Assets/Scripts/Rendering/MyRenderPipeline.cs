using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace Example
{
    public class MyRenderPipeline : RenderPipeline
    {
        private CullingResults _cullingResults;
        
        private GraphicsController _graphicsController;
        
        // 描画したいShaderのLightModeタグ
        private readonly ShaderTagId[] _shaderTagIds = new ShaderTagId[]
        {
            new("SRPDefaultUnlit"),
            new("UniversalForward"),
        };

        public MyRenderPipeline()
        {
            _graphicsController = new GraphicsController();
        }

        protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            cmd.Clear();
            
            _graphicsController.Execute(cmd);
            foreach (Camera camera in cameras)
            {
                context.SetupCameraProperties(camera);
                
                cmd.ClearRenderTarget(true, true, Color.gray);
                DrawOpaqueGeometry(context, camera, cmd);
                // DrawSky(context, camera, cmd);
            }
            context.ExecuteCommandBuffer(cmd);
            context.Submit();
            
            CommandBufferPool.Release(cmd);
        }

        private void DrawOpaqueGeometry(ScriptableRenderContext context, Camera camera, CommandBuffer cmd)
        {
            // カリング
            if (!camera.TryGetCullingParameters(out ScriptableCullingParameters p))
            {
                return;
            }
            _cullingResults = context.Cull(ref p);
            
            // Opaqueジオメトリの描画
            RendererListDesc desc = new(_shaderTagIds, _cullingResults, camera);
            desc.renderQueueRange = RenderQueueRange.opaque;
            desc.sortingCriteria = SortingCriteria.CommonOpaque;
            RendererList rendererList = context.CreateRendererList(desc);
            cmd.DrawRendererList(rendererList);
        }

        private static void DrawSky(ScriptableRenderContext context, Camera cam, CommandBuffer cmd)
        {
            // Skyboxの描画
            RendererList rendererList = context.CreateSkyboxRendererList(cam);
            cmd.DrawRendererList(rendererList);
        }
    }
}
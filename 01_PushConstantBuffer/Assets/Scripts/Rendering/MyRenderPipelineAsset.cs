using UnityEngine;
using UnityEngine.Rendering;

namespace Example
{
    [CreateAssetMenu(menuName = "Rendering/My RP Asset")]
    public class MyRenderPipelineAsset : RenderPipelineAsset<MyRenderPipeline>
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new MyRenderPipeline();
        }
    }
}
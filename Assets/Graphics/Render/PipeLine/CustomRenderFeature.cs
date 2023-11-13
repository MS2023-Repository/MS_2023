using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Graphics.Render.PipeLine
{
    public class CustomRenderFeature: ScriptableRendererFeature
    {
        // Render Texture の横幅
        [SerializeField] private int _width = 960;

        // Render Texture の縦幅
        [SerializeField] private int _height = 540;

        // レンダリングパイプラインに追加する描画パス
        private CustomRenderPass _customRenderPass;

        
        public override void Create()
        {
            // Renderer Feature が生成されたときの処理
            _customRenderPass = new CustomRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRendering
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            // Render Pass をレンダリングパイプラインに追加する
            // 描画パスのセットアップと，レンダリングパイプラインへの追加
            _customRenderPass.Setup(_width, _height);
            renderer.EnqueuePass(_customRenderPass);
        }
    }
}
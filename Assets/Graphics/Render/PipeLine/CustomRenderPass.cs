using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Graphics.Render.PipeLine
{
    public class CustomRenderPass : ScriptableRenderPass
    {
        private RenderTexture _renderTexture;

        // Render Texture の横幅
        private int _width;

        // Render Texture の縦幅
        private int _height;

        public void Setup(int width, int height)
        {
            _width = width;
            _height = height;
        }

        // 描画の設定
        public override void Configure(CommandBuffer commandBuffer, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Render Texture を生成する
            _renderTexture = RenderTexture.GetTemporary(_width, _height);

            // 描画先を Render Texture にする
            ConfigureTarget(new RenderTargetIdentifier(_renderTexture));

            // クリアフラグの設定(描画されなかった領域は青色で塗りつぶす)
            ConfigureClear(ClearFlag.Color, Color.blue);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // コマンドバッファの取得
            var commandBuffer = CommandBufferPool.Get();

            // あとでこの描画を確認する際にわかりやすくするために，"Sample"で切り分ける
            using (new ProfilingScope(commandBuffer, new ProfilingSampler("CustomToonRenderPass")))
            {
                // 事前に実行しておくコマンドバッファ
                context.ExecuteCommandBuffer(commandBuffer);
                commandBuffer.Clear();

                // 描画に必要なカメラ等の設定
                var camera = renderingData.cameraData.camera;
                var sortingSettings = new SortingSettings(camera);
                var drawingSettings = new DrawingSettings(new ShaderTagId("ForwardBase"), sortingSettings);
                var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

                // 上記設定で描画の実行
                context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
            }

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            // Render Texture をリリース
            if (_renderTexture)
            {
                RenderTexture.ReleaseTemporary(_renderTexture);
            }
        }
    }
}
namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    // The HDRenderPipeline assumes linear lighting. Doesn't work with gamma.
    public class HDRenderPipelineAsset : RenderPipelineAsset
    {
        HDRenderPipelineAsset()
        {
        }

        protected override IRenderPipeline InternalCreatePipeline()
        {
            return new HDRenderPipeline(this);
        }

        [SerializeField]
        RenderPipelineResources m_RenderPipelineResources;
        public RenderPipelineResources renderPipelineResources
        {
            get { return m_RenderPipelineResources; }
            set { m_RenderPipelineResources = value; }
        }

        // To be able to turn on/off FrameSettings properties at runtime for debugging purpose without affecting the original one
        // we create a runtime copy (m_ActiveFrameSettings that is used, and any parametrization is done on serialized frameSettings)
        public FrameSettings serializedFrameSettings = new FrameSettings(); // This are the defaultFrameSettings for all the camera and apply to sceneView, public to be visible in the inspector
        // Not serialized, not visible, the settings effectively used
        FrameSettings m_FrameSettings = new FrameSettings();

        public FrameSettings GetFrameSettings()
        {
            return m_FrameSettings;
        }

        public void OnEnable()
        {
            // At creation we need to copy serializedFrameSettings to m_FrameSettings
            OnValidate();
        }

        public void OnValidate()
        {
            // Modification of defaultFrameSettings in the inspector will call OnValidate().
            // We do a copy of the settings to those effectively used
            serializedFrameSettings.CopyTo(m_FrameSettings);

            // All instance created in the editor have obsolete settings
            // So we must dispose them
            DestroyCreatedInstances();
        }

        // Store the various RenderPipelineSettings for each platform (for now only one)
        public RenderPipelineSettings renderPipelineSettings = new RenderPipelineSettings();

        // Return the current use RenderPipelineSettings (i.e for the current platform)
        public RenderPipelineSettings GetRenderPipelineSettings()
        {
            return renderPipelineSettings;
        }

        [SerializeField]
        public SubsurfaceScatteringSettings sssSettings;

        public override Shader GetDefaultShader()
        {
            return m_RenderPipelineResources.defaultShader;
        }

        public override Material GetDefaultMaterial()
        {
            return m_RenderPipelineResources.defaultDiffuseMaterial;
        }

        public override Material GetDefaultParticleMaterial()
        {
            return null;
        }

        public override Material GetDefaultLineMaterial()
        {
            return null;
        }

        public override Material GetDefaultTerrainMaterial()
        {
            return null;
        }

        public override Material GetDefaultUIMaterial()
        {
            return null;
        }

        public override Material GetDefaultUIOverdrawMaterial()
        {
            return null;
        }

        public override Material GetDefaultUIETC1SupportedMaterial()
        {
            return null;
        }

        public override Material GetDefault2DMaterial()
        {
            return null;
        }
    }
}

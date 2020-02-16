using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class NoribenLightBeam : MonoBehaviour
    {

        public Color color;
        public float intensity = 1f;
        public float intensityMultiplier = 0.5f;

        private MeshRenderer meshRenderer;
        private MaterialPropertyBlock materialPropertyBlock;


        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            materialPropertyBlock = new MaterialPropertyBlock();
        }

        // Update is called once per frame
        void Update()
        {

            meshRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor("_Color", color);
            materialPropertyBlock.SetFloat("_Intensity", intensity * intensityMultiplier);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);

        }
    }
}
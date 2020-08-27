using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class NoribenLightBeam : MonoBehaviour
    {

        public Color color;
        public float intensity = 1f;
        public float intensityMultiplier = 0.5f;

        [SerializeField]
        MeshRenderer beamGeometry;

        private MaterialPropertyBlock materialPropertyBlock;

        public void Process()
        {

            if (beamGeometry)
            {
                if (materialPropertyBlock == null)
                {
                    materialPropertyBlock = new MaterialPropertyBlock();
                }


                beamGeometry.GetPropertyBlock(materialPropertyBlock);
                materialPropertyBlock.SetColor("_Color", color);
                materialPropertyBlock.SetFloat("_Intensity", intensity * intensityMultiplier);
                beamGeometry.SetPropertyBlock(materialPropertyBlock);
            }

        }
    }
}
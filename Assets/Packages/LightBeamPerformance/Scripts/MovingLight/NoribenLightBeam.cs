using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class NoribenLightBeam : Beam
    {

        [SerializeField]
        MeshRenderer beamGeometry;

        private MaterialPropertyBlock materialPropertyBlock;

        protected override void ProcessInternal()
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
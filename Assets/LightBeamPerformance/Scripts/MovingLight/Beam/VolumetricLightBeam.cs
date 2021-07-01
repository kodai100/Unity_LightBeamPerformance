using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
#if VOLUMETRIC_LIGHT_BEAM
    public class VolumetricLightBeam : Beam
    {
        [SerializeField] VLB.VolumetricLightBeam vlb;

        protected override void ProcessInternal()
        {
            if (vlb)
            {
                vlb.color = color;
                vlb.intensityGlobal = intensity * intensityMultiplier * 2f;
            }
        }
    }

#endif
}
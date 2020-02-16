using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class LightHead : MonoBehaviour
    {

        public NoribenLightBeam beam;

        public float intensity = 1f;

        public float headOnlyIntensityMultiplier = 1f;

        public Color color = Color.red;

        MeshRenderer renderer;
        Material mat;

        
        void Start()
        {
            renderer = GetComponent<MeshRenderer>();


            mat = new Material(Shader.Find("Standard"));
            renderer.material = mat;

            mat.EnableKeyword("_EMISSION");
        }
        
        void Update()
        {

            beam.color = color;
            beam.intensity = intensity;

            mat.SetColor("_EmissionColor", color * intensity * headOnlyIntensityMultiplier);
        }



    }
}
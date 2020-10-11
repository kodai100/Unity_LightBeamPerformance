using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class LightHead : MonoBehaviour
    {

        public Beam beam;

        public float intensity = 1f;

        public float headOnlyIntensityMultiplier = 1f;

        public Color color = Color.red;

        [SerializeField]
        MeshRenderer lightLensMesh;

        Material mat;

        public void Process()
        {

            if (lightLensMesh)
            {
                if (!mat)
                {
                    mat = new Material(Shader.Find("Standard"));
                    GetComponent<Renderer>().material = mat;

                    mat.EnableKeyword("_EMISSION");
                }
                else
                {
                    mat.SetColor("_EmissionColor", color * intensity * headOnlyIntensityMultiplier);
                }
            }

            if (beam)
            {
                beam.color = color;
                beam.intensity = intensity;

                beam.Process();
            }
            
        }


    }
}
using System;
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

        [SerializeField] private Renderer renderer = null;
        [SerializeField] private Shader litShader;

        private Material mat = null;

        [ContextMenu("Re generate material")]
        private void ReGenerateMaterial()
        {
            var material = new Material(litShader);
            material.color = Color.black;

            renderer.material = material;
            DestroyImmediate(mat);
            mat = material;
        }

        private void CheckMaterial()
        {
            if (!mat)
            {
                mat = new Material(litShader);
                mat.color = Color.black;

                renderer.material = mat;
            }
        }

        private void CheckRenderer()
        {
            if (!renderer)
            {
                renderer = GetComponent<Renderer>();
            }
        }

        public void Process()
        {
            CheckRenderer();
            CheckMaterial();


            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_Color", color);
            mat.SetColor("_EmissionColor", color * intensity * headOnlyIntensityMultiplier);

            if (beam)
            {
                beam.color = color;
                beam.intensity = intensity;

                beam.Process();
            }
        }

        private void OnDestroy()
        {
            GameObject.Destroy(mat);
        }
    }
}
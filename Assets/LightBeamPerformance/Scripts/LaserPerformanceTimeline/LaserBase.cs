using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance {

    public class LaserBase : MonoBehaviour
    {

        public int id { get; set; }
        public float AddressOffset { get; set; }
        public int numLaserBases { get; set; }
        

        Material mat;

        float defaultIndicatorDegree = 0;
        float defaultOffsetDegree = 0;

        public void Initialize(int id, float addressOffset, int numLaserBases, float defaultIndicatorDegree, Shader shader)
        {
            this.id = id;
            this.AddressOffset = addressOffset;
            this.numLaserBases = numLaserBases;
            SetDafaultDegrees(defaultIndicatorDegree);

            mat = new Material(shader);

            GetComponent<MeshRenderer>().material = mat;
        }

        public void SetDafaultDegrees(float defaultIndicatorDegree)
        {
            this.defaultIndicatorDegree = defaultIndicatorDegree;
            defaultOffsetDegree = defaultIndicatorDegree * 2 / (numLaserBases - 1);
        }

        public void DefaultDegree()
        {
            transform.localRotation = Quaternion.Euler(0, -defaultIndicatorDegree + id * defaultOffsetDegree, 0);
        }

        public void Degree(float indicatorDegree)
        {
            float offsetDegree = indicatorDegree * 2 / (numLaserBases - 1);

            transform.localRotation = Quaternion.Euler(0, -indicatorDegree + id * offsetDegree, 0);
        }
        

        public void SetColor(Color col)
        {
            mat.SetColor("_Color", col);
        }

        public void SetDimmer(float val)
        {
            mat.SetFloat("_Dimmer", val);
        }


    }
}
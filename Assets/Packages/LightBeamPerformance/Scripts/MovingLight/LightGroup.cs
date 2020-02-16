using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance {

    [System.Serializable]
    public class LightGroup : MonoBehaviour
    {
        public List<MovingLight> lights = new List<MovingLight>();
    }

}

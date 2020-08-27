using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProjectBlue.LightBeamPerformance {

    public class MovingLightControllerTest : MonoBehaviour
    {

        public List<MovingLight> lights = new List<MovingLight>();

        public Transform target;


        void Update()
        {

            lights.ForEach(light =>{
                light.LookAt(target.position);
            });

            
        }
    }

}
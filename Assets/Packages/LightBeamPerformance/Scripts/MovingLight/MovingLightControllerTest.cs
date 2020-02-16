using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProjectBlue.LightBeamPerformance {

    public class MovingLightControllerTest : MonoBehaviour
    {

        public List<MovingLight> lights = new List<MovingLight>();

        public Transform target; 
    
    // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            lights.ForEach(light =>{
                light.LookAt(target);
            });

            
        }
    }

}
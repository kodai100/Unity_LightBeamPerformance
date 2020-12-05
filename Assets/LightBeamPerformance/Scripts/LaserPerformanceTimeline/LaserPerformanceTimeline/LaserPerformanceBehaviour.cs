using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [Serializable]
    public class LaserPerformanceBehaviour : PlayableBehaviour
    {

        public LaserColorMode color = LaserColorMode.SingleColor;
        public LaserDimmerMode dimmer = LaserDimmerMode.On;
        public LaserMotionMode motion = LaserMotionMode.Open;

        public float bpm = 120;

        public Color laserColor = Color.white;
        public float intensityMultiplier = 1f;

        public float speed = 1f;
        public float offsetStrength = 1f;
        
        public FloatRange panRange = new FloatRange(-90, 90, -180, 180);
        public FloatRange tiltRange = new FloatRange(-20, 20, -90, 90);
    }

}
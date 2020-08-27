using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [Serializable]
    public class LightPerformanceBehaviour : PlayableBehaviour
    {
        [SerializeField]
        public AddressType addressType = AddressType.Group;

        [SerializeField]
        public ColorAnimationMode color = ColorAnimationMode.SingleColor;

        [Range(0f, 1f)]
        public float saturation = 1f;

        [SerializeField]
        public DimmerAnimationMode dimmer = DimmerAnimationMode.On;
        [SerializeField]
        public MotionAnimationMode motion = MotionAnimationMode.Default;

        [SerializeField]
        public float bpm = 120;

        [SerializeField]
        public Color lightColor = Color.white;
        [SerializeField]
        public float intensityMultiplier = 1f;
        
        [SerializeField]
        public float speed = 1f;
        [SerializeField]
        public float offsetStrength = 1f;

    }

}
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
        AddressType addressType = AddressType.Group;

        [SerializeField]
        ColorAnimationMode color = ColorAnimationMode.SingleColor;

        [Range(0f, 1f)]
        public float saturation = 1f;

        [SerializeField]
        DimmerAnimationMode dimmer = DimmerAnimationMode.On;
        [SerializeField]
        MotionAnimationMode motion = MotionAnimationMode.Default;

        [SerializeField]
        float bpm = 120;

        [SerializeField]
        Color lightColor = Color.white;
        [SerializeField]
        float intensityMultiplier = 1f;
        
        [SerializeField]
        float speed = 1f;
        [SerializeField]
        float offsetStrength = 1f;

        [HideInInspector] public LightBeamPerformance lightBeamPerformance;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!lightBeamPerformance) return;

            lightBeamPerformance.AddressType = addressType;

            lightBeamPerformance.ChangeState(color, dimmer, motion);

            lightBeamPerformance.ChangeBpm(bpm);

            lightBeamPerformance.LightColor = lightColor;
            lightBeamPerformance.Saturation = saturation;
            lightBeamPerformance.IntensityMultiplier = intensityMultiplier;

            lightBeamPerformance.Speed = speed;
            lightBeamPerformance.OffsetStrength = offsetStrength;
        }
    }

}
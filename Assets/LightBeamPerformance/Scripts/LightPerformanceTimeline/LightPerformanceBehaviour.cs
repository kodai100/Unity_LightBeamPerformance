using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ProjectBlue.LightBeamPerformance
{
    [Serializable]
    public class LightPerformanceBehaviour : PlayableBehaviour
    {
        [SerializeField] public AddressType addressType = AddressType.Group;

        [SerializeField] public ColorAnimationMode color = ColorAnimationMode.Gradient;

        [Range(0f, 1f)] public float saturation = 1f;

        [SerializeField] public DimmerAnimationMode dimmer = DimmerAnimationMode.On;
        [SerializeField] public MotionAnimationMode motion = MotionAnimationMode.Default;

        [SerializeField] public float bpm = 120;

        [SerializeField] public Gradient lightGradient = new Gradient();
        [SerializeField] public float intensityMultiplier = 1f;

        [SerializeField] public float speed = 1f;
        [SerializeField] public float offsetStrength = 1f;

        [SerializeField] public FloatRange panRange = new FloatRange(-80, 80, -180, 180);
        [SerializeField] public FloatRange tiltRange = new FloatRange(-70, -10, -90, 90);
    }
}
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
    [CreateAssetMenu(fileName = "LightPerformanceClipPreset", menuName = "LightBeamPerformance/CreateLightPerformacePreset")]
    public class LightPerformanceClipPreset : ScriptableObject
    {
        public ColorAnimationMode colorAnimationMode = ColorAnimationMode.Gradient;
        [Range(0f, 1f)] public float saturation = 1f;
        public DimmerAnimationMode dimmerAnimationMode = DimmerAnimationMode.On;
        public MotionAnimationMode motionAnimationMode = MotionAnimationMode.Default;
        public float bpm = 120;

        public Gradient gradient = new Gradient();
        public float intensityMultiplier = 1f;
        public float speed = 1f;
        public float offsetStrength = 1f;
        
        public FloatRange panRange = new FloatRange(-80, 80, -180, 180);
        public FloatRange tiltRange = new FloatRange(-70, -10, -90, 90);
    }

}


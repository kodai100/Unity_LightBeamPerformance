using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProjectBlue.LightBeamPerformance
{
    
    public class LightBeamPerformance : MonoBehaviour
    {
        public ColorAnimationMode ColorAnimationMode = ColorAnimationMode.HSVLoop;
        public DimmerAnimationMode DimmerAnimationMode = DimmerAnimationMode.Beat;
        public MotionAnimationMode MotionAnimationMode = MotionAnimationMode.Default;

        public Transform Target;

        public Gradient LightGradient = new();
        public float Saturation = 1f;

        public float Speed = 1f; // includes "wave speed"
        public float OffsetStrength = 5f; // includes "frequency"

        public Range panRange = new(-45, 45);
        public Range tiltRange = new(0, 60);

        [HideInInspector] public float IntensityMultiplier = 1f;
        [SerializeField] private List<LightGroup> lightGroup = new();

        private bool initialized;

        private readonly BPM bpm = new(120);

        private DimmerAnimation dimmerAnimation;
        private ColorAnimation colorAnimation;
        private MotionAnimation motionAnimation;
        
        private void Start()
        {
            Initialize();
        }

        private void OnValidate()
        {
            initialized = false;
        }

        public void Initialize()
        {
            // Count all of lights and set address to each light
            var lightNum = 0;
            ;
            for (var u = 0; u < lightGroup.Count; u++)
            {
                for (var a = 0; a < lightGroup[u].lights.Count; a++)
                {
                    // local address
                    lightGroup[u].lights[a].group = u;
                    lightGroup[u].lights[a].address = a;
                    lightGroup[u].lights[a].LocalAddressOffset = (float) a / lightGroup[u].lights.Count;
                    lightGroup[u].lights[a].globalAddress = lightNum;
                    lightNum++;
                }
            }
            
            foreach (var movingLight in lightGroup.SelectMany(group => group.lights))
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(movingLight);
#endif
            }

            Debug.Log("Light Beam Initialized");
            initialized = true;

            dimmerAnimation = new DimmerAnimation(bpm);
            colorAnimation = new ColorAnimation(bpm);
            motionAnimation = new MotionAnimation(bpm);

            lightGroup.ForEach(group =>
            {
                group.lights.ForEach(fixture =>
                {
                    fixture.SetIntensity(0);
                    fixture.Process();
                });
            });
        }

        public void ProcessFrame(double clipTime)
        {
            if (!initialized) Initialize();
            
            dimmerAnimation.Update((float)clipTime);
            colorAnimation.Update((float)clipTime);
            motionAnimation.Update((float)clipTime);

            lightGroup.ForEach(group =>
            {
                group.lights.ForEach(ProcessPerLight);
            });
        }


        private void ProcessPerLight(MovingLight fixture)
        {
            colorAnimation.Animate(ColorAnimationMode, fixture, LightGradient, Saturation);
            dimmerAnimation.Animate(DimmerAnimationMode, fixture, OffsetStrength, Speed, IntensityMultiplier);
            motionAnimation.Animate(MotionAnimationMode, fixture, OffsetStrength, Speed, Target, panRange, tiltRange);

            fixture.Process();
        }
        
        public void ChangeBpm(float b)
        {
            bpm.Bpm = b;
        }

        public void ChangeState(ColorAnimationMode color, DimmerAnimationMode dimmer, MotionAnimationMode motion)
        {
            ColorAnimationMode = color;
            DimmerAnimationMode = dimmer;
            MotionAnimationMode = motion;
        }
    }
}
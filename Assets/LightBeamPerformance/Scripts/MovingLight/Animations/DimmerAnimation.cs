using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum DimmerAnimationMode
    {
        On,
        Off,
        Beat,
        BeatReverse,
        Run,
        RunReverse,
        Sin,
        ReverseSin
    }
    
    public class DimmerAnimation : AnimationBase
    {
        
        public DimmerAnimation(BPM bpm) : base(bpm)
        {
        }

        public void Animate(DimmerAnimationMode animationMode, MovingLight light, float offsetStrength, float speed, float intensityMultiplier)
        {
            switch (animationMode)
            {
                case DimmerAnimationMode.Beat:
                    light.SetIntensity(beat);
                    break;
                case DimmerAnimationMode.BeatReverse:
                    light.SetIntensity(1 - beat);
                    break;
                case DimmerAnimationMode.Run:
                    light.SetIntensity(light.GetAddressOffset() >= beat - 0.1 ? 1f : 0f);
                    break;
                case DimmerAnimationMode.RunReverse:
                    light.SetIntensity(light.GetAddressOffset() >= beat - 0.1 ? 0f : 1f);
                    break;
                case DimmerAnimationMode.On:
                    light.SetIntensity(1f);
                    break;
                case DimmerAnimationMode.Off:
                    light.SetIntensity(0f);
                    break;
                case DimmerAnimationMode.Sin:
                    light.SetIntensity(0.5f *
                        Mathf.Sin(light.GetAddressOffset() * offsetStrength + time * speed) + 0.5f);
                    break;
                case DimmerAnimationMode.ReverseSin:
                    light.SetIntensity(0.5f *
                        Mathf.Sin(light.GetAddressOffset() * offsetStrength - time * speed) + 0.5f);
                    break;
                default:
                    light.SetIntensity(1f);
                    break;
            }

            light.MultiplyIntensity(intensityMultiplier);
        }
        
    }

}
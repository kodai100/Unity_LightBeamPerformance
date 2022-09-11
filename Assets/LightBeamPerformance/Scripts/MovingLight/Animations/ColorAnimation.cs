using System;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum ColorAnimationMode
    {
        Gradient,
        GradientLoop,
        RGB,
        HSVLoop,
        HSV
    }
    
    public class ColorAnimation : AnimationBase
    {
        
        public ColorAnimation(BPM bpm) : base(bpm)
        {
        }
        
        public void Animate(ColorAnimationMode colorAnimationMode, MovingLight light, Gradient lightGradient, float saturation)
        {
            switch (colorAnimationMode)
            {
                case ColorAnimationMode.Gradient:
                    light.SetColor(lightGradient.Evaluate(light.GetAddressOffset()));
                    break;
                case ColorAnimationMode.GradientLoop:
                    light.SetColor(lightGradient.Evaluate((time + light.GetAddressOffset()) % 1f));
                    break;
                case ColorAnimationMode.HSVLoop:
                    light.SetColor(Color.HSVToRGB((time + light.GetAddressOffset()) % 1f, saturation, 1));
                    break;
                case ColorAnimationMode.HSV:
                    light.SetColor(Color.HSVToRGB(light.GetAddressOffset(), saturation, 1));
                    break;
                case ColorAnimationMode.RGB:
                    if (light.GetAddressOffset() < 1f / 3f)
                    {
                        light.SetColor(Color.red);
                    }
                    else if (light.GetAddressOffset() < 2f / 3f)
                    {
                        light.SetColor(Color.green);
                    }
                    else
                    {
                        light.SetColor(Color.blue);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(colorAnimationMode), colorAnimationMode, null);
            }
        }
    }

}
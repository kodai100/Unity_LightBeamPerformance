using System;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum MotionAnimationMode
    {
        PanLoop,
        TiltLoop,
        OffsetTiltLoop,
        PanTiltLoop,
        OffsetPanTiltLoop,
        Focus,
        Default
    }
    
    public class MotionAnimation : AnimationBase
    {
        
        public MotionAnimation(BPM bpm) : base(bpm)
        {
        }
        
        public void Animate(MotionAnimationMode motionAnimationMode,  MovingLight light, float offsetStrength, float speed, Transform target, Range panRange, Range tiltRange)
        {
            switch (motionAnimationMode)
            {
                case MotionAnimationMode.Focus:
                    if (target != null)
                        light.LookAt(target.position);
                    break;
                case MotionAnimationMode.PanLoop:
                    light.SetPanAngle(panRange.Sin(time));
                    light.SetTiltDefaultAngle();
                    break;
                case MotionAnimationMode.TiltLoop:
                    light.SetTiltAngle(tiltRange.Sin(time));
                    light.SetPanDefaultAngle();
                    break;
                case MotionAnimationMode.OffsetTiltLoop:
                    light.SetTiltAngle(
                        tiltRange.Sin(time * speed + light.GetAddressOffset() * offsetStrength));
                    light.SetPanDefaultAngle();
                    break;
                case MotionAnimationMode.PanTiltLoop:
                    light.SetPanAngle(panRange.Cos(time));
                    light.SetTiltAngle(tiltRange.Sin(time));
                    break;
                case MotionAnimationMode.OffsetPanTiltLoop:
                    light.SetPanAngle(
                        panRange.Cos(time * speed + light.GetAddressOffset() * offsetStrength));
                    light.SetTiltAngle(
                        tiltRange.Sin(time * speed + light.GetAddressOffset() * offsetStrength));
                    break;
                case MotionAnimationMode.Default:
                    light.SetPanDefaultAngle();
                    light.SetTiltDefaultAngle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(motionAnimationMode), motionAnimationMode, null);
            }
        }
        
    }

}
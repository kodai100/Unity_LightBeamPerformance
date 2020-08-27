using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum ColorAnimationMode
    {
        SingleColor, RGB, HSVLoop, HSV
    }

    public enum DimmerAnimationMode
    {
        On, Off, Beat, BeatReverse, Run, RunReverse, Sin, ReverseSin
    }

    public enum MotionAnimationMode
    {
        PanLoop, TiltLoop, OffsetTiltLoop, PanTiltLoop, OffsetPanTiltLoop, Focus, Default 
    }
    

    public class LightBeamPerformance : MonoBehaviour
    {

        public AddressType AddressType = AddressType.Group;

        public ColorAnimationMode ColorAnimationMode = ColorAnimationMode.HSVLoop;
        public DimmerAnimationMode DimmerAnimationMode = DimmerAnimationMode.Beat;
        public MotionAnimationMode MotionAnimationMode = MotionAnimationMode.Default;

        public Transform Target;

        public Color LightColor = Color.white;
        public float Saturation = 1f;

        public float Speed = 1f;            // includes "wave speed"
        public float OffsetStrength = 5f;   // includes "frequency"

        public Range panRange = new Range(-90, 90);
        public Range tiltRange = new Range(-10, 10);
        
        [HideInInspector] public float IntensityMultiplier = 1f;
        [SerializeField] List<LightGroup> lightGroup = new List<LightGroup>();

        float beat;

        BPM bpm = new BPM(120);

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Count all of lights and set address to each light
            int lightNum = 0; ;
            for (int u = 0; u < lightGroup.Count; u++)
            {
                for (int a = 0; a < lightGroup[u].lights.Count; a++)
                {
                    // local address
                    lightGroup[u].lights[a].group = u;
                    lightGroup[u].lights[a].address = a;
                    lightGroup[u].lights[a].LocalAddressOffset = (float)a / lightGroup[u].lights.Count;

                    lightGroup[u].lights[a].globalAddress = lightNum;
                    lightNum++;
                }
            }

            // Set global address offset
            for (int u = 0; u < lightGroup.Count; u++)
            {
                for (int a = 0; a < lightGroup[u].lights.Count; a++)
                {
                    lightGroup[u].lights[a].GlobalAddressOffset = (float)lightGroup[u].lights[a].globalAddress / lightNum;
                }
            }

        }

        public void ProcessFrame(double time)
        {
            beat = bpm.SecondsToBeat((float)time);

            lightGroup.ForEach(group => {

                group.lights.ForEach(light => {
                    ProcessPerLight(light, (float) time);
                });

            });

        }


        void ProcessPerLight(MovingLight light, float time)
        {
            ColorAnimation(light, time);
            DimmerAnimation(light, time);
            MotionAnimation(light, time);

            light.Process();
        }

        
        void ColorAnimation(MovingLight light, float time)
        {

            switch (ColorAnimationMode)
            {
                case ColorAnimationMode.SingleColor:
                    light.SetColor(LightColor);
                    break;
                case ColorAnimationMode.HSVLoop:
                    light.SetColor(Color.HSVToRGB((time + light.GetAddressOffset(AddressType)) % 1f, Saturation, 1));
                    break;
                case ColorAnimationMode.HSV:
                    light.SetColor(Color.HSVToRGB(light.GetAddressOffset(AddressType), Saturation, 1));
                    break;
                case ColorAnimationMode.RGB:
                    if(light.GetAddressOffset(AddressType) < 1f / 3f)
                    {
                        light.SetColor(Color.red);
                    } else if(light.GetAddressOffset(AddressType) < 2f / 3f)
                    {
                        light.SetColor(Color.green);
                    }
                    else
                    {
                        light.SetColor(Color.blue);
                    }
                    break;
                default:
                    break;
            }

        }

        void DimmerAnimation(MovingLight light, float time)
        {
            switch (DimmerAnimationMode)
            {
                case DimmerAnimationMode.Beat:
                    light.SetIntensity(beat);
                    break;
                case DimmerAnimationMode.BeatReverse:
                    light.SetIntensity(1 - beat);
                    break;
                case DimmerAnimationMode.Run:
                    light.SetIntensity(light.GetAddressOffset(AddressType) >= beat - 0.1 ? 1f : 0f);
                    break;
                case DimmerAnimationMode.RunReverse:
                    light.SetIntensity(light.GetAddressOffset(AddressType) >= beat - 0.1 ? 0f : 1f);
                    break;
                case DimmerAnimationMode.On:
                    light.SetIntensity(1f);
                    break;
                case DimmerAnimationMode.Off:
                    light.SetIntensity(0f);
                    break;
                case DimmerAnimationMode.Sin:
                    light.SetIntensity(0.5f * Mathf.Sin(light.GetAddressOffset(AddressType) * OffsetStrength + time * Speed) + 0.5f);
                    break;
                case DimmerAnimationMode.ReverseSin:
                    light.SetIntensity(0.5f * Mathf.Sin(light.GetAddressOffset(AddressType) * OffsetStrength - time * Speed) + 0.5f);
                    break;
                default:
                    light.SetIntensity(1f);
                    break;
            }

            light.MultiplyIntensity(IntensityMultiplier);
        }

        void MotionAnimation(MovingLight light, float time)
        {
            switch (MotionAnimationMode)
            {
                case MotionAnimationMode.Focus:
                    if(Target != null)
                        light.LookAt(Target.position);
                    break;
                case MotionAnimationMode.PanLoop:
                    light.SetPanAngle(panRange.Sin(time));
                    light.SetTiltDefaltAngle();
                    break;
                case MotionAnimationMode.TiltLoop:
                    light.SetTiltAngle(tiltRange.Sin(time));
                    light.SetPanDefaultAngle();
                    break;
                case MotionAnimationMode.OffsetTiltLoop:
                    light.SetTiltAngle(tiltRange.Sin(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    light.SetPanDefaultAngle();
                    break;
                case MotionAnimationMode.PanTiltLoop:
                    light.SetPanAngle(panRange.Cos(time));
                    light.SetTiltAngle(tiltRange.Sin(time));
                    break;
                case MotionAnimationMode.OffsetPanTiltLoop:
                    light.SetPanAngle(panRange.Cos(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    light.SetTiltAngle(tiltRange.Sin(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    break;
                case MotionAnimationMode.Default:
                    light.SetPanDefaultAngle();
                    light.SetTiltDefaltAngle();
                    break;
            }
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
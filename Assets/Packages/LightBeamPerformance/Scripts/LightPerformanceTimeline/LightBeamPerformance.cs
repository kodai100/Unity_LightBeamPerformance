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

        float time = 0;
        
        float beat;
        float timeForBeat = 0;

        BPM bpm = new BPM(120);
        float previousBpm = 120f;

        void Start()
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
                    lightGroup[u].lights[a].addressOffset = (float)a / lightGroup[u].lights.Count;

                    lightGroup[u].lights[a].globalAddress = lightNum;
                    lightNum++;
                }
            }

            // Set global address offset
            for (int u = 0; u < lightGroup.Count; u++)
            {
                for(int a = 0; a < lightGroup[u].lights.Count; a++)
                {
                    lightGroup[u].lights[a].globalAddressOffset = (float)lightGroup[u].lights[a].globalAddress / lightNum;
                }
            }
        }

        void Update()
        {
            
            time += Time.deltaTime;

            timeForBeat += Time.deltaTime;
            beat = bpm.SecondsToBeat(timeForBeat);

            lightGroup.ForEach(group => {

                group.lights.ForEach(light => {
                    ProcessPerLight(light);
                });
                
            });

        }


        void ProcessPerLight(MovingLight light)
        {
            ColorAnimation(light);
            DimmerAnimation(light);
            MotionAnimation(light);
        }

        
        void ColorAnimation(MovingLight light)
        {

            switch (ColorAnimationMode)
            {
                case ColorAnimationMode.SingleColor:
                    light.Head.color = LightColor;
                    break;
                case ColorAnimationMode.HSVLoop:
                    light.Head.color = Color.HSVToRGB((time + light.GetAddressOffset(AddressType)) % 1f, Saturation, 1);
                    break;
                case ColorAnimationMode.HSV:
                    light.Head.color = Color.HSVToRGB(light.GetAddressOffset(AddressType), Saturation, 1);
                    break;
                case ColorAnimationMode.RGB:
                    if(light.GetAddressOffset(AddressType) < 1f / 3f)
                    {
                        light.Head.color = Color.red;
                    } else if(light.GetAddressOffset(AddressType) < 2f / 3f)
                    {
                        light.Head.color = Color.green;
                    }
                    else
                    {
                        light.Head.color = Color.blue;
                    }
                    break;
                default:
                    break;
            }

        }

        void DimmerAnimation(MovingLight light)
        {
            switch (DimmerAnimationMode)
            {
                case DimmerAnimationMode.Beat:
                    light.Head.intensity = beat;
                    break;
                case DimmerAnimationMode.BeatReverse:
                    light.Head.intensity = 1 - beat;
                    break;
                case DimmerAnimationMode.Run:
                    light.Head.intensity = light.GetAddressOffset(AddressType) >= beat - 0.1 ? 1f : 0f;
                    break;
                case DimmerAnimationMode.RunReverse:
                    light.Head.intensity = light.GetAddressOffset(AddressType) >= beat - 0.1 ? 0f : 1f;
                    break;
                case DimmerAnimationMode.On:
                    light.Head.intensity = 1f;
                    break;
                case DimmerAnimationMode.Off:
                    light.Head.intensity = 0f;
                    break;
                case DimmerAnimationMode.Sin:
                    light.Head.intensity = 0.5f * Mathf.Sin(light.GetAddressOffset(AddressType) * OffsetStrength + time * Speed) + 0.5f;
                    break;
                case DimmerAnimationMode.ReverseSin:
                    light.Head.intensity = 0.5f * Mathf.Sin(light.GetAddressOffset(AddressType) * OffsetStrength - time * Speed) + 0.5f;
                    break;
                default:
                    light.Head.intensity = 1f;
                    break;
            }

            light.Head.intensity *= IntensityMultiplier;
        }

        void MotionAnimation(MovingLight light)
        {
            switch (MotionAnimationMode)
            {
                case MotionAnimationMode.Focus:
                    if(Target != null)
                        light.LookAt(Target);
                    break;
                case MotionAnimationMode.PanLoop:
                    light.pan.SetRotation(panRange.Sin(time));
                    light.tilt.SetDefault();
                    break;
                case MotionAnimationMode.TiltLoop:
                    light.tilt.SetRotation(tiltRange.Sin(time));
                    light.pan.SetDefault();
                    break;
                case MotionAnimationMode.OffsetTiltLoop:
                    light.tilt.SetRotation(tiltRange.Sin(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    light.pan.SetDefault();
                    break;
                case MotionAnimationMode.PanTiltLoop:
                    light.pan.SetRotation(panRange.Cos(time));
                    light.tilt.SetRotation(tiltRange.Sin(time));
                    break;
                case MotionAnimationMode.OffsetPanTiltLoop:
                    light.pan.SetRotation(panRange.Cos(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    light.tilt.SetRotation(tiltRange.Sin(time * Speed + light.GetAddressOffset(AddressType) * OffsetStrength));
                    break;
                case MotionAnimationMode.Default:
                    light.pan.SetDefault();
                    light.tilt.SetDefault();
                    break;
            }
        }

        public void ChangeBpm(float b)
        {
            if (b == previousBpm) return;

            bpm.Bpm = b;
            timeForBeat = 0;
            previousBpm = b;
        }

        public void ChangeState(ColorAnimationMode color, DimmerAnimationMode dimmer, MotionAnimationMode motion)
        {
            ColorAnimationMode = color;
            DimmerAnimationMode = dimmer;
            MotionAnimationMode = motion;
        }
        
    }

}
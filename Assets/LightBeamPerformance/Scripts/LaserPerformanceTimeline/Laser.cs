using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
    public class Laser : MonoBehaviour
    {
        [Header("Animatable Properties")] public LaserColorMode LaserColorMode = LaserColorMode.SingleColor;
        public LaserDimmerMode LaserDimmerMode = LaserDimmerMode.On;
        public LaserMotionMode LaserMotionMode = LaserMotionMode.Open;

        public Color LaserColor = Color.white;

        public float Speed = 1f; // includes "wave speed"
        public float OffsetStrength = 5f; // includes "frequency"

        public Range indicatorRange = new Range(0, 40);
        public Range panRange = new Range(-90, 90);
        public Range tiltRange = new Range(-10, 10);

        public float IntensityMultiplier { get; set; } = 1f;

        [Header("Essential")] public int LaserNum = 6;

        public float defaultIndicatorDegree = 40;

        [SerializeField] float laserWidth = 0.1f;
        [SerializeField] float laserLength = 100f;

        [Header("Essential Objects")] [SerializeField]
        LaserBase laserBasePrefab;

        [SerializeField] Pan pan;
        [SerializeField] Tilt tilt;
        [SerializeField] Shader shader;

        [SerializeField] private List<LaserBase> laserBases = new List<LaserBase>();

        public void Initialize()
        {
            for (var i = 0; i < laserBases.Count; i++)
            {
                laserBases[i].transform.localScale = new Vector3(laserWidth, 1, laserLength);
                laserBases[i].Initialize(i, (float) i / LaserNum, LaserNum, defaultIndicatorDegree, shader);
            }
        }

        private void Start()
        {
            foreach (var laser in laserBases)
            {
                laser.SetDimmer(0);
                laser.SetColor(LaserColor);
            }
        }

        public void SetPanRange(Range range)
        {
            panRange = range;
        }

        public void SetTiltRange(Range range)
        {
            tiltRange = range;
        }

        public void Process(float time, float beat)
        {
            foreach (var laserBase in laserBases)
            {
                ColorAnimation(laserBase, time, beat);
                DimmerAnimation(laserBase, time, beat);
                MotionAnimation(laserBase, time, beat);
            }
        }

        private void ColorAnimation(LaserBase laser, float time, float beat)
        {
            switch (LaserColorMode)
            {
                case LaserColorMode.SingleColor:
                    laser.SetColor(LaserColor);
                    break;
                case LaserColorMode.HSVLoop:
                    laser.SetColor(Color.HSVToRGB((time + laser.AddressOffset) % 1f, 1, 1));
                    break;
                case LaserColorMode.HSV:
                    laser.SetColor(Color.HSVToRGB(laser.AddressOffset, 1, 1));
                    break;
                case LaserColorMode.RGB:
                    if (laser.AddressOffset < 1f / 3f)
                    {
                        laser.SetColor(Color.red);
                    }
                    else if (laser.AddressOffset < 2f / 3f)
                    {
                        laser.SetColor(Color.green);
                    }
                    else
                    {
                        laser.SetColor(Color.blue);
                    }

                    break;
                default:
                    break;
            }
        }

        private void DimmerAnimation(LaserBase laser, float time, float beat)
        {
            float intensity = 0;

            switch (LaserDimmerMode)
            {
                case LaserDimmerMode.Beat:
                    intensity = beat;
                    break;
                case LaserDimmerMode.BeatReverse:
                    intensity = 1 - beat;
                    break;
                case LaserDimmerMode.On:
                    intensity = 1f;
                    break;
                case LaserDimmerMode.Off:
                    intensity = 0f;
                    break;
                case LaserDimmerMode.Sin:
                    intensity = 0.5f * Mathf.Sin(laser.AddressOffset * OffsetStrength + time * Speed) + 0.5f;
                    break;
                case LaserDimmerMode.ReverseSin:
                    intensity = 0.5f * Mathf.Sin(laser.AddressOffset * OffsetStrength - time * Speed) + 0.5f;
                    break;
            }

            intensity *= IntensityMultiplier;
            laser.SetDimmer(intensity);
        }

        private void MotionAnimation(LaserBase laser, float time, float beat)
        {
            switch (LaserMotionMode)
            {
                case LaserMotionMode.PanLoop:
                    pan.SetRotation(panRange.Sin(time * Speed));
                    tilt.SetDefault();
                    break;
                case LaserMotionMode.TiltLoop:
                    tilt.SetRotation(tiltRange.Sin(time * Speed));
                    pan.SetDefault();
                    break;
                case LaserMotionMode.Open:
                    laser.DefaultDegree();
                    pan.SetDefault();
                    tilt.SetDefault();
                    break;
                case LaserMotionMode.OpenClose:
                    laser.Degree(indicatorRange.Sin(time * Speed));
                    pan.SetDefault();
                    tilt.SetDefault();
                    break;
                case LaserMotionMode.OpenCloseTilt:
                    laser.Degree(indicatorRange.Sin(time * 2 * Speed));
                    pan.SetDefault();
                    tilt.SetRotation(tiltRange.Sin(time * Speed));
                    break;
            }
        }
    }
}
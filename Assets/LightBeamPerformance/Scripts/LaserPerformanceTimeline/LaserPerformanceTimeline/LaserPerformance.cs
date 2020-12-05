using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum LaserMotionMode
    {
        Open, OpenClose, TiltLoop, OpenCloseTilt, PanLoop
    }

    public enum LaserColorMode
    {
        SingleColor, RGB, HSV, HSVLoop
    }

    public enum LaserDimmerMode
    {
        On, Off, Beat, BeatReverse, Sin, ReverseSin
    }

    public class LaserPerformance : MonoBehaviour
    {

        [Header("Initial Settings")]

        [Header("Editable in Timeline")]
        public LaserColorMode LaserColorMode = LaserColorMode.SingleColor;
        public LaserDimmerMode LaserDimmerMode = LaserDimmerMode.On;
        public LaserMotionMode LaserMotionMode = LaserMotionMode.Open;

        public Color LaserColor = Color.white;

        public float Speed = 1f;            // includes "wave speed"
        public float OffsetStrength = 5f;   // includes "frequency"

        public float IntensityMultiplier { get; set; } = 1;
        
        public Range panRange = new Range(-90, 90);
        public Range tiltRange = new Range(-90, -50);

        [SerializeField] private List<LaserGroup> laserGroups = new List<LaserGroup>();

        private bool initialized = false;
        private int laserBaseNum;

        private float beat;
        BPM bpm = new BPM(120);

        private void Start()
        {
            Initialize();
        }
        
        private void OnValidate()
        {
            initialized = false;
        }

        private void Initialize()
        {
            laserGroups.ForEach(laserGroup =>
            {
                laserGroup.Lasers.ForEach(laser =>
                {
                    laser.Initialize();
                });
            });
            
            initialized = true;
        }

        public void ProcessFrame(double masterTime, double clipTime)
        {
            if (!initialized)
            {
                Initialize();
            }
            
            beat = bpm.SecondsToBeat((float)clipTime);
            
            laserGroups.ForEach(group => {
                group.Lasers.ForEach(laser => {
                    laser.LaserColorMode = LaserColorMode;
                    laser.LaserDimmerMode = LaserDimmerMode;
                    laser.LaserMotionMode = LaserMotionMode;

                    laser.LaserColor = LaserColor;
                    laser.IntensityMultiplier = IntensityMultiplier;

                    laser.SetPanRange(panRange);
                    laser.SetTiltRange(tiltRange);
                    laser.Process((float)clipTime, beat);
                });
            });
        }

        public void ChangeState(LaserColorMode color, LaserDimmerMode dimmer, LaserMotionMode motion)
        {
            LaserColorMode = color;
            LaserDimmerMode = dimmer;
            LaserMotionMode = motion;
        }
    }

}
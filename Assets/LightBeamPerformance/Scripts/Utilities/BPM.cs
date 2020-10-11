using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    [System.Serializable]
    public class BPM
    {

        float bpm;
        float seconds;

        public float Bpm {
            get { return bpm; }
            set {
                bpm = value;
                seconds = ToSeconds();
            }
        }

        public BPM(float bpm)
        {
            this.bpm = bpm;
            this.seconds = ToSeconds();
        }

        public float ToSeconds()
        {
            return 60f / bpm;
        }

        public float SecondsToBeat(float time)
        {
            return 1f - (time % seconds) / seconds;
        }
    }

}
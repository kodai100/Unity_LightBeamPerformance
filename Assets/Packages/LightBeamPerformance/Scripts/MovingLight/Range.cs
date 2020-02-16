using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    [System.Serializable]
    public class Range
    {
        public float min = 0f;
        public float max = 1f;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float Lerp(float t)
        {
            return Mathf.Lerp(min, max, t);
        }

        public float Sin(float time)
        {
            return min + (max - min) * (Mathf.Sin(time) * 0.5f + 0.5f);
        }

        public float Cos(float time)
        {
            return min + (max - min) * (Mathf.Cos(time) * 0.5f + 0.5f);
        }

        public bool IsInside(float value)
        {
            if (min <= value && value <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float Noise(float time)
        {
            return Mathf.Lerp(min, max, Mathf.PerlinNoise(time, 0));
        }
    }

}
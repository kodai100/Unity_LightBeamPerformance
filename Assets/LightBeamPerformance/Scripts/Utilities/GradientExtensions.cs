using System.Collections.Generic;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
    public static class GradientExtensions
    {
        public static Gradient Lerp(Gradient a, Gradient b, float t)
        {
            return Lerp(a, b, t, false, false);
        }

        public static Gradient LerpNoAlpha(Gradient a, Gradient b, float t)
        {
            return Lerp(a, b, t, true, false);
        }

        public static Gradient LerpNoColor(Gradient a, Gradient b, float t)
        {
            return Lerp(a, b, t, false, true);
        }

        static Gradient Lerp(Gradient a, Gradient b, float t, bool noAlpha, bool noColor)
        {
            //list of all the unique key times
            var keysTimes = new List<float>();

            if (!noColor)
            {
                for (var i = 0; i < a.colorKeys.Length; i++)
                {
                    var k = a.colorKeys[i].time;
                    if (!keysTimes.Contains(k))
                        keysTimes.Add(k);
                }

                for (var i = 0; i < b.colorKeys.Length; i++)
                {
                    var k = b.colorKeys[i].time;
                    if (!keysTimes.Contains(k))
                        keysTimes.Add(k);
                }
            }

            if (!noAlpha)
            {
                for (var i = 0; i < a.alphaKeys.Length; i++)
                {
                    var k = a.alphaKeys[i].time;
                    if (!keysTimes.Contains(k))
                        keysTimes.Add(k);
                }

                for (var i = 0; i < b.alphaKeys.Length; i++)
                {
                    var k = b.alphaKeys[i].time;
                    if (!keysTimes.Contains(k))
                        keysTimes.Add(k);
                }
            }

            var clrs = new GradientColorKey[keysTimes.Count];
            var alphas = new GradientAlphaKey[keysTimes.Count];

            //Pick colors of both gradients at key times and lerp them
            for (var i = 0; i < keysTimes.Count; i++)
            {
                var key = keysTimes[i];
                var clr = Color.Lerp(a.Evaluate(key), b.Evaluate(key), t);
                clrs[i] = new GradientColorKey(clr, key);
                alphas[i] = new GradientAlphaKey(clr.a, key);
            }

            var g = new Gradient();
            g.SetKeys(clrs, alphas);

            return g;
        }
    }
}
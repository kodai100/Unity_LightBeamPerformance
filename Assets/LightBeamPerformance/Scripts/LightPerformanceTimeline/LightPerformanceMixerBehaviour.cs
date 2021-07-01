using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{
    public class LightPerformanceMixerBehaviour : PlayableBehaviour
    {
        public TimelineClip[] Clips { get; set; }
        public PlayableDirector Director { get; set; }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var trackBinding = playerData as LightBeamPerformance;

            if (!trackBinding)
                return;

            var clipTime = Director.time;

            var gradient = new Gradient();
            float intensity = 0;

            for (var i = 0; i < playable.GetInputCount(); i++)
            {
                var inputWeight = playable.GetInputWeight(i);
                var inputPlayable = (ScriptPlayable<LightPerformanceBehaviour>) playable.GetInput(i);
                var inputBehaviour = inputPlayable.GetBehaviour();

                if (inputWeight > 0.5f)
                {
                    trackBinding.AddressType = inputBehaviour.addressType;

                    trackBinding.ChangeState(inputBehaviour.color, inputBehaviour.dimmer, inputBehaviour.motion);

                    trackBinding.ChangeBpm(inputBehaviour.bpm);

                    trackBinding.panRange = new Range(inputBehaviour.panRange.min, inputBehaviour.panRange.max);
                    trackBinding.tiltRange = new Range(inputBehaviour.tiltRange.min, inputBehaviour.tiltRange.max);

                    trackBinding.Saturation = inputBehaviour.saturation;

                    trackBinding.Speed = inputBehaviour.speed;
                    trackBinding.OffsetStrength = inputBehaviour.offsetStrength;

                    var clip = Clips[i];
                    clipTime = Director.time - clip.start;
                }

                if (inputWeight > 0)
                {
                    intensity = Mathf.Lerp(intensity, inputBehaviour.intensityMultiplier, inputWeight);
                    gradient = GradientExtensions.Lerp(gradient, inputBehaviour.lightGradient, inputWeight);
                }
            }

            trackBinding.IntensityMultiplier = intensity;
            trackBinding.LightGradient = gradient;

            trackBinding.ProcessFrame(Director.time, clipTime);
        }
    }
}
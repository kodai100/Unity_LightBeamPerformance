using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    public class LaserPerformanceMixerBehaviour : PlayableBehaviour
    {
        public TimelineClip[] Clips { get; set; }
        public PlayableDirector Director { get; set; }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            
            var trackBinding = playerData as LaserPerformance;
            
            if (!trackBinding) return;
            
            var clipTime = Director.time;
            var col = Color.black;

            for (var i = 0; i < playable.GetInputCount(); i++)
            {
                var inputWeight = playable.GetInputWeight(i);
                var inputPlayable = (ScriptPlayable<LaserPerformanceBehaviour>)playable.GetInput(i);
                var inputBehaviour = inputPlayable.GetBehaviour();

                if (inputWeight > 0.5f)
                {
                    trackBinding.ChangeState(inputBehaviour.color, inputBehaviour.dimmer, inputBehaviour.motion);
                    
                    trackBinding.IntensityMultiplier = inputBehaviour.intensityMultiplier;

                    trackBinding.Speed = inputBehaviour.speed;
                    trackBinding.OffsetStrength = inputBehaviour.offsetStrength;
                    
                    trackBinding.panRange = new Range(inputBehaviour.panRange.min, inputBehaviour.panRange.max);
                    trackBinding.tiltRange = new Range(inputBehaviour.tiltRange.min, inputBehaviour.tiltRange.max);
                    
                    var clip = Clips[i];
                    clipTime = Director.time - clip.start;
                }
                
                col = Color.Lerp(col, inputBehaviour.laserColor, inputWeight);
            }

            trackBinding.LaserColor = col;
            
            trackBinding.ProcessFrame(Director.time, clipTime);
        }
    }

}
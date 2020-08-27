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


            int inputCount = playable.GetInputCount();

            double clipTime = Director.time;

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                // ScriptPlayable<MeshRendererActivationBehaviour> inputPlayable = (ScriptPlayable<MeshRendererActivationBehaviour>)playable.GetInput(i);
                // MeshRendererActivationBehaviour input = inputPlayable.GetBehaviour ();

                if (playable.GetInputWeight(i) > 0)
                {

                    ScriptPlayable<LightPerformanceBehaviour> inputPlayable = (ScriptPlayable<LightPerformanceBehaviour>)playable.GetInput(i);
                    var inputBehaviour = inputPlayable.GetBehaviour();

                    trackBinding.AddressType = inputBehaviour.addressType;

                    trackBinding.ChangeState(inputBehaviour.color, inputBehaviour.dimmer, inputBehaviour.motion);

                    trackBinding.ChangeBpm(inputBehaviour.bpm);

                    trackBinding.LightColor = inputBehaviour.lightColor;
                    trackBinding.Saturation = inputBehaviour.saturation;
                    trackBinding.IntensityMultiplier = inputBehaviour.intensityMultiplier;

                    trackBinding.Speed = inputBehaviour.speed;
                    trackBinding.OffsetStrength = inputBehaviour.offsetStrength;

                    var clip = Clips[i];
                    clipTime = Director.time - clip.start;

                    break;
                }
            }

            trackBinding.ProcessFrame(Director.time, clipTime);

        }
    }

}
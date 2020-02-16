using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    public class LightPerformanceMixerBehaviour : PlayableBehaviour
    {
        public LightBeamPerformance trackBinding;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {

            if (!trackBinding)
                return;

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<LightPerformanceBehaviour> inputPlayable = (ScriptPlayable<LightPerformanceBehaviour>)playable.GetInput(i);
                LightPerformanceBehaviour input = inputPlayable.GetBehaviour();

                input.lightBeamPerformance = trackBinding;
            }

        }

        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {

        }
    }

}
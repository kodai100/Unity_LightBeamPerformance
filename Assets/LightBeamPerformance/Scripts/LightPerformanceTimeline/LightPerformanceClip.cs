using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [Serializable]
    public class LightPerformanceClip : PlayableAsset, ITimelineClipAsset
    {
        public LightPerformanceBehaviour data = new LightPerformanceBehaviour();
        public ClipCaps clipCaps => ClipCaps.Blending;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<LightPerformanceBehaviour>.Create(graph, data);

            return playable;
        }
    }


}
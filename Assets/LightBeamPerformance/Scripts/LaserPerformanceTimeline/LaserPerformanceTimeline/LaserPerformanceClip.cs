using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [Serializable]
    public class LaserPerformanceClip : PlayableAsset, ITimelineClipAsset
    {
        public LaserPerformanceBehaviour data = new LaserPerformanceBehaviour();

        public ClipCaps clipCaps => ClipCaps.Blending;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<LaserPerformanceBehaviour>.Create(graph, data);
            return playable;
        }
    }


}
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [TrackColor(0, 1, 1)]
    [TrackClipType(typeof(LaserPerformanceClip))]
    [TrackBindingType(typeof(LaserPerformance))]
    public class LaserPerformanceTrack : TrackAsset
    {

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {

            var playable = ScriptPlayable<LaserPerformanceMixerBehaviour>.Create(graph, inputCount);
            
            playable.GetBehaviour().Clips = GetClips().ToArray();
            playable.GetBehaviour().Director = go.GetComponent<PlayableDirector>();

            return playable;
        }
    }


}
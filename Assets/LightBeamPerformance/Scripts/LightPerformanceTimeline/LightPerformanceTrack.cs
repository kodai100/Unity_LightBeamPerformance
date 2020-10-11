using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ProjectBlue.LightBeamPerformance
{

    [TrackColor(1, 0, 0)]
    [TrackClipType(typeof(LightPerformanceClip))]
    [TrackBindingType(typeof(LightBeamPerformance))]
    public class LightPerformanceTrack : TrackAsset
    {

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {

            var playable = ScriptPlayable<LightPerformanceMixerBehaviour>.Create(graph, inputCount);

            playable.GetBehaviour().Clips = GetClips().ToArray();
            playable.GetBehaviour().Director = go.GetComponent<PlayableDirector>();

            return playable;
        }
    }


}
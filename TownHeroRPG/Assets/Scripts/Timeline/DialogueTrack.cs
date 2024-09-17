using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Definimos la pista personalizada
[TrackClipType(typeof(DialogueClip))]
public class DialogueTrack : TrackAsset
{
    // Podemos sobrescribir CreateTrackMixer si necesitamos personalizar la pista
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<PlayableBehaviour>.Create(graph, inputCount);
    }
}

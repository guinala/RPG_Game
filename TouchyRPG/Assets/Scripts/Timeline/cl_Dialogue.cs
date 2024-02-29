using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[Serializable]
public class cl_Dialogue : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    public playable_dialogue template = new playable_dialogue();

    public ClipCaps clipCaps
    {
        get
        {
            return ClipCaps.None;
        }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<playable_dialogue>.Create(graph, template);
    }

}

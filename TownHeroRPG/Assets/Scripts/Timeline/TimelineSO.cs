using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public enum TimelineStartType
{
    FirstSceneAwake,
    Trigger,
    Checkpoint
}

[CreateAssetMenu(fileName = "Cutscene", menuName = "ScriptableObjects/Cutscenes/Cutscene")]
public class TimelineSO : ScriptableObject
{
    public TimelineAsset cutscene;
    public TimelineStartType startType;
    public bool visited;
}

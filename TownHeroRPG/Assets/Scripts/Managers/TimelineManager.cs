using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public TimelineSO[] sceneCutscenes;
    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    
    private void Start()
    {
        DisplayAwakeCutscenes();
    }

    private void DisplayAwakeCutscenes()
    {
        for (int i = 0; i < sceneCutscenes.Length; i++)
        {
            if (sceneCutscenes[i].startType == TimelineStartType.FirstSceneAwake && sceneCutscenes[i].visited == false)
            {
                director.playableAsset = sceneCutscenes[i].cutscene;
                sceneCutscenes[i].visited = true;
                director.Play();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public TimelineSO[] sceneCutscenes;
    private PlayableDirector director;
    public UnityEvent onCutsceneStart;
    public CanvasGroup fade;
    

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

    public void FadeEffect()
    {
        StartCoroutine(FadeCoroutine());
    }
    
    private IEnumerator FadeCoroutine()
    {
        StartCoroutine(Helper.IEFade(fade, 1f, 1f));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Helper.IEFade(fade, 0f, 1f));
    }
}

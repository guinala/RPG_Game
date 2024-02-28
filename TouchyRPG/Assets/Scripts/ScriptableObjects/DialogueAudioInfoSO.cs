using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudio", menuName = "ScriptableObjects/Audio/DialogueAudio")]
public class DialogueAudioInfoSO : ScriptableObject
{
    public string id;
    public AudioClip[] dialogueSounds;
    public bool stopAudio = false;

    [Range(1, 5)]
    public int frequencyLevel = 2;

    [Range(-3, 3)]
    public float minPitch = 0.5f;

    [Range(-3, 3)]
    public float maxPitch = 3f;

}

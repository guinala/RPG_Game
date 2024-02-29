using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue character")]
public class s_dialogueChar : ScriptableObject
{
    public float voicePitch = 1;
    public AudioClip voice;
    //TODO: HAVE PORTRAITS OF CHARACTERS represented by numbers
}

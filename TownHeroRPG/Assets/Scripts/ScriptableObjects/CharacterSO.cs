using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Conversation/Character")]
public class CharacterSO : ScriptableObject
{
    public string fullname;
    public Sprite portrait;
    public string audioID;
}

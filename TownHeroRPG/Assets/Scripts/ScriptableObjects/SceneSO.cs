using UnityEngine;

[CreateAssetMenu(fileName = "NewFile", menuName = "ScriptableObjects/Scene")]
public class SceneSO : ScriptableObject
{
    [Header("Scene Information")]
    public string sceneName;
}

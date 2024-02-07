using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestList", menuName = "ScriptableObjects/Missions/QuestList")]
public class QuestListSO : ScriptableObject
{
    public List<MissionSO> questList = new List<MissionSO>();   



    public void reload()
    {
        foreach (MissionSO s in questList)
        {
            s.reload();
        }
    }
}

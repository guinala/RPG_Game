using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionSO", menuName = "ScriptableObjects/Missions/Mission")]
public class MissionSO : ScriptableObject
{
    [Serializable]
    public class State
    {
        public string scene;
        public int state;
    }

    public string title;
    public Vector3 missionDestinationPoint;
    [TextArea(3, 10)]
    public List<string> information = new List<string>();
    public int currentState = 0;
    public int goldReward;
    public GameObject unitPrefab;
    //public Dictionary<int, string> estados = new Dictionary<int, string>();
    public List<State> listState = new List<State>();



    public void Start()
    {
        /**
        foreach(var state in listState)
        {
            estados.Add(state.state, state.scene);
        }
        */

        currentState = listState[0].state;
        Debug.Log("El estado actual del estado es:" + currentState);
        instantiateMissionIcon();
    }


    public void reload()
    {
        if(currentState == 1)
        {
            instantiateMissionIcon();
        }
    }


    public void instantiateMissionIcon()
    {
        GameObject icon;

        icon = Instantiate(unitPrefab, missionDestinationPoint, Quaternion.identity);

    }
    
}

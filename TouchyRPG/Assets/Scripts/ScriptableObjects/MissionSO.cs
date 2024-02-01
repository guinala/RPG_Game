using UnityEngine;

[CreateAssetMenu(fileName = "MissionSO", menuName = "ScriptableObjects/Missions")]
public class MissionSO : ScriptableObject
{
    public string title;
    public Vector3 missionDestinationPoint;
    [TextArea(3, 10)]
    public string information;
    public int goldReward;
    public GameObject unitPrefab;


    
    public void instantiateMissionIcon()
    {
        GameObject icon;

        icon = Instantiate(unitPrefab, missionDestinationPoint, Quaternion.identity);

    }
    
}
